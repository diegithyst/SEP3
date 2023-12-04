package org.via.sep3.persistentserver;

import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.via.sep3.persistentserver.model.Account;
import org.via.sep3.persistentserver.model.Administrator;
import org.via.sep3.persistentserver.model.MoneyTransfer;
import org.via.sep3.persistentserver.proto.*;

import java.util.List;

public class PersistentServerImpl extends PersistentServerGrpc.PersistentServerImplBase {
    private SessionFactory sf;
    public PersistentServerImpl(SessionFactory sf) {
        super();
        this.sf = sf;
    }
    @Override
    public void getClientById(ClientBasicDTO request, StreamObserver<Client> responseObserver) {
        try(Session s = sf.openSession()){
            org.via.sep3.persistentserver.model.Client c = s.get(org.via.sep3.persistentserver.model.Client.class,request.getClientId());
            if(c != null){
                responseObserver.onNext(c.getProtoClient());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void getClientByUsername(ClientUsernameDTO request, StreamObserver<Client> responseObserver) {
        try(Session s = sf.openSession()){
            org.via.sep3.persistentserver.model.Client c = s.get(org.via.sep3.persistentserver.model.Client.class,request.getUsername());
            if(c != null){
                responseObserver.onNext(c.getProtoClient());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void getAccountById(AccountBasicDTO request, StreamObserver<GrpcAccount> responseObserver) {
        try(Session s = sf.openSession()){
            org.via.sep3.persistentserver.model.Account a = s.get(org.via.sep3.persistentserver.model.Account.class,request.getAccountId());
            if(a != null){
                responseObserver.onNext(a.getProtoAccount());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }
    @Override
    public void getClientAccounts(ClientBasicDTO request, StreamObserver<GrpcAccounts> responseObserver) {
        try(Session s = sf.openSession()){
            org.via.sep3.persistentserver.model.Client c = s.get(org.via.sep3.persistentserver.model.Client.class,request.getClientId());
            if(c != null){
                GrpcAccounts.Builder gas = GrpcAccounts.newBuilder();
                for(Account a : c.getAccounts()){
                    gas.addAccounts(a.getProtoAccount());
                }
                responseObserver.onNext(gas.build());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void createClient(ClientCreationDTO request, StreamObserver<Client> responseObserver) {
        try(Session s = sf.openSession()){
            Transaction t = s.beginTransaction();
            org.via.sep3.persistentserver.model.Client c = new org.via.sep3.persistentserver.model.Client();
            c.setUserName(request.getUserName());
            c.setFirstName(request.getFirstName());
            c.setLastName(request.getLastName());
            c.setPassword(request.getPassword());
            c.setCountry(request.getCountry());
            c.setIdentityDocument(request.getIdentityDocument());
            c.setBirthday(request.getBirthday());
            c.setPlanType(request.getPlanType());
            s.persist(c);
            t.commit();
            responseObserver.onNext(c.getProtoClient());
            responseObserver.onCompleted();
        }
    }

    @Override
    public void createAccount(AccountCreationDTO request, StreamObserver<GrpcAccount> responseObserver) {
        try(Session s = sf.openSession()){
            Transaction t = s.beginTransaction();
            org.via.sep3.persistentserver.model.Account a = new org.via.sep3.persistentserver.model.Account();
            a.setAccountViewId(request.getAccountViewId());
            a.setOwner( s.get(org.via.sep3.persistentserver.model.Client.class,request.getClientId()));
            a.setMainCurrency(request.getMainCurrency());
            a.setEuro(request.getEuro());
            a.setKrone(request.getKrone());
            a.setPound(request.getPound());
            a.setName(request.getName());
            a.setLoan(request.getLoan());
            s.persist(a);

            t.commit();
            responseObserver.onNext(a.getProtoAccount());
            responseObserver.onCompleted();
        }
    }

    @Override
    public void getAdministratorByID(AdministratorBasicDTO request, StreamObserver<GrpcAdministrator> responseObserver) {
        try(Session s = sf.openSession()){
            Administrator a = s.get(Administrator.class,request.getAdministratorId());
            if(a != null){
                responseObserver.onNext(a.getProtoAdministrator());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void getClients(Empty request, StreamObserver<GrpcClients> responseObserver) {
        try(Session s = sf.openSession()){
           List<org.via.sep3.persistentserver.model.Client> clients = s.createQuery("from Client", org.via.sep3.persistentserver.model.Client.class)
                   .getResultList();
            GrpcClients.Builder gas = GrpcClients.newBuilder();
           for(org.via.sep3.persistentserver.model.Client cl : clients){
               gas.addClients(cl.getProtoClient());
           }
           responseObserver.onNext(gas.build());
           responseObserver.onCompleted();
        }
    }
    @Override
    public void getMoneyTransferById(MoneyTransferBasicDTO request, StreamObserver<GrpcMoneyTransfer> responseObserver) {
        try(Session s = sf.openSession()){
            MoneyTransfer m = s.get(MoneyTransfer.class,request.getMoneyTransferId());
            if(m != null){
                responseObserver.onNext(m.getProtoMoneyTransfer());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void getMoneyTransfers(AccountBasicDTO request, StreamObserver<GrpcMoneyTransfers> responseObserver) {
        try(Session s = sf.openSession()){
            Account a = s.get(Account.class,request.getAccountId());
            if(a != null){
                GrpcMoneyTransfers.Builder gas = GrpcMoneyTransfers.newBuilder();
                for(MoneyTransfer m : a.getMoneyTransfers()){
                    gas.addMoneyTransfers(m.getProtoMoneyTransfer());
                }
                responseObserver.onNext(gas.build());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void deleteClient(ClientBasicDTO request, StreamObserver<GrpcResult> responseObserver) {
        try(Session s = sf.openSession()){
             s.remove(s.getReference(org.via.sep3.persistentserver.model.Client.class,request.getClientId()));
             responseObserver.onNext(GrpcResult.newBuilder().setSuccess(true).build());
             responseObserver.onCompleted();
        }
    }


    @Override
    public void updateClient(ClientUpdateDTO request, StreamObserver<Client> responseObserver) {
        try(Session s = sf.openSession()){
            org.via.sep3.persistentserver.model.Client c = s.get(org.via.sep3.persistentserver.model.Client.class,request.getClientId());
            if(c != null){
                c.setUserName(request.getUserName());
                c.setFirstName(request.getFirstName());
                c.setLastName(request.getLastName());
                c.setPassword(request.getPassword());
                c.setBirthday(request.getBirthday());
                c.setCountry(request.getCountry());
                c.setIdentityDocument(request.getIdentityDocument());
                c.setPlanType(request.getPlanType());
                s.flush();
                responseObserver.onNext(c.getProtoClient());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void updateAccount(AccountUpdateDTO request, StreamObserver<GrpcAccount> responseObserver) {
        try(Session s = sf.openSession()){
            org.via.sep3.persistentserver.model.Account a = s.get(org.via.sep3.persistentserver.model.Account.class,request.getAccountId());
            if(a != null){
                a.setMainCurrency(request.getMainCurrency());
                a.setEuro(request.getEuro());
                a.setKrone(request.getKrone());
                a.setPound(request.getPound());
                a.setName(request.getName());
                responseObserver.onNext(a.getProtoAccount());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void deleteAccount(AccountBasicDTO request, StreamObserver<GrpcResult> responseObserver) {
        try(Session s = sf.openSession()){
            s.remove(s.getReference(org.via.sep3.persistentserver.model.Account.class,request.getAccountId()));
            responseObserver.onNext(GrpcResult.newBuilder().setSuccess(true).build());
            responseObserver.onCompleted();
        }
    }

    @Override
    public void makeMoneyTransfer(CreateMoneyTransferDTO request, StreamObserver<GrpcMoneyTransfer> responseObserver) {
        try(Session s = sf.openSession()){
            Transaction t = s.beginTransaction();
            MoneyTransfer mt = new MoneyTransfer();
            mt.setSender(request.getSender());
            mt.setSenderCurrency(request.getSenderCurrency());
            mt.setAmount(request.getAmount());
            mt.setCommission(request.getCommission());
            mt.setRecipient(request.getReceipt());
            s.persist(mt);
            t.commit();
            responseObserver.onNext(mt.getProtoMoneyTransfer());
            responseObserver.onCompleted();
        }
    }
}
