(function (self, $, undefined) {
    var lot_actions_btn_group = $("#lot-actions-btn-group");

    Index.Start = function () {
        var table = $('#lots-table').DataTable({
            language: datatablesLangES,
            ajax: {
                url: "/Lot/GetLots",
                dataSrc: "",
                error: AjaxErrorHandler
            },
            responsive: true,
            autoWidth: true,
            rowId: "lot_id",
            order: [[0, "asc"]],
            columns: [
                {
                    title: $("#id-column-title").html(),
                    data: "lot_id",
                    responsivePriority: 1
                },
                {
                    title: $("#description-column-title").html(),
                    data: "lot_Description",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 2
                },
                {
                    title: $("#state-column-title").html(),
                    data: "state",
                    render: function (data, type, full, meta) {
                        var priority = data;
                        switch (priority) {
                            case "Activado":
                                priority = $("#active-field").html();
                                break;
                            case "Desactivado":
                                priority = $("#inactive-field").html();
                                break;
                            default:
                                priority = "";
                        }
                        return priority;
                    },
                    responsivePriority: 3
                },
                //{
                //    title: $("#state-column-title").html(),
                //    data: "state",
                //    responsivePriority: 6
                //},
                //{
                //    title: $("#createDate-column-title").html(),
                //    data: { _: "create_Date", sort: "create_Date" },
                //    responsivePriority: 4
                //},
                //{
                //    title: $("#upDate-column-title").html(),
                //    data: { _: "update_Date", sort: "update_Date" },
                //    responsivePriority: 5
                //},
                {
                    title: $("#lotArticles-column-title").html(),
                    data: "lot_article",
                    responsivePriority: 6
                },
                {
                    title: $("#actions-column-title").html(),
                    visible: lot_actions_btn_group.children().first().children().length > 0,
                    orderable: false,
                    responsivePriority: 8,
                    render: function (data, type, full, meta) {

                        var btn_group = lot_actions_btn_group.clone();

                        if (full.state === "Activado")
                            btn_group.find("#toggleAble").remove();

                        if (full.state === "Desactivado") {
                            btn_group.find("#toggleDisable").remove();
                            //btn_group.find("#btnLotClosure").hide();
                        }

                        //if (full.state === "CERRADO") {
                        //    btn_group.find("#edit").hide();
                        //    btn_group.find("#enableDisable").hide();
                        //    btn_group.find("#toggleAble").remove();
                        //    btn_group.find("#btnLotClosure").hide();
                        //}

                        console.log()
                        return btn_group.html().replace(/_lotID_/g, full.lot_id);
                    }
                }
            ]
        });

        /////////////

        // #region Toggle modal
        //$(document).delegate('#btnLotClosure', 'click', function (e) {
        //    var lotId = $(this).attr('item-id');

        //    Swal.fire({
        //        title: $("#are-you-sure").text(),
        //        text: $("#this-operation-can-not-be-revert").text(),
        //        icon: 'warning',
        //        showCancelButton: true,
        //        confirmButtonColor: '#28a745',
        //        cancelButtonColor: '#dc3545',
        //        confirmButtonText: $("#yes-text").text(),
        //        cancelButtonText: $("#no-text").text()
        //    }).then(function (result) {
        //        if (result.value) {
        //            var data = {
        //                lotId: lotId
        //            };
        //            $.ajax({
        //                url: '/Lot/LotClosure',
        //                data: data,
        //                success: function (data) {
        //                    if (data === true) {
        //                        Swal.fire({
        //                            title: $("#success-text").text(),
        //                            text: $("#approved").text(),
        //                            type: 'success',
        //                            icon: "success",
        //                            confirmButtonText: $("#btnsuccess-text").text(),
        //                            timer: 1500
        //                        }).then(function (result) {
        //                            window.location.reload();
        //                        });
        //                    }
        //                    else if (data === false) {

        //                        Swal.fire(
        //                            $("#error-text").text(),
        //                            $("#couldnt-rejected").text(),
        //                            "error"
        //                        )
        //                    }
        //                    else {
        //                        window.location.reload();
        //                    }
        //                },
        //                error: function () {
        //                    Swal.fire(
        //                        $("#error-text").text(),
        //                        $("#couldnt-rejected").text(),
        //                        "error"
        //                    )
        //                }
        //            });
        //        }
        //        swal.close();
        //    }
        //    );
        //});

        // #endregion

        ////////////////
    };

}(window.Index = window.Index || {}, jQuery));

jQuery(function () {
    Index.Start();
});
