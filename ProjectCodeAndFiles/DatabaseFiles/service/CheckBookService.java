package com.libDB.api.service;

import org.springframework.http.ResponseEntity;

public interface CheckBookService {

    ResponseEntity<String> checkOut(String branchID, String memberID, String bookID);
    
    ResponseEntity<String> checkIn(String branchID, String memberID, String bookID);

}