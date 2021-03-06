﻿// Menu Controller (for main page sections tree menu)
var MenuController = function () {

    var menuPlaceholderId;
    var contentPlaceholderId;
    
    var initialize = function (menuId, contentId) {
        menuPlaceholderId = menuId;
        contentPlaceholderId = contentId;
        initializeSectionsMenu();
        initializeBackButton();
    };
    //Initializes tree menu and menu item's selection behaviour
    var initializeSectionsMenu = function () {
        getTreeMenu();
        initializeSelection();
    };

    var initializeBackButton = function () {
        $(".back-button").click(function () {
            var button = $(this);
            $.ajax({
                url: '/api/Menu/',
                dataType: 'json',
                type: 'GET',
                success: function (data) {
                    $(menuPlaceholderId).tree('loadData', data);
                    loadWelcomePage();
                    button.hide();
                }
            });
        });
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
    };
    
    //Manage selection behaviour for menu items
    var initializeSelection = function () {
        $(menuPlaceholderId).bind('tree.click', function (event) {
            switch (event.node.sectionType.name) {
                case "Course":
                    goToCourse(event.node);
                    break;
                case "TextLesson":
                case "VideoLesson":
                    loadContent(event.node);
                    break;
            }
        });
    };

    //Change categories menu for course menu
    var goToCourse = function (node) {
        $.ajax({
            url: '/api/Menu/' + node.id,
            dataType: 'json',
            type: 'GET',
            success: function (data) {
                $(menuPlaceholderId).tree('loadData', data);
                loadContent(node);
                $(".back-button").css({ display: "block" });
            }
        });
    };

    var loadContent = function (node) {
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
    };

    var loadWelcomePage = function () {
        $.ajax({
            url: '/Home/Welcome/',
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
    };
    return {
        initialize: initialize
    };
}();



