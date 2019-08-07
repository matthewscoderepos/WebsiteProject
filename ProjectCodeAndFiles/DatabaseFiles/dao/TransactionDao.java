package com.libDB.api.dao;

import java.util.List;

import com.libDB.api.entity.TransactionView;

public interface TransactionDao {
    
    List<TransactionView> getTransactionsByMember(String memberID);
}