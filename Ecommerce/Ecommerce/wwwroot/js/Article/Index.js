(function (self, $, undefined) {
    var article_actions_btn_group = $("#article-actions-btn-group");

    Index.Start = function () {

        var lotId = $("#LotId").val();

        var table = $('#articles-table').DataTable({

            language: datatablesLangES,
            dom:
                "<'row'<'col-sm-5'B><'col-sm-4'l><'col-sm-3'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            buttons: [
                { extend: 'excelHtml5', className: "fa fa-file-excel-o btn-rounded btn-success", text: $("#excel-text").html(), titleAttr: $("#excel-title").html(), filename: $("#filename").html(), exportOptions: { columns: 'th:not(:last-child)' } },
                {
                    extend: 'pdfHtml5',
                    customize: function (doc) {
                        doc.content[1].margin = [0, 0, 0, 0]; //left, top, right, bottom
                        doc.styles.tableHeader.fontSize = 12;
                    }, className: "fa fa-file-pdf-o btn-danger", text: $("#pdf-text").html(), titleAttr: $("#pdf-title").html(), filename: $("#filename").html(), exportOptions: { columns: 'th:not(:last-child)' }
                },
                { extend: 'print', className: "fa fa-print btn-rounded btn-primary", text: $("#print-text").html(), titleAttr: $("#print-text").html(), filename: $("#filename").html(), exportOptions: { columns: 'th:not(:last-child)' } }
            ],
            ajax: {
                url: "/Article/GetArticles",
                data: { lotId: lotId },
                dataSrc: "",
                error: AjaxErrorHandler
            },
            responsive: true,
            autoWidth: true,
            rowId: "article_id",
            order: [[0, "desc"]],
            columns: [
                {
                    title: $("#id-column-title").html(),
                    data: "article_id",
                    responsivePriority: 2
                },
                {
                    title: $("#articleDescription-column-title").html(),
                    data: "article_Description",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 4
                },
                {
                title: $("#state-column-title").html(),
                data: "state",
                render: function (data, type, full, meta) {
                    var priority = data;
                    switch (priority) {
                        case "Activo":
                            priority = $("#active-field").html();
                            break;
                        case "Inactivo":
                            priority = $("#inactive-field").html();
                            break;
                        default:
                            priority = "";
                    }
                    return priority;
                },
                responsivePriority: 3
                },
                {
                    title: $("#type-column-title").html(),
                    data: "type",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 7
                },
                {
                    title: $("#serialNumber-column-title").html(),
                    data: "serialNumber",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 5
                },
                {
                    title: $("#price-column-title").html(),
                    data: "price",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 5
                },
                {
                    title: $("#adjudicatedUser-column-title").html(),
                    data: "adjudicated",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 7
                },
                {
                    title: $("#actions-column-title").html(),
                    visible: article_actions_btn_group.children().first().children().length > 0,
                    orderable: false,
                    responsivePriority: 1,
                    render: function (data, type, full, meta) {
                        var btn_group = article_actions_btn_group.clone();

                        if (full.state === "Activo")
                            btn_group.find("#toggleAble").remove();

                        if (full.state === "Inactivo")
                            btn_group.find("#toggleDisable").remove();

                        return btn_group.html().replace(/_articleID_/g, full.article_id).replace(/_lotID_/g, lotId);
                    }
                }
            ]
        });

        //Oculta las acciones (buttons) del articulo si el lote esta cerrado
        if ($("#State").val() == "CLOSED")
            table.column(9).visible(false);

    };
}(window.Index = window.Index || {}, jQuery));

jQuery(function () {
    Index.Start();
});
