package com.libDB.api.service;

import java.util.List;

import javax.annotation.Resource;

import org.springframework.stereotype.Component;

import com.libDB.api.dao.TransactionDao;
import com.libDB.api.entity.TransactionView;

@Component
public class TransactionServiceImpl implements TransactionService {
    
    @Resource
    TransactionDao TransactionDao;

    @Override
    public List<TransactionView> getTransactionsByMember(String memberID) {
        return TransactionDao.getTransactionsByMember(memberID);
    }
}