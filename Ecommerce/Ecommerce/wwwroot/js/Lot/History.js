(function (self, $, undefined) {

    History.Start = function () {
        $('#lots-history-table').DataTable({
            language: datatablesLangES,
            ajax: {
                url: "/Lot/GetHistory",
                dataSrc: "",
                data: { lotId: $('#LotId').val() },
                error: AjaxErrorHandler
            },
            responsive: true,
            autoWidth: true,
            order: [[3, "desc"]],
            columns: [
                {
                    title: $("#lots-column-title").html(),
                    data: "description",
                    responsivePriority: 2
                },
                {
                    title: $("#users-column-title").html(),
                    data: "user",
                    responsivePriority: 3
                },
                {
                    title: $("#actions-column-title").html(),
                    data: "action",
                    render: function (data, type, full, meta) {
                        var priority = data;
                        switch (priority) {
                            case "EDITAR":
                                priority = $("#edit-field").html();
                                break;
                            case "CREAR":
                                priority = $("#create-field").html();
                                break;
                            case "INACTIVAR":
                                priority = $("#inactivate-field").html();
                                break;
                            case "ACTIVAR":
                                priority = $("#activate-field").html();
                                break;
                            case "CERRAR":
                                priority = $("#closed-field").html();
                                break;
                            default:
                                priority = "";
                        }
                        return priority;
                    },
                    responsivePriority: 4
                },
                {
                    title: $("#date-column-title").html(),
                    data: "date",
                    responsivePriority: 5
                }
            ]
        });
    }

}(window.History = window.History || {}, jQuery));

jQuery(function () {
    History.Start();
})