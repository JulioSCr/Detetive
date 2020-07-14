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
    Jogar.CarregarCartas();    

    Jogar.MapearTabuleiro();
};

$(document).ready(function () {
    Jogar.MontarTela();
});

Jogar.CarregarCartas = function () {
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
}

//#region Botões de ação

Jogar.btnFinalizarTurno_OnClick = function () {
    try {
        
        $.ajax({
            url: gstrGlobalPath + 'Partida/Finalizar',
            type: 'post',
            data: {
                idJogadorSala: Jogar.mID_JOGADOR_SALA
            },
            success: function (data, textStatus, XMLHttpRequest) {
                Loading.Carregamento(false);
                var retorno = JSON.parse(data);
                Sala.mHubSala.server.finalizarTurno(Jogar.mID_SALA, retorno.Retorno).done(function () { });
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

Jogar.TransmitirFinalizarTurno = function (pintIdSala, pintIdJogadorSala) {
    try {
        if (Jogar.mID_JOGADOR_SALA == pintIdJogadorSala) {
            Jogar.DesativarBotoes(false);
        }
    } catch (ex) {
        PopUp.Erro(ex);
    }
}

Jogar.btnDireita_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocalAtual = new Number();
    var lIDLocalDestino = new Number();
    var lstrOrientacaoMovimento = new String('');
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        lIDLocalAtual = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        if (lIDLocalAtual == 0) {
            lIDLocalDestino = Jogar.CoordenadaToLocal(lLinha, lColuna + 1);
        } else {
            lstrOrientacaoMovimento = 'direita';
            lobjCoordenadasSaida = Jogar.PosicaoPortaSaida(lIDLocalAtual, lstrOrientacaoMovimento);

            lLinha = lobjCoordenadasSaida.Linha;
            lColuna = lobjCoordenadasSaida.Coluna;
        }
        if (lLinha == null && lColuna == null) { throw 'Movimento inválido mova-se para outra direção.'; }
        Sala.EnviarMovimento(lLinha, lColuna + 1, lIDLocalDestino);
    } catch (ex) {
        PopUp.Erro(ex);
    }
};

Jogar.btnEsquerda_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocalAtual = new Number();
    var lIDLocalDestino = new Number();
    var lstrOrientacaoMovimento = new String('');
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        lIDLocalAtual = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        if (lIDLocalAtual == 0) {
            lIDLocalDestino = Jogar.CoordenadaToLocal(lLinha, lColuna - 1);
        } else {
            lstrOrientacaoMovimento = 'esquerda';
            lobjCoordenadasSaida = Jogar.PosicaoPortaSaida(lIDLocalAtual, lstrOrientacaoMovimento);

            lLinha = lobjCoordenadasSaida.Linha;
            lColuna = lobjCoordenadasSaida.Coluna;
        }
        if (lLinha == null && lColuna == null) { throw 'Movimento inválido mova-se para outra direção.'; }
        Sala.EnviarMovimento(lLinha, lColuna - 1, lIDLocalDestino);
    } catch (ex) {
        PopUp.Erro(ex);
    }
};

Jogar.btnAcima_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocalAtual = new Number();
    var lIDLocalDestino = new Number();
    var lstrOrientacaoMovimento = new String('');
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        lIDLocalAtual = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        if (lIDLocalAtual == 0) {
            lIDLocalDestino = Jogar.CoordenadaToLocal(lLinha - 1, lColuna);
        } else {
            lstrOrientacaoMovimento = 'cima';
            lobjCoordenadasSaida = Jogar.PosicaoPortaSaida(lIDLocalAtual, lstrOrientacaoMovimento);

            lLinha = lobjCoordenadasSaida.Linha;
            lColuna = lobjCoordenadasSaida.Coluna;
        }
        if (lLinha == null && lColuna == null) { throw 'Movimento inválido mova-se para outra direção.'; }
        Sala.EnviarMovimento(lLinha - 1, lColuna, lIDLocalDestino);
    } catch (ex) {
        PopUp.Erro(ex);
    }
};

