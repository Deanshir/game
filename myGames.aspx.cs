using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Newtonsoft.Json;

public partial class _Default : System.Web.UI.Page
{
    XmlDocument myGameDoc = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        myGameDoc = XmlDataSource1.GetXmlDocument();
        checkAllGamePublish();
    }

    protected void checkAllGamePublish()
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            //חיפוש הלייבל שבו מופיע ה ID של המשחק
            Label gameCodeTXT = (Label)row.FindControl("gameCodeTXT");
            //בעזרת האי-די של המשחק נוכל לבדוק האם עומד בתנאי הפרסום
            string GameCode = gameCodeTXT.Text;

            //בדיקה האם כמות השאלות הינה 9 12 או 15 
            XmlNodeList QuestionNum = myGameDoc.SelectNodes("WildWestGame/Game[@GameCode='" + GameCode + "']/questions/question");
            int QuestionCounter = QuestionNum.Count;

            //חיפוש הצ'אק-בוקס על פי האי-די שלו
            CheckBox GameIsPublishCb = (CheckBox)row.FindControl("PublishCB");
            Label toolTip = (Label)row.FindControl("alert_notPublished");


            if ((QuestionCounter == 9) || (QuestionCounter == 12) || (QuestionCounter == 15))
            {
                GameIsPublishCb.Enabled = true;
                toolTip.CssClass = "tooltiptext_myGames_noActive ";
                
            }
            else
            {
                GameIsPublishCb.Enabled = false;
                toolTip.CssClass = "tooltiptext_myGames";
            
                ////אם מקודם המשחק היה מפורסם, אנחנו רוצים להחזיר אותו ללא מפורסם בעץ
                XmlNode GameIsPublish = myGameDoc.SelectSingleNode("WildWestGame/Game[@GameCode='" + GameCode + "']");
                GameIsPublish.Attributes["isPublish"].InnerText = "False";
               // XmlDataSource1.Save();

                //וגם לשנות את הפקד עצמו ללא לחוץ
                GameIsPublishCb.Checked = false;
            

            }
        }
        XmlDataSource1.Save();
    }


    protected void createGame_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        xmlDoc.Load(Server.MapPath("trees/Wildwest.xml"));

        int gridViewRows = GridView1.Rows.Count;
        GridView1.DataBind();
        gridViewRows = GridView1.Rows.Count;

        int MaxId;

        if (gridViewRows == 0)
        {
            MaxId = 1000;
        }
        else
        {
            MaxId = Convert.ToInt16(xmlDoc.SelectSingleNode("//Game[not(@GameCode < //Game/@id)]/@GameCode").Value);
            MaxId++;
        }

        // יצירת ענף משחק     
        XmlElement myNewGame = xmlDoc.CreateElement("Game");
        myNewGame.SetAttribute("GameCode", MaxId.ToString());
        myNewGame.SetAttribute("isPublish", "false");
        myNewGame.SetAttribute("timePerQuest", "30");


        // יצירת ענף שם משחק
        XmlElement myNewGameSubject = xmlDoc.CreateElement("GameSubject");
        XmlText subjectTXT = myGameDoc.CreateTextNode(addNameTB.Text);
        myNewGameSubject.AppendChild(subjectTXT);
        myNewGame.AppendChild(myNewGameSubject);

        // יצירת ענף שאלות
        XmlElement myNewGameQuetions = xmlDoc.CreateElement("questions");
        myNewGame.AppendChild(myNewGameQuetions);

       
        XmlNode FirstGame = xmlDoc.SelectNodes("/WildWestGame/Game").Item(0);
        xmlDoc.SelectSingleNode("/WildWestGame").InsertBefore(myNewGame, FirstGame);

        XmlDataSource1.Save();
        GridView1.DataBind();
        addNameTB.Text = "";
    }

    protected void isPublish_CheckedChanged(object sender, EventArgs e)
    {
        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        //string gamecodeToPublish = Session["GameCodeSession"].ToString();

        // תחילה אנו בודקים מהו הקוד של המשחק הזה בעץ 
        CheckBox myCheckBox = (CheckBox)sender;

        // מושכים את הקוד באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theCode = myCheckBox.Attributes["theItemId"];


        //שאילתא למציאת המשחק שברצוננו לעדכן
        XmlNode theGame = xmlDoc.SelectSingleNode("/WildWestGame/Game[@GameCode='" + theCode + "']");

        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPablish = myCheckBox.Checked;

        //עדכון של המאפיין בעץ
        theGame.Attributes["isPublish"].InnerText = NewIsPablish.ToString();
      


        //שמירה בעץ והצגה
        XmlDataSource1.Save();
        GridView1.DataBind();

        checkAllGamePublish();
    }

    protected void deleteRow(object sender, EventArgs e)
    {

        string gamecodeToDelete = Session["GameCodeSession"].ToString();

        XmlDocument Document = XmlDataSource1.GetXmlDocument();

        XmlNode node = Document.SelectSingleNode("/WildWestGame/Game[@GameCode='" + gamecodeToDelete + "']");
        node.ParentNode.RemoveChild(node);

        XmlDataSource1.Save();
        GridView1.DataBind();
        ((Panel)FindControl("Wrap_deleteGame")).CssClass = "hideAlert";


    }

    protected void rowCommand(object sender, GridViewCommandEventArgs e)
    {
        ImageButton i = (ImageButton)e.CommandSource;

        string theId = i.Attributes["theItemId"];
        Session["GameCodeSession"] = theId;


        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                AlertFunction_BackToMyGames();
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":
                Response.Redirect("edit.aspx");
                break;

            //אם נלחץ על כפתור הגדרות יקרא לפונקציה של הגדרות                    
            case "SettingsRow":
                Response.Redirect("settings.aspx");
                break;

            ////אם נלחץ על כפתור צ'ק בוקס                    
            //case "publishRow":
            //    isPublish_CheckedChanged(e);
            //    break;
        }
    }

    //אל תמחק- מחיקת משחק
    protected void No_CloseAlert_BackToMyGames(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteGame")).CssClass = "hideAlert";
    }
    //חלונית למחיקת משחק
    protected void AlertFunction_BackToMyGames()
    {
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        string gamecodeToDelete = Session["GameCodeSession"].ToString();
        XmlNode GameName = Document.SelectSingleNode("/WildWestGame/Game[@GameCode='" + gamecodeToDelete + "']/GameSubject");
        string GameNameToDelete = GameName.InnerXml.ToString();
        deleteGame_label.Text = "המשחק " + GameNameToDelete +" ימחק"+"</br>"+"האם אתה בטוח?";
        ((Panel)FindControl("Wrap_deleteGame")).CssClass = "showAlert";


    }
}



    