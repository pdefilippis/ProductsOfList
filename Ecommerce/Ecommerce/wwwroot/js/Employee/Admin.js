(function (self, $, undefined) {
    var lot_actions_btn_group = $("#lot-actions-btn-group");

    Index.Start = function () {
        var table = $('#employee-table').DataTable({
            language: datatablesLangES,
            ajax: {
                url: "/asd/asd",
                dataSrc: "",
                error: AjaxErrorHandler
            },
            responsive: true,
            autoWidth: true,
            rowId: "lot_id",
            order: [[0, "desc"]],
            columns: [
                {
                    title: $("#id-column-title").html(),
                    data: "lot_id",
                    responsivePriority: 1
                },
                {
                    title: $("#date-column-title").html(),
                    data: "date",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 2
                },
                {
                    title: $("#name-column-title").html(),
                    data: "name_lot",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 2
                },
                {
                    title: $("#articleApply-column-title").html(),
                    data: { _: "article_Apply", sort: "create_Date" },
                    responsivePriority: 4
                },
                {
                    title: $("#probabilityWinner-column-title").html(),
                    data: { _: "probability", sort: "update_Date" },
                    responsivePriority: 5
                },
                {
                    title: $("#winner-column-title").html(),
                    data: "winner",
                    responsivePriority: 6
                }
            ]
        });
    };

}(window.Index = window.Index || {}, jQuery));

jQuery(function () {
    Index.Start();
});
