function AlertShow(x) {
    x.style.classList.add("showElemant");
}

function AlertHide(x) {
    x.style.classList.add("hideElemant");
}


//כאשר העמוד נטען
$(document).ready(function () {
    //בהקלדה בתיבת הטקסט
    $(".textBoxGAME").keyup(function () {
        checkCharsQ($(this)); //קריאה לפונקציית ספירת תווים בעמוד המשחקים שלי
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".textBoxGAME").on("paste", function () {
        checkCharsQ($(addNameTB));//קריאה לפונקציית ספירת תווים בעמוד כניסה לעורך
    });

    $("#about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

  
//--------------------------------------------------------------------------------------------> בדיקת שם המשחק
    function checkCharsQ(myTextBox) {
        var btnToShow = myTextBox.attr("CharacterForBTN");//הכפתור שיושפע
        var lableToShow = myTextBox.attr("CharacterForLbl");//הלייבל שיושפע
        var countCurrentC = myTextBox.val().length;//אורך הטקסט שהוקלד

        //לא עומד בתנאים
        if ((countCurrentC < 2) || (countCurrentC > 20)) {
            document.getElementById(btnToShow).style.opacity = "0.5";
            document.getElementById(lableToShow).style.color = "red";
        }
        //עומד בתנאים
        else {
            $("#createGame").prop("disabled", false);
            document.getElementById(btnToShow).style.opacity = "1";
            document.getElementById(lableToShow).style.color = "green";          
        }

        if (countCurrentC == 0) {
            document.getElementById(lableToShow).style.color = "black";
        }

    }

 




});
