package org.via.sep3.persistentserver;

import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.via.sep3.persistentserver.model.Account;
import org.via.sep3.persistentserver.model.Currency;
import org.via.sep3.persistentserver.proto.*;

public class PersistentServerImpl extends PersistentServerGrpc.PersistentServerImplBase {
    private SessionFactory sf;
    public PersistentServerImpl(SessionFactory sf) {
        super();
        this.sf = sf;
    }

    @Override
    public void getTotalBalance(ClientBasicDTO request, StreamObserver<TotalBalance> responseObserver) {
        TotalBalance reply = TotalBalance.newBuilder().setTotalBalance(122.2).setCurrencyName(2).build();
        responseObserver.onNext(reply);
        responseObserver.onCompleted();
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
    public void getCurrencyById(CurrencyBasicDTO request, StreamObserver<GrpcCurrency> responseObserver) {
        try(Session s = sf.openSession()){
            Currency c = s.get(Currency.class,request.getCurrencyId());
            if(c != null){
                responseObserver.onNext(c.getProtoCurrency());
                responseObserver.onCompleted();
            }else {
                responseObserver.onError(Status.NOT_FOUND.asException());
            }
        }
    }

    @Override
    public void getCurrencies(AccountBasicDTO request, StreamObserver<GrpcCurrencies> responseObserver) {
        try(Session s = sf.openSession()){
            Account a = s.get(Account.class,request.getAccountId());
            if(a != null){
                GrpcCurrencies.Builder gas = GrpcCurrencies.newBuilder();
                for(Currency c : a.getCurrencies()){
                    gas.addCurrencies(c.getProtoCurrency());
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
            a.setOwner( s.get(org.via.sep3.persistentserver.model.Client.class,request.getClientId()));
            a.setMainCurrency(request.getMainCurrency());
            s.persist(a);
            s.persist(new Currency(request.getMainCurrency(),0.00,a));
            t.commit();
            responseObserver.onNext(a.getProtoAccount());
            responseObserver.onCompleted();
        }
    }
}
