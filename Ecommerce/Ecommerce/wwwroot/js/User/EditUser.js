(function (self, $, undefined) {
    var is_admin_check = $("#IsAdmin");
    var is_admin_toggle_button = $('#is-admin-toggle-button');

    EditUser.Start = function () {


        is_admin_toggle_button.on("click", function () {
            is_admin_check.bootstrapToggle('toggle');
        });
    }

    EditUser.Check = function () {
        if ($("#IsAdmin").is(":checked")) {
            is_admin_check.bootstrapToggle('enable');
            is_admin_toggle_button.prop('disabled', false);
        } else {
            is_admin_check.bootstrapToggle('disabled');
            is_admin_toggle_button.prop('enable', true);
        }
    }
}(window.EditUser = window.EditUser || {}, jQuery));

jQuery(function () {
    EditUser.Start();
})
