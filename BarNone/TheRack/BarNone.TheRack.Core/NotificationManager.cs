using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;

namespace BarNone.TheRack.Core
{
    //public class NotificationEntity<TDTO>
    //{
    //    public static readonly NotificationEntity<Comment> Comment = new NotificationEntity<Comment>();
    //}

    public interface INotificationProvider
    {
        void Register(int userID, NotificationSocket socket, WhereFunc filter);

        void Run(int userID, object dto);
    }

    public class NotificationSubscription<TDTO>
    {
        public NotificationSubscription(int userID, NotificationSocket socket, WhereFunc filter = null)
        {
            UserID = userID;
            Socket = socket;
            if (filter == null) filter = (dto) => true;
            Filter = filter;
        }

        public int UserID { get; private set; }

        public NotificationSocket Socket { get; private set; }

        public WhereFunc Filter { get; private set; }

        public async void Notify()
        {
            await Socket.SendAsync("Test");
        }
    }

    public class NotificationSocket
    {
        private WebSocket _socket;
        private CancellationToken _token;

        public NotificationSocket(WebSocket socket, CancellationToken token)
        {
            _socket = socket;
            _token = token;
        }

        public async Task SendAsync(string data)
        {
            if (_socket.State != WebSocketState.Open)
            {
                return;
            }

            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            await _socket.SendAsync(segment, WebSocketMessageType.Text, true, _token);
        }

        public async Task<NotificationRequestDTO> ReceiveAsync()
        {
            if (_socket.State != WebSocketState.Open)
            {
                return null;
            }

            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    _token.ThrowIfCancellationRequested();

                    result = await _socket.ReceiveAsync(buffer, _token);
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
                    return JsonConvert
                        .DeserializeObject<NotificationRequestDTO>(await reader.ReadToEndAsync());
                }
            }
        }
    }

    public class NotificationRequestDTO
    {
        public string Type { get; set; }

        public FilterDTO Filter { get; set; }
    }

    public static class NotificationManager
    {
        private static ConcurrentDictionary<string, INotificationProvider> _managers
            = new ConcurrentDictionary<string, INotificationProvider>();

        public static readonly NotificationProvider<Comment> Comment = Register("Comment", new NotificationProvider<Comment>());

        public static readonly NotificationProvider<User> User = Register("User", new NotificationProvider<User>());



        public static async Task AcceptSocket(int userID, NotificationSocket socket)
        {
            var request = await socket.ReceiveAsync();
            if (request == null) return;
            if (!_managers.ContainsKey(request.Type)) return;

            _managers[request.Type].Register(userID, socket, request.Filter.GetWhere());
        }

        private static NotificationProvider<TDTO> Register<TDTO>(string name, NotificationProvider<TDTO> manager)
        {
            _managers.TryAdd(name, manager);
            return manager;
        }
    }

    public class NotificationProvider<TDTO> : INotificationProvider
    {
        List<NotificationSubscription<TDTO>> _subscribers;

        public NotificationProvider()
        {
            _subscribers = new List<NotificationSubscription<TDTO>>();
        }

        //public void Run(int userID, TDTO dto = default(TDTO))
        //{
        //    _subscribers
        //        .Where(s => s.UserID == userID && s.Filter(dto))
        //        .ToList()
        //        .ForEach(s => s.Notify(dto));
        //}

        public void Run(int userID, object dto)
        {
            _subscribers
                //.Where(s => s.UserID == userID)
                .Where(s => s.Filter(dto))
                .ToList()
                .ForEach(s => s.Notify());
        }

        public void Register(int userID, NotificationSocket socket, WhereFunc filter)
        {
            _subscribers
                .Add(new NotificationSubscription<TDTO>(userID, socket, filter));
        }

        void INotificationProvider.Register(int userID, NotificationSocket socket, WhereFunc filter)
        {
            //if (filter == null)
            //{
            //    Register(userID, socket, ();
            //} else
            //{
                Register(userID, socket, (dto) => filter(dto));
            //}
        }
    }
}
