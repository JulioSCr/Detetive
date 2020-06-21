var Sala = window.Sala || {
    mHubSala: new Object(),                             // Objeto do SignalR
    mWebSocketTryingToReconnect: new Boolean(false),    // Flag tentando reconectar
    mID_JOGADOR_SALA: new Number()                      // Id do jogador sala
};

$(document).ready(function () {
    Sala.Iniciar();
    Sala.mID_JOGADOR_SALA = $('#inpID_JOGADOR_SALA').val();
});

Sala.Iniciar = function () {
    if ($.connection != undefined) {
        Sala.mHubSala = $.connection.hubSala;
        Sala.Configurar();
        Sala.Conectar();
    }
}

Sala.Configurar = function () {
    Sala.mHubSala.connection.connectionSlow(function () { });

    Sala.mHubSala.connection.reconnecting(function () {
        Sala.mWebSocketTryingToReconnect = true;
    });

    Sala.mHubSala.connection.reconnected(function () {
        Sala.mWebSocketTryingToReconnect = false;
    });

    Sala.mHubSala.connection.disconnected(function () {
        if (Sala.mWebSocketTryingToReconnect == true) {
            setTimeout(function () {
                Sala.Conectar();
            }, 5000);
        }
        if (Sala.mHubSala.state.connection == null || Sala.mHubSala.state.connection == undefined) {
            Sala.Conectar();
        }
    });

    Sala.mHubSala.client.erro = function (vstrMensagem, vstrMensagemTecnica) {
        console.log(vstrMensagemTecnica);
    };

    Sala.mHubSala.client.TransmitirMensagem = function (apelido, msg) {
        Listar.TransmitirMensagem(apelido, msg);
    };

    Sala.mHubSala.client.TransmitirMovimento = function (ID_JOGADOR_SALA, pLinha, pColuna, pIDLocal) {
        Jogar.TransmitirMovimento(ID_JOGADOR_SALA, pLinha, pColuna, pIDLocal);
    }

    Sala.mHubSala.client.TransmitirTeletransporte = function (pintID_JOGADOR_SALA, pintIDLocal) {
        Jogar.TransmitirTeletransporte(pintID_JOGADOR_SALA, pintIDLocal);
    }

    Sala.mHubSala.client.receberMensagemAcao = function (
        pintIdJogadorSalaRemetente,
        pintIdJogadorSalaDestinatario,
        pstrChatDsMensagem
    ) {
        var larrMensagensAcao = [
            {
                UPCHAT_DS_MSG: vstrUPCHAT_DS_MSG,
                UPCHAT_DT_INCLUSAO: vstrUPCHAT_DT_INCLUSAO,
                UPCHAT_DT_INCLUSAO_DS: vstrUPCHAT_DT_INCLUSAO_DS,
                UPCHAT_IN_USUARIO_LOGADO: vstrUPCHAT_IN_USUARIO_LOGADO,
                UPUSGR_NM: vstrUPUSGR_NM_REMET
            }
        ]

        //Verifica se o chat está visível e se a ação selecionada é a ação da mensagem
        if ($('#divUPCENCM02_Chat').length > 0 && Nvl(UPMENCM.mintATACPJ_NS_Selecionado, 0) == vintATACPJ_NS) {
            if (
                (
                    $("#tabUPECNEM02_Messenger_Chat").dxTabs('instance').option("selectedItem").UPUSER_NS == 0 && //Aba selecionada = Todos e
                    vintUPUSER_NS_DESTIN == 0 //Destinatário = Todos
                ) || //ou
                (
                    vintUPUSER_NS_REMET == $("#tabUPECNEM02_Messenger_Chat").dxTabs('instance').option("selectedItem").UPUSER_NS && //Remetente = aba selecionada e
                    vintUPUSER_NS_DESTIN == UPCHAT.mintHidUPCHAT_UPUSER_NS //Destinatário = Usuário logado
                ) || //ou
                (
                    vintUPUSER_NS_REMET == UPCHAT.mintHidUPCHAT_UPUSER_NS && //Remetente = Usuário logado e
                    vintUPUSER_NS_DESTIN == $("#tabUPECNEM02_Messenger_Chat").dxTabs('instance').option("selectedItem").UPUSER_NS //Destinatário = aba selecionada
                )
            ) {
                UPCENCM02.GerarMensagens(larrMensagens);
            } else {
                if (
                    vintUPUSER_NS_DESTIN == UPCHAT.mintHidUPCHAT_UPUSER_NS || //Se o destinatário for o usuário logado ou
                    vintUPUSER_NS_DESTIN == 0 //Se o destinatário for "Todos"
                ) {
                    //Colocar o badge no lugar correto
                    UPCENCM02.AtualizarBadge((vintUPUSER_NS_DESTIN == 0 ? 0 : vintUPUSER_NS_REMET));
                }
            }
        } else {
            //Criar notificação
            UPCHAT.criarNotificacao(vstrTitulo_Notificacao, vstrCorpo_Notificacao);
        }
    };

    //Sala.mHubSala.client.erro = function (vstrMensagem, vstrMensagemTecnica) {
    //    console.log(vstrMensagemTecnica);
    //};

    //Sala.mHubSala.client.AtualizarListaUsuarios = function (vobjTBUPUSER, vstrUPUSER_NS_Online, vstrUPUSER_NS_Offline) {
    //    UPCHAT.ListaUsuarios_SignalR(vobjTBUPUSER, vstrUPUSER_NS_Online, vstrUPUSER_NS_Offline);
    //};

    //Sala.mHubSala.client.ReceberMensagem = function (vintUPCHAT_NR_LOTE, vintUPUSER_NS_REMET, vintUPUSER_NS_DESTIN, vintATACPJ_NS, vintUPENCM_NS, vstrJobLabel, vstrUPCHAT_DS_MSG, vstrTimeStamp) {
    //    UPCHAT.ReceberMensagem(vintUPCHAT_NR_LOTE, vintUPUSER_NS_REMET, vintUPUSER_NS_DESTIN, vintATACPJ_NS, vintUPENCM_NS, vstrJobLabel, vstrUPCHAT_DS_MSG, vstrTimeStamp);
    //};

    //Sala.mHubSala.client.RetornarListaUsariosOn = function (vobjTBUPUSER) { };

};

