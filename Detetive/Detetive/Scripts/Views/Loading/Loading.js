var Loading = window.Loading || {

}

Loading.Carregamento = function (pblnCarregar) {
    try {
        debugger;
        if (pblnCarregar) {
            $('#divLoading #lblMensagem').text('Carregando . . .');
            $('#divLoading #DetetiveModal #divImagem').html('<div class="Loader"></div>')
            $('#divLoading #DetetiveModal .fecha').css('visibility', 'hidden');
            $('#divLoading').Detetive_Modal('carregar');
        } else {
            $('#divLoading').Detetive_Modal('hide');
            $('#divLoading #DetetiveModal #divImagem').remove('.Loader');
            $('#divLoading #DetetiveModal .fecha').css('visibility', 'visible');
        }
    } catch (ex) {
        alert(ex);
    }
}