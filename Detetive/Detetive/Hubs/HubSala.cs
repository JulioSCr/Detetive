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

        /// <summary>Envia o movimento para os outros jogadores da sala.</summary>
        /// <param name="ID_JOGADOR_SALA" type="int">ID do JoggadorSala.</param>
        /// <param name="pLinha" type="int">Número da linha.</param>
        /// <param name="pColuna" type="int">Número da coluna.</param>
        /// <param name="pIDLocal" type="int">ID do local em que o jogador está.</param>
        /// <returns type="Void"></returns>
        public void EnviarMovimento(int ID_JOGADOR_SALA, int pLinha, int pColuna, int pIDLocal)
        {
            Clients.All.TransmitirMovimento(ID_JOGADOR_SALA, pLinha, pColuna, pIDLocal);
        }
    }
}