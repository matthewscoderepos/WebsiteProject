package com.libDB.api.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import com.libDB.api.entity.Member;

public class MemberRowMapper implements RowMapper<Member> {

    @Override
    public Member mapRow(ResultSet rs, int arg1) throws SQLException {
        Member member = new Member();
        member.setId(rs.getString("MemberID"));
        member.setJoined(rs.getString("Joined"));
        member.setFirstName(rs.getString("FirstNAme"));
        member.setLastName(rs.getString("LastName"));
        member.setAddress(rs.getString("Address"));

        return member;
    }
}