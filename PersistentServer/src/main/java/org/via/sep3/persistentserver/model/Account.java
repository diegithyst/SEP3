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

    private String name;
    @ManyToOne
    @JoinColumn(name = "client_id")
    private Client owner;
    @OneToMany(mappedBy = "account", cascade = CascadeType.ALL,orphanRemoval = true)
    private List<Currency> currencies = new ArrayList<>();
    @OneToMany(mappedBy = "account", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<MoneyTransfer> moneyTransfers = new ArrayList<>();
    public Account(String mainCurrency, Boolean loan, Client owner, String name) {
        this.mainCurrency = mainCurrency;
        this.loan = loan;
        this.owner = owner;
        this.name = name;
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

    public Client getOwner() {
        return owner;
    }

    public void setOwner(Client owner) {
        this.owner = owner;
    }
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public GrpcAccount getProtoAccount(){
        return GrpcAccount.newBuilder().setAccountId(getId())
                .setMainCurrency(getMainCurrency())
                .setLoan(getLoan())
                .setName(getName())
                .setClientId(getOwner().getId()).build();

    }

    public List<Currency> getCurrencies() {
        return currencies;
    }

    public List<MoneyTransfer> getMoneyTransfers() {
        return moneyTransfers;
    }

    @Override
    public String toString() {
        return "Account{" +
                "identifier=" + id +
                ", mainCurrency='" + mainCurrency + '\'' +
                ", loan=" + loan +
                ", currencies=" + currencies +
                ", ownerId='" + owner.getId() + '\'' +
                ", name=" + name +
                '}';
    }
}
