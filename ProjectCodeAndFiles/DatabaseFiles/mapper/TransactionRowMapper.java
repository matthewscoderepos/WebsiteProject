package com.libDB.api.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import com.libDB.api.entity.Transaction;

public class TransactionRowMapper implements RowMapper<Transaction> {

    @Override
    public Transaction mapRow(ResultSet rs, int arg1) throws SQLException {
        Transaction transaction = new Transaction();
        transaction.setBookID(rs.getString("BookID"));
        transaction.setMemberID(rs.getString("MemberID"));
        transaction.setTimeOut(rs.getString("TimeOut"));
        transaction.setTimeIn(rs.getString("TimeIn"));
        transaction.setBranchOut(rs.getString("BranchOut"));
        transaction.setBranchIn(rs.getString("BranchIn"));

        return transaction;
    }
}