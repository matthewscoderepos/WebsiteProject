package com.libDB.api.dao;

import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.HashMap;
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

import com.libDB.api.entity.Book;
import com.libDB.api.mapper.BookRowMapper;
import com.libDB.util.StringUtils;

@Repository
public class BookDaoImpl implements BookDao {
    
    NamedParameterJdbcTemplate template;

    public BookDaoImpl(NamedParameterJdbcTemplate template) {
        this.template = template;
    }

    @Override
    public List<Book> getBooksByOptions(String id, String isbn, String title, String author, String genre, String address) {

        String query = "select * from (select \"Book\".\"BookID\", \"ISBN\", \"Title\", \"Author\", \"Genre\", \"NumPages\", \"Address\" from public.\"Book\" left outer join \"InStock\" on \"Book\".\"BookID\" = \"InStock\".\"BookID\" full outer join \"Branch\" on \"InStock\".\"BranchID\" = \"Branch\".\"BranchID\" where \"Book\".\"BookID\" is not NULL ) as \"Book\" where 1=1";
        
        if (!StringUtils.IsNullOrWhiteSpace(id)) {
            query += " and \"BookID\" = \'" + id + "\'";
        }
        
        if (!StringUtils.IsNullOrWhiteSpace(isbn)) {
            query += " and \"ISBN\" = \'" + isbn + "\'";
        }
        
        if (!StringUtils.IsNullOrWhiteSpace(title)) {
            query += " and \"Title\" LIKE \'%" + title + "%\'";
        }
        
        if (!StringUtils.IsNullOrWhiteSpace(author)) {
            query += " and \"Author\" LIKE \'%" + author + "%\'";
        }
        
        if (!StringUtils.IsNullOrWhiteSpace(genre)) {
            query += " and \"Genre\" = \'" + genre + "\'";
        }
        
        if (!StringUtils.IsNullOrWhiteSpace(address)) {
            query += " and \"Address\" = \'" + address + "\'";
        }

        query += "  order by \"Author\", \"Title\"";

        return template.query(query, new BookRowMapper());
    }
}

