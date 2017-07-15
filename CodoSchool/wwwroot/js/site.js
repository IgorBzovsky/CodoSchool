// Menu Controller (for main page sections tree menu)
var MenuController = function () {

    var menuPlaceholderId;
    var contentPlaceholderId;
    
    var initialize = function (menuId, contentId) {
        menuPlaceholderId = menuId;
        contentPlaceholderId = contentId;
        initializeSectionsMenu();
    };

    //Initializes tree menu and menu item's selection behaviour
    var initializeSectionsMenu = function () {
        getTreeMenu();
        initializeSelection();
    };

    //Getting data for menu items from api controller
    var getTreeMenu = function () {
        $.ajax({
            url: '/api/Menu/',
            dataType: 'json',
            async: true,
            type: 'GET',
            success: function (data) {
                $(menuPlaceholderId).tree({
                    data: data,
                    closedIcon: $('<i class="glyphicon glyphicon-menu-right"></i>'),
                    openedIcon: $('<i class="glyphicon glyphicon-menu-down"></i>'),
                    selectable: false
                });
            }
        });
    }
    
    //Manage selection behaviour for menu items
    var initializeSelection = function () {
        $(menuPlaceholderId).bind('tree.click', function (event) {
            switch (event.node.sectionType.name) {
                case "Course":
                    goToCourse(event.node);
                    break;
                case "TextLesson":
                case "VideoLesson":
                    goToLesson(event.node);
                    break;
            }
        });
    }

    //Change categories menu for course menu
    var goToCourse = function(node) {
        $.ajax({
            url: '/api/Menu/' + node.id,
            dataType: 'json',
            type: 'GET',
            success: function (data) {
                $(menuPlaceholderId).tree('loadData', data);
            }
        });
    }

    //Loading content of the lesson to the content placeholder
    var goToLesson = function(node) {
        $.ajax({
            url: '/Home/' + node.sectionType.name + '/' + node.id,
            dataType: "html",
            type: 'GET',
            success: function (data) {
                $(contentPlaceholderId).html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }

    return {
        initialize: initialize
    }
}();



