var ModalAcusar = window.ModalAcusar || {

}

ModalAcusar.Acusar = function () {
    var lintIdArma = new Number();
    var lintIdLocal = new Number();
    var lobjSuspeito = new Object();
    try {
        lintIdArma = $('#ddlModalAcusar_Armas').val();
        lintIdLocal = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        lintIdJogadorSala = Jogar.mID_JOGADOR_SALA;
        lintIdSala = Jogar.mID_SALA;
        lobjSuspeito.Id = $('#ddlModalAcusar_Suspeito').val();
        lobjSuspeito.Descricao = $('#ddlModalAcusar_Suspeito').text().trim()

        $.ajax({
            url: gstrGlobalPath + 'Partida/Acusar',
            type: 'Post',
            data: {
                idJogadorSala: lintIdJogadorSala,
                idSala: lintIdSala,
                idArma: lintIdArma,
                idLocal: lintIdLocal,
                idSuspeito: lobjSuspeito.Id
            },
            success: function (data, textStatus, XMLHttpRequest) {
                debugger;
                var leleSuspeito = new Object();
                var lintIdJogadorSalaAcusado = new Number();
                var lobjRetorno = new Object();
                try {
                    debugger;
                    Loading.Carregamento(false);
                    lobjRetorno = JSON.parse(data);
                    if (!lobjRetorno.Status) { throw lobjRetorno.Retorno; }

                    PopUp.Visualizar({
                        TipoPopUp: 'Alerta',
                        Mensagem: lobjRetorno.Retorno,
                        Evento: function () {
                            try {
                                $('#divPopUp').Detetive_Modal('hide');
                            } catch (ex) {
                                PopUp.Erro(ex);
                            }
                        }
                    });

                    leleSuspeito = $('#divTabuleiro > #div' + removeAcentos(lobjSuspeito.Descricao)).length == 0 ? $('#divTabuleiro >> #div' + removeAcentos(lobjSuspeito.Descricao)) : $('#divTabuleiro > #div' + removeAcentos(lobjSuspeito.Descricao));
                    if (leleSuspeito.length != 0) {
                        lintIdJogadorSalaAcusado = $(leleSuspeito).attr('idJogadorSala');
                        Sala.Teletransporte(lintIdJogadorSalaAcusado, lintIdLocal);
                    }
                    Sala.EnviarMensagem(lintIdSala);
                    Jogar.btnFinalizarTurno_OnClick();
                    Sala.AtualizarCartas();
                    $('#ModalAcusar').Detetive_Modal('hide');
                    Loading.Carregamento(false);
                } catch (ex) {
                    Loading.Carregamento(false);
                    $('#ModalAcusar').Detetive_Modal('hide');
                    PopUp.Erro(ex);
                }
            },
            error: function (data, textStatus, XMLHttpRequest) {
                $('#ModalAcusar').Detetive_Modal('hide');
                alert('Erro na chamada da Acusação');
            }
        });
    } catch (ex) {
        alert(ex);
    }
}