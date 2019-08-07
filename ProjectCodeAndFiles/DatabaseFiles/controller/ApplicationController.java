package com.libDB.api.controller;

import java.util.List;

import javax.annotation.Resource;

import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.http.ResponseEntity;

import com.libDB.api.entity.Book;
import com.libDB.api.entity.Branch;
import com.libDB.api.service.BookService;
import com.libDB.api.service.BranchService;
import com.libDB.api.entity.TransactionView;
import com.libDB.api.service.TransactionService;
import com.libDB.api.service.LoginService;
import com.libDB.api.service.CheckBookService;


@RestController
@RequestMapping("/api")
class ApplicationController {

    @Resource
    BookService bookService;

    @GetMapping(value = "/getBooks")
    public List<Book> getBooks(
            @RequestParam(name="id", required=false, defaultValue="") String id,
            @RequestParam(name="isbn", required=false, defaultValue="") String isbn,
            @RequestParam(name="title", required=false, defaultValue="") String title,
            @RequestParam(name="author", required=false, defaultValue="") String author,
            @RequestParam(name="genre", required=false, defaultValue="") String genre,
            @RequestParam(name="address", required=false, defaultValue="") String address)
    {
        return bookService.getBooksByOptions(id, isbn, title, author, genre, address);
    }

    @Resource
    TransactionService transactionService;

    @GetMapping(value = "/getTransactions")
    public List<TransactionView> getTransactions(
            @RequestParam(name="memberID", required=true, defaultValue="") String memberID)
    {
        return transactionService.getTransactionsByMember(memberID);
    }

    @Resource
    BranchService branchService;

    @GetMapping(value = "/getBranchByID")
    public Branch getBranchByID(
            @RequestParam(name="employeeID", required=true, defaultValue="") String employeeID)
    {
        return branchService.getBranchByID(employeeID);   
    }
    
    @Resource
    LoginService loginService;

    @PostMapping(value = "/memberLogin")
    public ResponseEntity<String> memberLogin(
        @RequestParam(name="username", required=true, defaultValue="") String id,
        @RequestParam(name="password", required=true, defaultValue="") String password)
    {
        return (loginService.validateMember(id, password))
            ? ResponseEntity.ok().body("Access granted")
            : ResponseEntity.status(401).body("Invalid credentials.");
    }
    
    @PostMapping(value = "/employeeLogin")
    public ResponseEntity<String> employeeLogin(
        @RequestParam(name="username", required=true, defaultValue="") String id,
        @RequestParam(name="password", required=true, defaultValue="") String password)
    {
        return (loginService.validateEmployee(id, password))
            ? ResponseEntity.ok().body("Access granted")
            : ResponseEntity.status(401).body("Invalid credentials.");
    }

    @Resource
    CheckBookService checkBookService;

    @PostMapping(value = "/checkOutBook")
    public ResponseEntity<String> checkOutBook(
        @RequestParam(name="branchID", required=true, defaultValue="") String branchID,
        @RequestParam(name="memberID", required=true, defaultValue="") String memberID,
        @RequestParam(name="bookID", required=true, defaultValue="") String bookID)
    {
        return checkBookService.checkOut(branchID, memberID, bookID);
    }
    
    @PostMapping(value = "/checkInBook")
    public ResponseEntity<String> checkInBook(
        @RequestParam(name="branchID", required=true, defaultValue="") String branchID,
        @RequestParam(name="memberID", required=true, defaultValue="") String memberID,
        @RequestParam(name="bookID", required=true, defaultValue="") String bookID)
    {
        return checkBookService.checkIn(branchID, memberID, bookID);
    }
}