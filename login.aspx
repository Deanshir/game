<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>המערב הפרוע | כניסת עורך</title>
        <%-- css --%>
    <link href="Styles/editorStyle.css" rel="stylesheet" />
    <%--הפניה לקובץ jquary--%>
    <script src="jScripts/jquery-1.7.1.min.js"></script>
    
     <%--הפניה לקובץ JS שלנו--%>
    <script src="jScripts/JavaScript -login.js"></script>

     <!--Favicon-->
    <link rel="apple-touch-icon" sizes="180x180" href="favicon/apple-touch-icon.png" />
    <link rel="icon" sizes="32X32" href="favicon/favicon-32x32.png" />
    <link rel="icon" sizes="16x16" href="favicon/favicon-16x16.png" />

</head>
<body id="loginPage">
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
       
      <img id="title" src="image/big_title.png" />
    <form id="form1" runat="server">
      
        <div id="whiteBK" dir="rtl">
            <section class="loginTXT">
                  <a >שם משתמש </a><br />
            <asp:TextBox ID="UserName" class="textBoxLogin" runat="server" CssClass="CharacterCount" CharacterForBTN="EnterEditor"></asp:TextBox>
            </section>
         <br />
         <section class="loginTXT">
             <a >סיסמא</a><br />
            <asp:TextBox ID="Password" class="textBoxLogin" runat="server" TextMode="Password" CssClass="CharacterCount" CharacterForBTN="EnterEditor"></asp:TextBox>
         </section>
            
            <br />
            <asp:Label ID="alert_login"  runat="server" Text=""></asp:Label>
            <br />
            <asp:ImageButton src="image/editorEnter.png" ID="EnterEditor" runat="server" Height="51px" OnClick="EnterEditor_Click" Width="199px" disabled="true"   />
            <h5>שם משתמש: admin      סיסמא: telem</h5>
            </div>

    </form>
</body>
</html>
