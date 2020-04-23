var datatablesLangES = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    },
    select: {
        rows: {
            _: "Seleccionados %d registros",
            0: "Click en última celda para seleccionar registro",
            1: "Seleccionado 1 solo registro"
        }
    }
}

DataTablesLang = function () {
    switch ($("#current-culture").val()) {
        case "es-ES":
            return datatablesLangES;
        default:
            return null;
    }
}

Select2SingleConfig = function () {
    return {
        width: "100%",
        theme: "bootstrap",
        language: Select2Lang()
    }
}

Select2MultiConfig = function (selector) {
    selector.select2({
        width: "100%",
        theme: "bootstrap",
        allowClear: true,
        placeholder: "",
        language: Select2Lang()
    }).on("select2:unselecting", function (e) {
        $(this).data('unselecting', true);
    }).on('select2:open', function (e) {
        if ($(this).data('unselecting')) {
            $(this).select2('close').removeData('unselecting');
        }
    });
}

Select2MultiConfigArticle = function (selector) {
    selector.select2({
        width: "100%",
        theme: "bootstrap",
        allowClear: false,
        placeholder: "",
        language: Select2Lang()
    }).on("select2:unselecting", function (e) {
        $(this).data('unselecting', true);
    }).on('select2:open', function (e) {
        if ($(this).data('unselecting')) {
            $(this).select2('close').removeData('unselecting');
        }
    });
}

Select2Lang = function () {
    return $("#current-culture-short").val();
}

DateTimePickerLang = function () {
    return $("#current-culture-short").val();
}

LinkedDateTimePickers = function (from, to) {
    from.datetimepicker({
        useCurrent: false,
        locale: "ES",
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar"
        }
    });
    to.datetimepicker({
        useCurrent: false,
        locale: "ES",
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar"
        }
    });
    from.on("dp.change", function (e) {
        to.data("DateTimePicker").minDate(e.date);
    });
    to.on("dp.change", function (e) {
        from.data("DateTimePicker").maxDate(e.date);
    });
}

AjaxErrorHandler = function (jqXHR, textStatus, errorThrown, callback) {
    var statusCode = jqXHR.status;

    switch (statusCode) {
        case 400:
            window.location.href = "/error/status/400";
            break;
        case 401:
            window.location.href = "/account/login";
            break;
        case 403:
            window.location.href = "/account/login";
            break;
        case 500:
            window.location.href = "/error";
            break;
        default:
            alert(statusCode);
    }

    callback();
}



SetStatusIconUpdating = function (statusIcon) {
    statusIcon.html($("#updating-icon").html());
    statusIcon.attr("title", $("#title-updating").html());
}

SetStatusIconDone = function (statusIcon) {
    statusIcon.html($("#done-icon").html());
    statusIcon.attr("title", $("#title-done").html());
}

SetStatusIconFail = function (statusIcon) {
    statusIcon.html($("#fail-icon").html());
    statusIcon.attr("title", $("#title-fail").html());
}

CheckSetButton = function (selection, new_selection, button) {
    if (selection.sort().join(',') === new_selection.sort().join(',')) {
        button.prop("disabled", true);
    } else {
        button.prop("disabled", false);
    }
}

SetButtonSelectionControl = function (table, selection, button) {
    table.off('user-select');
    table.on('select deselect', function (e, dt, type, cell, originalEvent) {
        CheckSetButton(selection, table.rows({ selected: true }).ids().toArray(), button);
    });
}

PreventTableSelection = function (table) {
    table.on('user-select', function (e, dt, type, cell, originalEvent) {
        e.preventDefault();
    });
}

BindTableSelectAll = function (button, table) {
    button.click(function () {
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            this.select();
        });
    });
}

BindTableSelectNone = function (button, table) {
    button.click(function () {
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            this.deselect();
        });
    });
}

BindTableSelectUndo = function (button, table, selection) {
    button.click(function () {
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            if ($.inArray(this.id(), selection) !== -1) {
                this.select();
            } else {
                this.deselect();
            }
        });
    });
}


$(document).ready(function () {
    var go_to_top_btn = $('#go-to-top-btn');

    $(window).scroll(function () {
        if ($(window).scrollTop() > 0) {
            go_to_top_btn.show();
        } else {
            go_to_top_btn.hide();
        }
    });

    go_to_top_btn.on('click', function () {
        $('html, body').animate({ scrollTop: 0 }, '300');
    });
});
