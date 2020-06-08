var Jogar = window.Jogar || {
    mID_JOGADOR_SALA: new Number(),     // ID do jogador sala
    marrMapeamento: new Array()         // Mapeamento do tabuleiro
};

Jogar.MontarTela = function () {
    Jogar.mID_JOGADOR_SALA = $('#inpID_JOGADOR_SALA').val();
    // Mapeia tabuleiro
    Jogar.MapearTabuleiro();
    //Posicionando suspeitos
    $('#divReitor').css('grid-column', 1);
    $('#divDiretora').css('grid-column', 2);
    $('#divProfessora').css('grid-column', 3);
    $('#divEstudante').css('grid-column', 4);
    $('#divZelador').css('grid-column', 5);
    $('#divPolicial').css('grid-column', 6);
    $('#divReporter').css('grid-column', 7);
    $('#divBibliotecaria').css('grid-column', 8);
    $('#divReitor').css('grid-row', 2);
    $('#divDiretora').css('grid-row', 2);
    $('#divProfessora').css('grid-row', 2);
    $('#divEstudante').css('grid-row', 2);
    $('#divZelador').css('grid-row', 2);
    $('#divPolicial').css('grid-row', 2);
    $('#divReporter').css('grid-row', 2);
    $('#divBibliotecaria').css('grid-row', 2);
};

$(document).ready(function () {
    Jogar.MontarTela();
    //document.getElementsByClassName("caminho")[0].style.gridArea
});

Jogar.btnDireita_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        debugger;
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

Jogar.Movimentar = function (pLinha, pColuna, pIDLocal = 0) {
    try {
        Sala.EnviarMovimento(pLinha, pColuna, pIDLocal);
    } catch (ex) {
        throw ex;
    }
}

