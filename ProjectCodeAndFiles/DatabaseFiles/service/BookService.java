package com.libDB.api.service;

import java.util.List;

import com.libDB.api.entity.Book;

public interface BookService {

    List<Book> getBooksByOptions(String id, String isbn, String title, String author, String genre, String address);
}