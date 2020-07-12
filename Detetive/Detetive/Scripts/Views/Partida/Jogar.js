var Jogar = window.Jogar || {
    mID_JOGADOR_SALA: new Number(),     // ID do jogador sala
    mID_SALA: new Number(),             // ID do jogador sala
    marrMapeamento: new Array()         // Mapeamento do tabuleiro
};

Jogar.MontarTela = function () {
    Jogar.mID_SALA = $('#inpID_SALA').val();
    Jogar.mID_JOGADOR_SALA = $('#inpID_JOGADOR_SALA').val();
    // Cria a modal palpite
    $('#ModalPalpite').Detetive_Modal({
        Titulo: 'Palpite'
    });
    // Cria a modal acusar
    $('#ModalAcusar').Detetive_Modal({
        Titulo: 'Acusar'
    });
    // Listagem de cartas
    $('.slider-nav').slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        infinite: false,
        dots: true,
    });

    $('.responsive').slick({
        dots: true,
        infinite: false,
        speed: 300,
        slidesToShow: 5,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });

    $('a[data-slide]').click(function (e) {
        e.preventDefault();
        var slideno = $(this).data('slide');
        $('.slider-nav').slick('slickGoTo', slideno - 1);
    });

    $('#divCaixaInformacoes').animate({
        scrollTop: $('#divCaixaInformacoes').get(0).scrollHeight
    }, 500);
};

$(document).ready(function () {
    Jogar.MontarTela();
});

//#region Botões de ação

Jogar.btnFinalizarTurno_OnClick = function () {
    try {
        //$("#btnEsquerda").attr("disabled", true);
        //$("#btnEsquerda").css('background', 'darkgrey');

        //$("#btnDireita").attr("disabled", true);
        //$("#btnDireita").css('background', 'darkgrey');

        //$("#btnAcima").attr("disabled", true);
        //$("#btnAcima").css('background', 'darkgrey');

        //$("#btnAbaixo").attr("disabled", true);
        //$("#btnAbaixo").css('background', 'darkgrey');

        //$("#btnLancarDados").attr("disabled", true);
        //$("#btnLancarDados").css('background', 'darkgrey');

        //$("#btnPalpite").attr("disabled", true);
        //$("#btnAcusar").attr("disabled", true);
        //$("#btnPassagemSecreta").attr("disabled", true);

        //$("#btnFinalizarTurno").attr("disabled", true);
        //$("#btnFinalizarTurno").css('background', 'darkgrey');

        //$("#divCaixaInformacoes").append("Você finalizou seu turno!");
        $.ajax({
            url: gstrGlobalPath + 'Partida/Finalizar',
            type: 'post',
            data: {
                idJogadorSala: Jogar.mID_JOGADOR_SALA
            },
            success: function (data, textStatus, XMLHttpRequest) {
                Loading.Carregamento(false);
                var retorno = JSON.parse(data);

                if (!retorno.Status) {
                    PopUp.Erro(retorno.Retorno)
                }
                Sala.EnviarMensagem(Jogar.mID_SALA);
            },
            error: function (data, textStatus, XMLHttpRequest) {
                Loading.Carregamento(false);
                alert(data.Retorno);
            }
        });

    } catch (ex) {
        PopUp.Erro(ex);
    }

}

Jogar.btnDireita_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        //({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'direita'));
        Sala.EnviarMovimento(lLinha, lColuna + 1);
    } catch (ex) {
        PopUp.Erro(ex);
    }
};

Jogar.btnEsquerda_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        //({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'esquerda'));
        Sala.EnviarMovimento(lLinha, lColuna - 1);
    } catch (ex) {
        PopUp.Erro(ex);
    }
};

Jogar.btnAcima_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        //({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'cima'));
        Sala.EnviarMovimento(lLinha - 1, lColuna);
    } catch (ex) {
        PopUp.Erro(ex);
    }
};

