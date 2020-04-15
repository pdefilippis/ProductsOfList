(function (self, $, undefined) {

    EditArticle.Start = function () {

        Select2MultiConfigArticle($('#articles-multiselect'));
    }
}(window.EditArticle = window.EditArticle || {}, jQuery));

jQuery(function () {
    EditArticle.Start();
})