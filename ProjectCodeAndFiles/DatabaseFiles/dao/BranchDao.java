package com.libDB.api.dao;

import java.util.List;

import com.libDB.api.entity.Branch;

public interface BranchDao {
    
    Branch getBranchByID(String employeeID);
}