Jogar.btnAbaixo_OnClick = function () {
    var lLinha = new Number();
    var lColuna = new Number();
    var lIDLocalAtual = new Number();
    var lIDLocalDestino = new Number();
    var lstrOrientacaoMovimento = new String('');
    try {
        ({ lLinha, lColuna } = Jogar.Posicao(lLinha, lColuna));
        lIDLocalAtual = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        if (lIDLocalAtual == 0) {
            lIDLocalDestino = Jogar.CoordenadaToLocal(lLinha + 1, lColuna);
        } else {
            lstrOrientacaoMovimento = 'baixo';
            lobjCoordenadasSaida = Jogar.PosicaoPortaSaida(lIDLocalAtual, lstrOrientacaoMovimento);
            
            lLinha = lobjCoordenadasSaida.Linha;
            lColuna = lobjCoordenadasSaida.Coluna;
        }
        if (lLinha == null && lColuna == null) { throw 'Movimento inválido mova-se para outra direção.'; }
        Sala.EnviarMovimento(lLinha + 1, lColuna, lIDLocalDestino);
    } catch (ex) {
        PopUp.Erro(ex);
    }
};

Jogar.btnPassagemSecreta_OnClick = function () {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Partida/PassagemSecreta',
            type: 'Post',
            data: {
                pIdJogadorSala: Sala.mID_JOGADOR_SALA
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var lobjResultado = new Object();
                var lintIdLocal = new Number();
                try {
                    lobjResultado = JSON.parse(data);
                    if (!lobjResultado.Status) { throw lobjResultado.Retorno; }
                    lintIdLocal = lobjResultado.Retorno
                    // Verifica qual o local que ele pode ir
                    //lintLocalIDDestino = Jogar.marrMapeamento.filter((lElemTabuleiro, lIndexTabuleiro, lArrTabuleiro) => lElemTabuleiro.ID == lintLocalID)[0].PassagemSecreta;
                    Sala.Teletransporte(Jogar.mID_JOGADOR_SALA, lintIdLocal);
                } catch (ex) {
                    PopUp.Erro(ex);
                }
            },
            error: function (data, textStatus, XMLHttpRequest) {
                PopUp.Erro('Erro ao utilizar a passagem secreta.');
            }
        });
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
    $('#btnFinalizarTurno').prop('disabled', pblnAtivar);
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
                    if (!lobjResltado.Status) { throw lobjResltado.Retorno; }
                    Sala.EnviarMensagem(Jogar.mID_SALA);
                } catch (ex) {
                    if (ex == null) {
                        ex = 'Você não pode lançar os dados.'
                    }
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

Jogar.TransmitirAtualizarCartas = function () {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Partida/AtualizarCartas',
            type: 'post',
            data: {
                pIdJogadorSala: Jogar.mID_JOGADOR_SALA,
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var lobjResultado = new Object();
                var lstrElemento = new String();
                try {
                    Loading.Carregamento(false);
                    lstrElemento = '<div class="slider slider-nav">';

                    lobjResultado = JSON.parse(data);
                    if (!lobjResultado.Status) { throw lobjResultado.Retorno; }
                    lobjResultado = JSON.parse(lobjResultado.Retorno);
                    $('#divCartas > .slider').html();
                    for (var imgCarta in lobjResultado) {
                        lstrElemento += '<div class="carta centralizar"><img src="' + gstrGlobalPath + lobjResultado[imgCarta].replace(/^(~\/|\/)/, '') + '"/></div >';
                    }
                    lstrElemento += '</div>'
                    $('#divCartas').html(lstrElemento);
                    Jogar.CarregarCartas();
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

Jogar.VisibilidadeBotoesAcao = function (pblnMostrar, pIDLocal = 0) {
    var lintLocalAtual = new Number();
    if (pblnMostrar) {
        $("#btnPalpite").css("visibility", "visible");
        $("#btnAcusar").css("visibility", "visible");
        lintLocalAtual = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        for (var lobjLocal in Jogar.marrMapeamento) {
            if (Jogar.marrMapeamento[lobjLocal].Id == lintLocalAtual && Jogar.marrMapeamento[lobjLocal].IdLocalPassagemSecreta > 0) {
                $("#btnPassagemSecreta").css("visibility", "visible");
                break;
            } else {
                $("#btnPassagemSecreta").css("visibility", "hidden");
            }
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

Jogar.PosicaoPortaSaida = function (pintIdLocal, pstrOrientacaoMovimento) {
    var lobjRetorno = new Object();
    try {
        lobjRetorno.Linha = null;
        lobjRetorno.Coluna = null;
        for (var lobjLocal in Jogar.marrMapeamento) {
            if (Jogar.marrMapeamento[lobjLocal].Id == pintIdLocal) {
                for (var lobjPorta in Jogar.marrMapeamento[lobjLocal].Portas) {
                    if (Jogar.marrMapeamento[lobjLocal].Portas[lobjPorta].Direcao == pstrOrientacaoMovimento) {
                        lobjRetorno.Linha = Jogar.marrMapeamento[lobjLocal].Portas[lobjPorta].CoordenadaLinha;
                        lobjRetorno.Coluna = Jogar.marrMapeamento[lobjLocal].Portas[lobjPorta].CoordenadaColuna;
                        return lobjRetorno;
                    }
                }
                throw 'Tente sair por outra direção.';
            }
        }
        return lobjRetorno;
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
        PopUp.Erro(ex);
    }
}

Jogar.CoordenadaToLocal = function (lintLinha, lintColuna) {
    try {
        for (var lobjLocal in Jogar.marrMapeamento) {
            if (lintLinha >= Jogar.marrMapeamento[lobjLocal].CoordenadaALinha && lintLinha <= Jogar.marrMapeamento[lobjLocal].CoordenadaBLinha
                && lintColuna >= Jogar.marrMapeamento[lobjLocal].CoordenadaAColuna && lintColuna <= Jogar.marrMapeamento[lobjLocal].CoordenadaBColuna) {
                for (var lobjPorta in Jogar.marrMapeamento[lobjLocal].Portas) {
                    if (Jogar.marrMapeamento[lobjLocal].Portas[lobjPorta].CoordenadaLinha == lintLinha && Jogar.marrMapeamento[lobjLocal].Portas[lobjPorta].CoordenadaColuna == lintColuna) {
                        return Jogar.marrMapeamento[lobjLocal].Id;
                    }
                }
                throw 'Entre pela porta.';
            }
        }
        return null;
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
                Loading.Carregamento(false);
                var retorno = JSON.parse(data).Retorno;

                PopUp.Visualizar({
                    TipoPopUp: 'Alerta',
                    Mensagem: retorno,
                    Evento: function () {
                        try {
                            $('#divPopUp').Detetive_Modal('hide');
                        } catch (ex) {
                            PopUp.Erro(ex);
                        }
                    }
                });
            },
            error: function (data, textStatus, XMLHttpRequest) {
                Loading.Carregamento(false);
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
                Loading.Carregamento(false);
                var retorno = JSON.parse(data).Retorno;

                PopUp.Visualizar({
                    TipoPopUp: 'Alerta',
                    Mensagem: retorno,
                    Evento: function () {
                        try {
                            $('#divPopUp').Detetive_Modal('hide');
                        } catch (ex) {
                            PopUp.Erro(ex);
                        }
                    }
                });
            },
            error: function (data, textStatus, XMLHttpRequest) {
                Loading.Carregamento(false);
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
                Loading.Carregamento(false);
                var retorno = JSON.parse(data).Retorno;
                
                PopUp.Visualizar({
                    TipoPopUp: 'Alerta',
                    Mensagem: retorno,
                    Evento: function () {
                        try {
                            $('#divPopUp').Detetive_Modal('hide');
                        } catch (ex) {
                            PopUp.Erro(ex);
                        }
                    }
                });
            },
            error: function (data, textStatus, XMLHttpRequest) {
                Loading.Carregamento(false);
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

Jogar.MapearTabuleiro = function () {
    try {
        $.ajax({
            url: gstrGlobalPath + 'Partida/MapearTabuleiro',
            type: 'post',
            success: function (data, textStatus, XMLHttpRequest) {
                var lobjResltado = new Object();
                try {
                    lobjResltado = JSON.parse(data);
                    if (!lobjResltado.Status) { throw data.Retorno; }
                    Jogar.marrMapeamento = JSON.parse(lobjResltado.Retorno);
                } catch (ex) {
                    PopUp.Erro(ex);
                }
            },
            error: function (request, status, error) {
                PopUp.Erro(request.responseText);
            }
        });
    } catch (ex) {
        alert(ex);
    }
}