var ModalPalpite = window.ModalPalpite || {

}

ModalPalpite.Palpitar = function () {
    var lintIdJogadorSala = new Number();
    var lintIdArma = new Number();
    var lintIdLocal = new Number();
    try {
        lintIdJogadorSala = $('#lslbJogador').val();
        lintIdArma = $('#lslbArmas').val();
        lintIdLocal = Jogar.GetLocalAtual(Jogar.mID_JOGADOR_SALA);
        debugger;
        $.ajax({
            url: gstrGlobalPath + 'Partida/Palpite',
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
                    debugger;
                    Sala.Teletransporte(lintIdJogadorSala, lintIdLocal);
                } catch (ex) {
                    throw ex;
                }
            }
        });

    } catch (ex) {
        alert(ex);
    }
}