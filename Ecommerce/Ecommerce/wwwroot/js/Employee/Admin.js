(function (self, $, undefined) {
    var lot_actions_btn_group = $("#lot-actions-btn-group");

    Index.Start = function () {
        var table = $('#employee-table').DataTable({
            language: datatablesLangES,
            ajax: {
                url: "/Employee/GetDataEmployee",
                dataSrc: "",
                error: AjaxErrorHandler
            },
            responsive: true,
            autoWidth: true,
            order: [[3, "desc"]],
            columns: [
                {
                    title: $("#name-column-title").html(),
                    data: "nameLot",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 1
                },
                {
                    title: $("#articleApply-column-title").html(),
                    data: "articleName",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 2
                },
                {
                    title: $("#probabilityWinner-column-title").html(),
                    data: "probability",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 3
                },
                {
                    title: $("#winner-column-title").html(),
                    data: "winner",
                    responsivePriority: 4,
                    render: function (data, type, full, meta) {
                        if (full.winner === "v") {
                            return $("#yes-template").html()
                        }
                        else if (full.winner === "x") {
                            return $("#no-template").html();
                        } else {
                            return $("#sin-cerrar-template").html();
                        }
                    }
                }
            ]
        });
    };

}(window.Index = window.Index || {}, jQuery));

jQuery(function () {
    Index.Start();
});
