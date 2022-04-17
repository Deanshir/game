//הגדלת תמונה
function imgBigQ(imgNum) {
    $('#ImageforUpload' + imgNum).css("width", "160");
    $('#ImageforUpload' + imgNum).css("height", "110");
    $('#ImageforUpload' + imgNum).css("z-index", "5");


}

function imgBigA(imgNum) {
    $('#ImageforUpload' + imgNum).css("width", "90");
    $('#ImageforUpload' + imgNum).css("height", "60");
    $('#ImageforUpload' + imgNum).css("z-index", "5");
}


//תמונה גזע השאלה
function openFileUploader0() {
    $('#FileUpload0').click();
   
}

//תמונות מסיחים
function openFileUploader1() {
    $('#FileUpload1').click();


}

function openFileUploader2() {
    $('#FileUpload2').click();

}

function openFileUploader3() {
    $('#FileUpload3').click();
 
}

function openFileUploader4() {
    $('#FileUpload4').click();
    
}

function openFileUploader5() {
    $('#FileUpload5').click();
    
}



function countRowsGridview() {
    var countRows = $("#GridView2 tr").length;

    if (countRows == 10 || countRows == 13 || countRows == 16) {
        document.getElementById("tableStatus").innerText = "ניתן לפרסם את המשחק";
        document.getElementById("tableStatus").classList.add("publish");
    }
    else {
        document.getElementById("tableStatus").innerText = "בכדי לפרסם משחק יש ליצור 9/12/15 שאלות בדיוק";
        document.getElementById("tableStatus").classList.add("tableStatus");
    }

    if ((countRows == 16) && ($("#savingQuestion").prop("disabled") == true)) {
        $("#question").prop("disabled", true);
        $("#correct").prop("disabled", true);
        $("#incorrect1").prop("disabled", true);
        $("#ImageforUpload0").prop("disabled", true);
        $("#ImageforUpload1").prop("disabled", true);
        $("#ImageforUpload2").prop("disabled", true);
        $("#addingAnswer").prop("disabled", true);
        $("#restart").prop("disabled", true);
        $("#savingQuestion").prop("disabled", true);

        document.getElementById("ImageforUpload0").classList.add("disabledBTN");
        document.getElementById("ImageforUpload1").classList.add("disabledBTN");
        document.getElementById("ImageforUpload2").classList.add("disabledBTN");
        document.getElementById("restart").classList.add("disabledBTN");
        document.getElementById("question").style.add("opacity", "0.5");
        document.getElementById("correct").style.add("opacity", "0.5");
        document.getElementById("incorrect1").style.add("opacity", "0.5");
        document.getElementById("addingAnswer").style.add("opacity", "0.5");
        document.getElementById("savingQuestion").style.add("opacity", "0.5");
    }
}

