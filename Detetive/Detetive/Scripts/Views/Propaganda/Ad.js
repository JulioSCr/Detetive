
var myVideo = document.getElementById("ad"); 


window.onload = function () {
    EsconderAd()
    setTimeout(ExibirAd, 5000)
    setTimeout(EsconderAd, 15000)
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


