var ModalAcusar = window.ModalAcusar || {

}

ModalAcusar.Acusar = function () {
    var lintIdJogadorSala = new Number();
    var lintIdArma = new Number();
    var lintIdLocal = new Number();
    try {
        lintIdJogadorSala = $('#lslbModalAcusar_Suspeito').val();
        lintIdArma = $('#lslbModalAcusar_Armas').val();
        lintIdLocal = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        $.ajax({
            url: gstrGlobalPath + 'Partida/Acusar',
            data: {
                idJogadorSala: lintIdJogadorSala,
                idArma: lintIdArma,
                idLocal: lintIdLocal
            },
            success: function (data, textStatus, XMLHttpRequest) {
                try {
                    //if (!JSON.parse(data.toLowerCase())) { throw 'Movimento inválido'; }

                    //Jogar.RemoveDoLocal(Jogar.mID_JOGADOR_SALA, 0, 0);
                    //Jogar.TransmitirMovimento(Jogar.mID_JOGADOR_SALA, 0, 0, lintIdLocal);
                    Sala.Teletransporte(lintIdJogadorSala, lintIdLocal);
                    $('#ModalAcusar').Detetive_Modal('hide');
                } catch (ex) {
                    throw ex;
                }
            }
        });

    } catch (ex) {
        alert(ex);
    }
}