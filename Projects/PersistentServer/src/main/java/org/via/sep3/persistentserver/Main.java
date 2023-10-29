package org.via.sep3.persistentserver;
import io.grpc.Grpc;
import io.grpc.InsecureServerCredentials;
import io.grpc.Server;
import io.grpc.stub.StreamObserver;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;
import org.via.sep3.persistentserver.model.Client;

import java.io.File;
import java.io.IOException;
import java.util.concurrent.TimeUnit;
import java.util.logging.Logger;
public class Main {
    private final Logger logger = Logger.getLogger(Main.class.getName());

    private SessionFactory sf = null;
    private Server server;

    private void start() throws IOException {
        /* The port on which the server should run */
        int port = 50051;
        server = Grpc.newServerBuilderForPort(port, InsecureServerCredentials.create())
                .addService(new PersistentServerImpl(sf))
                .build()
                .start();
        logger.info("Server started, listening on " + port);
        Runtime.getRuntime().addShutdownHook(new Thread() {
            @Override
            public void run() {
                // Use stderr here since the logger may have been reset by its JVM shutdown hook.
                System.err.println("*** shutting down gRPC server since JVM is shutting down");
                try {
                    Main.this.stop();
                } catch (InterruptedException e) {
                    e.printStackTrace(System.err);
                }
                sf.close();
                System.err.println("*** server shut down");
            }
        });
    }

    private void stop() throws InterruptedException {
        if (server != null) {
            server.shutdown().awaitTermination(30, TimeUnit.SECONDS);
        }
    }

    /**
     * Await termination on the main thread since the grpc library uses daemon threads.
     */
    private void blockUntilShutdown() throws InterruptedException {
        if (server != null) {
            server.awaitTermination();
        }
    }

    public Main(SessionFactory sf){
        this.sf = sf;
    }
    /**
     * Main launches the server from the command line.
     */
    public static void main(String[] args) throws IOException, InterruptedException {
        MyLogger.getInstance();
        final StandardServiceRegistry registry = new StandardServiceRegistryBuilder().loadProperties(new File("src/main/config/hibernate.properties")).build();
        try {
            SessionFactory sf = new MetadataSources(registry)
                            .addAnnotatedClasses(Client.class)
                            .buildMetadata()
                            .buildSessionFactory();
            try(Session s = sf.openSession()){
                if (s.createQuery("from Client", Client.class).stream().count()<1){
                    Transaction t = s.beginTransaction();
                    MyLogger.getInstance().log("***psinit","db is empty so put some bootstrap data into it. ");
                    s.persist(new Client("en","hungary","AAAAA","31042000","Premium"));
                    t.commit();
                } else {
                    for(Client c:s.createQuery("from Client", Client.class).list()){
                        MyLogger.getInstance().log("***psinit","db has a client: " +c);
                    }
                }
            }
            final Main server = new Main(sf);
            server.start();
            server.blockUntilShutdown();
        }

        catch (Exception e) {
            // The registry would be destroyed by the SessionFactory, but we
            // had trouble building the SessionFactory so destroy it manually.
            StandardServiceRegistryBuilder.destroy(registry);
        }

    }
}
