package com.libDB.api.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import com.libDB.api.entity.InStock;

public class InStockRowMapper implements RowMapper<InStock> {

    @Override
    public InStock mapRow(ResultSet rs, int arg1) throws SQLException {
        InStock inStock = new InStock();
        inStock.setBookID(rs.getString("BookID"));
        inStock.setBranchID(rs.getString("BranchID"));

        return inStock;
    }
}