package com.libDB.api.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import com.libDB.api.entity.Book;

public class BookRowMapper implements RowMapper<Book> {

    @Override
    public Book mapRow(ResultSet rs, int arg1) throws SQLException {
        Book book = new Book();
        book.setId(rs.getString("BookID"));
        book.setIsbn(rs.getString("ISBN"));
        book.setTitle(rs.getString("Title"));
        book.setAuthor(rs.getString("Author"));
        book.setGenre(rs.getString("Genre"));
        book.setPages(rs.getString("NumPages"));
        book.setAddress(rs.getString("Address"));

        return book;
    }
}