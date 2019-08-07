package com.libDB.api.service;

import java.util.List;

import com.libDB.api.entity.TransactionView;

public interface TransactionService {

    List<TransactionView> getTransactionsByMember(String memberID);
}