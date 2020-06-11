(function (self, $, undefined) {

    Salir.Start = function () {
        // #region Toggle modal
        $(document).delegate('#btnLogOut', 'click', function (e) {
            var logOut = $(this).attr('item-id');

            Swal.fire({
                title: $("#are-you-sure2").text(),
                text: $("#this-operation-can-not-be-revert2").text(),
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#28a745',
                cancelButtonColor: '#dc3545',
                confirmButtonText: $("#yes-text2").text(),
                cancelButtonText: $("#no-text2").text()
            }).then(function (result) {
                if (result.value) {
                    var data = {
                        logOut: logOut
                    };
                    $.ajax({
                        url: '/Login/LogOut',
                        data: data,
                        success: function (data) {
                            if (data === true) {
                                Swal.fire({
                                    title: $("#success-text2").text(),
                                    text: $("#approved2").text(),
                                    type: 'success',
                                    icon: "success",
                                    confirmButtonText: $("#btnsuccess-text").text(),
                                    timer: 1500
                                }).then(function (result) {
                                    window.location.reload();
                                });
                            }
                            else if (data === false) {

                                Swal.fire(
                                    $("#error-text2").text(),
                                    $("#couldnt-rejected2").text(),
                                    "error"
                                )
                            }
                            else {
                                window.location.reload();
                            }
                        },
                        error: function () {
                            Swal.fire(
                                $("#error-text2").text(),
                                $("#couldnt-rejected2").text(),
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

        ////////////////
    };

}(window.Salir = window.Salir || {}, jQuery));

jQuery(function () {
    Salir.Start();
});