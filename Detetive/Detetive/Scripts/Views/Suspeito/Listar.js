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
        Sala.mIdSala = Listar.mintIdSala;
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

Listar.TransmitirSelecaoSuspeito = function (pintIdJogadorSala, pintIdSuspeito, pstrDescricaoJogador, pstrDescricaoSuspeitoSelecionado, pstrDescricaoSuspeitoDesconsiderado) {
    var lstrJogadorSuspeito = new String();
    try {

        //#region Cartas

        if ($('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').length > 0) {
            Listar.TransmitirDesconsideracaoSuspeito(pintIdJogadorSala, pstrDescricaoSuspeitoDesconsiderado);
        }
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').attr('title', pstrDescricaoJogador);
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').addClass('selected');
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').data().idjogadorsala = pintIdJogadorSala;
        $('.cartaSuspeito[data-id=' + pintIdSuspeito + ']').attr('data-idjogadorsala', pintIdJogadorSala);

        //#endregion 
        
        //#region Listagem de seleção de personagens

        lstrJogadorSuspeito = '<ul id="lista"> ' + pstrDescricaoJogador + ' — ' + pstrDescricaoSuspeitoSelecionado + ' </ul>';
        $('#divInformaIDSala').append(lstrJogadorSuspeito);

        leleSuspeitosSelecionados = $('#divInformaIDSala').children('ul');
        if (leleSuspeitosSelecionados.length < 3) {
            $('#btnVamosAoCaso').prop('disabled', true);
        } else {
            $('#btnVamosAoCaso').prop('disabled', false);
        }

        //#endregion
        
    } catch (ex) {
        alert(ex);
    }
}

Listar.TransmitirDesconsideracaoSuspeito = function (pintIdJogadorSala, pstrDescricaoSuspeito) {
    var leleSuspeitosSelecionados = Array();
    try {

        //#region Cartas

        if ($('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').length > 0) {
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').attr('title', pstrDescricaoSuspeito);
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').removeClass('selected');
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').data().idjogadorsala = 0;
            $('.cartaSuspeito[data-idjogadorsala=' + pintIdJogadorSala + ']').attr('data-idjogadorsala', 0);
        }

        //#endregion 
        
        //#region Listagem de seleção de personagens

        leleSuspeitosSelecionados = $('#divInformaIDSala').children('ul');
        for (var i = 0; i < leleSuspeitosSelecionados.length; i++) {
            if (leleSuspeitosSelecionados[i].textContent.trim().includes(pstrDescricaoSuspeito)) {
                $(leleSuspeitosSelecionados[i]).remove();
                leleSuspeitosSelecionados.splice(i);
                break;
            }
        }
        
        if (leleSuspeitosSelecionados.length < 3) {
            $('#btnVamosAoCaso').prop('disabled', true);
        }

        //#endregion 

    } catch (ex) {
        alert(ex);
    }
}

Listar.btnVamosAoCaso_OnClick = function () {
    try {
        //$.ajax({
        //    url: gstrGlobalPath + 'Suspeito/Ingressar',
        //    data: {
        //        idJogadorSala: Listar.mintIdJogadorSala
        //    },
        //    type: 'post',
        //    success: function (data, textStatus, XMLHttpRequest) {

        //    }
        //});
        location.href = '/Partida/Jogar?idJogadorSala=' + Listar.mintIdJogadorSala;
    } catch (ex) {
        alert(ex);
    }
}
