package com.libDB.api.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import com.libDB.api.entity.TransactionView;

public class TransactionViewRowMapper implements RowMapper<TransactionView> {

    @Override
    public TransactionView mapRow(ResultSet rs, int arg1) throws SQLException {
        TransactionView transaction = new TransactionView();
        transaction.setBookTitle(rs.getString("Title"));
        transaction.setBookAuthor(rs.getString("Author"));
        transaction.setTimeOut(rs.getString("TimeOut"));
        transaction.setTimeIn(rs.getString("TimeIn"));
        transaction.setBranchAddressOut(rs.getString("BranchAddressOut"));
        transaction.setBranchAddressIn(rs.getString("BranchAddressIn"));

        return transaction;
    }
}