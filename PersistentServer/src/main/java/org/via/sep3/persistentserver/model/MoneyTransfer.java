package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcMoneyTransfer;

@Entity(name = "MoneyTransfer")
@Table(name = "moneyTransfer")
public class MoneyTransfer {
    @Id
    @GeneratedValue
    private Long id;
    private Long senderId;
    private Long recipientId;
    private String senderCurrency;
    private Double amount;
    private Double commission;
    @ManyToOne
    @JoinColumn(name = "account_id")
    private Account account;
    public MoneyTransfer(Long senderId, Long recipientId, String senderCurrency, Double amount, Double commission, Account account) {
        this.senderId = senderId;
        this.recipientId = recipientId;
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

    public Long getSenderId() {
        return senderId;
    }

    public void setSenderId(Long senderId) {
        this.senderId = senderId;
    }

    public Long getRecipientId() {
        return recipientId;
    }

    public void setRecipientId(Long recipientId) {
        this.recipientId = recipientId;
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
                .setSenderId(getSenderId())
                .setRecipientId(getRecipientId())
                .setSenderCurrency(getSenderCurrency())
                .setAmount(getAmount())
                .setCommission(getCommission())
                .build();
    }
    @Override
    public String toString() {
        return "MoneyTransfer{" +
                "sender='" + senderId + '\'' +
                ", recipient='" + recipientId + '\'' +
                ", senderCurrency='" + senderCurrency + '\'' +
                ", amount=" + amount +
                ", commission=" + commission +
                '}';
    }
}
