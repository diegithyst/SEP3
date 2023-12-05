package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcMoneyTransfer;

@Entity(name = "MoneyTransfer")
@Table(name = "moneyTransfer")
public class MoneyTransfer {
    @Id
    @GeneratedValue
    private Long id;
    private String sender;
    private String recipient;
    private String senderCurrency;
    private Double amount;
    private Double commission;
    @ManyToOne
    @JoinColumn(name = "account_id")
    private Account account;
    public MoneyTransfer(String sender, String recipient, String senderCurrency, Double amount, Double commission, Account account) {
        this.sender = sender;
        this.recipient = recipient;
        this.senderCurrency = senderCurrency;
        this.amount = amount;
        this.commission = commission;
        this.account = account;
    }
    public MoneyTransfer() {
    }
    public Long getId() {
        return id;
    }

    public String getSender() {
        return sender;
    }

    public void setSender(String sender) {
        this.sender = sender;
    }

    public String getRecipient() {
        return recipient;
    }

    public void setRecipient(String recipient) {
        this.recipient = recipient;
    }

    public String getSenderCurrency() {
        return senderCurrency;
    }

    public void setSenderCurrency(String senderCurrency) {
        this.senderCurrency = senderCurrency;
    }

    public Double getAmount() {
        return amount;
    }

    public void setAmount(Double amount) {
        this.amount = amount;
    }

    public Double getCommission() {
        return commission;
    }

    public void setCommission(Double commission) {
        this.commission = commission;
    }

    public Account getAccount() {
        return account;
    }

    public void setAccount(Account account) {
        this.account = account;
    }

    public GrpcMoneyTransfer getProtoMoneyTransfer(){
        return GrpcMoneyTransfer.newBuilder()
                .setSender(getSender())
                .setReceipt(getRecipient())
                .setSenderCurrency(getSenderCurrency())
                .setAmount(getAmount())
                .setCommission(getCommission())
                .build();
    }
    @Override
    public String toString() {
        return "MoneyTransfer{" +
                "sender='" + sender + '\'' +
                ", recipient='" + recipient + '\'' +
                ", senderCurrency='" + senderCurrency + '\'' +
                ", amount=" + amount +
                ", commission=" + commission +
                '}';
    }
}
