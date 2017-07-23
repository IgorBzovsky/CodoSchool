// Menu Controller (for main page sections tree menu)
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
                    goToLesson(event.node);
                    break;
                case "VideoLesson":
                    loadContent(event.node);
                    break;
                case "Quiz":
                    goToQuiz(event.node);
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
    }

    var goToQuiz = function (node) {

        $.ajax({
            url: '/Home/Quiz/' + node.id,
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
//    return {
//        initialize: initialize
//    };
//}();
var QuizController = function () {
    function formSubmitHandler() {
        
        $("#quizForm").on("submit", function (e) {
            var regExp = /^question=([-]*\d+)&answer=(\d).*$/;
            var form = $("#quizForm").serialize();
            //console.log(form);
            var resArr = regExp.exec(form);
            var currentQuestion = resArr[1];
            var selectedIndex = resArr[2];
            var param = "?question=" + currentQuestion + "&answer= " + selectedIndex;

            e.preventDefault();
            console.log($(this).attr("action") + param);
                $.ajax({
                    url: $(this).attr("action") + param,
                    data: $(this).serialize(),
                    type: $(this).attr("method")
                })
                    .done(function (result) {
                        
                        $("#content-block").html(result);
                    })
            });
        
    };

    return {
        submitHandler: formSubmitHandler
    }
}();



