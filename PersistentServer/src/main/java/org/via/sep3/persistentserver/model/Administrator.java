package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcAdministrator;

import java.util.List;

@Entity(name = "Administrator")
@Table(name = "administrator",
        uniqueConstraints = @UniqueConstraint(name = "uk_administrator_username",columnNames = {"username"}))
public class Administrator {
    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "administratoridseq")
    @SequenceGenerator(allocationSize = 1, name = "administratoridseq")
    private Long id;
    private String username;
    private String password;

    public Administrator(String username, String password) {
        this.username = username;
        this.password = password;
    }

    public Administrator() {
    }

    public Long getId() {
        return id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }


    public GrpcAdministrator getProtoAdministrator(){
        return GrpcAdministrator.newBuilder()
                .setAdministratorId(getId())
                .setUsername(getUsername())
                .setPassword(getPassword())
                .build();
    }
    @Override
    public String toString() {
        return "Administrator{" +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                '}';
    }
}
