//כאשר העמוד נטען
$(document).ready(function () {
    //בהקלדה בתיבת הטקסט
    $("#GameSubjectName").keyup(function () {
        checkCharsQ($(this)); //קריאה לפונקציית ספירת תווים בעמוד המשחקים שלי
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $("#GameSubjectName").on("paste", function () {
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
       
        var countCurrentC = myTextBox.val().length;//אורך הטקסט שהוקלד

        //לא עומד בתנאים
        if ((countCurrentC < 2) || (countCurrentC > 20)) {
            document.getElementById("saveChanges").style.opacity = "0.5";
            document.getElementById("CharsNumError").style.color = "red";
            $("#saveChanges").prop("disabled", true);

        }
        //עומד בתנאים
        else {
            $("#saveChanges").prop("disabled", false);
            document.getElementById("saveChanges").style.opacity = "1";
            document.getElementById("CharsNumError").style.color = "green";
        }

        if (countCurrentC == 0) {
            document.getElementById("CharsNumError").style.color = "black";
            $("#saveChanges").prop("disabled", true);
            document.getElementById("saveChanges").style.opacity = "1";
        }

    }

});
