using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication2.Models.SignalR;

namespace WebApplication2.Hubs
{
    public class Chat : Hub
    {
        //список подключенных пользователей
        static List<User> users = new List<User>();

        //метод, который будет получать сообщения от пользователей и пересылать его другим пользователям
        public void SendMessage(string id, string message)
        {
            Clients.All.sendMessage(id, message);
        }


        //срабатывает при подключении нового пользователя
        public void OnConnected(string nickname)
        {
            //идентификатор подключения нового пользователя
            var connId = Context.ConnectionId;

            //пользователя не существует
            if (!users.Any(u => u.ConnectionId.Equals(connId)))
            {
                //создание нового пользователя
                var user = new User { ConnectionId = connId, Nickname = nickname };
                //добавление пользователя к общему списку
                users.Add(user);

                //отправка сообщениями подключенному пользователю
                Clients.Caller.onUserConnected(user.ConnectionId, user.Nickname, users.Select(u => new { connectionId = u.ConnectionId, nickname = u.Nickname }));

                //рассылка уведомления подключенным пользователям
                Clients.AllExcept(user.ConnectionId).onNewUserConnected(user.ConnectionId, user.Nickname);

            }
        }

        //срабатывает при отключении пользователя
        public override Task OnDisconnected(bool stopCalled)
        {
            //id отключаемого пользователя
            var connId = Context.ConnectionId;
            //поиск пользователя в списке всех пользователей
            var user = users.FirstOrDefault(u => u.ConnectionId.Equals(connId));

            //пользователь существует
            if (user != null)
            {
                //рассылка уведомления об отключении пользователя
                Clients.All.onUserDisconnected(user.ConnectionId);

                //удаление пользователя из общего списка
                users.Remove(user);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}