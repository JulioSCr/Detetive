$.fn.Detetive_Modal = function (vobjMetodosOuParametros) {
    if (typeof vobjMetodosOuParametros === 'string') {
        switch (vobjMetodosOuParametros.toUpperCase()) {
            case 'SHOW':
                return show(this);
                break;
            case 'HIDE':
                return hide(this);
                break;
            case 'TOGGLE':
                return toggle(this);
                break;
            default:
                return null;
        }
    } else { //Criação do componente
        //Componente
        var lobjComponente = this;
        var lstrComponenteID = new String('');
        var lstrPopUpID = new String('');
        //Parâmetros de Configuração
        var lstrExibirTitulo = new String();
        if (vobjMetodosOuParametros != undefined) {
            lstrExibirTitulo = vobjMetodosOuParametros.Titulo == undefined ? '' : vobjMetodosOuParametros.Titulo;
        }
        //Parâmetros de Conteúdo

        lstrComponenteID = $(lobjComponente).attr('id');
        var lstrUrl = $('#' + lstrComponenteID).data('url');

        $.ajax({
            url: lstrUrl,
            success: function (data, textStatus, XMLHttpRequest) {
                $('#' + lstrComponenteID).html(data);
                if (lstrExibirTitulo != '') {
                    SetTitulo(lstrComponenteID, lstrExibirTitulo)
                }
            }
        });
    }
}

function show(vobjComponente) {
    var lobjModalID = new String();
    try {
        lobjModalID = $(vobjComponente).attr('id');
        $('#' + lobjModalID + ' .modal-container').addClass('mostrar');
        vobjComponente[0].addEventListener('click', (e) => {
            if (e.target.id == $('#' + lobjModalID + ' .modal-container').attr('id') || e.target.className == $('#' + lobjModalID + ' .fecha').attr('class')) {
                $('#' + lobjModalID + ' .modal-container').removeClass('mostrar');
            }
        });
    } catch (ex) {
        alert(ex);
    }
}

function hide(vobjComponente) {
    try {
        lobjModalID = $(vobjComponente).attr('id');
        $('#' + lobjModalID + ' .modal-container').removeClass('mostrar');
    } catch (ex) {
        alert(ex);
    }
}

function SetTitulo(pstrComponenteID, pstrTitulo) {
    try {
        $('#' + pstrComponenteID + ' #lblDetetiveModalTitulo').text(pstrTitulo);
    } catch (ex) {
        alert(ex);
    }
}