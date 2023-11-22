using Google.Protobuf.WellKnownTypes;
using gRoom.gRPC.Messages;
using Grpc.Core;

namespace gRoom.gRPC.Services;

public class GroomService : Groom.GroomBase
{
    private readonly ILogger<GroomService> _logger;

    public GroomService(ILogger<GroomService> logger)
    {
        _logger = logger;
    }

    public override Task<RoomRegistrationResponse> RegisterToRoom(
        RoomRegistrationRequest request,
        ServerCallContext context
    )
    {
        _logger.LogInformation("Service called...");
        var rnd = new Random();
        var roomNum = rnd.Next(1, 100);
        _logger.LogInformation($"Room no. {roomNum}");
        var resp = new RoomRegistrationResponse { RoomId = roomNum };
        return Task.FromResult(resp);
    }

    public override async Task<NewsStreamStatus> SendNewsFlash(
        IAsyncStreamReader<NewsFlash> newStream,
        ServerCallContext context
    )
    {
        while (await newStream.MoveNext())
        {
            var news = newStream.Current;
            Console.WriteLine($"News flash: {news.NewsItem}");
        }
        return new NewsStreamStatus { Success = true };
    }

    public override async Task StartMonitoring(
        Empty request,
        IServerStreamWriter<ReceivedMessage> streamWriter,
        ServerCallContext context
    )
    {
        while (true)
        {
            await streamWriter.WriteAsync(
                new ReceivedMessage
                {
                    MsgTime = Timestamp.FromDateTime(DateTime.UtcNow),
                    User = "1",
                    Contents = "Test msg"
                }
            );
            await Task.Delay(500);
        }
    }
}
