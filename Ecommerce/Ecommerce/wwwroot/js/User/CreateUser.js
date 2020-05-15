﻿(function (self, $, undefined) {
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
            is_admin_check.bootstrapToggle('off');
            is_admin_check.bootstrapToggle('disable');
            is_admin_toggle_button.prop('disabled', true);
        }
    }
}(window.CreateUser = window.CreateUser || {}, jQuery));

jQuery(function () {
    CreateUser.Start();
})