Jogar.TransmitirMovimento = function (pID_JOGADOR_SALA, pLinha, pColuna, pIDLocal) {
    var lstrSuspeito = new String();
    var lstrTitulo = new String();
    try {
        /// TO DO
        /// Preciso que retorne um parâmetro a mais que será opcional
        /// Este parâmetro vai posicionar o pino no centro do local caso ele possua um valor
        debugger;
        Jogar.marrMapeamento.forEach(function (Local, index, Mapa) {
            for (var i = Local.Linhas[0]; i < Local.Linhas[1]; i++) {
                for (var j = Local.Colunas[0]; j < Local.Colunas[1]; j++) {
                    if (pLinha == i && pColuna == j) {
                        Local.Portas.forEach(function (Porta, indexP, PortasP) {
                            if (pLinha == Porta.Linha && pColuna == Porta.Coluna) {
                                pIDLocal = Local.ID;
                            }
                            else if (pIDLocal == 0 || pIDLocal == -1) {
                                pIDLocal = -1;
                            }
                        });
                    }
                }
            }
        });

        if (pIDLocal == 0) {
            $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-row', pLinha);
            $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-column', pColuna);
        } else if (pIDLocal != -1) {
            if ($('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').parent().attr('idLocal') > 0 && $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').parent().attr('idLocal') != undefined) {
                lstrSuspeito = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('id');
                lstrTitulo = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('title');
                $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').remove();
                $('#divTabuleiro').prepend('<div id="' + lstrSuspeito + '" title="' + lstrTitulo + '" idJogadorSala="' + pID_JOGADOR_SALA + '" class="suspeito dentro"></div>');
                $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-row', pLinha);
                $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-column', pColuna);
            } else {
                lstrSuspeito = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('id');
                lstrTitulo = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('title');
                $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').remove();
                $('div[idLocal=' + pIDLocal + ']').prepend('<div id="' + lstrSuspeito + '" title="' + lstrTitulo + '" idJogadorSala="' + pID_JOGADOR_SALA + '" class="suspeito dentro"></div>');
            }
        }
    } catch (ex) {

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

Jogar.PosicionarJogador = function () {

    $.ajax({
        url: gstrGlobalPath + 'Partida/Posicionar',
        success: function (data, textStatus, XMLHttpRequest) {
            try {
                //if (!JSON.parse(data.toLowerCase())) { throw 'Movimento inválido'; }

            } catch (ex) {
                throw ex;
            }
        }
    });
    //$('#divReitor').css('grid-column', 1);
    //$('#divDiretora').css('grid-column', 2);
    //$('#divProfessora').css('grid-column', 3);
    //$('#divEstudante').css('grid-column', 4);
    //$('#divZelador').css('grid-column', 5);
    //$('#divPolicial').css('grid-column', 6);
    //$('#divReporter').css('grid-column', 7);
    //$('#divBibliotecaria').css('grid-column', 8);
    //$('#divReitor').css('grid-row', 2);
    //$('#divDiretora').css('grid-row', 2);
    //$('#divProfessora').css('grid-row', 2);
    //$('#divEstudante').css('grid-row', 2);
    //$('#divZelador').css('grid-row', 2);
    //$('#divPolicial').css('grid-row', 2);
    //$('#divReporter').css('grid-row', 2);
    //$('#divBibliotecaria').css('grid-row', 2);
}

Jogar.MapearTabuleiro = function () {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Partida/MapearTabuleiro',
            success: function (data, textStatus, XMLHttpRequest) {
                try {
                    //if (!JSON.parse(data.toLowerCase())) { throw 'Movimento inválido'; }

                } catch (ex) {
                    throw ex;
                }
            }
        });
        Jogar.marrMapeamento = [
            {
                Nome: 'PredioA',
                ID: 1,
                Linhas: [11, 18],
                Colunas: [1, 7],
                Portas: [
                    {
                        Linha: 14,
                        Coluna: 6,
                        Direcao: 'direita'
                    }
                ]
            },
            {
                Nome: 'PredioB',
                ID: 2,
                Linhas: [11, 18],
                Colunas: [9, 15],
                Portas: [
                    {
                        Linha: 14,
                        Coluna: 9,
                        Direcao: 'esquerda'
                    }
                ]
            },
            {
                Nome: 'Santiago',
                ID: 3,
                Linhas: [1, 10],
                Colunas: [27, 33],
                Portas: [
                    {
                        Linha: 9,
                        Coluna: 29,
                        Direcao: 'baixo'
                    }
                ]
            },
            {
                Nome: 'Praca',
                ID: 4,
                Linhas: [20, 26],
                Colunas: [16, 25],
                Portas: [
                    {
                        Linha: 25,
                        Coluna: 16,
                        Direcao: 'esquerda'
                    },
                    {
                        Linha: 20,
                        Coluna: 23,
                        Direcao: 'cima'
                    }
                ]
            },
            {
                Nome: 'Etesp',
                ID: 5,
                Linhas: [20, 26],
                Colunas: [9, 15],
                Portas: [
                    {
                        Linha: 24,
                        Coluna: 9,
                        Direcao: 'esquerda'
                    },
                    {
                        Linha: 22,
                        Coluna: 14,
                        Direcao: 'direita'
                    }
                ]
            },
            {
                Nome: 'CantinaAB',
                ID: 6,
                Linhas: [20, 26],
                Colunas: [1, 7],
                Portas: [
                    {
                        Linha: 20,
                        Coluna: 1,
                        Direcao: 'cima'
                    }
                ]
            },
            {
                Nome: 'CA',
                ID: 7,
                Linhas: [12, 26],
                Colunas: [27, 33],
                Portas: [
                    {
                        Linha: 12,
                        Coluna: 31,
                        Direcao: 'cima'
                    },
                    {
                        Linha: 24,
                        Coluna: 27,
                        Direcao: 'esquerda'
                    }
                ]
            },
            {
                Nome: 'Auditorio',
                ID: 8,
                Linhas: [3, 9],
                Colunas: [18, 25],
                Portas: [
                    {
                        Linha: 4,
                        Coluna: 18,
                        Direcao: 'esquerda'
                    },
                    {
                        Linha: 3,
                        Coluna: 24,
                        Direcao: 'cima'
                    }
                ]
            },
            {
                Nome: 'Ginasio',
                ID: 9,
                Linhas: [9, 18],
                Colunas: [18, 25],
                Portas: [
                    {
                        Linha: 17,
                        Coluna: 18,
                        Direcao: 'esquerda'
                    }
                ]
            }
        ];
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
