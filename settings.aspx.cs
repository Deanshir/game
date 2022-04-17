using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Newtonsoft.Json;


public partial class settings : System.Web.UI.Page
{
    protected void Page_init(object sender, EventArgs e)
    {
        //קבלת המשחק שממנו הגענו לעמוד הגדרות
        string GameCode = Session["GameCodeSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("trees/Wildwest.xml"));

        //הכנסת שם המשחק שהמשתמש כתב לתיבת הטקסט
        GameSubjectName.Text = myDoc.SelectNodes("/WildWestGame/Game[@GameCode='" + GameCode + "']/GameSubject").Item(0).InnerXml;

        //מציאת המשחק 
        XmlNode game = myDoc.SelectNodes("/WildWestGame/Game[@GameCode='" + GameCode + "']").Item(0);
        int currentTime = Convert.ToInt16(game.Attributes["timePerQuest"].Value);

        TimeLimitPerQ.SelectedValue = currentTime.ToString();
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cancelChanges_Click(object sender, EventArgs e)
    {
        Response.Redirect("myGames.aspx"); //חזרה למשחקים שלי
    }

    protected void saveChanges_Click(object sender, EventArgs e)
    {
        //טעינת העץ
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("trees/Wildwest.xml"));

        ////בדיקה שכל השינויים עומדים בתנאים
        //if (GameSubjectName.Text.Length < 2 && GameSubjectName.Text.Length > 20)
        //{
        //    //תצבע את התווים באדום
        //}
        //else
        //{
        //קבלת המשחק שממנו הגענו לעמוד הגדרות
        string GameCode = Session["GameCodeSession"].ToString();

        //מציאת הענף לשינוי שם המשחק
        XmlNode gameSubject = myDoc.SelectNodes("/WildWestGame/Game[@GameCode='" + GameCode + "']/GameSubject").Item(0);
        gameSubject.InnerText = GameSubjectName.Text;

        //מציאת המשחק ושינוי הזמן
        XmlNode game = myDoc.SelectNodes("/WildWestGame/Game[@GameCode='" + GameCode + "']").Item(0);
        game.Attributes["timePerQuest"].Value = TimeLimitPerQ.SelectedValue;

        //שמירת העץ החדש
        myDoc.Save(Server.MapPath("trees/Wildwest.xml"));
        Response.Redirect("myGames.aspx"); //חזרה למשחקים שלי




    }


}