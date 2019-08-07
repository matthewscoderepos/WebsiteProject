package com.libDB.api.entity;

public class Transaction {
    private String bookID;
    private String memberID;
    private String timeOut;
    private String timeIn;
    private String branchOut;
    private String branchIn;

    public Transaction() {}

    public Transaction(String bookID, String memberID, String timeOut, String timeIn, String branchOut, String branchIn) {
        this.setBookID(bookID);
        this.setMemberID(memberID);
        this.setTimeOut(timeOut);
        this.setTimeIn(timeIn);
        this.setBranchOut(branchOut);
        this.setBranchIn(branchIn);
    }

    /**
     * @return the branchIn
     */
    public String getBranchIn() {
        return branchIn;
    }

    /**
     * @param branchAddressIn the branchIn to set
     */
    public void setBranchIn(String branchIn) {
        this.branchIn = branchIn;
    }

    /**
     * @return the branchOut
     */
    public String getBranchOut() {
        return branchOut;
    }

    /**
     * @param branchOut the branchOut to set
     */
    public void setBranchOut(String branchOut) {
        this.branchOut = branchOut;
    }

    /**
     * @return the timeIn
     */
    public String getTimeIn() {
        return timeIn;
    }

    /**
     * @param timeIn the timeIn to set
     */
    public void setTimeIn(String timeIn) {
        this.timeIn = timeIn;
    }

    /**
     * @return the timeOut
     */
    public String getTimeOut() {
        return timeOut;
    }

    /**
     * @param timeOut the timeOut to set
     */
    public void setTimeOut(String timeOut) {
        this.timeOut = timeOut;
    }

    /**
     * @return the memberID
     */
    public String getMemberID() {
        return memberID;
    }

    /**
     * @param memberID the memberID to set
     */
    public void setMemberID(String memberID) {
        this.memberID = memberID;
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
}