//ברגע שהעמוד נטען
$(document).ready(function () {
    check_chars($()); //קריאה לפונקציה שבודקת תווים עבור כפתורים

    $("#SaveChanges").click(function () {
        check_chars($()); //קריאה לפונקציה שבודקת תווים עבור כפתורים
    });
    //בהקלדה בתיבת הטקסט
    $(".counterChars").keyup(function () {
        check_chars($()); //קריאה לפונקציה שבודקת תווים עבור כפתורים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".counterChars").on("paste", function () {
        check_chars($(this));//קריאה לפונקציה שבודקת את מספר התווים

    });

//-----------------------------------> תפריט נייוט-אודות
    $("#about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

//-------------------------------------->ספירת מספר השאלות במשחק
    countRowsGridview($());

//---------------------------------------> פונקציה לבדיקת תנאים בתיבות טקסט בעבור כפתורים
    function check_chars() {

        //------------------------------בדיקת צבעים

        //משתנים ששומרים את שמות התמונות העדכניות 
        var imgkeeper0 = $('#imgKeeper0').attr("value");
        var imgkeeper1 = $('#imgKeeper1').attr("value");
        var imgkeeper2 = $('#imgKeeper2').attr("value");

        //בדיקה וצביעה גזע שאלה
        var countCurrent_Q = $("#question").val().length;//אורך הטקסט שהוקלד
        var lableToShow_Q = $("#question").attr("CharacterForLblQ"); //הלייבל שיושפע

        if (imgkeeper0 != "~/image/pic_upload.png") {
            var maxChars_Q = 45;
            $("#question").attr("MaxLength", 45);
        }
        else {
            var maxChars_Q = 100;
            $("#question").attr("MaxLength", 100);
        }
        if (countCurrent_Q == 1 || countCurrent_Q > maxChars_Q) {
            document.getElementById(lableToShow_Q).style.color = "red";
        }
        else {
            document.getElementById(lableToShow_Q).style.color = "green";
        }
        if (countCurrent_Q == 0) {
            document.getElementById(lableToShow_Q).style.color = "black";
        }

        //בדיקה וצביעה תיבה תשובה נכונה
        var countCurrent_C = $("#correct").val().length;//אורך הטקסט שהוקלד
        var lableToShow_C = $("#correct").attr("CharacterForLblC"); //הלייבל שיושפע
        if (countCurrent_C == 0) {
            document.getElementById(lableToShow_C).style.color = "black";
            document.getElementById("ImageforUpload1").style.opacity = "1";
            $("#ImageforUpload1").prop("disabled", false);
        }
        else {
            document.getElementById(lableToShow_C).style.color = "green";
            document.getElementById("ImageforUpload1").style.opacity = "0.5"; //הסתרת כפתור שמירת שאלה
            $("#ImageforUpload1").prop("disabled", true);//כיבוי כפתור הוספת מסיח

        }

        //בדיקת צבעים מסיחים
        var PanelToOpen = parseInt($("#AddingAnswerCounter").val());

        for (var x = 1; x <= PanelToOpen; x++) {
            var countCurrent_Ans = $("#incorrect" + x).val().length;//אורך הטקסט שהוקלד
            var lableToShow_Ans = $("#incorrect" + x).attr("CharacterForLblW"); //הלייבל שיושפע
            if (countCurrent_Ans == 0) {
                document.getElementById(lableToShow_Ans).style.color = "black";
                document.getElementById("ImageforUpload" + (x+1)).style.opacity = "1";
                $("#ImageforUpload" + (x+1)).prop("disabled", false);
            }
            else {
                document.getElementById(lableToShow_Ans).style.color = "green";
                document.getElementById("ImageforUpload" + (x + 1)).style.opacity = "0.5"; //הסתרת כפתור שמירת שאלה
                $("#ImageforUpload" + (x+1)).prop("disabled", true);//כיבוי כפתור הוספת מסיח
            }

        }
        $('#AddingAnswerCounter').attr('value', PanelToOpen.toString());


        //                   פתיחת/סגירת כפתורים
//----------------------------------------------------------------------------------------->
        var countCurrent_W = $("#incorrect1").val().length;//אורך הטקסט שהוקלד

        var editstatus = $("#EditStatus").val();//בדיקת סטטוס-יצירת שאלה/עריכת שאלה

        if (editstatus == "false") //מצב יצירת שאלה
        {
            if (((countCurrent_Q >= 2) && (countCurrent_Q <= maxChars_Q))
                &&((countCurrent_C != 0) || (imgkeeper1 != "~/image/pic_upload.png"))
                &&((countCurrent_W !=0) || (imgkeeper2 != "~/image/pic_upload.png")))
            {
                document.getElementById("addingAnswer").style.opacity = "1"; //חשיפת כפתור הוספת מסיח
                document.getElementById("savingQuestion").style.opacity = "1"; //חשיפת כפתור שמירת שאלה

                $("#addingAnswer").prop("disabled", false);//הפעלת כפתור הוספת מסיח
                $("#savingQuestion").prop("disabled", false);//הפעלת כפתור שמירת שאלה
                document.getElementById("savingQuestion_cant").classList.add("tooltiptext_buttons_noActive");
                document.getElementById("addingAnswer_noMore").classList.add("tooltiptext_addbutton_noActive");
     
            }
            else//התנאים לא מתקיימים
            {
                document.getElementById("addingAnswer").style.opacity = "0.5"; //הסתרת כפתור הוספת מסיח
                document.getElementById("savingQuestion").style.opacity = "0.5"; //הסתרת כפתור שמירת שאלה

                $("#addingAnswer").prop("disabled", true);//כיבוי כפתור הוספת מסיח
                $("#savingQuestion").prop("disabled", true);//כיבוי כפתור שמירת שאלה
                document.getElementById("savingQuestion_cant").classList.add("tooltiptext_buttons");
                document.getElementById("addingAnswer_noMore").classList.add("tooltiptext_addbutton");

            }
        }
        else {//מצב עריכת שאלה
            if (((countCurrent_Q >= 2) && (countCurrent_Q <= maxChars_Q))
                && ((countCurrent_C != 0) || (imgkeeper1 != "~/image/pic_upload.png"))
                && ((countCurrent_W != 0) || (imgkeeper2 != "~/image/pic_upload.png")))
            {
                document.getElementById("addingAnswer").style.opacity = "1"; //חשיפת כפתור הוספת מסיח
                document.getElementById("SaveChanges").style.opacity = "1"; //חשיפת כפתור שמירת שאלה

                $("#addingAnswer").prop("disabled", false);//הפעלת כפתור הוספת מסיח
                $("#SaveChanges").prop("disabled", false);//הפעלת כפתור שמירת שאלה
                document.getElementById("SaveChanges_cant").classList.add("tooltiptext_buttons_noActive");
                document.getElementById("addingAnswer_noMore").classList.add("tooltiptext_addbutton_noActive");

            }
            else//התנאים לא מתקיימים
            {
                document.getElementById("addingAnswer").style.opacity = "0.5"; //הסתרת כפתור הוספת מסיח
                document.getElementById("SaveChanges").style.opacity = "0.5"; //הסתרת כפתור שמירת שאלה

                $("#addingAnswer").prop("disabled", true);//כיבוי כפתור הוספת מסיח
                $("#SaveChanges").prop("disabled", true);//כיבוי כפתור שמירת שאלה

                document.getElementById("SaveChanges_cant").classList.remove("tooltiptext_buttons_noActive");
                document.getElementById("addingAnswer_noMore").classList.remove("tooltiptext_addbutton_noActive");
                document.getElementById("SaveChanges_cant").classList.add("tooltiptext_buttons");
                document.getElementById("addingAnswer_noMore").classList.add("tooltiptext_addbutton");

            }
        }

        //סגירת כפתור הוספת מסיח במקרה של 5 מסיחים
        if (PanelToOpen==4  )
        {
            document.getElementById("addingAnswer").style.opacity = "0.5"; //הסתרת כפתור הוספת מסיח
            $("#addingAnswer").prop("disabled", true);//כיבוי כפתור הוספת מסיח
            document.getElementById("addingAnswer_noMore").classList.add("tooltiptext_addbutton");

        }

        //שמירת פאנלים של תמונת גזע השאלה
        if ($('#imgKeeper0').attr("value") != "~/image/pic_upload.png") {
            $("#glass0").css({ "display": "block" });
            $("#editPic0").css({ "display": "block" });
            $("#deletPic0").css({ "display": "block" });
            $("#picTitle").css("opacity", "0");
            $('#ImageforUpload0').css("width", "150");
            $('#ImageforUpload0').css("height", "100");
        }
        
        //כיבוי תיבת טקסט בהעלאת תמונה-מסיח נכון
        if ($('#imgKeeper1').attr("value") != "~/image/pic_upload.png") {
            $("#correct").prop("disabled", true);//כיבוי תיבת טקסט
            $("#glass1").css({ "display": "block" });
            $("#editPic1").css({ "display": "block" });
            $("#deletPic1").css({ "display": "block" });
            $('#ImageforUpload1').css("width", "80");
            $('#ImageforUpload1').css("height", "50");
        }

        //כיבוי תיבת טקסט בהעלאת תמונה-מסיחים שגויים
            for (var y = 2; y <= 4; y++) {
                if ($('#imgKeeper' + y).attr("value") != "~/image/pic_upload.png") {
                    $("#incorrect" + (y - 1)).prop("disabled", true);//כיבוי תיבת טקסט
                    $("#glass" + y).css({ "display": "block" });
                    $("#editPic" + y).css({ "display": "block" });
                    $("#deletPic" + y).css({ "display": "block" });
                    $('#ImageforUpload' + y).css("width", "80");
                    $('#ImageforUpload' + y).css("height", "50");
                }
              
        }
    }

       


    //העלאת תמונה גזע שאלה
    $("#FileUpload0").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            $("#picTitle").css("opacity", "0");
            $('#ImageforUpload0').css("width", "150");
            $('#ImageforUpload0').css("height", "100");
            reader.onload = function (e) {
                $('#ImageforUpload0').attr('src', e.target.result);

                $('#imgKeeper0').attr('value', e.target.result);
                $('#picPanelKeeper0').attr('value', "true");


                $("#glass0").css({ "display": "block" });
                $("#editPic0").css({ "display": "block" });
                $("#deletPic0").css({ "display": "block" });
                document.getElementById("questionChars").innerText = "2-45 תווים";
                $("#question").attr("MaxLength", 45);
                check_chars($());

            }
            reader.readAsDataURL(this.files[0]);
            
        }

    });

    //העלאת תמונה תשובה נכונה
    $("#FileUpload1").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            $('#ImageforUpload1').css("width", "80");
            $('#ImageforUpload1').css("height", "50");

            reader.onload = function (e) {
                $('#ImageforUpload1').attr('src', e.target.result);

                $('#imgKeeper1').attr('value', e.target.result);
                $('#picPanelKeeper1').attr('value', "true");

                $("#glass1").css({ "display": "block" });
                $("#editPic1").css({ "display": "block" });
                $("#deletPic1").css({ "display": "block" });
                $("#correct").prop("disabled", true);
                check_chars($());
            }
            reader.readAsDataURL(this.files[0]);
            
           
        }
    });

    //העלאת תמונה מסיח 1-מסיח חובה
    $("#FileUpload2").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            $('#ImageforUpload2').css("width", "80");
            $('#ImageforUpload2').css("height", "50");

            reader.onload = function (e) {

                $('#imgKeeper2').attr('value', e.target.result);
                $('#picPanelKeeper2').attr('value', "true");

                $('#ImageforUpload2').attr('src', e.target.result);
                $("#glass2").css({ "display": "block" });
                $("#editPic2").css({ "display": "block" });
                $("#deletPic2").css({ "display": "block" });
                $("#incorrect1").prop("disabled", true);
                check_chars($());
            }
            reader.readAsDataURL(this.files[0]);
           
           
        }
    });

    //העלאת תמונה מסיח 2
    $("#FileUpload3").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            $('#ImageforUpload3').css("width", "80");
            $('#ImageforUpload3').css("height", "50");

            reader.onload = function (e) {

                $('#imgKeeper3').attr('value', e.target.result);
                $('#picPanelKeeper3').attr('value', "true");

                $('#ImageforUpload3').attr('src', e.target.result);
                $("#glass3").css({ "display": "block" });
                $("#editPic3").css({ "display": "block" });
                $("#deletPic3").css({ "display": "block" });
                $("#incorrect2").prop("disabled", true);
                check_chars($());
            }
            reader.readAsDataURL(this.files[0]);
            
           
        }
    });

    //העלאת תמונה מסיח 3
    $("#FileUpload4").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            $('#ImageforUpload1').css("width", "80");
            $('#ImageforUpload1').css("height", "50");

            reader.onload = function (e) {

                $('#imgKeeper4').attr('value', e.target.result);
                $('#picPanelKeeper4').attr('value', "true");

                $('#ImageforUpload4').attr('src', e.target.result);
                $("#glass4").css({ "display": "block" });
                $("#editPic4").css({ "display": "block" });
                $("#deletPic4").css({ "display": "block" });
                $("#incorrect3").prop("disabled", true);
                check_chars($());
            }
            reader.readAsDataURL(this.files[0]);
          
        
        }
    });

    // העלאת תמונה מסיח 4 
    $("#FileUpload5").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            $('#ImageforUpload5').css("width", "80");
            $('#ImageforUpload5').css("height", "50");

            reader.onload = function (e) {

                $('#imgKeeper5').attr('value', e.target.result);
                $('#picPanelKeeper5').attr('value', "true");

                $('#ImageforUpload5').attr('src', e.target.result);
                $("#glass5").css({ "display": "block" });
                $("#editPic5").css({ "display": "block" });
                $("#deletPic5").css({ "display": "block" });
                $("#incorrect4").prop("disabled", true);
                check_chars($());
            }
            reader.readAsDataURL(this.files[0]);
           
            

        }
    });


});