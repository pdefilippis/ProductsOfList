(function (self, $, undefined) {

    Edit.Start = function () {

        // #region File1
        $("#Image").on("change", function () {
            var file = this.files[0];
            var length = file.size;
            var type = file.type;
            var validImageTypes = ['image/gif', 'image/jfif' ,'image/jpg', 'image/jpeg', 'image/png'];
            if (file !== null) {
                if (length > 52428800 || validImageTypes.indexOf(type) === -1) {
                    $('#Image').val("");
                    $('#report-name').val("");
                    $("#image-error").show();
                } else {
                    $("#FlagImage").val(false);
                    $('#report-name').val(file.name);
                    $("#image-error").hide();
                }
            }
        });

        $("#report-set").on("click", function () {
            $('#Image').trigger("click");
        });

        $("#report-clear").on("click", function () {
            $('#Image').val("");
            $('#report-name').val("");
            $("#FlagImage").val(false);
        });
        // #endregion
    };

}(window.Edit = window.Edit || {}, jQuery));

jQuery(function () {
    Edit.Start();
});