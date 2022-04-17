<%@ WebHandler Language="C#" Class="Handler" %>
using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string gameCode = context.Request["GameCode"]; //קוד המשחק שנשלח מאנימייט

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(context.Server.MapPath("trees/Wildwest.xml")); //טעינת העץ שלכם

        XmlNode gameNode = myDoc.SelectSingleNode("//Game[@GameCode='" + gameCode + "']"); //שליפת הענף של המשחק המתאים

        if (gameNode != null) //אם קיים משחק שתואם לקוד
        {
            //כאן תתבצע הבדיקה לפרסום
            if(gameNode.Attributes["isPublish"].InnerText=="False")
            {
                    context.Response.Write("notpublish"); //שליחת תשובה שהמשחק טרם פורסם
            }
            else
            {

            //ההמרה לג'ייסון תתבצע רק אם המשחק קיים ומפורסם
            string jsonText = JsonConvert.SerializeXmlNode(gameNode); //המרת הענף מהעץ לטקסט במבנה של ג'ייסון
            context.Response.Write(jsonText); //שליחת המחרוזת אל אנימייט
            }

        }
        else //אם המשחק לא קיים
        {
            context.Response.Write("noGameFound"); //שליחת תשובה שלא נמצא משחק
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
















