var Jogar = window.Jogar || {
    mID_JOGADOR_SALA: new Number(),     // ID do jogador sala
    marrMapeamento: new Array()         // Mapeamento do tabuleiro
};

Jogar.MontarTela = function () {
    Jogar.mID_JOGADOR_SALA = $('#inpID_JOGADOR_SALA').val();
    // Mapeia tabuleiro
    Jogar.MapearTabuleiro();
    // Cria a modal palpite
    $('#ModalPalpite').Detetive_Modal({
        Titulo: 'Palpite'
    });
    // Cria a modal acusar
    $('#ModalAcusar').Detetive_Modal({
        Titulo: 'Acusar'
    });
};

$(document).ready(function () {
    Jogar.MontarTela();
});

//#region Botões de ação

Jogar.btnFinalizarTurno_OnClick = function () {
    try {

    } catch (ex) {
        alert(ex);
    }
}

Jogar.btnDireita_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        ({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'direita'));
        Jogar.Movimentar(lLinha, lColuna + 1, lIDLocal);
    } catch (ex) {
        alert(ex);
    }
};

Jogar.btnEsquerda_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        ({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'esquerda'));
        Jogar.Movimentar(lLinha, lColuna - 1, lIDLocal);
    } catch (ex) {
        alert(ex);
    }
};

Jogar.btnAcima_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        ({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'cima'));
        Jogar.Movimentar(lLinha - 1, lColuna, lIDLocal);
    } catch (ex) {
        alert(ex);
    }
};

Jogar.btnAbaixo_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        ({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'baixo'));
        Jogar.Movimentar(lLinha + 1, lColuna, lIDLocal);
    } catch (ex) {
        alert(ex);
    }
};

Jogar.btnPassagemSecreta_OnClick = function () {
    var lintLocalID = new Number();
    var lintLocalIDDestino = new Number();
    try {
        // Pega local atual
        lintLocalID = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        if (lintLocalID <= 0) { throw 'Você não está em um local.'; }
        // Verifica qual o local que ele pode ir
        lintLocalIDDestino = Jogar.marrMapeamento.filter((lElemTabuleiro, lIndexTabuleiro, lArrTabuleiro) => lElemTabuleiro.ID == lintLocalID)[0].PassagemSecreta;
        if (lintLocalIDDestino <= 0) { throw 'Você não está em um local com passagem secreta.'; }
        Sala.Teletransporte(Jogar.mID_JOGADOR_SALA, lintLocalIDDestino);
    } catch (ex) {
        alert(ex);
    }
}

Jogar.btnPalpite_OnClick = function () {
    try {
        $('#ModalPalpite').Detetive_Modal('show');
    } catch (ex) {
        alert(ex);
    }
}

Jogar.btnAcusar_OnClick = function () {
    try {
        $('#ModalAcusar').Detetive_Modal('show');
    } catch (ex) {
        alert(ex);
    }
}

//#endregion

Jogar.Movimentar = function (pLinha, pColuna, pIDLocal = 0) {
    try {
        Sala.EnviarMovimento(pLinha, pColuna, pIDLocal);
    } catch (ex) {
        throw ex;
    }
}

Jogar.TransmitirMovimento = function (pID_JOGADOR_SALA, pLinha, pColuna, pIDLocal) {
    var lIDLocalPassagemSecreta = new Number();
    Jogar.marrMapeamento.forEach(function (Local, index, Mapa) {
        for (var i = Local.Linhas[0]; i < Local.Linhas[1]; i++) {
            for (var j = Local.Colunas[0]; j < Local.Colunas[1]; j++) {
                if (pLinha == i && pColuna == j) {
                    Local.Portas.forEach(function (Porta, indexP, PortasP) {
                        if (pLinha == Porta.Linha && pColuna == Porta.Coluna) {
                            pIDLocal = Local.ID;
                            lIDLocalPassagemSecreta = Local.PassagemSecreta;
                        }
                        else if (pIDLocal == 0 || pIDLocal == -1) {
                            pIDLocal = -1;
                        }
                    });
                } else if (pLinha == 0 && pColuna == 0 && pIDLocal == Local.ID) {
                    lIDLocalPassagemSecreta = Local.PassagemSecreta;
                }
            }
        }
    });
    if (pIDLocal == 0) {
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-row', pLinha);
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-column', pColuna);
        if (pID_JOGADOR_SALA == Jogar.mID_JOGADOR_SALA) {
            Jogar.VisibilidadeBotoesAcao(false);
        }
    } else if (pIDLocal != -1) {
        if (Jogar.GetLocalAtual(pID_JOGADOR_SALA) > 0) {
            Jogar.RemoveDoLocal(pID_JOGADOR_SALA, pLinha, pColuna);
        } else {
            Jogar.ColocanNoLocal(pID_JOGADOR_SALA, pIDLocal, lIDLocalPassagemSecreta);
        }
    }

}

