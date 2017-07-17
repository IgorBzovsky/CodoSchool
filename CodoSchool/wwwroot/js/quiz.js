var QuizController = function () {
    var questionCount;
    var initialize = function (count) {
        questionCount = count;
        $("#add-question").on("click", function () {
            addQuestion();
        });

        $("#questions-container").on('click', '.js-delete-question', function () {
            var button = $(this);
            var questionIndex = button.attr("data-question-id");
            var questionToDelete = "#question" + questionIndex;
            $(questionToDelete).remove();
            questionCount--;
            refreshQuestions();
        });

        $("#questions-container").on('click', '.js-add-answer', function () {
            var button = $(this);
            var questionIndex = button.attr("data-question-id");
            var answersDivId = "#answers-wrapper" + questionIndex;
            var answerIndex = $(answersDivId).children(".answer-container").length;
            addAnswer(questionIndex, answerIndex);
        });

        $("#questions-container").on('click', '.js-delete-answer', function () {
            var button = $(this);
            var answerIndex = button.attr("data-answer-id");
            var questionIndex = button.attr("data-question-id");
            var answerId = "#question-" + questionIndex + "-answer-" + answerIndex;
            $(answerId).remove();
            refreshAnswers(questionIndex);
        });
    };

    var addQuestion = function () {
        var questionName = "SectionDto.Questions[" + questionCount + "].QuestionText";
        var questionId = "SectionDto_Questions_" + questionCount + "__QuestionText";
        var questionDivId = "question" + questionCount;
        var answersDivId = "answers-wrapper" + questionCount;
        var correctAnswerId = "SectionDto.Questions[" + questionCount + "].CorrectAnswerId";

        var question = $("<div id='" + questionDivId + "' class='form-group question-container'><button class='btn btn-danger question-button js-delete-question' type='button' data-question-id='" + questionCount + "' data-db-question-id='0'>Delete Question</button><div class='form-group'><label class='question-label'>Question</label><textarea name='" + questionName + "' class='form-control question-textarea' id='" + questionId + "' rows='3'></textarea><span class='text-danger field-validation-valid question-validation' data-valmsg-for='" + questionName + "' data-valmsg-replace='true'></span></div><div id='" + answersDivId + "' class='answers-container'></div></div>");

        $("#questions-container").append(question);
        addAnswers(questionDivId, correctAnswerId);
    };

    var addAnswers = function (questionDivId, correctAnswerId) {
        for (var i = 0; i < 4; i++) {
            addAnswer(questionCount, i);
        }
        $("#answers-wrapper" + questionCount).parent().append("<button class='btn btn-primary js-add-answer' type='button' data-question-id = '" + questionCount + "'>Add Answer</button>");
        $("#" + questionDivId).append("<span class='text-danger field-validation-valid correct-answer-validation' data-valmsg-for='" + correctAnswerId + "' data-valmsg-replace='true'></span>");
        questionCount++;
    };

    //Add answer
    var addAnswer = function (questionIndex, answerIndex) {
        var answerName = "SectionDto.Questions[" + questionIndex + "].Answers[" + answerIndex + "].AnswerText";

        var answerId = "SectionDto_Questions_" + questionIndex + "__Answers_" + answerIndex + "__AnswerText";

        var correctAnswerName = "SectionDto.Questions[" + questionIndex + "].CorrectAnswerId";
        var correctAnswerId = "SectionDto.Questions_" + questionIndex + "__CorrectAnswerId";

        var answer = $("<div id='question-" + questionIndex + "-answer-" + answerIndex + "' class='radio answer-container form-group'><label><input name='" + correctAnswerName + "' id='" + correctAnswerId + "' value='" + answerIndex + "' type='radio' class='answer-radio'>Answer</label><textarea name='" + answerName + "' class='form-control answer-textarea' id='" + answerId + "'></textarea><span class='text-danger field-validation-valid answer-validation' data-valmsg-for='" + answerName + "' data-valmsg-replace='true'></span><button class='btn btn-link js-delete-answer' type='button' data-answer-id = '" + answerIndex + "' data-question-id = '" + questionIndex + "' data-db-answer-id='0'>Delete Answer</button></div>");

        var answersDivId = "#answers-wrapper" + questionIndex;
        $(answersDivId).append(answer);
    };

    var refreshAnswers = function (questionIndex) {
        var question = $("#question" + questionIndex);
        $(question).find(".answer-container").each(function (j) {
            $(this).attr('id', 'question-' + questionIndex + '-answer-' + j);
            $(this).find(".answer-label").text('Question ' + j);
            $(this).find('.answer-radio').attr({ name: 'SectionDto.Questions[' + questionIndex + '].CorrectAnswerId', id: 'SectionDto.Questions_' + questionIndex + '__CorrectAnswerId', value: j });
            $(this).find('.answer-textarea').attr({ name: 'SectionDto.Questions[' + questionIndex + '].Answers[' + j + '].AnswerText', id: 'SectionDto.Questions_' + questionIndex + '__Answers_' + j + '__AnswerText' });
            $(this).find('.answer-validation').attr('data-valmsg-for', 'SectionDto.Questions[' + questionIndex + '].Answers[' + j + '].AnswerText');
            $(this).find('.js-delete-answer').attr({ 'data-question-id': questionIndex, 'data-answer-id': j });
        });
    };

    var refreshQuestions = function () {
        $("#questions-container").find(".question-container").each(function (i) {
            $(this).attr('id', 'question' + i);
            $(this).find(".js-delete-question").attr('data-question-id', i);
            $(this).find(".question-label").text('Question ' + i);
            $(this).find(".js-add-answer").attr('data-question-id', i);
            $(this).find(".question-textarea").attr({ name: 'SectionDto.Questions[' + i + '].QuestionText', id: 'SectionDto_Questions_' + i + '__QuestionText' });
            $(this).find(".question-validation").attr('data-valmsg-for', 'SectionDto.Questions[' + i + '].QuestionText');
            $(this).find(".answers-container").attr('id', 'answers-wrapper' + i);
            refreshAnswers(i);
        });
    };

    return {
        initialize: initialize
    };
}();
