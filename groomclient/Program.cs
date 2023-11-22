using gRoom.gRPC.Messages;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5009");

var client = new Groom.GroomClient(channel);

Console.WriteLine("Enter room name to Register: ");

var roomName = Console.ReadLine();

var resp = client.RegisterTo(new RoomRegistrationRequest { RoomName = roomName });

Console.WriteLine($"Room Id: {resp.RoomId}");

Console.Read();