Jogar.btnAbaixo_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocal = new Number();
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        //({ lIDLocal, lLinha, lColuna } = Jogar.SairLocal(lIDLocal, lLinha, lColuna, 'baixo'));
        Sala.EnviarMovimento(lLinha + 1, lColuna);
    } catch (ex) {
        PopUp.Erro(ex);
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
        PopUp.Erro(ex);
    }
}

Jogar.btnPalpite_OnClick = function () {
    try {
        $('#ModalPalpite').Detetive_Modal('show');
    } catch (ex) {
        PopUp.Erro(ex);
    }
}

Jogar.btnAcusar_OnClick = function () {
    try {
        $('#ModalAcusar').Detetive_Modal('show');
    } catch (ex) {
        PopUp.Erro(ex);
    }
}

Jogar.DesativarBotoes = function (pblnAtivar) {
    $('#btnDireita').prop('disabled', pblnAtivar);
    $('#btnEsquerda').prop('disabled', pblnAtivar);
    $('#btnAcima').prop('disabled', pblnAtivar);
    $('#btnAbaixo').prop('disabled', pblnAtivar);
    $('#btnLancarDados').prop('disabled', pblnAtivar);
    $('#btnPalpite').prop('disabled', pblnAtivar);
    $('#btnAcusar').prop('disabled', pblnAtivar);
    $('#btnPassagemSecreta').prop('disabled', pblnAtivar);
}

Jogar.btnLancarDados_OnClick = function () {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Partida/RolarDados',
            type: 'post',
            data: {
                idJogadorSala: Jogar.mID_JOGADOR_SALA,
                idSala: Jogar.mID_SALA
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var lobjResltado = new Object();
                var lstrDescricaoJogador = new String();
                var lstrDescricaoSuspeitoSelecionado = new String();
                var lstrDescricaoSuspeitoDesconsiderado = new String();
                try {
                    Loading.Carregamento(false);
                    lobjResltado = JSON.parse(data);
                    if (!lobjResltado.Status) { throw data.Retorno; }
                    Sala.EnviarMensagem(Jogar.mID_SALA);
                } catch (ex) {
                    PopUp.Erro(ex);
                }
            },
            error: function (request, status, error) {
                Loading.Carregamento(false);
                PopUp.Erro(request.responseText);
            }
        });
    } catch (ex) {
        PopUp.Erro(ex);
    }
}

//#endregion

Jogar.TransmitirMovimento = function (pID_JOGADOR_SALA, pLinha, pColuna, pIDLocal) {
    var lIDLocalPassagemSecreta = new Number();
    if (pIDLocal == null || pIDLocal == 0) {
        if (Jogar.GetLocalAtual(pID_JOGADOR_SALA) > 0) {
            Jogar.RemoveDoLocal(pID_JOGADOR_SALA, pLinha, pColuna);
        }
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-row', pLinha);
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').css('grid-column', pColuna);
        if (pID_JOGADOR_SALA == Jogar.mID_JOGADOR_SALA) {
            Jogar.VisibilidadeBotoesAcao(false);
        }
    } else {
        Jogar.ColocanNoLocal(pID_JOGADOR_SALA, pIDLocal, lIDLocalPassagemSecreta, pLinha, pColuna);
    }

}

