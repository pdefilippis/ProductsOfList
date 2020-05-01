function showHidePassword() {
    var x = document.getElementById("passw");
    var y = document.getElementById("eyePass");
    if (x.type === "password") {
        y.className = "fa fa-eye-slash eye-style";
        x.type = "text";
    } else {
        y.className = "fa fa-eye eye-style";
        x.type = "password";
    }
}