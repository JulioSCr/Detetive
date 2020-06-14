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
        Sala.mHubSala.server.enviarMensagem(apelido, mensagem);
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