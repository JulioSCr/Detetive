using Detetive.Business.Business;
using Detetive.Business.Business.Interfaces;
using Detetive.Injection;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Detetive.Hubs
{
    public class HubSala : Hub
    {
      
        public void FinalizarTurno (int pIdSala, int pIdJogadorSala)
        {
            try
            {
                Clients.Group(pIdSala.ToString()).TransmitirFinalizarTurno(pIdJogadorSala, pIdJogadorSala);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
        }
        
        /// <summary>Envia o movimento para os outros jogadores da sala.</summary>
        /// <param name="ID_JOGADOR_SALA" type="int">ID do JoggadorSala.</param>
        /// <param name="pLinha" type="int">Número da linha.</param>
        /// <param name="pColuna" type="int">Número da coluna.</param>
        /// <param name="pIDLocal" type="int">ID do local em que o jogador está.</param>
        /// <returns type="Void"></returns>
        public void EnviarMovimento(int pIdSala, int ID_JOGADOR_SALA, int pLinha, int pColuna, int? pIDLocal)
        {
            try
            {
                Clients.Group(pIdSala.ToString()).TransmitirMovimento(ID_JOGADOR_SALA, pLinha, pColuna, pIDLocal);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
        }

        public void Teletransporte(int ID_JOGADOR_SALA, int pIDLocal, int pIdSala)
        {
            try
            {
                Clients.Group(pIdSala.ToString()).TransmitirTeletransporte(ID_JOGADOR_SALA, pIDLocal);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
        }

        #region Chat

        public void EnviarMensagem(int pIdSala, string pDescricaoMensagem)
        {
            try
            {
                Clients.Group(pIdSala.ToString()).TransmitirMensagem(pIdSala, pDescricaoMensagem);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
        }

        #endregion

        #region Sala/Manter

        public void IngressarGrupo(string nomeGrupo)
        {
            try
            {
                Groups.Add(Context.ConnectionId, nomeGrupo);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
        }

        public void DeixarGrupo(string nomeGrupo)
        {
            try
            {
                Groups.Remove(Context.ConnectionId, nomeGrupo);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
            
        }

        #endregion

        #region Suspeito/Listar

        /// <summary>
        /// Seleciona o personagem para o jogador da sala
        /// </summary>
        /// <param name="pIdSala"></param>
        /// <param name="pIdJogadorSala"></param>
        public void selecaoSuspeito(int pIdSala, int pIdJogadorSala, int pIdSuspeito, string pDescricaoJogador, string pDescricaoSuspeitoSelecionado, string pDescricaoSuspeitoDesconsiderado)
        {
            try
            {
                Clients.Group(pIdSala.ToString()).TransmitirSelecaoSuspeito(pIdJogadorSala, pIdSuspeito, pDescricaoJogador, pDescricaoSuspeitoSelecionado, pDescricaoSuspeitoDesconsiderado);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
        }

        public void DesconsiderarSuspeito(int pIdSala, int pIdJogadorSala, string pDescricaoSuspeito)
        {
            try
            {
                Clients.Group(pIdSala.ToString()).TransmitirDesconsideracaoSuspeito(pIdJogadorSala, pDescricaoSuspeito);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
            }
        }

        public void IniciarPartida(int pIdSala, int pIdJogadorSala)
        {
            try
            {
                Clients.Group(pIdSala.ToString()).TransmitirIniciarPartida(pIdJogadorSala);
            }
            catch (Exception ex)
            {
                Clients.Caller.erro(ex.Message, ex.ToString());
                throw;
            }
        }

        #endregion
    }
}