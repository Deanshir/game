//כאשר העמוד נטען
$(document).ready(function () {
    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציית ספירת תווים בעמוד כניסה לעורך
    });

    $(".CharacterCount").keyup(function () {
        checkCharsQ($(this)); //קריאה לפונקציית ספירת תווים בעמוד המשחקים שלי
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציית ספירת תווים בעמוד כניסה לעורך
    });

    $("#about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

//--------------------------------------------------------------------------------------------> דף כניסה לעורך
    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים בעמוד כניסה לעורך
    function checkCharacter(myTextBox) {        
        var LableToShow = myTextBox.attr("CharacterForBTN");
        var countCurrentC = myTextBox.val().length;

        //אם תיבת הטקסט ריקה
        if (countCurrentC ==0) {
            document.getElementById(LableToShow).style.opacity = "0.5";
            $("#alert").text("");
        }
       
        //אם הוקלדו תווים לתיבת הטקסט
        if (countCurrentC > 0) {   
            $("#" + LableToShow).prop("disabled", false);
            document.getElementById(LableToShow).style.opacity = "1";
        }
    }

});





