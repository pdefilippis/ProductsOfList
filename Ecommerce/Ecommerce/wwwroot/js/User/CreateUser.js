(function (self, $, undefined) {
    var is_admin_check = $("#IsAdmin");
    var is_admin_toggle_button = $('#is-admin-toggle-button');

    CreateUser.Start = function () {


        is_admin_toggle_button.on("click", function () {
            is_admin_check.bootstrapToggle('toggle');
        });
    }

    CreateUser.Check = function () {
        if ($("#IsAdmin").is(":checked")) {
            is_admin_check.bootstrapToggle('enable');
            is_admin_toggle_button.prop('disabled', false);
        } else {
            is_admin_check.bootstrapToggle('disabled');
            is_admin_toggle_button.prop('enable', true);
        }
    }
}(window.CreateUser = window.CreateUser || {}, jQuery));

jQuery(function () {
    CreateUser.Start();
})



    function showHidePassword() {
        var x = document.getElementById("passwd");
        var y = document.getElementById("eyePasswd");
        if (x.type === "password") {
            y.className = "fa fa-eye-slash eye-style";
            x.type = "text";
        } else {
            y.className = "fa fa-eye eye-style";
            x.type = "password";
        }
    }