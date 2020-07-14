var PopUp = window.PopUp || {
}

PopUp.Visualizar = function (pobjEntrada) {
    try {
        PopUp.Validar(pobjEntrada);

        $('#divPopUp').Detetive_Modal({
            SetTitulo: true,
            Titulo: pobjEntrada.TipoPopUp.toUpperCase() == 'QUESTAO' ? 'Questão' : pobjEntrada.TipoPopUp,
            SetImagem: true,
            Imagem: pobjEntrada.TipoPopUp
        });

        $('#lblMensagem').text(pobjEntrada.Mensagem);

        if (pobjEntrada.TipoPopUp.toUpperCase() == 'QUESTAO') {
            $('#divPopUp #DetetiveModal .fecha').css('visibility', 'visible');
            $('#divPopUp').Detetive_Modal('show');
        } else {
            $('#divPopUp #DetetiveModal .fecha').css('visibility', 'hidden');
            $('#divPopUp').Detetive_Modal('popup');
        }

        PopUp.btnOk_OnClick = pobjEntrada.Evento;

    } catch (ex) {
        alert(ex);
    }
}

PopUp.Erro = function (pstrMensagem) {
    try {
        PopUp.Visualizar({
            TipoPopUp: 'Erro',
            Mensagem: pstrMensagem,
            Evento: function () {
                $('#divPopUp').Detetive_Modal('hide');
            }
        });
    } catch (ex) {
        alert(ex);
    }
}

// Não apague, está vazio pois é substituido por outra função
PopUp.btnOk_OnClick = function () {

}

PopUp.Validar = function (pobjPopUp) {
    var ex = new String('');

    if (pobjPopUp == null || pobjPopUp == undefined) {
        throw 'Objeto de popup nulo!';
    }

    if (pobjPopUp.TipoPopUp == undefined || pobjPopUp.TipoPopUp == null || !(typeof pobjPopUp.TipoPopUp === 'string')) {
        ex = '\nTipo de popup nulo!';
    }

    if (pobjPopUp.Mensagem == undefined || pobjPopUp.Mensagem == null) {
        ex = '\nMensagem de popup nulo!';
    }

    if (pobjPopUp.Evento == undefined || pobjPopUp.Evento == null) {
        ex = '\nEvento de popup nulo!';
    }

    if (ex != '') {
        throw ex;
    }

    return;
}