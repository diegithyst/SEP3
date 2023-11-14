package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcAccount;

import java.util.ArrayList;
import java.util.List;

@Entity (name = "Account")
@Table (name = "account")
public class Account {
    @Id
    @GeneratedValue
    private Long id;
    private String mainCurrency;
    private Boolean loan;
    private Double balance;
    @ManyToOne
    @JoinColumn(name = "client_id")
    private Client owner;
    @OneToMany(mappedBy = "account", cascade = CascadeType.ALL,orphanRemoval = true)
    private List<Currency> currencies = new ArrayList<>();
    public Account(String mainCurrency, Boolean loan, Double balance, Client owner) {
        this.mainCurrency = mainCurrency;
        this.loan = loan;
        this.balance = balance;
        this.owner = owner;
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

    public Client getOwner() {
        return owner;
    }

    public void setOwner(Client owner) {
        this.owner = owner;
    }

    public GrpcAccount getProtoAccount(){
        return GrpcAccount.newBuilder().setAccountId(getId())
                .setBalance(getBalance())
                .setMainCurrency(getMainCurrency())
                .setLoan(getLoan())
                .setClientId(getOwner().getId()).build();
    }

    public List<Currency> getCurrencies() {
        return currencies;
    }

    @Override
    public String toString() {
        return "Account{" +
                "identifier=" + id +
                ", mainCurrency='" + mainCurrency + '\'' +
                ", loan=" + loan +
                ", balance=" + balance +
                ", currencies=" + currencies +
                ", ownerId='" + owner.getId() + '\'' +
                '}';
    }
}
