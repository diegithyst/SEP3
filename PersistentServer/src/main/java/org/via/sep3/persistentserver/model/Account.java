package org.via.sep3.persistentserver.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import org.via.sep3.persistentserver.proto.GrpcAccount;

@Entity (name = "Account")
@Table (name = "account")
public class Account {
    @Id
    @GeneratedValue
    private Long id;
    private String mainCurrency;
    private Boolean loan;
    private Double balance;
    private Long clientId;

    public Account(String mainCurrency, Boolean loan, Double balance, Long clientId) {
        this.mainCurrency = mainCurrency;
        this.loan = loan;
        this.balance = balance;
        this.clientId = clientId;
    }

    public Account() {
    }

    public Long getId() {
        return id;
    }

    public String getMainCurrency() {
        return mainCurrency;
    }

    public void setMainCurrency(String mainCurrency) {
        this.mainCurrency = mainCurrency;
    }

    public Boolean getLoan() {
        return loan;
    }

    public void setLoan(Boolean loan) {
        this.loan = loan;
    }

    public Double getBalance() {
        return balance;
    }

    public void setBalance(Double balance) {
        this.balance = balance;
    }

    public Long getClientId() {
        return clientId;
    }

    public void setClientId(Long clientId) {
        this.clientId = clientId;
    }

    public GrpcAccount getProtoAccount(){
        return GrpcAccount.newBuilder().setAccountId(getId())
                .setBalance(getBalance())
                .setMainCurrency(getMainCurrency())
                .setLoan(getLoan())
                .setClientId(getClientId()).build();
    }
    @Override
    public String toString() {
        return "Account{" +
                "identifier=" + id +
                ", mainCurrency='" + mainCurrency + '\'' +
                ", loan=" + loan +
                ", balance=" + balance +
                ", ownerId='" + clientId + '\'' +
                '}';
    }
}
