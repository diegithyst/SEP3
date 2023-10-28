package org.via.sep3.persistentserver;

import io.grpc.stub.StreamObserver;
import org.via.sep3.persistentserver.proto.ClientBasicDTO;
import org.via.sep3.persistentserver.proto.PersistentServerGrpc;
import org.via.sep3.persistentserver.proto.TotalBalance;

public class PersistentServerImpl extends PersistentServerGrpc.PersistentServerImplBase {
    @Override
    public void getTotalBalance(ClientBasicDTO request, StreamObserver<TotalBalance> responseObserver) {
        TotalBalance reply = TotalBalance.newBuilder().setTotalBalance(122.2).setCurrencyType(2).build();
        responseObserver.onNext(reply);
        responseObserver.onCompleted();
    }
}
