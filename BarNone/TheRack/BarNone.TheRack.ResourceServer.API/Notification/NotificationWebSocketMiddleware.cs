using BarNone.TheRack.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarNone.TheRack.ResourceServer.API
{
    public class NotificationWebSocketMiddleware
    {
        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        private readonly RequestDelegate _next;

        public NotificationWebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            var claim = context.User.Identities.FirstOrDefault(ci =>
            {
                var c = ci.FindFirst("User");
                if (c == null) return false;
                return (c.Value == "IAmMickey");
            });

            if (claim == null || !claim.IsAuthenticated)
            {
                await _next.Invoke(context);
                return;
            }

            var claimsIdentity = context.User.Identity as ClaimsIdentity;
            var userIDClaim = claimsIdentity.FindFirst("UserID");

            CancellationToken ct = context.RequestAborted;
            WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();

            try
            {
                await NotificationManager.AcceptSocket(Convert.ToInt32(userIDClaim.Value), new NotificationSocket(currentSocket, ct));
            } catch(Exception e)
            {
                Debug.WriteLine(e);
            }

            //await _next.Invoke(context);
            //return;

            //return Convert.ToInt32(userIDClaim.Value);


            //var socketId = Guid.NewGuid().ToString();

            //_sockets.TryAdd(socketId, currentSocket);

            //NotificationManager.AcceptSocket(new NotificationSocket(currentSocket, ct));

            //while (true)
            //{
            //    if (ct.IsCancellationRequested)
            //    {
            //        break;
            //    }

            //    var response = await ReceiveStringAsync(currentSocket, ct);
            //    if (string.IsNullOrEmpty(response))
            //    {
            //        if (currentSocket.State != WebSocketState.Open)
            //        {
            //            break;
            //        }

            //        continue;
            //    }

            //    foreach (var socket in _sockets)
            //    {
            //        if (socket.Value.State != WebSocketState.Open)
            //        {
            //            continue;
            //        }

            //        await SendStringAsync(socket.Value, response, ct);
            //    }
            //}

            //WebSocket dummy;
            //_sockets.TryRemove(socketId, out dummy);

            //await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
            //currentSocket.Dispose();
            //}

            await Receive(currentSocket, (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Close)
                {
                //await _socketManager.RemoveSocket(id);
                return;
                }
            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }

        public static async Task NotifyAll(string message)
        {
            foreach (var socket in _sockets)
            {
                if (socket.Value.State != WebSocketState.Open)
                {
                    continue;
                }

                await SendStringAsync(socket.Value, message);
            }
        }

        private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }

        private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();

                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return null;
                }

                // Encoding UTF8: https://tools.ietf.org/html/rfc6455#section-5.6
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
