window.onload = () => {
	let offset = 0;
    const loginBox = document.getElementById("login-box");
    const flag = setInterval(() => {
        loginBox.style.marginTop = -300 + offset++ + "px";
        loginBox.style.opacity = offset / 70;
        if (offset > 50) clearInterval(flag);
    }, 10);
}

$(document).ready(() => {
    const user = $("#txtUser");
    const pwd = $("#txtPwd");

    function defText(txt, pwd = false) {
        const def = "gray";
        const slc = "black"
        const str = txt.val();
        txt.css("color", def);
        txt.focus(() => {
            if (txt.val() == str) {
                txt.val("");
                txt.css("color", slc)
                if (pwd) txt.attr("type", "password");
            }
        });
        txt.blur(() => {
            if (txt.val() == "") {
                txt.val(str);
                txt.css("color", def)
                if (pwd) txt.attr("type", "text");
            }
        });
    }

    defText(user);
    defText(pwd, true);

    function light(txt, select, move) {
        if (select) {
            txt.css("border", "3px solid green");
            txt.css("box-shadow", "0 0 20px green");
        } else if (move) {
            txt.css("border", "3px solid blue");
        } else {
            txt.css("border", "3px solid pink");
            txt.css("box-shadow", "");
        }
    }

    function select(txt) {
        txt.width(txt.width() - 6);
        txt.height(txt.height() - 6);
        txt.css("border", "3px solid pink");
        txt.focus(() => light(txt, true, null));
        txt.blur(() => light(txt, false, false));
        txt.hover(() => {
            if (!txt.is(":focus")) light(txt, false, true);
        }, () => {
            if (!txt.is(":focus")) light(txt, false, false);
        });
    }

    select(user);
    select(pwd);

    function btnlight(btn) {
        btn.hover(() => {
            btn.css("box-shadow", "0 0 20px green");
        }, () => {
            btn.css("box-shadow", "");
        });
    }

    btnlight($("#btnSignIn"))
    btnlight($("#btnSignOut"))
});
