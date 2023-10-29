package org.via.sep3.persistentserver.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity(name = "Client")
@Table(name = "client")
public class Client {
    @Id
    @GeneratedValue
    private Long id;
    private String name;
    private String country;
    private String identityDocument;
    private String birthday;
    private String planType;

    public Client(String name, String country, String identityDocument, String birthday, String planType) {
        this.name = name;
        this.country = country;
        this.identityDocument = identityDocument;
        this.birthday = birthday;
        this.planType = planType;
    }

    public Client(){}


    public Long getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
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

    public org.via.sep3.persistentserver.proto.Client getProtoClient(){
        return org.via.sep3.persistentserver.proto.Client.newBuilder()
                .setClientId(getId())
                .setName(getName())
                .setCountry(getCountry())
                .setBirthday(getBirthday())
                .setIdentityDocument(getIdentityDocument())
                .setPlanType(getPlanType())
                .build();
    }
    @Override
    public String toString() {
        return "Client{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", country='" + country + '\'' +
                ", identityDocument='" + identityDocument + '\'' +
                ", birthday='" + birthday + '\'' +
                ", planType='" + planType + '\'' +
                '}';
    }
}
