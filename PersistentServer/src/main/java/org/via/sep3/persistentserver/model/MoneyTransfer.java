package org.via.sep3.persistentserver.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import org.via.sep3.persistentserver.proto.GrpcMoneyTransfer;

@Entity(name = "MoneyTransfer")
@Table(name = "moneyTransfer")
public class MoneyTransfer {
    @Id
    @GeneratedValue
    private long id;
    private String sender;
    private String recipient;
    private String senderCurrency;
    private Double amount;
    private Double commission;

    public MoneyTransfer(String sender, String recipient, String senderCurrency, Double amount, Double commission) {
        this.sender = sender;
        this.recipient = recipient;
        this.senderCurrency = senderCurrency;
        this.amount = amount;
        this.commission = commission;
    }

    public long getId() {
        return id;
    }

    public MoneyTransfer() {
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