Sala.Conectar = function () {
    try {
        Sala.mHubSala.connection.start().done(function () {
            try {
                Sala.mHubSala.server.iniciarSessao().done(function () { });
            } catch (ex) {
                //msgExibir(ex);
                //UPCHAT.Desconectar();
            }
        });
    } catch (ex) {
        console.log(ex);
    }
};

Sala.Desconectar = function () {
    try {
        Sala.mWebSocketTryingToReconnect = false;
        $.connection.hub.stop();
        Sala.mHubSala.server.FinalizarSessao().done(function () {
            try {
                Sala.mWebSocketTryingToReconnect = false;
                if (Sala.mHubSala.connection.state == $.signalR.connectionState.connected) {
                    Sala.mHubSala.connection.stop();
                }
            } catch (ex) {
                console.log(ex);
            }
        });
    } catch (ex) {
        console.log(ex);
    }
};


Sala.EnviarMensagem = function (apelido, mensagem) {
    try {
        Sala.mHubSala.server.enviarMensagem(apelido, mensagem).done(function () { });;
    } catch (ex) {
        console.log(ex);
    }
}

/// <summary>Envia o movimento para todos os jogadores da sala.</summary>
/// <param name="pLinha" type="Number">Número da linha.</param>
/// <param name="pColuna" type="Number">Número da coluna.</param>
/// <param name="pIDLocal" type="Number">ID do local onde o jogador está.</param>
/// <returns type="Void"></returns>
Sala.EnviarMovimento = function (pLinha, pColuna, pIDLocal) {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Partida/Mover',
            data: {
                idJogadorSala: Sala.mID_JOGADOR_SALA,
                linha: pLinha,
                coluna: pColuna
            },
            success: function (data, textStatus, XMLHttpRequest) {
                try {
                    if (!JSON.parse(data.toLowerCase())) { throw 'Movimento inválido'; }

                    //Movimentar para os outros usuários
                    Sala.mHubSala.server.enviarMovimento(Sala.mID_JOGADOR_SALA, pLinha, pColuna, pIDLocal).done(function () { });
                } catch (ex) {
                    throw ex;
                }
            }
        });
    } catch (ex) {
        throw ex;
    }
}

Sala.Teletransporte = function (pintIdJogadorSala, pintIdLocal) {
    try {
        Sala.mHubSala.server.teletransporte(pintIdJogadorSala, pintIdLocal).done(function () { });
    } catch (ex) {
        alert(ex);
    }
}