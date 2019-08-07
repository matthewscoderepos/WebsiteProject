package com.libDB.api.service;

import javax.annotation.Resource;

import org.springframework.stereotype.Component;

import com.libDB.api.dao.BranchDao;
import com.libDB.api.entity.Branch;

@Component
public class BranchServiceImpl implements BranchService {
    
    @Resource
    BranchDao branchDao;

    @Override
    public Branch getBranchByID(String employeeID) {
        return branchDao.getBranchByID(employeeID);
    }
}