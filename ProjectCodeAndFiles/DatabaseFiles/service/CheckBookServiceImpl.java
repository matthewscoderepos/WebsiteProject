package com.libDB.api.service;

import javax.annotation.Resource;

import org.springframework.stereotype.Component;
import org.springframework.http.ResponseEntity;

import com.libDB.api.dao.CheckBookDao;

@Component
public class CheckBookServiceImpl implements CheckBookService {

    @Resource
    CheckBookDao checkBookDao;

    public ResponseEntity<String> checkOut(String branchID, String memberID, String bookID) {
        return checkBookDao.checkOut(branchID, memberID, bookID);
    }
    
    public ResponseEntity<String> checkIn(String branchID, String memberID, String bookID) {
        return checkBookDao.checkIn(branchID, memberID, bookID);
    }

}