﻿(function (self, $, undefined) {
    var user_actions_btn_group = $("#user-actions-btn-group");

    Index.Start = function () {
        $('#users-table').DataTable({
            language: datatablesLangES,
            ajax: {
                url: "/Users/GetUsers",
                dataSrc: "",
                error: AjaxErrorHandler
            },
            responsive: true,
            autoWidth: true,
            rowId: "user_id",
            order: [[1, "asc"]],
            columns: [
                {
                    title: $("#id-column-title").html(),
                    data: "user_id",
                    responsivePriority: 2
                },
                {
                    title: $("#username-column-title").html(),
                    data: "username",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 4
                },
                {
                    title: $("#name-column-title").html(),
                    data: "name",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 6
                },
                {
                    title: $("#surname-column-title").html(),
                    data: "surname",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 7
                },
                {
                    title: $("#mail-column-title").html(),
                    data: "mail",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 5
                },
                {
                    title: $("#state-column-title").html(),
                    data: "state_text",
                    responsivePriority: 3
                },
                {
                    title: $("#created-column-title").html(),
                    data: { _: "creation_timestamp", sort: "creation_timestamp_ticks" },
                    responsivePriority: 8
                },
                {
                    title: $("#last-enabled-column-title").html(),
                    data: { _: "last_enabled_timestamp", sort: "last_enabled_timestamp_ticks" },
                    responsivePriority: 9
                },
                {
                    title: $("#last-login-column-title").html(),
                    data: { _: "last_login_timestamp", sort: "last_login_timestamp_ticks" },
                    responsivePriority: 10
                },
                {
                    title: $("#actions-column-title").html(),
                    visible: user_actions_btn_group.children().first().children().length > 0,
                    orderable: false,
                    responsivePriority: 1,
                    render: function (data, type, full, meta) {
                        var btn_group = user_actions_btn_group.clone();
                        (full.state === 10 || full.state === 20) ? btn_group.find(".btn-enable-user").remove() : btn_group.find(".btn-disable-user").remove();
                        if (!full.has_session)
                            btn_group.find(".btn-kill-session").addClass("disabled");
                        return btn_group.html().replace(/_USERID_/g, full.user_id);
                    }
                }
            ]
        });
    }

}(window.Index = window.Index || {}, jQuery));

jQuery(function () {
    Index.Start();
})