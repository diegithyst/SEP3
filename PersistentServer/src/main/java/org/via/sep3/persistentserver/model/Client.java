package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;

import java.util.ArrayList;
import java.util.List;

@Entity(name = "Client")
@Table(name = "client")
public class Client {
    @Id
    @GeneratedValue
    private Long id;
    private String userName;
    private String firstName;
    private String lastName;
    private String password;
    private String country;
    private String identityDocument;
    private String birthday;
    private String planType;

    @OneToMany(mappedBy = "owner", cascade = CascadeType.ALL,orphanRemoval = true)
    private List<Account> accounts = new ArrayList<>();

    public Client(String userName,String firstName, String lastName, String password, String country, String identityDocument, String birthday, String planType) {
        this.userName = userName;
        this.firstName = firstName;
        this.lastName = lastName;
        this.password = password;
        this.country = country;
        this.identityDocument = identityDocument;
        this.birthday = birthday;
        this.planType = planType;
    }

    public Client(){}


    public Long getId() {
        return id;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }

    public String getIdentityDocument() {
        return identityDocument;
    }

    public void setIdentityDocument(String identityDocument) {
        this.identityDocument = identityDocument;
    }

    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    public String getPlanType() {
        return planType;
    }

    public void setPlanType(String planType) {
        this.planType = planType;
    }

    public List<Account> getAccounts() {
        return accounts;
    }

    public org.via.sep3.persistentserver.proto.Client getProtoClient(){
        return org.via.sep3.persistentserver.proto.Client.newBuilder()
                .setClientId(getId())
                .setUserName(getUserName())
                .setFirstName(getFirstName())
                .setLastName(getLastName())
                .setPassword(getPassword())
                .setCountry(getCountry())
                .setBirthday(getBirthday())
                .setIdentityDocument(getIdentityDocument())
                .setPlanType(getPlanType())
                .build();
    }

    @Override
    public String toString() {
        return "Client{" +
                "userName='" + userName + '\'' +
                ", firstName='" + firstName + '\'' +
                ", lastName='" + lastName + '\'' +
                ", password='" + password + '\'' +
                ", country='" + country + '\'' +
                ", identityDocument='" + identityDocument + '\'' +
                ", birthday='" + birthday + '\'' +
                ", planType='" + planType + '\'' +
                '}';
    }
}
