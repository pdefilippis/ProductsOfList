(function (self, $, undefined) {

    Create.Start = function () {

        // #region File1
        $("#Image").on("change", function () {
            var file = this.files[0];
            var length = file.size;
            var type = file.type;
            var validImageTypes = ['image/gif', 'image/jfif', 'image/jpg', 'image/jpeg', 'image/png'];
            if (file !== null) {
                if (length > 52428800 || validImageTypes.indexOf(type) === -1) {
                    $('#Image').val("");
                    $('#report-name').val("");
                    $("#image-error").show();
                } else {
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
        });
        // #endregion
    };

}(window.Create = window.Create || {}, jQuery));

jQuery(function () {
    Create.Start();
});