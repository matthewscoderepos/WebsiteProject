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

import com.libDB.api.entity.Branch;
import com.libDB.api.mapper.BranchRowMapper;
import com.libDB.util.StringUtils;

@Repository
public class BranchDaoImpl implements BranchDao {

    NamedParameterJdbcTemplate template;

    public BranchDaoImpl(NamedParameterJdbcTemplate template) {
        this.template = template;
    }

    @Override
    public Branch getBranchByID(String employeeID) {
        String query = "select * from public.\"Branch\", "
                + "(select \"BranchID\" as \"bid\" from public.\"Employed\" "
                + "where \"SSN\" = \'"+ employeeID +"\') as \"b\" "
                + "where \"BranchID\" = \"bid\"";

        List<Branch> result = new ArrayList<Branch>();
        if (!StringUtils.IsNullOrWhiteSpace(employeeID)) {
            result = template.query(query, new BranchRowMapper());
        }

        return (result.size() > 0) ? result.get(0) : new Branch();
    }
}