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

import com.libDB.api.entity.Login;
import com.libDB.api.mapper.LoginRowMapper;
import com.libDB.util.StringUtils;

@Repository
public class LoginDaoImpl implements LoginDao {

    NamedParameterJdbcTemplate template;

    public LoginDaoImpl(NamedParameterJdbcTemplate template) {
        this.template = template;
    }

    @Override
    public boolean validateMember(String id, String password) {
        String query = "select * from public.\"MemberLogin\" where \"Username\" = \'" 
                    + id + "\' and \"Password\" = \'" + password + "\'";

        List<Login> result = new ArrayList<Login>();
        if (!StringUtils.IsNullOrWhiteSpace(id) && !StringUtils.IsNullOrWhiteSpace(password)) {
            result = template.query(query, new LoginRowMapper());
        }

        return result.size() == 1;
    }

    @Override
    public boolean validateEmployee(String id, String password) {
        String query = "select * from public.\"EmployeeLogin\" where \"Username\" = \'" 
                    + id + "\' and \"Password\" = \'" + password + "\'";

        List<Login> result = new ArrayList<Login>();
        if (!StringUtils.IsNullOrWhiteSpace(id) && !StringUtils.IsNullOrWhiteSpace(password)) {
            result = template.query(query, new LoginRowMapper());
        }

        return result.size() == 1;
    }
}