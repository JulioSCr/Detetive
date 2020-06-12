using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Hubs
{
    public class HubSala : Hub
    {
        public void EnviarMensagem(string apelido, string mensagem)
        {
            Clients.All.TransmitirMensagem(apelido, mensagem);
        }

    }
}