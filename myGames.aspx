<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myGames.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>המערב הפרוע | המשחקים שלי</title>
    <%-- css --%>
    <link href="Styles/editorStyle.css" rel="stylesheet" />
    <%--הפניה לקובץ jquary--%>
    <script src="jScripts/jquery-1.7.1.min.js"></script>
    <%--הפניה לקובץ JS שלנו--%>
    <script src="jScripts/JavaScript%20-%20myGames.js"></script>

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
    <form id="form1" runat="server">
        <img id="myGamesSign" src="image/myGames_sign.png" />
        <div>
            <div id="newGAME">
                <asp:Label ID="gameNAME" runat="server" Text="שם המשחק:"> </asp:Label>
                <asp:TextBox ID="addNameTB" class="textBoxGAME" runat="server" CssClass="textBoxGAME" CharacterForBTN="createGame" CharacterForLbl="newGameChars" MaxLength="20"></asp:TextBox>
                <asp:Button ID="createGame" CssClass="gameBTN" runat="server" OnClick="createGame_Click" Text="צור משחק" BorderStyle="None" Height="28px" Width="90px" disabled="true" />
                <br />
                <label id="newGameChars">2-20 תווים</label>
            </div>
            <br />
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/Wildwest.xml" XPath="/WildWestGame/Game"></asp:XmlDataSource>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" CellPadding="2" OnRowCommand="rowCommand" ShowHeaderWhenEmpty="True" EmptyDataRowStyle-CssClass="empty">
                <AlternatingRowStyle BackColor="#EADABE" />
                <Columns>
                    <asp:TemplateField HeaderText="שם המשחק" ControlStyle-Width="350">
                        <ItemTemplate>
                            <asp:Label ID="gameNameTXT" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "GameSubject").ToString()%>' Width="150"></asp:Label>
                        </ItemTemplate>


                        <ControlStyle Width="350px"></ControlStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="קוד משחק" ControlStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="gameCodeTXT" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "@GameCode").ToString())%>'></asp:Label>
                        </ItemTemplate>

                        <ControlStyle Width="80px"></ControlStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="הגדרות" ControlStyle-Width="30">
                        <ItemTemplate>
                            <asp:ImageButton ID="SettingsBTN" runat="server" ImageUrl="~/image/settings.png" CommandName="SettingsRow" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' />
                        </ItemTemplate>

                        <ControlStyle Width="30px"></ControlStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="עריכה" ControlStyle-Width="20">
                        <ItemTemplate>

                            <asp:ImageButton ID="EditBTN" runat="server" ImageUrl="~/image/edit.png" CommandName="editRow" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' />



                        </ItemTemplate>


                        <ControlStyle Width="20px"></ControlStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחיקה" ControlStyle-Width="20" FooterText="לא הוזנו משחקים">
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteBTN" runat="server" ImageUrl="~/image/delete.png" CommandName="deleteRow" AlternateText="delete" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' />
                        </ItemTemplate>

                        <ControlStyle Width="20px"></ControlStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="פרסום" ControlStyle-Width="50">
                        <ItemTemplate>
                            <div class="tooltip_myGames">
                                <asp:CheckBox ID="PublishCB" class="alertPublish" OnCheckedChanged="isPublish_CheckedChanged" AutoPostBack="true" runat="server"   theItemId='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem, "@isPublish"))%>'  />
                                <asp:Label ID="alert_notPublished" CssClass="tooltiptext_myGames" runat="server" Text="לא ניתן לפרסם. המשחק חייב להכיל 9/12/15 שאלות בדיוק" ></asp:Label>

                            </div>
                        </ItemTemplate>


                        <ControlStyle Width="50px"></ControlStyle>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>לא הוזנו משחקים</EmptyDataTemplate>
                <EditRowStyle BackColor="#999999" Height="10px" Wrap="True" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#AC4238" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EADABE" ForeColor="Black" Height="10px" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" Height="10px" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>


        </div>

        <%--חלונית התראה למחיקת משחק--%>
        <asp:Panel ID="Wrap_deleteGame" class="hideAlert" runat="server">
            <asp:Panel ID="Alert_deleteGame" CssClass="AlertClass" runat="server">
                <asp:Label ID="deleteGame_label" class="alertLabel" runat="server" Text="" Visible="true"></asp:Label><br />
                <asp:Button ID="yesDelete_deleteGame" runat="server" CssClass="AlertBTN" Text="כן-מחק" OnClick="deleteRow" BorderStyle="None" Height="28px" />
                <asp:Button ID="noDontDelete_deleteGame" CssClass="AlertBTN" runat="server" Text="לא-אל תמחק" OnClick="No_CloseAlert_BackToMyGames" BorderStyle="None" Height="28px" />
            </asp:Panel>
        </asp:Panel>

    </form>
</body>
</html>
