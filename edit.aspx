<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="edit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <title>המערב הפרוע | עריכת משחק</title>
    <!--//---Favicon---//-->
    <link rel="icon" href="~image/Logo.png" />

    <%-- css --%>
    <link href="Styles/editorStyle.css" rel="stylesheet" />
    <%--הפניה לקובץ jquary--%>
    <script src="jScripts/jquery-1.7.1.min.js"></script>
    <%--הפניה לקובץ JS שלנו--%>
    <script src="jScripts/JavaScript%20-%20edit.js"></script>

    <!--Favicon-->
    <link rel="apple-touch-icon" sizes="180x180" href="favicon/apple-touch-icon.png" />
    <link rel="icon" sizes="32X32" href="favicon/favicon-32x32.png" />
    <link rel="icon" sizes="16x16" href="favicon/favicon-16x16.png" />

</head>
<body dir="rtl">
    <header>
        <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
        <a href="index.html">
            <img id="logo" src="image/Logo.png" width="150" />
            <!--הלוגו של המשחק שלכם-->

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
            <img src="image/aboutUs-03.png" width="1000" />
        </div>

        <a href="https://www.hit.ac.il/telem/overview">
            <div class="link">הפקולטה לטכנולוגיות למידה</div>
        </a>
        <div class="link2">
            במשחק זה נעשה שימוש בגרפיקות מאתר <a href="https://www.freepik.com/">freepik</a>
        </div>
    </div>

    <img id="signEdit" src="image/editGame_sign.png" />
    <br />

    <form id="form1" runat="server">

        <asp:Label ID="GameSubjectTXT" runat="server" Text=""></asp:Label><br />
        <%--טבלה--%>
        <div id="table">
            <a id="tableStatus" class="tableStatus" name="tableStatus">בכדי לפרסם משחק יש ליצור 9/12/15 שאלות <span id="bold">בדיוק</span></a>
            <br />
            <div id="tableBox">
                <label id="tbleTitle">מאגר שאלות</label>
            </div>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/Wildwest.xml" XPath="/WildWestGame/Game/questions/question"></asp:XmlDataSource>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" OnRowCommand="rowCommand1" HeaderStyle-BackColor="#EADABE" BorderColor="Black" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <asp:Label ID="questionNum" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "@id").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="השאלה" ControlStyle-Width="400">
                        <ItemTemplate>
                            <asp:Label ID="question" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "questionText").ToString()%>'></asp:Label>
                        </ItemTemplate>

                        <ControlStyle Width="400px"></ControlStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="עריכה" ControlStyle-Width="15">
                        <ItemTemplate>
                            <asp:ImageButton ID="EditQuestionBTN" runat="server" ImageUrl="~/image/edit.png" CommandName="editQuestionRow" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@id").ToString()%>' />
                        </ItemTemplate>

                        <ControlStyle Width="15px"></ControlStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחיקה" ControlStyle-Width="15">
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteQuestionBTN" runat="server" ImageUrl="~/image/delete.png" CommandName="deleteQuestionRow" AlternateText="delete" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@id").ToString()%>' />
                        </ItemTemplate>

                        <ControlStyle Width="15px"></ControlStyle>
                    </asp:TemplateField>
                </Columns>

                <HeaderStyle BackColor="#EADABE"></HeaderStyle>
                <RowStyle BackColor="White" />
            </asp:GridView>

        </div>
        <%-- עמוד עריכה --%>
        <div id="editPageDisplay">
            <%-- טופס --%>
            <div id="editPage">
                <%-- גזע השאלה --%>
                <div id="questionBox">
                    <%-- כותרת: שאלה --%>
                    <a class="QTitle">שאלה</a>
                    <%-- תיבת טקסט, כפתור העלאת תמונה, מס' תווים --%>

                    <div class="formPanel">
                        <div id="img_div">
                            <asp:TextBox ID="question" class="counterChars" CharacterForLblQ="questionChars" runat="server" Height="85px" MaxLength="100" MinLength="2" TextMode="MultiLine" Width="367px"></asp:TextBox>
                            <asp:FileUpload ID="FileUpload0" runat="server" Width="16px" />
                            <asp:HiddenField ID="fileKeeper0" OnValueChanged="Page_Load" Value="" runat="server" />

                            <asp:HiddenField ID="imgKeeper0" OnValueChanged="Page_Load" Value="~/image/pic_upload.png" runat="server" />
                            <asp:ImageButton ID="ImageforUpload0" runat="server" ImageUrl="~/image/pic_upload.png" OnClientClick="openFileUploader0(); return false;" />

                            <asp:HiddenField ID="picPanelKeeper0"  Value="false" runat="server" />
                            <div id="picPanel">
                                <%-- פאנל עריכת שאלה --%>
                                <asp:ImageButton ID="glass0" CssClass="imageOpptions" runat="server" ImageUrl="~/image/glass.png" OnClientClick="imgBigQ('0'); return false;" />
                                <asp:ImageButton ID="editPic0" CssClass="imageOpptions" runat="server" ImageUrl="~/image/editPic.png" OnClientClick="openFileUploader0();" />
                                <asp:ImageButton ID="deletPic0" CssClass="imageOpptions" runat="server" ImageUrl="~/image/deletPic.png" OnClick="deleteImg0" />

                                <a id="picTitle" runat="server">הוספת תמונה</a>
                            </div>

                        </div>
                    </div>
                    <div id="questionChars" runat="server">2-100 תווים</div>

                </div>
            </div>
            <%-- מסיחים --%>
            <div id="answers">

                <%-- תשובה נכונה --%>
                <div id="correctBox" class="answer">
                    <%-- כותרת: תשובה נכונה --%>
                    <a class="ATitle">תשובה נכונה</a>

                    <%-- תיבת טקסט, 'או', העלאת תמונה, מס' תווים  --%>
                    <div class="formPanelA" dir="rtl">
                        <asp:TextBox ID="correct" class="counterChars" CharacterForLblC="correctChars" runat="server" MaxLength="45" Width="363px"></asp:TextBox>
                        <a class="or">או</a>
                        <asp:FileUpload ID="FileUpload1" CssClass="picA" runat="server" Width="16px" />
                        <asp:HiddenField ID="fileKeeper1" OnValueChanged="Page_Load" Value="" runat="server" />

                        <asp:HiddenField ID="imgKeeper1" OnValueChanged="Page_Load" Value="~/image/pic_upload.png" runat="server" />
                        <asp:ImageButton ID="ImageforUpload1" CssClass="picA" runat="server" ImageUrl="~/image/pic_upload.png" OnClientClick="openFileUploader1(); return false;"
                            ImageAlign="AbsMiddle" />
                        <asp:HiddenField ID="picPanelKeeper1"  Value="false" runat="server" />

                        <div id="picPanel1" runat="server">
                            <%-- פאנל עריכת שאלה --%>

                            <asp:ImageButton ID="glass1" CssClass="imageOpptions" runat="server" ImageUrl="~/image/glass.png" OnClientClick="imgBigA('1'); return false;" />
                            <asp:ImageButton ID="editPic1" CssClass="imageOpptions" runat="server" ImageUrl="~/image/editPic.png" OnClientClick="openFileUploader1(); return false;" />
                            <asp:ImageButton ID="deletPic1" CssClass="imageOpptions" runat="server" ImageUrl="~/image/deletPic.png" OnClick="deleteImg1" />
                        </div>



                        <div id="correctChars" class="charsNum" runat="server">1-45 תווים</div>
                    </div>
                </div>




                <%-- תשובה שגויה --%>
                <div id="inCorrecetBox" class="answer">
                    <%-- כותרת: מסיחים --%>
                    <a class="ATitle">מסיחים</a>
                    <%-- תיבת טקסט, 'או', העלאת תמונה, מס' תווים --%>
                    <div class="formPanelA" dir="rtl">
                        <asp:TextBox ID="incorrect1" class="counterChars" CharacterForLblW="incorrect1Chars" btnHide="ImageforUpload2" runat="server" MaxLength="45" Width="364px"></asp:TextBox>
                        <a class="or">או</a>
                        <asp:FileUpload ID="FileUpload2" CssClass="picA" runat="server" Width="16px" />
                        <asp:HiddenField ID="fileKeeper2" OnValueChanged="Page_Load" Value="" runat="server" />

                        <asp:HiddenField ID="imgKeeper2" OnValueChanged="Page_Load" Value="~/image/pic_upload.png" runat="server" />
                        <asp:ImageButton ID="ImageforUpload2" runat="server" CssClass="picA" ImageUrl="~/image/pic_upload.png" OnClientClick="openFileUploader2(); return false;" ImageAlign="AbsMiddle" />
                        <asp:HiddenField ID="picPanelKeeper2"  Value="false" runat="server" />

                        <div id="picPanel2">
                            <%-- פאנל עריכת שאלה --%>
                            <asp:ImageButton ID="glass2" CssClass="imageOpptions" runat="server" ImageUrl="~/image/glass.png" OnClientClick="imgBigA('2'); return false;" />
                            <asp:ImageButton ID="editPic2" CssClass="imageOpptions" runat="server" ImageUrl="~/image/editPic.png" OnClientClick="openFileUploader2(); return false;" />
                            <asp:ImageButton ID="deletPic2" CssClass="imageOpptions" runat="server" ImageUrl="~/image/deletPic.png" OnClick="deleteImg2" />
                        </div>

                        <div id="incorrect1Chars" class="charsNum" runat="server">1-45 תווים</div>

                    </div>
                </div>

                <%-- מסיחים אופציונאליים --%>
                <div id="worngAnswers" class="formPanelA">
                    <%-- מסיח 2 --%>

                    <asp:Panel ID="PanelAns2" runat="server" Visible="false" Width="650px">

                        <asp:ImageButton ID="DeleteAns2" CssClass="DelPic" runat="server" ImageUrl="~/image/delete.png" AlternateText="delete" Height="26px" Width="25px" OnClick="AlertFunction_Ans2ToDel" ImageAlign="Baseline" />
                        <asp:TextBox ID="incorrect2" class="counterChars" CharacterForLblW="incorrect2Chars" btnHide="ImageforUpload3" runat="server" Width="363px" MaxLength="45"></asp:TextBox>
                        <a class="or">או</a>


                        <asp:FileUpload ID="FileUpload3" runat="server" Width="16px" />
                        <asp:HiddenField ID="fileKeeper3" OnValueChanged="Page_Load" Value="" runat="server" />

                        <asp:HiddenField ID="imgKeeper3"  Value="~/image/pic_upload.png" runat="server" />

                        <asp:ImageButton ID="ImageforUpload3" runat="server" ImageUrl="~/image/pic_upload.png" OnClientClick="openFileUploader3(); return false;"
                            ImageAlign="AbsMiddle" />
                        <asp:HiddenField ID="picPanelKeeper3" Value="false" runat="server" />

                        <div id="picPanel3">
                            <%-- פאנל עריכת שאלה --%>
                            <asp:ImageButton ID="glass3" CssClass="imageOpptions" runat="server" ImageUrl="~/image/glass.png" OnClientClick="imgBigA('3'); return false;" />
                            <asp:ImageButton ID="editPic3" CssClass="imageOpptions" runat="server" ImageUrl="~/image/editPic.png" OnClientClick="openFileUploader3(); return false;" />
                            <asp:ImageButton ID="deletPic3" CssClass="imageOpptions" runat="server" ImageUrl="~/image/deletPic.png" OnClick="deleteImg3" />
                        </div>
                        <div id="incorrect2Chars" class="charsNumW" runat="server">1-45 תווים</div>


                    </asp:Panel>

                    <%-- מסיח 3 --%>
                    <asp:Panel ID="PanelAns3" runat="server" Visible="false" Width="650px">
                        <asp:ImageButton ID="DeleteAns3" runat="server" ImageUrl="~/image/delete.png" AlternateText="delete" Height="26px" Width="25px" OnClick="AlertFunction_Ans3ToDel" />
                        <asp:TextBox ID="incorrect3" class="counterChars" CharacterForLblW="incorrect3Chars" btnHide="ImageforUpload4" runat="server" Width="363px" MaxLength="45"></asp:TextBox>
                        <a class="or">או</a>
                        <asp:FileUpload ID="FileUpload4" runat="server" Width="16px" />
                        <asp:HiddenField ID="fileKeeper4" OnValueChanged="Page_Load" Value="" runat="server" />

                        <asp:HiddenField ID="imgKeeper4" OnValueChanged="Page_Load" Value="~/image/pic_upload.png" runat="server" />
                        <asp:ImageButton ID="ImageforUpload4" runat="server" ImageUrl="~/image/pic_upload.png" OnClientClick="openFileUploader4(); return false;"
                            ImageAlign="AbsMiddle" />
                        <asp:HiddenField ID="picPanelKeeper4"  Value="false" runat="server" />

                        <div id="picPanel4">
                            <%-- פאנל עריכת שאלה --%>
                            <asp:ImageButton ID="glass4" CssClass="imageOpptions" runat="server" ImageUrl="~/image/glass.png" OnClientClick="imgBigA('4'); return false;" />
                            <asp:ImageButton ID="editPic4" CssClass="imageOpptions" runat="server" ImageUrl="~/image/editPic.png" OnClientClick="openFileUploader4(); return false;" />
                            <asp:ImageButton ID="deletPic4" CssClass="imageOpptions" runat="server" ImageUrl="~/image/deletPic.png" OnClick="deleteImg4" />
                        </div>

                        <div id="incorrect3Chars" class="charsNumW" runat="server">1-45 תווים</div>
                    </asp:Panel>
                    <%-- מסיח 4 --%>

                    <asp:Panel ID="PanelAns4" runat="server" Visible="false" Width="650px">
                        <asp:ImageButton ID="DeleteAns4" runat="server" ImageUrl="~/image/delete.png" AlternateText="delete" Height="26px" Width="25px" OnClick="AlertFunction_Ans4ToDel" />
                        <asp:TextBox ID="incorrect4" class="counterChars" CharacterForLblW="incorrect4Chars" btnHide="ImageforUpload5" runat="server" Width="363px" MaxLength="45"></asp:TextBox>
                        <a class="or">או</a>
                        <asp:FileUpload ID="FileUpload5" runat="server" Width="16px" />
                        <asp:HiddenField ID="fileKeeper5" OnValueChanged="Page_Load" Value="" runat="server" />

                        <asp:HiddenField ID="imgKeeper5" OnValueChanged="Page_Load" Value="~/image/pic_upload.png" runat="server" />
                        <asp:ImageButton ID="ImageforUpload5" runat="server" ImageUrl="~/image/pic_upload.png" OnClientClick="openFileUploader5(); return false;"
                            ImageAlign="AbsMiddle" />
                        <asp:HiddenField ID="picPanelKeeper5"  Value="false" runat="server" />

                        <div id="picPanel5">
                            <%-- פאנל עריכת שאלה --%>
                            <asp:ImageButton ID="glass5" CssClass="imageOpptions" runat="server" ImageUrl="~/image/glass.png" OnClientClick="imgBigA('5'); return false;" />
                            <asp:ImageButton ID="editPic5" CssClass="imageOpptions" runat="server" ImageUrl="~/image/editPic.png" OnClientClick="openFileUploader5(); return false;" />
                            <asp:ImageButton ID="deletPic5" CssClass="imageOpptions" runat="server" ImageUrl="~/image/deletPic.png" OnClick="deleteImg5" />
                        </div>

                        <div id="incorrect4Chars" class="charsNumW" runat="server">1-45 תווים</div>
                    </asp:Panel>
                </div>

