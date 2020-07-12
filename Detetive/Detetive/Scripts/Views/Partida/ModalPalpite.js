var ModalPalpite = window.ModalPalpite || {

}

ModalPalpite.Palpitar = function () {
    var lintIdJogadorSala = new Number();
    var lintIdArma = new Number();
    var lintIdLocal = new Number();
    var lobjSuspeito = new Object();
    try {
        lintIdArma = $('#lslbArmas').val();
        lintIdLocal = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        lintIdJogadorSala = Jogar.mID_JOGADOR_SALA;
        lintIdSala = Jogar.mID_SALA;
        lobjSuspeito.Id = $('#ddlModalPalpite_Suspeito').val();
        lobjSuspeito.Descricao = $('#ddlModalPalpite_Suspeito').text().trim()

        $.ajax({
            url: gstrGlobalPath + 'Partida/Palpite',
            data: {
                idJogadorSala: lintIdJogadorSala,
                idSala: lintIdSala,
                idArma: lintIdArma,
                idLocal: lintIdLocal,
                idSuspeito: lobjSuspeito.Id
            },
            success: function (data, textStatus, XMLHttpRequest) {
                var leleSuspeito = new Object();
                var lintIdJogadorSalaAcusado = new Number();
                var lobjRetorno = new Object();
                try {
                    debugger;
                    lobjRetorno = JSON.parse(data);
                    if (!lobjRetorno.Status) { throw lobjRetorno.Retorno; }

                    leleSuspeito = $('#divTabuleiro > #div' + removeAcentos(lobjSuspeito.Descricao)).length == 0 ? $('#divTabuleiro >> #div' + removeAcentos(lobjSuspeito.Descricao)) : $('#divTabuleiro > #div' + removeAcentos(lobjSuspeito.Descricao));
                    if (leleSuspeito.length != 0) {
                        lintIdJogadorSalaAcusado = $(leleSuspeito).attr('idJogadorSala');
                        Sala.Teletransporte(lintIdJogadorSalaAcusado, lintIdLocal);
                    }

                    $('#ModalPalpite').Detetive_Modal('hide');
                } catch (ex) {
                    $('#ModalPalpite').Detetive_Modal('hide');
                    alert(ex);
                }
            },
            error: function (data, textStatus, XMLHttpRequest) {
                $('#ModalPalpite').Detetive_Modal('hide');
                alert('Erro na chamada da Acusação');
            }
        });

    } catch (ex) {
        alert(ex);
    }
}