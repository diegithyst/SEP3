package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.checkerframework.checker.units.qual.K;
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
    private Double euro;
    private Double krone;
    private Double pound;
    @ManyToOne
    @JoinColumn(name = "client_id")
    private Client owner;
    @OneToMany(mappedBy = "senderId", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<MoneyTransfer> moneyTransfers = new ArrayList<>();
    public Account(String mainCurrency, Double euro, Double krone, Double pound, Boolean loan, Client owner, String name) {
        this.mainCurrency = mainCurrency;
        this.loan = loan;
        this.owner = owner;
        this.name = name;
        this.euro = euro;
        this.krone = krone;
        this.pound = pound;
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

    public Double getEuro() {
        return euro;
    }

    public void setEuro(Double euro) {
        this.euro = euro;
    }

    public Double getKrone() {
        return krone;
    }

    public void setKrone(Double krone) {
        this.krone = krone;
    }

    public Double getPound() {
        return pound;
    }

    public void setPound(Double pound) {
        this.pound = pound;
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
                .setEuro(getEuro())
                .setKrone(getKrone())
                .setPound(getPound())
                .setLoan(getLoan())
                .setName(getName())
                .setClientId(getOwner().getId()).build();

    }


    public List<MoneyTransfer> getMoneyTransfers() {
        return moneyTransfers;
    }

    @Override
    public String toString() {
        return "Account{" +
                "identifier=" + id +
                ", mainCurrency='" + mainCurrency + '\'' +
                ", euro='" + euro +
                ", krone='" + krone +
                ", pound='" + pound +
                ", loan=" + loan +
                ", ownerId='" + owner.getId() + '\'' +
                ", name=" + name +
                '}';
    }
}
