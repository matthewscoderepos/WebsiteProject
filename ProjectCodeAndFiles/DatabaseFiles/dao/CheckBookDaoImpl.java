package com.libDB.api.dao;

import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.PreparedStatementCallback;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.jdbc.core.namedparam.SqlParameterSource;
import org.springframework.jdbc.support.GeneratedKeyHolder;
import org.springframework.jdbc.support.KeyHolder;
import org.springframework.stereotype.Repository;
import org.springframework.http.ResponseEntity;

import com.libDB.api.entity.Member;
import com.libDB.api.entity.InStock;
import com.libDB.api.entity.Transaction;
import com.libDB.api.mapper.MemberRowMapper;
import com.libDB.api.mapper.InStockRowMapper;
import com.libDB.api.mapper.TransactionRowMapper;
import com.libDB.util.StringUtils;

@Repository
public class CheckBookDaoImpl implements CheckBookDao {

    NamedParameterJdbcTemplate template;

    public CheckBookDaoImpl(NamedParameterJdbcTemplate template) {
        this.template = template;
    }

    @Override
    public ResponseEntity<String> checkOut(String branchID, String memberID, String bookID) {

        // Verify member exists
        String query = "select * from public.\"Member\" where \"MemberID\"=\'" + memberID + "\'";
        List<Member> member = template.query(query, new MemberRowMapper());

        if (member.size() != 1) {
            return ResponseEntity.status(401).body("Invalid member ID.");
        }

        // Verify book exists at branch
        query = "select * from \"InStock\" where \"BookID\" = \'"
                + bookID +"\' and \"BranchID\" = \'"+ branchID +"\'";
        List<InStock> inStock = template.query(query, new InStockRowMapper());

        if (inStock.size() != 1) {
            return ResponseEntity.status(401).body("Invalid: Book not in stock at branch.");
        }

        // Insert new transaction
        query = "insert into public.\"Transaction\"(\"BookID\", \"MemberID\", \"TimeOut\","
                + " \"TimeIn\", \"BranchOut\", \"BranchIn\") "
                + "values (:bookID, :memberID, date_trunc('second', (select localtimestamp)), "
                + "NULL, :branchID, NULL)";

        KeyHolder insertHolder = new GeneratedKeyHolder();
        SqlParameterSource inserParam = new MapSqlParameterSource()
            .addValue("bookID", bookID)
            .addValue("branchID", branchID)
            .addValue("memberID", memberID);
        template.update(query, inserParam, insertHolder);

        // Delete inStock
        InStock inStockEntry = inStock.get(0);
        query = "delete from public.\"InStock\" where \"BookID\"=\'" + inStockEntry.getBookID()
                + "\' and \"BranchID\"=\'" + inStockEntry.getBranchID() +"\'";

        template.execute(query, new PreparedStatementCallback<Object>() {
            @Override
            public Object doInPreparedStatement(PreparedStatement ps) 
                    throws SQLException, DataAccessException {
                return ps.executeUpdate();
            }
        });

        return ResponseEntity.ok().body("Transaction complete.");
        
    }
    
    @Override
    public ResponseEntity<String> checkIn(String branchID, String memberID, String bookID) {
        
        // Verify member exists
        String query = "select * from public.\"Member\" where \"MemberID\"=\'" + memberID + "\'";
        List<Member> member = template.query(query, new MemberRowMapper());

        if (member.size() != 1) {
            return ResponseEntity.status(401).body("Invalid member ID.");
        }

        // Verify book exists at branch
        query = "select * from \"Transaction\" where \"BookID\" = \'"
                + bookID +"\' and \"TimeIn\" is NULL";
        List<Transaction> transactions = template.query(query, new TransactionRowMapper());

        if (transactions.size() != 1) {
            return ResponseEntity.status(401).body("Invalid: Transaction not found, or book already checked in.");
        }

        // Update transaction to reflect branchIn and timeIn
        query = "update public.\"Transaction\" set \"TimeIn\" = date_trunc('second', (select localtimestamp)), "
                + " \"BranchIn\"=:branchID where \"BookID\"=:bookID"
                +" and \"MemberID\"=:memberID and \"TimeIn\" is NULL and \"BranchIn\" is NULL";

        KeyHolder holder = new GeneratedKeyHolder();
        SqlParameterSource param = new MapSqlParameterSource()
            .addValue("branchID", branchID)
            .addValue("bookID", bookID)
            .addValue("memberID", memberID);
        template.update(query, param, holder);

        // Add new row to inStock for bookID and branchID
        query = "insert into public.\"InStock\"(\"BookID\", \"BranchID\") values (:bookID, :branchID)";
        KeyHolder insertHolder = new GeneratedKeyHolder();
        SqlParameterSource inserParam = new MapSqlParameterSource()
            .addValue("bookID", bookID)
            .addValue("branchID", branchID);
        template.update(query, inserParam, insertHolder);

        return ResponseEntity.ok().body("Transaction complete.");
    }
}