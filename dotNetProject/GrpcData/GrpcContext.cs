using Domain.DTOs;
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
            channel = GrpcChannel.ForAddress("http://localhost:50051");
            Psc = new PersistentServer.PersistentServerClient(channel);
        }

        public Client CreateClient(ClientCreationDTO dto)
        {
            Client createdClient = Psc.CreateClient(dto);
            return createdClient;
        }
    }
}
