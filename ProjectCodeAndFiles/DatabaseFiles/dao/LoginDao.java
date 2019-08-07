package com.libDB.api.dao;

public interface LoginDao {

    boolean validateMember(String id, String password);

    boolean validateEmployee(String id, String password);
}