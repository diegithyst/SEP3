package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcCurrency;

@Entity(name = "Currency")
@Table(name = "currency")
public class Currency {
    @Id
    @GeneratedValue
    private Long id;
    private String name;
    private Double balance;
    @ManyToOne
    @JoinColumn(name = "account_id")
    private Account account;

    public Currency(String name, Double balance, Account account) {
        this.name = name;
        this.balance = balance;
        this.account = account;
    }
    public Currency(){

    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Double getBalance() {
        return balance;
    }

    public void setBalance(Double balance) {
        this.balance = balance;
    }

    public Account getAccount() {
        return account;
    }

    public void setAccount(Account account) {
        this.account = account;
    }

    public Long getId() {
        return id;
    }

    public GrpcCurrency getProtoCurrency(){
        return GrpcCurrency.newBuilder().setCurrencyId(getId())
                .setName(getName())
                .setBalance(getBalance())
                .setAccountId(getAccount().getId()).build();
    }

    @Override
    public String toString() {
        return "Currency{" +
                "name='" + name + '\'' +
                ", balance=" + balance +
                ", accountId=" + account.getId() +
                '}';
    }
}