Jogar.TransmitirTeletransporte = function (pintID_JOGADOR_SALA, pintIDLocal) {
    try {
        Jogar.RemoveDoLocal(pintID_JOGADOR_SALA, 0, 0);
        Jogar.TransmitirMovimento(pintID_JOGADOR_SALA, 0, 0, pintIDLocal);
    } catch (ex) {
        alert(ex);
    }
}

Jogar.VisibilidadeBotoesAcao = function (pblnMostrar, pIDLocal = 0) {
    if (pblnMostrar) {
        $("#btnPalpite").css("visibility", "visible");
        $("#btnAcusar").css("visibility", "visible");
        if (pIDLocal > 0) {
            $("#btnPassagemSecreta").css("visibility", "visible");
        } else {
            $("#btnPassagemSecreta").css("visibility", "hidden");
        }
    } else {
        $("#btnPalpite").css("visibility", "hidden");
        $("#btnAcusar").css("visibility", "hidden");
        $("#btnPassagemSecreta").css("visibility", "hidden");
    }
}

Jogar.Posicao = function (lLinha, lColuna) {
    // Obtem a posição do jogador
    lLinha = $('div[idJogadorSala=' + Jogar.mID_JOGADOR_SALA + ']').css('grid-row');
    lLinha = parseInt(lLinha.replace(' / auto', ''));
    lColuna = $('div[idJogadorSala=' + Jogar.mID_JOGADOR_SALA + ']').css('grid-column');
    lColuna = parseInt(lColuna.replace(' / auto', ''));
    return { lLinha, lColuna };
}

Jogar.MapearTabuleiro = function () {
    try {
        //$.ajax({
        //    url: gstrGlobalPath + 'Partida/MapearTabuleiro',
        //    success: function (data, textStatus, XMLHttpRequest) {
        //        try {
        //            //if (!JSON.parse(data.toLowerCase())) { throw 'Movimento inválido'; }

        //        } catch (ex) {
        //            throw ex;
        //        }
        //    }
        //});
        //Jogar.marrMapeamento = [
        //    {
        //        Nome: 'PredioA',
        //        ID: 1,
        //        Linhas: [11, 18],
        //        Colunas: [1, 7],
        //        Portas: [
        //            {
        //                Linha: 14,
        //                Coluna: 6,
        //                Direcao: 'direita'
        //            }
        //        ],
        //        PassagemSecreta: 7
        //    },
        //    {
        //        Nome: 'PredioB',
        //        ID: 2,
        //        Linhas: [11, 18],
        //        Colunas: [9, 15],
        //        Portas: [
        //            {
        //                Linha: 14,
        //                Coluna: 9,
        //                Direcao: 'esquerda'
        //            }
        //        ],
        //        PassagemSecreta: 0
        //    },
        //    {
        //        Nome: 'Santiago',
        //        ID: 3,
        //        Linhas: [1, 10],
        //        Colunas: [27, 33],
        //        Portas: [
        //            {
        //                Linha: 9,
        //                Coluna: 29,
        //                Direcao: 'baixo'
        //            }
        //        ],
        //        PassagemSecreta: 0
        //    },
        //    {
        //        Nome: 'Praca',
        //        ID: 4,
        //        Linhas: [20, 26],
        //        Colunas: [16, 25],
        //        Portas: [
        //            {
        //                Linha: 25,
        //                Coluna: 16,
        //                Direcao: 'esquerda'
        //            },
        //            {
        //                Linha: 20,
        //                Coluna: 23,
        //                Direcao: 'cima'
        //            }
        //        ],
        //        PassagemSecreta: 0
        //    },
        //    {
        //        Nome: 'Etesp',
        //        ID: 5,
        //        Linhas: [20, 26],
        //        Colunas: [9, 15],
        //        Portas: [
        //            {
        //                Linha: 24,
        //                Coluna: 9,
        //                Direcao: 'esquerda'
        //            },
        //            {
        //                Linha: 22,
        //                Coluna: 14,
        //                Direcao: 'direita'
        //            }
        //        ],
        //        PassagemSecreta: 0
        //    },
        //    {
        //        Nome: 'CantinaAB',
        //        ID: 6,
        //        Linhas: [20, 26],
        //        Colunas: [1, 7],
        //        Portas: [
        //            {
        //                Linha: 20,
        //                Coluna: 1,
        //                Direcao: 'cima'
        //            }
        //        ],
        //        PassagemSecreta: 8
        //    },
        //    {
        //        Nome: 'CA',
        //        ID: 7,
        //        Linhas: [12, 26],
        //        Colunas: [27, 33],
        //        Portas: [
        //            {
        //                Linha: 12,
        //                Coluna: 31,
        //                Direcao: 'cima'
        //            },
        //            {
        //                Linha: 24,
        //                Coluna: 27,
        //                Direcao: 'esquerda'
        //            }
        //        ],
        //        PassagemSecreta: 1
        //    },
        //    {
        //        Nome: 'Auditorio',
        //        ID: 8,
        //        Linhas: [3, 9],
        //        Colunas: [18, 25],
        //        Portas: [
        //            {
        //                Linha: 4,
        //                Coluna: 18,
        //                Direcao: 'esquerda'
        //            },
        //            {
        //                Linha: 3,
        //                Coluna: 24,
        //                Direcao: 'cima'
        //            }
        //        ],
        //        PassagemSecreta: 6
        //    },
        //    {
        //        Nome: 'Ginasio',
        //        ID: 9,
        //        Linhas: [9, 18],
        //        Colunas: [18, 25],
        //        Portas: [
        //            {
        //                Linha: 17,
        //                Coluna: 18,
        //                Direcao: 'esquerda'
        //            }
        //        ],
        //        PassagemSecreta: 0
        //    }
        //];
    } catch (ex) {
        alert(ex);
    }
}

