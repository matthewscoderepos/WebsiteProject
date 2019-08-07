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

import com.libDB.api.entity.TransactionView;
import com.libDB.api.mapper.TransactionViewRowMapper;
import com.libDB.util.StringUtils;

@Repository
public class TransactionDaoImpl implements TransactionDao {
    
    NamedParameterJdbcTemplate template;

    public TransactionDaoImpl(NamedParameterJdbcTemplate template) {
        this.template = template;
    }

    @Override
    public List<TransactionView> getTransactionsByMember(String memberID) {
        List<TransactionView> transactions = new ArrayList<TransactionView>();

        if (!StringUtils.IsNullOrWhiteSpace(memberID)) {
            String query = "select \"BK\".\"Title\", \"BK\".\"Author\", \"T\".\"TimeOut\", coalesce(text (\"T\".\"TimeIn\"), \'-\') as \"TimeIn\", \"BO\".\"Address\" as \"BranchAddressOut\", \"BI\".\"Address\" as \"BranchAddressIn\""
            + " from \"Transaction\" \"T\", \"Book\" \"BK\", \"Branch\" \"BI\", \"Branch\" \"BO\""
            + " where \"MemberID\" = \'"+memberID+"\' and \"T\".\"BookID\" = \"BK\".\"BookID\""
            + " and \"T\".\"BranchOut\" = \"BO\".\"BranchID\" and coalesce(\"T\".\"BranchIn\", \'000000000\') = \"BI\".\"BranchID\";";
            
            transactions = template.query(query, new TransactionViewRowMapper());
        }

        return transactions;
    }
}