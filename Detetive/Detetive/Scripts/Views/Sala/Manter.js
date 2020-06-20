var ManterSala = window.ManterSala || {

};

ManterSala.MontarTela = function () {
    ManterSala.divInputs_Clear();
};

ManterSala.divInputs_Clear = function () {
    try {
        $('#txtIdSala').val('');
        $('#txtNick').val('');
    } catch (ex) {
        alert(ex);
    }
}

ManterSala.divInputs_Hide = function () {
    try {
        $('#divInputs').css('visibility', 'hidden');
    } catch (ex) {
        alert(ex);
    }
}

ManterSala.divInputs_Show = function () {
    try {
        $('#divInputs').css('visibility', 'visible');
    } catch (ex) {
        alert(ex);
    }
}

$(document).ready(function () {
    ManterSala.MontarTela();
});

ManterSala.IngressarSala = function () {
    try {
        ManterSala.divInputs_Clear();
        if ($('#divInputs').css('visibility') != 'visible') {
            ManterSala.divInputs_Show();
        }
        $('#txtIdSala').attr('placeholer', 'Digite o #idSala aqui');
        $('#lblIdSala').text('Informe o ID da Sala:');
    }
    catch (ex) {
        alert(ex);
    }
}

ManterSala.CriarSala = function () {
    var lintIdSala = new Number();
    try {
        if ($('#divInputs').css('visibility') != 'visible') {
            ManterSala.divInputs_Show();
        }
        $.ajax({
            url: gstrGlobalPath + 'Sala/CriarSala',
            success: function (data, textStatus, XMLHttpRequest) {
                try {
                    //if (!JSON.parse(data.toLowerCase())) { throw 'Movimento inválido'; }
                    //lintIdSala = parseInt(JSON.parse(data));
                    lstrIdSala = '#' + data;
                    $('#lblIdSala').text('ID da sala:');
                    $('#txtIdSala').val(lstrIdSala);
                } catch (ex) {
                    throw ex;
                }
            }
        });
    }
    catch (ex) {
        throw ex;
    }
}

ManterSala.divBtnVamosAoCaso_OnClick = function () {
    var lintIdSala = new Number();
    var lstrDsJogdor = new String();
    try {
        debugger;
        lintIdSala = parseInt(($('#txtIdSala').val()).replace('#', ''));
        lstrDsJogdor = $('#txtNick').val();
        $.ajax({
            url: gstrGlobalPath + 'Sala/Ingressar',
            type: 'post',
            data: {
                idSala: lintIdSala,
                dsJogador: lstrDsJogdor
            },
            success: function (data, textStatus, XMLHttpRequest) {
                try {
                    debugger;
                    //if (!JSON.parse(data.toLowerCase())) { throw 'Movimento inválido'; }
                    //lintIdSala = parseInt(JSON.parse(data));
                    
                } catch (ex) {
                    throw ex;
                }
            }
        });
    } catch (ex) {
        alert(ex);
    }
}