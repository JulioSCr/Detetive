var ManterSala = window.ManterSala || {

};

ManterSala.MontarTela = function () {

};

$(document).ready(function () {
    ManterSala.MontarTela();
});

Manter.IngressarSala = function () {
    try {
        debbuger;
        $('#txtIdSala').attr('placeholer', 'Digite o #idSala aqui');
        $('#lblIdSala').text('Informe o ID da Sala:');
    }
    catch (ex) {
        alert(ex);
    }
}

Manter.CriarSala = function () {
    var lintIdSala = new Number();
    try {
        debugger;
        $.ajax({
            url: gstrGlobalPath + ManterSala / this.CriarSala, success: function (data, textStatus, XMLHttpRequest) {
                try {
                    debbuger;
                    //if (!JSON.parse(data.toLowcase())) {throw 'Movimeno Invalido'; }
                    //lintIdSala =541234;
                    lintIdSala = parseInt(JSON.parse(data));
                    $('#itxtdSala').attr('placerholder', '#' + lintIdSala);
                    $('3lblIdSala').text('ID da sala');
                } catch (ex) {
                    throw ex;
                }
            }
        })
    }
}