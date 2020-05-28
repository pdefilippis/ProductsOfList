(function (self, $, undefined) {
    var article_actions_btn_group = $("#article-actions-btn-group");

    Index.Start = function () {
        var table = $('#articles-table').DataTable({
            language: datatablesLangES,
            ajax: {
                url: "/Article/GetArticlesPublic",
                data: { lotId: $('#LotId').val() },
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
                    responsivePriority: 1
                },
                {
                    title: $("#articleDescription-column-title").html(),
                    data: "article_Description",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 2
                },
                {
                    title: $("#type-column-title").html(),
                    data: "type",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 4
                },
                {
                    title: $("#brand-column-title").html(),
                    data: "brand",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 5
                },
                {
                    title: $("#serialNumber-column-title").html(),
                    data: "serialNumber",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 7
                },
                {
                    title: $("#price-column-title").html(),
                    data: "price",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 6
                },
                {
                    title: $("#userCount-column-title").html(),
                    data: "userCount",
                    render: $.fn.dataTable.render.text(),
                    responsivePriority: 8
                },
                {
                    title: $("#actions-column-title").html(),
                    visible: article_actions_btn_group.children().first().children().length > 0,
                    orderable: false,
                    responsivePriority: 9,
                    render: function (data, type, full, meta) {

                        var btn_group = article_actions_btn_group.clone();
                        var TakenId = full.tokenId;

                        (full.article_id == TakenId) ? btn_group.find("#btnApprove").remove() : btn_group.find("#btnReject").remove();

                        if (TakenId != 0) {
                            btn_group.find("#btnApprove").parent().remove();
                        }

                        return btn_group.html().replace(/_articleID_/g, full.article_id).replace(/_lotID_/g, $('#LotId').val());
                    }
                }
            ]
        });

        // #region Toggle modal
        $(document).delegate('#btnApprove', 'click', function (e) {
            //var table = $('articles-table');
            var articleId = $(this).attr('item-id');

            Swal.fire({
                title: $("#are-you-sure3").text(),
                text: $("#this-operation-can-not-be-revert3").text(),
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#28a745',
                cancelButtonColor: '#dc3545',
                confirmButtonText: $("#yes-text").text(),
                cancelButtonText: $("#no-text").text()
            }).then(function (result) {
                if (result.value) {
                    var data = {
                        articleId: articleId,
                        idLote: $('#LotId').val()
                    };
                    $.ajax({
                        url: '/Article/ApplyUnApply',
                        data: data,
                        type: "POST",
                        success: function (data) {
                            if (data === true) {
                                Swal.fire({
                                    title: $("#success-text3").text(),
                                    text: $("#approved3").text(),
                                    icon: "success",
                                    confirmButtonText: $("#btnsuccess-text3").text(),
                                    timer: 1500
                                }).then(function (result) {
                                    window.location.reload();
                                });
                            }
                            else if (data === false) {

                                Swal.fire(
                                    $("#error-text3").text(),
                                    $("#couldnt-approve3").text(),
                                    "error"
                                )
                            }
                            else {
                                window.location.reload();
                            }
                        },
                        error: function () {
                            Swal.fire(
                                $("#error-text3").text(),
                                $("#couldnt-approve3").text(),
                                "error"
                            )
                        }
                    });
                }
                swal.close();
            }
            );
        });

        // #endregion


        // #region Toggle modal
        $(document).delegate('#btnReject', 'click', function (e) {

            var articleId = $(this).attr('item-id');

            Swal.fire({
                title: $("#are-you-sure4").text(),
                text: $("#this-operation-can-not-be-revert4").text(),
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#28a745',
                cancelButtonColor: '#dc3545',
                confirmButtonText: $("#yes-text").text(),
                cancelButtonText: $("#no-text").text()
            }).then(function (result) {
                if (result.value) {
                    var data = {
                        articleId: articleId,
                        idLote: $('#LotId').val()
                    };
                    $.ajax({
                        url: '/Article/ApplyUnApply',
                        type: "POST",
                        data: data,
                        success: function (data) {
                            if (data === true) {
                                Swal.fire({
                                    title: $("#success-text4").text(),
                                    text: $("#approved4").text(),
                                    icon: "success",
                                    confirmButtonText: $("#btnsuccess-text4").text(),
                                    timer: 1500
                                }).then(function (result) {
                                    window.location.reload();
                                });
                            }
                            else if (data === false) {

                                Swal.fire(
                                    $("#error-text4").text(),
                                    $("#couldnt-rejected4").text(),
                                    "error"
                                )
                            }
                            else {
                                window.location.reload();
                            }
                        },
                        error: function () {
                            Swal.fire(
                                $("#error-text4").text(),
                                $("#couldnt-rejected4").text(),
                                "error"
                            )
                        }
                    });
                }
                swal.close();
            }
            );
        });
        // #endregion

    }
}(window.Index = window.Index || {}, jQuery));

jQuery(function () {
    Index.Start();
})
