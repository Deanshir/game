$(document).ready(function () {
    $("#howToPlay").click(function () {
        $("#howToPlayDiv").toggle();
    });

    $(".closeHowToPlay").click(function () {
        $("#howToPlayDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

    $("#about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

});