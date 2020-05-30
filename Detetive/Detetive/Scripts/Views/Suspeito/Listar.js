var Listar = window.Teste || {
};

$(document).ready(function () {
    
});

Listar.TransmitirMensagem = function (apelido, msg) {
    // Area do chat
    var chatWin = $("#chatWindow");
    // Publicando a mensagem no chat
    chatWin.html(chatWin.html() + "<b>" + apelido + "</b>: " + msg + "<br />");
}

$('#mensagem').keypress(function (e) {
    if (e.which == 13) {
        debugger;
        // Chamando o método de transmissão de mensagem no Hub
        Sala.EnviarMensagem($("#apelido").val(), $("#mensagem").val());
        // Limpando o texto da mensagem.
        $("#mensagem").val("");
    }
});