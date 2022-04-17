<%@ Page Language="C#" AutoEventWireup="true" CodeFile="settings.aspx.cs" Inherits="settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>המערב הפרוע | הגדרות</title>
        <%-- css --%>
   <link href="Styles/editorStyle.css" rel="stylesheet" />
    <%--הפניה לקובץ jquary--%>
    <script src="jScripts/jquery-1.7.1.min.js"></script>
     <%--הפניה לקובץ JS שלנו--%>
    <script src="jScripts/JavaScript%20-%20setting.js"></script>

     <!--Favicon-->
    <link rel="apple-touch-icon" sizes="180x180" href="favicon/apple-touch-icon.png" />
    <link rel="icon" sizes="32X32" href="favicon/favicon-32x32.png" />
    <link rel="icon" sizes="16x16" href="favicon/favicon-16x16.png" />

    <style type="text/css">
        #settingBK {
            width: 421px;
            height: 222px;
        }
    </style>
</head>
<body>
     <header>
            <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a href="index.html">
                <img id="logo" src="image/Logo.png" width="150" /> <!--הלוגו של המשחק שלכם-->
              
            </a>
            <!--תפריט הניווט בראש העמוד-->
            <nav>
                <ul>
                    <li><a id="help">עזרה</a></li>
                    <li><a id="toTheGame" href="index.html">למשחק |</a></li>
                    <li><a id="about">אודות |</a></li>
                </ul>
            </nav>
        </header>
        <div id="aboutDiv" class="popUp bounceInDown hide">
            <a class="closeAbout">X</a>
            <div>
                <img src="image/aboutUs-03.png"   width="1000"/>     
            </div>
                
                <a href="https://www.hit.ac.il/telem/overview">
                    <div class="link">הפקולטה לטכנולוגיות למידה</div>
                </a>
                <div class="link2">
                    במשחק זה נעשה שימוש בגרפיקות מאתר <a href="https://www.freepik.com/">freepik</a>     
                </div>
        </div>
    <form id="form1" runat="server">
        <img class="signs" src="image/gameSetting_sign.png" />
        <div dir="rtl">
            <div id="settingBK" dir="rtl">
            <a class="settingTXT">שם המשחק</a>
            <asp:TextBox ID="GameSubjectName" class="textBoxGAME" CharacterForBTN="saveChanges" CharacterForLbl="CharsNumError" runat="server" MaxLength="20"></asp:TextBox>
            <br />
            <label id="CharsNumError">2-20 תווים</label><br />

          <a class="settingTXT">  הגבלת זמן לשאלה</a>
            <asp:DropDownList ID="TimeLimitPerQ" class="settingTime" runat="server" Font-Names="Rubik"  Width="120px">
                <asp:ListItem Value="0">ללא הגבלה</asp:ListItem>
                <asp:ListItem Value="15">15 שניות</asp:ListItem>
                <asp:ListItem Selected="True" Value="30">30 שניות</asp:ListItem>
                <asp:ListItem Value="45">45 שניות</asp:ListItem>
                <asp:ListItem Value="60">60 שניות</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button ID="cancelChanges" CssClass="gameBTN_setting" runat="server" Text="בטל שינויים" OnClick="cancelChanges_Click" BorderStyle="None" Height="28px" />
            <asp:Button ID="saveChanges" CssClass="gameBTN_setting" runat="server" Text="שמור וחזור" OnClick="saveChanges_Click" BorderStyle="None" Height="28px" />
        </div>
              </div>
    </form>
</body>
</html>
