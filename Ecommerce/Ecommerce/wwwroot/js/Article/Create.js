(function (self, $, undefined) {

    createArticle.Start = function () {

        Select2MultiConfigArticle($('#articles-multiselect'));
    }
}(window.createArticle = window.createArticle || {}, jQuery));

jQuery(function () {
    createArticle.Start();
})