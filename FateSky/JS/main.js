window.onload = () => {
    const imgCount = 10;
    let index = Math.floor(Math.random() * imgCount);
    let last = index;
    document.getElementById("bg-img").style.background = "url('../Images/BGI/" + index + ".jpg')";
    function change() {
        do {
            index = Math.floor(Math.random() * imgCount);
        } while (index === last);
        last = index;
        gradual(index);
    }
    change();
    setInterval(() => change(), 5000);
}

function gradual(index) {
    let op1 = 100;
    let op2 = 0;
    const bgImg = document.getElementById("bg-img");
    const bgImgBak = document.getElementById("bg-img-bak");
    const flag = setInterval(() => {
        if (op1 > 0)
            bgImg.style.opacity = --op1 / 100;
        if (op1 < 50) {
            bgImgBak.style.background = "url('../Images/BGI/" + index + ".jpg')";
            bgImgBak.style.opacity = op2++ / 100;
        }
        if (op2 > 100 || document.hidden) {
            bgImg.style.background = "url('../Images/BGI/" + index + ".jpg')";
            bgImg.style.opacity = 1;
            bgImgBak.style.opacity = 0;
            clearInterval(flag);
        }
    }, 30)
}

$(document).ready(() => {
    $("#nav-link > li").hover(function() {
        $(this).find("ul").show(100);
    }, function () {
        $(this).find("ul").hide();
    });
    $("#nav-user").hover(() => {
        $("#nav-info").show(100);
    }, function () {
        $("#nav-info").hide();
    });
});
