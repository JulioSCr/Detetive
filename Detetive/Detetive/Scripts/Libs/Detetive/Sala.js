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

Sala.EnviarMovimento = function (pLinha, pColuna) {
    try {
        $.ajax({
            url: gstrGlobalPath + '/',
            data: {
                linha: pLinha,
                coluna: pColuna
            },
            success: function (data, textStatus, XMLHttpRequest) {
                try {
                    if (data.Exception != null) {
                        throw data.Exception;
                    }
                    //Movimentar para os outros usuários
                    if (data.Retorno) {

                    }
                    Sala.mHubSala.server.enviarMovimentoTabuleiro();
                } catch (ex) {
                    throw ex;
                }
            }
        });
    } catch (ex) {
        throw ex;
    }
}