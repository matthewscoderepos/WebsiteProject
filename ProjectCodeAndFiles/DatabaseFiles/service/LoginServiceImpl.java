package com.libDB.api.service;

import javax.annotation.Resource;

import org.springframework.stereotype.Component;

import com.libDB.api.dao.LoginDao;

@Component
public class LoginServiceImpl implements LoginService {

    @Resource
    LoginDao loginDao;

    public boolean validateEmployee(String id, String password) {
        return loginDao.validateEmployee(id, password);
    }
    
    public boolean validateMember(String id, String password) {
        return loginDao.validateMember(id, password);
    }

}