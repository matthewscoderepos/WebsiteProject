package com.libDB.api.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import com.libDB.api.entity.Branch;

public class BranchRowMapper implements RowMapper<Branch> {

    @Override
    public Branch mapRow(ResultSet rs, int arg1) throws SQLException {
        Branch branch = new Branch();
        branch.setId(rs.getString("BranchID"));
        branch.setName(rs.getString("Name"));
        branch.setAddress(rs.getString("Address"));

        return branch;
    }
}