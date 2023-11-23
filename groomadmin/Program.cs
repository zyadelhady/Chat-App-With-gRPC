using Google.Protobuf.WellKnownTypes;
using gRoom.gRPC.Messages;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5009");
var client = new Groom.GroomClient(channel);

Console.WriteLine("*** Admin Console started ***");
Console.WriteLine("Listening...");

// ADD THE MONITORING CODE BELOW THIS LINE
using var call = client.StartMonitoring(new Empty());

var cts = new CancellationTokenSource();

while (await call.ResponseStream.MoveNext(cts.Token))
{
    var msg = call.ResponseStream.Current;
    Console.WriteLine($"New Message {msg.Contents}, user: {msg.User}, at : {msg.MsgTime}");
}
