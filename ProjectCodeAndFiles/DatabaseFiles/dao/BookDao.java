package com.libDB.api.dao;

import java.util.List;

import com.libDB.api.entity.Book;

public interface BookDao {
    
    List<Book> getBooksByOptions(String id, String isbn, String title, String author, String genre, String address);
}