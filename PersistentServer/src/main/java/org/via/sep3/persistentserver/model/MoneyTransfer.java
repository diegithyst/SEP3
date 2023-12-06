package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcMoneyTransfer;

@Entity(name = "MoneyTransfer")
@Table(name = "moneyTransfer")
public class MoneyTransfer {
    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "moneytransferidseq")
    @SequenceGenerator(allocationSize = 1, name = "moneytransferidseq")
    private Long id;
    @ManyToOne
    @JoinColumn(name = "recipient_id")
    private Account recipient;
    private String senderCurrency;
    private Double amount;
    @ManyToOne
    @JoinColumn(name = "sender_id")
    private Account sender;
    public MoneyTransfer(Account recipient, String senderCurrency, Double amount, Account sender) {
        this.recipient = recipient;
        this.senderCurrency = senderCurrency;
        this.amount = amount;
        this.sender = sender;
    }
    public MoneyTransfer() {
    }
    public Long getId() {
        return id;
    }

    public Account getRecipient() {
        return recipient;
    }

    public void setRecipient(Account recipient) {
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

    public Account getSender() {
        return sender;
    }

    public void setSender(Account sender) {
        this.sender = sender;
    }

    public GrpcMoneyTransfer getProtoMoneyTransfer(){
        return GrpcMoneyTransfer.newBuilder().setMoneyTransferId(getId())
                .setSenderId(sender.getId())
                .setRecipientId(getRecipient().getId())
                .setSenderCurrency(getSenderCurrency())
                .setAmount(getAmount())
                .build();
    }
    @Override
    public String toString() {
        return "MoneyTransfer{" +
                "sender='" + sender.getId() + '\'' +
                ", recipient='" + recipient.getId() + '\'' +
                ", senderCurrency='" + senderCurrency + '\'' +
                ", amount=" + amount +
                '}';
    }
}
