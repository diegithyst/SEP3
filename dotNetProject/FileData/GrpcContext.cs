using Grpc.Net.Client;
using PersistentServerClient;

namespace FileData
{
    public class GrpcContext
    {
        GrpcChannel channel;
     public PersistentServer.PersistentServerClient Psc { get; }
        public GrpcContext()
        {
            channel = GrpcChannel.ForAddress("https://localhost:50051");
            Psc = new PersistentServer.PersistentServerClient(channel);
        }
    }
}
