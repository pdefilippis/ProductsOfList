function showHidePasswordOld() {
    var x = document.getElementById("oldpasswd");
    var y = document.getElementById("eyePasswOld");
    if (x.type === "password") {
        y.className = "fa fa-eye-slash eye-style";
        x.type = "text";
    } else {
        y.className = "fa fa-eye eye-style";
        x.type = "password";
    }
}

function showHidePasswordNew() {
    var x = document.getElementById("newpassd");
    var y = document.getElementById("eyePasswNew");
    if (x.type === "password") {
        y.className = "fa fa-eye-slash eye-style";
        x.type = "text";
    } else {
        y.className = "fa fa-eye eye-style";
        x.type = "password";
    }
}

function showHidePasswordConfirm() {
    var x = document.getElementById("newpassconfirm");
    var y = document.getElementById("eyePasswConf");
    if (x.type === "password") {
        y.className = "fa fa-eye-slash eye-style";
        x.type = "text";
    } else {
        y.className = "fa fa-eye eye-style";
        x.type = "password";
    }
}
