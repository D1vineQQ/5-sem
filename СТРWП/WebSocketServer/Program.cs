using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.Map("/ws", async (context) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var ws = await context.WebSockets.AcceptWebSocketAsync();
        while (true)
        {
            var message = DateTime.Now.ToString("HH:mm:ss");
            var bytes = Encoding.UTF8.GetBytes(message);
            var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
            if(ws.State == WebSocketState.Open)
            {
                await ws.SendAsync(
                    arraySegment,
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }
            else if(ws.State == WebSocketState.Closed || ws.State == WebSocketState.Aborted)
            {
                break;
            }
            Thread.Sleep(2000);
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});

app.MapGet("/", async (context) => {
    context.Response.ContentType = "text/html";

    await context.Response.SendFileAsync("WsDate.html");
});

app.Run();