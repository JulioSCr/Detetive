var Loader = window.Loader || {

}

Loader.Carregar = function () {
    try {
        $('#divLoadContainer').toggleClass('ocultar');
    } catch (ex) {
        PopUp.Erro(ex);
    }
}