Jogar.SairLocal = function (lIDLocal, lLinha, lColuna, lstrDirecao) {
    var larrLocal = new Array();
    var larrPorta = new Array();
    try {
        lIDLocal = $('div[idJogadorSala=' + Jogar.mID_JOGADOR_SALA + ']').parent().attr('idLocal');
        if (lIDLocal != null && lIDLocal != undefined) {
            larrLocal = Jogar.marrMapeamento.filter((lElemTabuleiro, lIndexTabuleiro, lArrTabuleiro) => lElemTabuleiro.ID == lIDLocal);
            larrPorta = larrLocal[0].Portas.filter((lElemPorta, lIndexPorta, lArrPorta) => lElemPorta.Direcao == lstrDirecao);
            if (larrPorta.length == 0) {
                throw 'Movimento inválido, mova-se para uma das saídas.';
            }
            else {
                lLinha = larrPorta[0].Linha;
                lColuna = larrPorta[0].Coluna;
            }
        }
        return { lIDLocal, lLinha, lColuna };
    } catch (ex) {
        throw ex;
    }
}

Jogar.GetLocalAtual = function (pintID_JOGADOR_SALA) {
    var lintLocalID = new Number();
    try {
        lintLocalID = $('div[idJogadorSala=' + pintID_JOGADOR_SALA + ']').parent().attr('idLocal');
        if (lintLocalID == null || lintLocalID == undefined) { lintLocalID = 0; }
        return lintLocalID;
    } catch (ex) {
        throw ex;
    }
}

Jogar.RemoveDoLocal = function (pID_JOGADOR_SALA, pLinha, pColuna) {
    var lstrSuspeito = new String();
    var lstrTitulo = new String();
    try {
        lstrSuspeito = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('id');
        lstrTitulo = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('title');
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').remove();
        $('#divTabuleiro').prepend('<div id="' + lstrSuspeito + '" title="' + lstrTitulo + '" idJogadorSala="' + pID_JOGADOR_SALA + '" class="suspeito"></div>');
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-row', pLinha);
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-column', pColuna);
        if (pID_JOGADOR_SALA == Jogar.mID_JOGADOR_SALA) { Jogar.VisibilidadeBotoesAcao(false); }
    } catch (ex) {
        throw ex;
    }
}

Jogar.ColocanNoLocal = function (pID_JOGADOR_SALA, pIDLocal, lIDLocalPassagemSecreta) {
    var lstrSuspeito = new String();
    var lstrTitulo = new String();
    try {
        lstrSuspeito = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('id');
        lstrTitulo = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('title');
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').remove();
        $('div[idLocal=' + pIDLocal + ']').prepend('<div id="' + lstrSuspeito + '" title="' + lstrTitulo + '" idJogadorSala="' + pID_JOGADOR_SALA + '" class="suspeito dentro"></div>');
        if (pID_JOGADOR_SALA == Jogar.mID_JOGADOR_SALA) { Jogar.VisibilidadeBotoesAcao(true, lIDLocalPassagemSecreta); }
    } catch (ex) {
        throw ex;
    }
}
