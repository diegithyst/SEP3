package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcMoneyTransfer;

@Entity(name = "MoneyTransfer")
@Table(name = "moneyTransfer")
public class MoneyTransfer {
    @Id
    @GeneratedValue
    private Long id;
    private Long recipientId;
    private String senderCurrency;
    private Double amount;
    @ManyToOne
    @JoinColumn(name = "account_id")
    private Account senderId;
    public MoneyTransfer(Long recipientId, String senderCurrency, Double amount, Account senderId) {
        this.recipientId = recipientId;
        this.senderCurrency = senderCurrency;
        this.amount = amount;
        this.senderId = senderId;
    }
    public MoneyTransfer() {
    }
    public Long getId() {
        return id;
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

    public Account getSenderId() {
        return senderId;
    }

    public void setSenderId(Account senderId) {
        this.senderId = senderId;
    }

    public GrpcMoneyTransfer getProtoMoneyTransfer(){
        return GrpcMoneyTransfer.newBuilder()
                .setRecipientId(getRecipientId())
                .setSenderCurrency(getSenderCurrency())
                .setAmount(getAmount())
                .build();
    }
    @Override
    public String toString() {
        return "MoneyTransfer{" +
                "sender='" + senderId + '\'' +
                ", recipient='" + recipientId + '\'' +
                ", senderCurrency='" + senderCurrency + '\'' +
                ", amount=" + amount +
                '}';
    }
}
