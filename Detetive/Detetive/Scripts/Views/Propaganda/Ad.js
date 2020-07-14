
var myVideo = document.getElementById("ad"); 


$(document).ready(function ()  {
    EsconderAd()
    setTimeout(ExibirAd, 10000)
   // setTimeout(EsconderAd, 15000)
    document.getElementById('close').onclick = function () {
        this.parentNode.parentNode.parentNode
            .removeChild(this.parentNode.parentNode);
        return false;
    };
});


setTimeout(EsconderAd, 30000)

function ExibirAd() {
    document.getElementById('propaganda').style.visibility = 'visible';
    myVideo.play()
}

function EsconderAd() {
    document.getElementById('propaganda').style.visibility = 'hidden';
    myVideo.pause()
}