<%--                שומרים את שם התמונות--%>
              <asp:HiddenField ID="SaveNamePic0"  Value="" runat="server" />
                <asp:HiddenField ID="SaveNamePic1"  Value="" runat="server" />
                <asp:HiddenField ID="SaveNamePic2"  Value="" runat="server" />
                <asp:HiddenField ID="SaveNamePic3"  Value="" runat="server" />
                <asp:HiddenField ID="SaveNamePic4"  Value="" runat="server" />
                <asp:HiddenField ID="SaveNamePic5"  Value="" runat="server" />




                <%-- כפתורים --%>
            </div>

        </div>
        <div id="buttons">
            <div id="editButtonsPanel">

                <div class="tooltip_addbutton">
                    <asp:HiddenField ID="AddingAnswerCounter" Value="1" runat="server" />
                    <asp:Button ID="addingAnswer" CssClass="gameBTN" runat="server" Text="הוספת מסיח" AutoPostBack="false" BorderStyle="None" OnClick="addingAnswer_function" Height="28px" />
                    <asp:Label ID="addingAnswer_noMore" CssClass="tooltiptext_addbutton" runat="server" Text="לא ניתן להוסיף מסיחים"></asp:Label>

                </div>
                <asp:Button ID="restart" CssClass="gameBTN" runat="server" Text="איפוס שדות" OnClick="AlertFunction_reset" BorderStyle="None" Height="28px" />

                <div class="tooltip_buttons">
                    <asp:HiddenField ID="savingQClickorNot" Value="false" runat="server" />
                    <asp:Button ID="savingQuestion" CssClass="gameBTN" runat="server" Text="שמירת שאלה " OnClick="savingQuestion_Click"  OnClientClick="check_chars();" BorderStyle="None" Height="28px" disabled="true" />
                    <asp:Label ID="savingQuestion_cant" CssClass="tooltiptext_buttons" runat="server" Text="לא ניתן לשמור את השאלה. אחד השדות ריק"></asp:Label>

                </div>
                <asp:Button ID="DontSaveChanges" CssClass="editBTN" runat="server" Text="ביטול שינויים" OnClick="DontSaveChanges_Click" Visible="false" AutoPostBack="true" BorderStyle="None" Height="28px" />

                <div class="tooltip_buttons">
                    <asp:Button ID="SaveChanges" CssClass="editBTN" runat="server" Text="שמירת שינויים" OnClick="SaveChanges_Click" Visible="false" AutoPostBack="true" BorderStyle="None" Height="28px" />
                    <asp:Label ID="SaveChanges_cant" CssClass="tooltiptext_buttons" runat="server" Text="לא ניתן לשמור את השאלה. אחד השדות ריק"></asp:Label>

                </div>
                <asp:HiddenField ID="EditStatus" Value="false" runat="server" />
                <asp:Button ID="backToMyGmaes" CssClass="gameBTN" runat="server" Text="חזרה למשחקים שלי" OnClick="backToMyGmaes_check" BorderStyle="None" Height="28px" />
            </div>

        </div>


        <asp:HiddenField ID="pathPic0" Value="" runat="server" />



        <%--התראות--%>
        <%--חלונית מחיקת מסיח שני--%>
        <asp:Panel ID="Wrap_deleteAns2" class="hideAlert" runat="server">
            <asp:Panel ID="Alert_deleteAns2" CssClass="AlertClass" runat="server">
                <asp:Label ID="deleteAns_lebel2" class="alertLabel" runat="server" Text="רק רגע,</br>אתה בטוח שברצונך למחוק את המסיח?" Visible="true"></asp:Label><br />
                <asp:Button ID="yesDelete_Ans2" CssClass="AlertBTN" runat="server" Text="כן-מחק" OnClick="DeleteAns2_Click" BorderStyle="None" Height="28px" />
                <asp:Button ID="noDontDelete_Ans2" CssClass="AlertBTN" runat="server" Text="לא- אל תמחק" OnClick="No_CloseAlertAns2" BorderStyle="None" Height="28px" />
            </asp:Panel>
        </asp:Panel>
        <%--חלונית מחיקת מסיח שלישי--%>
        <asp:Panel ID="Wrap_deleteAns3" class="hideAlert" runat="server">
            <asp:Panel ID="Alert_deleteAns3" CssClass="AlertClass" runat="server">
                <asp:Label ID="deleteAns_lebel3" class="alertLabel" runat="server" Text="רק רגע,</br>אתה בטוח שברצונך למחוק את המסיח?" Visible="true"></asp:Label><br />
                <asp:Button ID="yesDelete_Ans3" CssClass="AlertBTN" runat="server" Text="כן-מחק" OnClick="DeleteAns3_Click" BorderStyle="None" Height="28px" />
                <asp:Button ID="noDontDelete_Ans3" CssClass="AlertBTN" runat="server" Text="לא- אל תמחק" OnClick="No_CloseAlertAns3" BorderStyle="None" Height="28px" />
            </asp:Panel>
        </asp:Panel>
        <%--חלונית מחיקת מסיח רביעי--%>
        <asp:Panel ID="Wrap_deleteAns4" class="hideAlert" runat="server">
            <asp:Panel ID="Alert_deleteAns4" CssClass="AlertClass" runat="server">
                <asp:Label ID="deleteAns_lebel4" class="alertLabel" runat="server" Text="רק רגע,</br>אתה בטוח שברצונך למחוק את המסיח?" Visible="true"></asp:Label><br />
                <asp:Button ID="yesDelete_Ans4" CssClass="AlertBTN" runat="server" Text="כן-מחק" OnClick="DeleteAns4_Click" BorderStyle="None" Height="28px" />
                <asp:Button ID="noDontDelete_Ans4" CssClass="AlertBTN" runat="server" Text="לא- אל תמחק" OnClick="No_CloseAlertAns4" BorderStyle="None" Height="28px" />
            </asp:Panel>
        </asp:Panel>

        <%--חלונית איפוס שדות--%>
        <asp:Panel ID="Wrap_reset" class="hideAlert" runat="server">
            <asp:Panel ID="Alert_reset" CssClass="AlertClass" runat="server">
                <asp:Label ID="delete_lebelReset" class="alertLabel" runat="server" Text="רק רגע,</br>אתה בטוח שברצונך לאפס את כל </br>שדות השאלה?" Visible="true"></asp:Label><br />
                <asp:Button ID="yesDelete_reset" CssClass="AlertBTN" runat="server" Text="כן-מחק" OnClick="restart_Click" BorderStyle="None" Height="28px" />
                <asp:Button ID="noDontDelete_reset" CssClass="AlertBTN" runat="server" Text="לא- אל תמחק" OnClick="No_CloseAlertReset" BorderStyle="None" Height="28px" />
            </asp:Panel>
        </asp:Panel>


        <%--חלונית מחיקת שאלה--%>
        <asp:Panel ID="Wrap_deleteQuest" class="hideAlert" runat="server">
            <asp:Panel ID="Alert_deleteQuest" CssClass="AlertClass" runat="server">
                <asp:Label ID="delete_lebelquest" class="alertLabel" runat="server" Text="רק רגע,</br>אתה בטוח שברצונך למחוק את השאלה?" Visible="true"></asp:Label><br />
                <asp:Button ID="yesDelete_quest" runat="server" CssClass="AlertBTN" Text="כן-מחק" OnClick="deleteQuestionRow" BorderStyle="None" Height="28px" />
                <asp:Button ID="noDontDelete_quest" CssClass="AlertBTN" runat="server" Text="לא- אל תמחק" OnClick="No_CloseAlertDelete_quest" BorderStyle="None" Height="28px" />
            </asp:Panel>
        </asp:Panel>

        <%--חלונית התראה חזרה למשחקים שלי לפני שמירה--%>
        <asp:Panel ID="Wrap_BackToMyGames" class="hideAlert" runat="server">
            <asp:Panel ID="Alert_BackToMyGames" CssClass="AlertClass" runat="server">
                <asp:Label ID="delete_BackToMyGames" class="alertLabel" runat="server" Text="לא שמרת את השאלה הנוכחית.</br>השאלה תמחק. האם להמשיך?" Visible="true"></asp:Label><br />
                <asp:Button ID="yesDelete_BackToMyGames" runat="server" CssClass="AlertBTN" Text="כן" OnClick="yes_backToMyGmaes" BorderStyle="None" Height="28px" />
                <asp:Button ID="noDontDelete_BackToMyGames" CssClass="AlertBTN" runat="server" Text="לא" OnClick="No_CloseAlert_BackToMyGames" BorderStyle="None" Height="28px" />
            </asp:Panel>
        </asp:Panel>


    </form>
</body>

</html>
