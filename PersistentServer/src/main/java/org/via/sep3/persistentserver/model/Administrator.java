package org.via.sep3.persistentserver.model;

import jakarta.persistence.*;
import org.via.sep3.persistentserver.proto.GrpcAdministrator;

import java.util.List;

@Entity(name = "Administrator")
@Table(name = "administrator")
public class Administrator {
    @Id
    @GeneratedValue
    private long id;
    private String username;
    private String password;
    @OneToMany(mappedBy = "admin", cascade = CascadeType.ALL,orphanRemoval = true)
    private List<Client> clients;

    public Administrator(String username, String password) {
        this.username = username;
        this.password = password;
    }

    public Administrator() {
    }

    public long getId() {
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

    public List<Client> getClients() {
        return clients;
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
