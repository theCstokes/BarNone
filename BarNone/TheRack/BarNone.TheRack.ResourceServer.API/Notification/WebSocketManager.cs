using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarNone.TheRack.ResourceServer.API.Notification
{
    public static class WebSocketManager
    {
        private static Dictionary<Guid, WebSocket> _webSockets;

        static WebSocketManager()
        {
            _webSockets = new Dictionary<Guid, WebSocket>();
        }

        public static void Add(WebSocket socket)
        {
            _webSockets[Guid.NewGuid()] = socket;
        }

        public static async void NotifyAll(string message)
        {
            var token = CancellationToken.None;
            byte[] data = Encoding.ASCII.GetBytes(message);

            var type = WebSocketMessageType.Text;
            var buffer = new ArraySegment<Byte>(data);
            await Task.WhenAll(_webSockets.Values.Select(async socket => await socket.SendAsync(buffer, type, true, token)));
        }
    }
}
