$.fn.Detetive_Modal = function (vobjMetodosOuParametros) {
    if (typeof vobjMetodosOuParametros === 'string') {
        switch (vobjMetodosOuParametros.toUpperCase()) {
            case 'POPUP':
                return popup(this);
                break;
            case 'SHOW':
                return show(this);
                break;
            case 'HIDE':
                return hide(this);
                break;
            case 'TOGGLE':
                return toggle(this);
                break;
            case 'CARREGAR':
                return carregar(this);
                break;
            default:
                return null;
        }
    } else { //Criação do componente
        //Componente
        var lobjComponente = this;
        var lstrComponenteID = new String('');
        var lstrPopUpID = new String('');
        //Parâmetros de Conteúdo
        var lblnMudarTitulo = false;
        var lstrExibirTitulo = new String();
        var lblnMudarImagem = false;
        var lstrImagem = new String('');

        if (vobjMetodosOuParametros != undefined) {
            lstrExibirTitulo = vobjMetodosOuParametros.Titulo == undefined ? '' : vobjMetodosOuParametros.Titulo;

            lstrImagem = vobjMetodosOuParametros.Imagem == undefined ? '' : vobjMetodosOuParametros.Imagem;
            switch (lstrImagem.toUpperCase()) {
                case 'ALERTA':
                    lstrImagem = 'Content/Imagens/Shared/imgAlerta.svg';
                    break;
                case 'ERRO':
                    lstrImagem = 'Content/Imagens/Shared/imgErro.svg';
                    break;
                case 'QUESTAO':
                    lstrImagem = 'Content/Imagens/Shared/imgQuestion.svg';
                    break;
                default:
                    break;
            }

            if (vobjMetodosOuParametros.SetTitulo == null || vobjMetodosOuParametros.SetTitulo == undefined) {
                lblnMudarTitulo = false;
            } else {
                lblnMudarTitulo = vobjMetodosOuParametros.SetTitulo;
            }

            if (vobjMetodosOuParametros.SetImagem == null || vobjMetodosOuParametros.SetImagem == undefined) {
                lblnMudarImagem = false
            } else {
                lblnMudarImagem = vobjMetodosOuParametros.SetImagem;
            }

        }       
        //Parâmetros de Configuração

        lstrComponenteID = $(lobjComponente).attr('id');
        var lstrUrl = $('#' + lstrComponenteID).data('url');

        if (lblnMudarImagem && lstrImagem != '' && lblnMudarTitulo) {
            SetTitulo(lstrComponenteID, lstrExibirTitulo);
            SetImagem(lstrComponenteID, lstrImagem);
            return;
        }

        if (lblnMudarTitulo) {
            SetTitulo(lstrComponenteID, lstrExibirTitulo);
            return;
        }

        if (lblnMudarImagem && lstrImagem != '') {
            SetImagem(lstrComponenteID, lstrImagem);
            return;
        }

        $.ajax({
            url: lstrUrl,
            success: function (data, textStatus, XMLHttpRequest) {
                $('#' + lstrComponenteID).html(data);
                if (lstrExibirTitulo != '') {
                    SetTitulo(lstrComponenteID, lstrExibirTitulo)
                }
                if (lstrImagem != '') {
                    SetImagem(lstrComponenteID, lstrImagem)
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

function popup(vobjComponente) {
    var lobjModalID = new String();
    try {
        lobjModalID = $(vobjComponente).attr('id');
        $('#' + lobjModalID + ' .modal-container').addClass('mostrar');
    } catch (ex) {
        alert(ex);
    }
}

function carregar(vobjComponente) {
    var lobjModalID = new String();
    try {
        lobjModalID = $(vobjComponente).attr('id');
        $('#' + lobjModalID + ' .modal-container').addClass('delay');
    } catch (ex) {
        alert(ex);
    }
}

function hide(vobjComponente) {
    try {
        lobjModalID = $(vobjComponente).attr('id');
        $('#' + lobjModalID + ' .modal-container').removeClass('delay');
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

function SetImagem(pstrComponenteID, pstrImagem) {
    var lstrElementoImagem = new String();
    try {
        if (pstrImagem == '') {
            $('#' + pstrComponenteID + ' #divImagem img').remove();
        } else {
            pstrImagem = gstrGlobalPath + pstrImagem.replace(/^(~\\|\\)/, '');
            lstrElementoImagem = '<img id="imgPopUp" src="' + pstrImagem + '"/>';

            $('#' + pstrComponenteID + ' #divImagem').html(lstrElementoImagem);
        }
    } catch (ex) {
        alert(ex);
    }
}