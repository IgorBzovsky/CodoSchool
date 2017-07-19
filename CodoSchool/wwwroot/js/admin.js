var AdminController = new function () {
    var createMenu = function (menuId) {
        $(menuId).treegrid();
        $(menuId).on("click", ".js-delete", function () {
            var button = $(this);
            button.attr('disabled', 'disabled');
            $.ajax({
                url: "/../api/Menu/" + button.attr("data-section-id"),
                type: "DELETE",
                success: function () {
                    $('.treegrid-' + button.attr("data-section-id")).treegrid('remove');
                },
                error: function () {
                    alert("Error occured while attempting to delete section.");
                    button.attr('disabled', 'enabled');
                }
            });
        });
    };
    return {
        createMenu: createMenu
    };
}();