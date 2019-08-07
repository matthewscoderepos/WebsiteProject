package com.libDB.api.entity;

public class InStock {
    private String bookID;
    private String branchID;

    public InStock() {}

    public InStock(String bookID, String branchID) {
        this.bookID = bookID;
        this.branchID = branchID;
    }

    /**
     * @return the bookID
     */
    public String getBookID() {
        return bookID;
    }

    /**
     * @param bookID the bookID to set
     */
    public void setBookID(String bookID) {
        this.bookID = bookID;
    }

    /**
     * @return the branchID
     */
    public String getBranchID() {
        return branchID;
    }

    /**
     * @param branchID the branchID to set
     */
    public void setBranchID(String branchID) {
        this.branchID = branchID;
    }
}