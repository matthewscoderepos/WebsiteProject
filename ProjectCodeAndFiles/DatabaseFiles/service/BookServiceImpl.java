package com.libDB.api.service;

import java.util.List;

import javax.annotation.Resource;

import org.springframework.stereotype.Component;

import com.libDB.api.dao.BookDao;
import com.libDB.api.entity.Book;

@Component
public class BookServiceImpl implements BookService {
    
    @Resource
    BookDao bookDao;

    @Override
    public List<Book> getBooksByOptions(String id, String isbn, String title, String author, String genre, String address) {
        return bookDao.getBooksByOptions(id, isbn, title, author, genre, address);
    }
}