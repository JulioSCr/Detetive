function isNull(vobjValor, vobjValorSeNulo) {
    var lobjRetorno;

    if (vobjValor == null || vobjValor == undefined) {
        lobjRetorno = vobjValorSeNulo;
    } else {
        lobjRetorno = vobjValor;
    }

    return lobjRetorno;
}

/**
 * Remove acentos de caracteres
 * @param  {String} stringComAcento [string que contem os acentos]
 * @return {String}                 [string sem acentos]
 */
function removeAcentos(pstrStringComAcento) {
    var lstrStringSemAcento = pstrStringComAcento;

    var larrMapaAcentosHex = {
        a: /[\xE0-\xE6]/g,
        A: /[\xC0-\xC6]/g,
        e: /[\xE8-\xEB]/g,
        E: /[\xC8-\xCB]/g,
        i: /[\xEC-\xEF]/g,
        I: /[\xCC-\xCF]/g,
        o: /[\xF2-\xF6]/g,
        O: /[\xD2-\xD6]/g,
        u: /[\xF9-\xFC]/g,
        U: /[\xD9-\xDC]/g,
        c: /\xE7/g,
        C: /\xC7/g,
        n: /\xF1/g,
        N: /\xD1/g,
    };

    for (var lchrLetra in larrMapaAcentosHex) {
        var lchrExpressaoRegular = larrMapaAcentosHex[lchrLetra];
        lstrStringSemAcento = lstrStringSemAcento.replace(lchrExpressaoRegular, lchrLetra);
    }

    return lstrStringSemAcento;
}

$(document).ready(function () {
    
});