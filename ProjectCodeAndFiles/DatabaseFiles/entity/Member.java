package com.libDB.api.entity;

import java.sql.Date;

public class Member {
    private String id;
    private String joined;
    private String firstName;
    private String lastName;
    private String address;

    public Member() {}

    public Member(String id, String joined, String firstName, 
        String lastName, String address) {
        this.id = id;
        this.joined = joined;
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = address;
    }

    /**
     * @return the address
     */
    public String getAddress() {
        return address;
    }

    /**
     * @return the id
     */
    public String getId() {
        return id;
    }

    /**
     * @param id the id to set
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     * @return the joined
     */
    public String getJoined() {
        return joined;
    }

    /**
     * @param joined the joined to set
     */
    public void setJoined(String joined) {
        this.joined = joined;
    }

    /**
     * @return the firstName
     */
    public String getFirstName() {
        return firstName;
    }

    /**
     * @param firstName the firstName to set
     */
    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    /**
     * @return the lastName
     */
    public String getLastName() {
        return lastName;
    }

    /**
     * @param lastName the lastName to set
     */
    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    /**
     * @param address the address to set
     */
    public void setAddress(String address) {
        this.address = address;
    }    
}