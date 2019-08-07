package com.libDB.api.service;

public interface LoginService {

    boolean validateEmployee(String id, String password);
    
    boolean validateMember(String id, String password);

}