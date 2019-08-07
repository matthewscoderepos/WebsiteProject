package com.libDB.api.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import com.libDB.api.entity.Login;

public class LoginRowMapper implements RowMapper<Login> {

    @Override
    public Login mapRow(ResultSet rs, int arg1) throws SQLException {
        Login login = new Login();
        login.setUsername(rs.getString("Username"));
        login.setPassword(rs.getString("Password"));

        return login;
    }
}