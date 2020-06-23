var Listar = window.Listar || {
    mintIdSuspeito: new Number(),
    mintIdSala: new Number(),
    mintIdJogadorSala: new Number()
};

$(document).ready(function () {
    Listar.MontarTela();
});

Listar.MontarTela = function () {
    try {
        Listar.mintIdSala = $('#divInformaIDSala').data().id;
        Listar.mintIdJogadorSala = $('#inpID_JOGADOR_SALA').data().id;
        if (Listar.mintIdSala == null || Listar.mintIdSala == undefined) { throw 'Sala não encontrada.' }
        Sala.mintIdSala = Listar.mintIdSala;
    } catch (ex) {
        alert(ex);
    }
}

Listar.Suspeito_OnClick = function (e) {
    try {
        if ($(e.currentTarget).children().data().idjogadorsala == Listar.mintIdJogadorSala) {
            Sala.DesconsiderarSuspeito(Listar.mintIdSala, Listar.mintIdJogadorSala);
        } else {
            if ($(e.currentTarget).children().data().idjogadorsala == 0) {
                $('#carouselSuspeitos').carousel('pause');
                Listar.mintIdSuspeito = parseInt($(e.currentTarget).children().get(0).dataset.id);
                Sala.SelecionarSuspeito(Listar.mintIdSala, Listar.mintIdJogadorSala, Listar.mintIdSuspeito);
            } else {
                return;
            }
        }
    } catch (ex) {
        alert(ex);
    }
}

Listar.TransmitirSelecaoSuspeito = function (pintIdJogadorSala, pintIdSuspeito, pstrDescricaoJogador, pstrDescricaoSuspeito) {
    try {
        if ($('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').length > 0) {
            Listar.TransmitirDesconsideracaoSuspeito(pintIdJogadorSala, pstrDescricaoSuspeito);
        }
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').attr('title', pstrDescricaoJogador);
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').addClass('selected');
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').data().idjogadorsala = pintIdJogadorSala;
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').attr('data-idjogadorsala', pintIdJogadorSala);
    } catch (ex) {
        alert(ex);
    }
}

Listar.TransmitirDesconsideracaoSuspeito = function (pintIdJogadorSala, pstrDescricaoSuspeito) {
    try {
        if ($('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').length > 0) {
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').attr('title', pstrDescricaoSuspeito);
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').removeClass('selected');
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').data().idjogadorsala = 0;
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').attr('data-idjogadorsala', 0);
        }
    } catch (ex) {
        alert(ex);
    }
}
