(function (self, $, undefined) {
    var user_actions_btn_group = $("#user-actions-btn-group");

    Index.Start = function () {
        $('#users-table').DataTable({
            language: datatablesLangES,
            ajax: {
                url: "/User/GetUsers",
                dataSrc: "",
                error: AjaxErrorHandler
            },
            responsive: true,
            autoWidth: true,
            rowId: "user_id",
            order: [[1, "desc"]],
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
                    title: $("#created-column-title").html(),
                    data: { _: "creation_timestamp", sort: "creation_timestamp_ticks" },
                    responsivePriority: 8
                },
                {
                    title: $("#last-login-column-title").html(),
                    data: { _: "last_login_timestamp", sort: "last_login_timestamp_ticks" },
                    responsivePriority: 10
                },
                {
                    title: $("#isadmin-column-title").html(),
                    data: "isadmin",
                    responsivePriority: 9,
                    render: function (data, type, full, meta) {
                        if (type === "display") {
                            return data ? $("#yes-template").html() : $("#no-template").html();
                        } else {
                            return data;
                        }
                    }
                },
                {
                    title: $("#enabled-column-title").html(),
                    data: "enabled",
                    responsivePriority: 3,
                    render: function (data, type, full, meta) {
                        if (type === "display") {
                            return data ? $("#yes-template").html() : $("#no-template").html();
                        } else {
                            return data;
                        }
                    }
                },
                {
                    title: $("#actions-column-title").html(),
                    visible: user_actions_btn_group.children().first().children().length > 0,
                    orderable: false,
                    responsivePriority: 1,
                    render: function (data, type, full, meta) {
                        var btn_group = user_actions_btn_group.clone();

                        if (full.user_id === full.current_user_id) {
                            btn_group.find(".btn-enable-user").remove();
                            btn_group.find(".btn-disable-user").remove();
                        } else {
                            (full.enabled) ? btn_group.find(".btn-enable-user").remove() : btn_group.find(".btn-disable-user").remove();
                        }

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
