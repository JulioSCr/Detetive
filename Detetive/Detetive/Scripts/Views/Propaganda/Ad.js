
var myVideo = document.getElementById("ad"); 


window.onload = function () {
    EsconderAd()
    setTimeout(ExibirAd, 3000)
   // setTimeout(EsconderAd, 15000)
    document.getElementById('close').onclick = function () {
        this.parentNode.parentNode.parentNode
            .removeChild(this.parentNode.parentNode);
        return false;
    };

};


function ExibirAd() {
    document.getElementById('propaganda').style.visibility = 'visible';
    myVideo.play();
    myVideo.autoplay()
}

function EsconderAd() {
    document.getElementById('propaganda').style.visibility = 'hidden';
    myVideo.pause()
}
