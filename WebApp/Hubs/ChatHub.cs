using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApp
{
    public class ChatHub : Hub
    {
        public void EnviarMensagem(string name, string message, string datahora)
        {
            datahora = Convert.ToString(DateTime.Now);

            Clients.All.addNewMessageToPage(name, message, datahora);
        }
    }
}