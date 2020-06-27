var Sala = window.Sala || {
    mHubSala: new Object(),                             // Objeto do SignalR
    mWebSocketTryingToReconnect: new Boolean(false),    // Flag tentando reconectar
    mID_JOGADOR_SALA: new Number(),                     // Id do jogador sala
    mintIdSala: new Number()                            // Id da sala
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

    Sala.mHubSala.client.TransmitirSelecaoSuspeito = function (pintIdJogadorSala, pintIdSuspeito, pstrDescricaoJogador, pstrDescricaoSuspeito) {
        Listar.TransmitirSelecaoSuspeito(pintIdJogadorSala, pintIdSuspeito, pstrDescricaoJogador, pstrDescricaoSuspeito);
    }

    Sala.mHubSala.client.TransmitirDesconsideracaoSuspeito = function (pintIdJogadorSala, pDescricaoSuspeito) {
        Listar.TransmitirDesconsideracaoSuspeito(pintIdJogadorSala, pDescricaoSuspeito);
    }

    Sala.mHubSala.client.erro = function (vstrMensagem, vstrMensagemTecnica) {
        console.log(vstrMensagemTecnica);
    };
};

Sala.Conectar = function () {
    try {
        Sala.mHubSala.connection.start().done(function () {
            if (Sala.mintIdSala > 0 && Sala.mintIdSala != undefined && Sala.mintIdSala != null) {
                Sala.IngressarGrupo(Sala.mintIdSala.toString());
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
        Sala.DeixarGrupo(Sala.mintIdSala.toString());
    } catch (ex) {
        console.log(ex);
    }
};


Sala.IngressarGrupo = function (pstrNomeGrupo) {
    Sala.mHubSala.server.ingressarGrupo(pstrNomeGrupo).done(function () { });
}

Sala.DeixarGrupo = function (pstrNomeGrupo) {
    Sala.mHubSala.server.deixarGrupo(pstrNomeGrupo).done(function () { });
}

//#region Suspeitos/Listar

Sala.SelecionarSuspeito = function (pintIdSala, pintIdJogadorSala, pintIdSuspeito) {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Suspeito/SelecionarSuspeito',
            type: 'post',
            data: {
                idSala: pintIdSala,
                idJogadorSala: pintIdJogadorSala,
                idSuspeito: pintIdSuspeito
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var lobjResltado = new Object();
                var lstrDescricaoJogador = new String();
                var lstrDescricaoSuspeito = new String();
                try {
                    lobjResltado = JSON.parse(data);
                    if (!lobjResltado.Status) { throw data.Retorno; }
                    lstrDescricaoJogador = JSON.parse(lobjResltado.Retorno).Data.DescricaoJogador;
                    lstrDescricaoSuspeito = JSON.parse(lobjResltado.Retorno).Data.DescricaoSuspeito
                    Sala.mHubSala.server.selecaoSuspeito(pintIdSala, pintIdJogadorSala, pintIdSuspeito, lstrDescricaoJogador, lstrDescricaoSuspeito).done(function () { });
                } catch (ex) {
                    throw ex;
                }
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });
    } catch (ex) {
        throw ex;
    }
}

Sala.DesconsiderarSuspeito = function (pintIdSala, pintIdJogadorSala) {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Suspeito/DesconsiderarSuspeito',
            type: 'post',
            data: {
                idJogadorSala: pintIdJogadorSala
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var lobjResltado = new Object();
                var lstrDescricaoSuspeito = new String();
                try {
                    lobjResltado = JSON.parse(data);
                    if (!lobjResltado.Status) { throw data.Retorno; }
                    lstrDescricaoSuspeito = JSON.parse(lobjResltado.Retorno).DescricaoSuspeito
                    Sala.mHubSala.server.desconsiderarSuspeito(pintIdSala, pintIdJogadorSala, lstrDescricaoSuspeito).done(function () { });
                } catch (ex) {
                    throw ex;
                }
            }
        });
    } catch (ex) {
        throw ex;
    }
}

//#endregion

Sala.EnviarMensagem = function (apelido, mensagem) {
    try {
        Sala.mHubSala.server.enviarMensagem(apelido, mensagem).done(function () { });
    } catch (ex) {
        console.log(ex);
    }
}

/// <summary>Envia o movimento para todos os jogadores da sala.</summary>
/// <param name="pLinha" type="Number">Número da linha.</param>
/// <param name="pColuna" type="Number">Número da coluna.</param>
/// <param name="pIDLocal" type="Number">ID do local onde o jogador está.</param>
/// <returns type="Void"></returns>
Sala.EnviarMovimento = function (pLinha, pColuna) {
    try {
        debugger;
        $.ajax({
            url: gstrGlobalPath + 'Partida/MoverJogador',
            type: 'post',
            data: {
                idJogadorSala: Sala.mID_JOGADOR_SALA,
                novaCoordenadaLinha: pLinha,
                novaCoordenadaColuna: pColuna
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var lobjResultado = new Object();
                var lobjRetorno = new Object();
                var lintLinha = new Number();
                var lintColuna = new Number();
                var lintIdLocal = new Number();
                try {
                    debugger;
                    lobjResultado = JSON.parse(data);
                    if (!lobjResultado.Status) { throw lobjResultado.Retorno; }
                    lobjRetorno = JSON.parse(lobjResultado.Retorno);
                    lintLinha = lobjRetorno.Posicao.Linha;
                    lintColuna = lobjRetorno.Posicao.Coluna;
                    lintIdLocal = lobjRetorno.Posicao.IdLocal;
                    Sala.mHubSala.server.enviarMovimento(Sala.mID_JOGADOR_SALA, lintLinha, lintColuna, lintIdLocal).done(function () { });
                } catch (ex) {
                    alert(ex);
                }
            },
            error: function (data, textStatus, XMLHttpRequest) {
                alert("Erro na chamada da movimentação.");
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