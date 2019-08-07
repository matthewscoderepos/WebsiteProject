package com.libDB.api.dao;

import org.springframework.http.ResponseEntity;

public interface CheckBookDao {

    ResponseEntity<String> checkOut(String branchID, String memberID, String bookID);
    
    ResponseEntity<String> checkIn(String branchID, String memberID, String bookID);
}