Jogar.TransmitirTeletransporte = function (pintID_JOGADOR_SALA, pintIDLocal) {
    try {
        Jogar.RemoveDoLocal(pintID_JOGADOR_SALA, 0, 0);
        Jogar.TransmitirMovimento(pintID_JOGADOR_SALA, 0, 0, pintIDLocal);
    } catch (ex) {
        PopUp.Erro(ex);
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
    //if (isNaN(lLinha) && isNaN(lColuna)) {
    //    lLinha = 0;
    //    lColuna = 0;
    //}
    return { lLinha, lColuna };
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

Jogar.ColocanNoLocal = function (pID_JOGADOR_SALA, pIDLocal, lIDLocalPassagemSecreta, pLinha, pColuna) {
    var lstrSuspeito = new String();
    var lstrTitulo = new String();
    try {
        lstrSuspeito = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('id');
        lstrTitulo = $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').attr('title');
        $('div[idJogadorSala=' + pID_JOGADOR_SALA + ']').remove();
        $('div[idLocal=' + pIDLocal + ']').prepend('<div id="' + lstrSuspeito + '" title="' + lstrTitulo + '" idJogadorSala="' + pID_JOGADOR_SALA + '" class="suspeito dentro" style="grid-column:' + pColuna + ';grid-row:' + pLinha + ';"></div>');
        if (pID_JOGADOR_SALA == Jogar.mID_JOGADOR_SALA) { Jogar.VisibilidadeBotoesAcao(true, lIDLocalPassagemSecreta); }
    } catch (ex) {
        throw ex;
    }
}

Jogar.AnotacaoArma_OnChange = function (input) {
    try {
        var idArma = parseInt($(input).attr("idArma"));
        var valor = $(input).is(":checked");

        $.ajax({
            url: gstrGlobalPath + 'Anotacao/MarcarArma',
            type: 'post',
            data: {
                idArma: idArma,
                idJogadorSala: Jogar.mID_JOGADOR_SALA,
                valor: valor
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var retorno = PopUp.Erro(JSON.parse(data).Retorno);

                if (!retorno.Status) {
                    PopUp.Erro(retorno.Retorno)
                }
            },
            error: function (data, textStatus, XMLHttpRequest) {
                PopUp.Erro('Erro durante chamada da controller MarcarArma');
            }
        });
    } catch (ex) {
        alert(ex);
    }
}

Jogar.AnotacaoLocal_OnChange = function (input) {
    try {
        var idLocal = parseInt($(input).attr("idLocal"));
        var valor = $(input).is(":checked");

        $.ajax({
            url: gstrGlobalPath + 'Anotacao/MarcarLocal',
            type: 'post',
            data: {
                idLocal: idLocal,
                idJogadorSala: Jogar.mID_JOGADOR_SALA,
                valor: valor
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var retorno = PopUp.Erro(JSON.parse(data).Retorno);

                if (!retorno.Status) {
                    PopUp.Erro(retorno.Retorno);
                }
            },
            error: function (data, textStatus, XMLHttpRequest) {
                PopUp.Erro('Erro durante chamada da controller MarcarLocal');
            }
        });
    } catch (ex) {
        PopUp.Erro(ex);
    }
}

Jogar.AnotacaoSuspeito_OnChange = function (input) {
    try {
        var idSuspeito = parseInt($(input).attr("idSuspeito"));
        var valor = $(input).is(":checked");
        $.ajax({
            url: gstrGlobalPath + 'Anotacao/MarcarSuspeito',
            type: 'post',
            data: {
                idSuspeito: idSuspeito,
                idJogadorSala: Jogar.mID_JOGADOR_SALA,
                valor: valor
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var retorno = PopUp.Erro(JSON.parse(data).Retorno);

                if (!retorno.Status) {
                    PopUp.Erro(retorno.Retorno);
                }
            },
            error: function (data, textStatus, XMLHttpRequest) {
                PopUp.Erro('Erro durante chamada da controller MarcarSuspeito');
            }
        });
    } catch (ex) {
        PopUp.Erro(ex);
    }
}

//#region Chat

Jogar.TransmitirMensagem = function (pintIdSala, parrDescricaoMensagem) {
    var lstrHtml = new String();
    var larrDescricao = new Array();
    try {
        $('#divCaixaInformacoes').animate({
            scrollTop: $('#divCaixaInformacoes').get(0).scrollHeight
        }, 500);
        larrDescricao = JSON.parse(parrDescricaoMensagem);
        for (var i = 0; i < larrDescricao.length; i++) {
            lstrHtml += '<label class="informacao">' + larrDescricao[i].Descricao + '</label>';
        }
        $('#divCaixaInformacoes').html(lstrHtml);
    } catch (ex) {
        alert(ex);
    }
}

//#endregion