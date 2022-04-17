using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
using Newtonsoft.Json;

public partial class edit : System.Web.UI.Page
{
    //נתיב לשמירה התמונות
    string imagesLibPath = "uploadedFiles/";

    bool Check_Saving_BeforeExit = false;  //משתנה ששומר האם נשמרו הנתונים לפני חזרה למשחקים שלי

    protected void Page_init(object sender, EventArgs e)
    {

        //קבלת המשחק שממנו הגענו לעמוד העריכה
        string GameCode = Session["GameCodeSession"].ToString();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("trees/Wildwest.xml"));
        //התאמת השאלות למשחק שנבחר
        XmlDataSource1.XPath = "/WildWestGame/Game[@GameCode=" + GameCode + "]/questions/question";

        GameSubjectTXT.Text = myDoc.SelectSingleNode("/WildWestGame/Game[@GameCode='" + GameCode + "']/GameSubject").InnerText;

        //מציאת כמות השאלות
        XmlNodeList QuestionNum = myDoc.SelectNodes("/WildWestGame/Game[@GameCode='" + GameCode + "']/questions/question");
        int QuestionCounter = QuestionNum.Count;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i <= 5; i++)
        {
            ((ImageButton)FindControl("ImageforUpload" + i)).ImageUrl = ((HiddenField)FindControl("imgKeeper" + i)).Value;
            if (((HiddenField)FindControl("picPanelKeeper" + i)).Value == "true")
            {
                ((ImageButton)FindControl("glass" + i)).Style.Add("display", "block");
                ((ImageButton)FindControl("editPic" + i)).Style.Add("display", "block");
                ((ImageButton)FindControl("deletPic" + i)).Style.Add("display", "block");
            }
            else
            {
                ((ImageButton)FindControl("glass" + i)).Style.Add("display", "none");
                ((ImageButton)FindControl("editPic" + i)).Style.Add("display", "none");
                ((ImageButton)FindControl("deletPic" + i)).Style.Add("display", "none");
            }

        }

        
    }

    protected void rowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void addingAnswer_function(object sender, EventArgs e)
    {
        if (((HiddenField)FindControl("savingQClickorNot")).Value == "false") //אם מי ששלח את הפוסטבק הוא לא כפתור שמירת שאלה
        {
            for (int j = 0; j <= 5; j++)
            {
                if (j > 2) //מסיחים אופציונאליים
                {
                    if (((Panel)FindControl("PanelAns" + (j - 1))).Visible == true) //בדירה האם הפאנל פתוח
                    {

                        if (((ImageButton)FindControl("ImageforUpload3")).ImageUrl != "~/image/pic_upload.png")
                        {
                            if (((HiddenField)FindControl("SaveNamePic3")).Value == "")
                            {
                                ((HiddenField)FindControl("SaveNamePic3")).Value = UpPic(3);
                                Console.WriteLine(((HiddenField)FindControl("SaveNamePic3")).Value);
                            }
                        }
                        if (((ImageButton)FindControl("ImageforUpload4")).ImageUrl != "~/image/pic_upload.png")
                        {
                            if (((HiddenField)FindControl("SaveNamePic4")).Value == "")
                            {
                                ((HiddenField)FindControl("SaveNamePic4")).Value = UpPic(4);
                                Console.WriteLine(((HiddenField)FindControl("SaveNamePic4")).Value);
                            }
                        }
                        if (((ImageButton)FindControl("ImageforUpload5")).ImageUrl != "~/image/pic_upload.png")
                        {
                            if (((HiddenField)FindControl("SaveNamePic5")).Value == "")
                            {
                                ((HiddenField)FindControl("SaveNamePic5")).Value = UpPic(5);
                                Console.WriteLine(((HiddenField)FindControl("SaveNamePic5")).Value);
                            }
                        }

                    }

                }
                else //מסיחים רגילים
                {
                    if (((ImageButton)FindControl("ImageforUpload0")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic0")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic0")).Value = UpPic(0);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic0")).Value);
                        }
                    }
                    if (((ImageButton)FindControl("ImageforUpload1")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic1")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic1")).Value = UpPic(1);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic1")).Value);
                        }
                    }
                    if (((ImageButton)FindControl("ImageforUpload2")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic2")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic2")).Value = UpPic(2);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic2")).Value);
                        }
                    }
                }

            }
        }

        //מציאת כמות המסיחים והעלה ב 1
        int CounterAns = Convert.ToInt16(((HiddenField)FindControl("AddingAnswerCounter")).Value);
            CounterAns++;
            ((HiddenField)FindControl("AddingAnswerCounter")).Value = CounterAns.ToString();

            if (PanelAns4.Visible == true && PanelAns3.Visible == true && PanelAns2.Visible == true) //אם יש כבר 3 תיבות טקסט
            {


            }
             else //אם אין 3 תיבות טקסט
             {
            if (PanelAns3.Visible == true) //אם יש כבר 2 תיבות טקסט
            {
                PanelAns4.Visible = true; //תוסיף תיבה 3
                addingAnswer.Enabled = false; //מקסימום מסיחים נוספו כפתור לא פעיל יותר
                addingAnswer.Style.Add("oppacity", "0.5");
            }
            else //אם אין 2 תיבות טקסט
            {
                addingAnswer.Enabled = true; //כפתור פעיל
                addingAnswer.Style.Add("oppacity", "1");
                if (PanelAns2.Visible == true) // אם יש 1 תיבות טקסט
                {
                    PanelAns3.Visible = true; //תוסיף תיבה 2

                }
                else //אם אין 1 תיבות טקסט
                {
                    PanelAns2.Visible = true; //תוסיף תיבה 2
                }
            }
        }
    }
    protected void restart_Click(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_reset")).CssClass = "hideAlert";
        Restart();

    }
    protected void DeleteAns2_Click(object sender, EventArgs e)
    {
        //הפעלת כפתור הוספת מסיח
        addingAnswer.Style.Add("oppacity", "1");

            XmlDocument Document = new XmlDocument();
            string gamecodeToDel = Session["GameCodeSession"].ToString(); //סשן של קוד המשחק
            Document.Load(Server.MapPath("trees/Wildwest.xml"));
       

        if (PanelAns3.Visible == false)//אם מסיח 3 סגור
            { //עריכת תמונה- מחיקת תמונה מהעץ
              //משחק חדש- מחיקת תמונה מהבמה 

                incorrect2.Text = ""; //תאפס את מסיח 2
                ((ImageButton)FindControl("ImageforUpload3")).ImageUrl = "~/image/pic_upload.png";
                ((HiddenField)FindControl("SaveNamePic3")).Value = "";


            if (SaveChanges.Visible == true) //האם אתה במצב עריכה
                {
                    string questionIDToDel = Session["QuestionIDSession"].ToString(); //קוד המשחק הנוכחי  
                                                                                      //מספר המסיח למחיקה
                    XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns=2]").Item(0);

                    //מציאת כמות המסיחים
                    XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer");
                    int n = answersNum.Count;

                    if (n >= 3)//אם בשאלה המקורית יש יותר מ-3 מסיחים
                    {
                        //תמחק את הענף של המסיח
                        //incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                        //תצמצם את המסיחים בהתאם
                        for (int i = 3; i < n; i++)
                        {
                            XmlNode AnswerID = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns='" + i + "']").Item(0);
                            AnswerID.Attributes["idAns"].Value = (i - 1).ToString();
                            
                        }

                        Document.Save(Server.MapPath("trees/Wildwest.xml"));
                    }

                }
                PanelAns2.Visible = false;//תסתיר את פאנל 2

        }

            if (PanelAns3.Visible == true && PanelAns4.Visible == false)//אם מסיח 3 פתוח ומסיח 4 סגור
            {
                //תצמצם את המסיחים
                incorrect2.Text = incorrect3.Text;

            incorrect3.Text = ""; 
             ((ImageButton)FindControl("ImageforUpload3")).ImageUrl = "~/image/pic_upload.png";


            if (SaveChanges.Visible == true)//אם אתה במצב עריכה
                {
                    string questionIDToDel = Session["QuestionIDSession"].ToString(); //סשן של מספר השאלה  
                    XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns=2]").Item(0);

                    //מציאת כמות המסיחים
                    XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer");
                    int n = answersNum.Count;

                    if (n > 4)//אם בשאלה המקורית יש יותר מ-3 מסיחים
                    {
                        //תמחק את הענף של המסיח
                        //incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                        //תצמצם את המסיחים בהתאם
                        for (int i = 3; i < n; i++)
                        {
                            XmlNode AnswerID = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns='" + i + "']").Item(0);
                            AnswerID.Attributes["idAns"].Value = (i - 1).ToString();
                        }
                        Document.Save(Server.MapPath("trees/Wildwest.xml"));
                    }

                }
                PanelAns3.Visible = false;
        }

            if (PanelAns4.Visible == true)//אם מסיח 4 פתוח
            {
                //תצמצם את המסיחים
                incorrect2.Text = incorrect3.Text;


            incorrect3.Text = incorrect4.Text;


            incorrect4.Text = "";
            ((ImageButton)FindControl("ImageforUpload3")).ImageUrl = "~/image/pic_upload.png";

            if (SaveChanges.Visible == true)//אם אתה במצב עריכה
                {
                    string questionIDToDel = Session["QuestionIDSession"].ToString(); //סשן של מספר השאלה  
                    XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns=2]").Item(0);

                    //מציאת כמות המסיחים
                    XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer");
                    int n = answersNum.Count;

                    if (n == 5)//אם בשאלה המקורית יותר מ-4 מסיחים
                    {
                        //תמחק את הענף של המסיח
                        //incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                        //תצמצם את המסיחים בהתאם
                        for (int i = 3; i < n; i++)
                        {
                            XmlNode AnswerID = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns='" + i + "']").Item(0);
                            AnswerID.Attributes["idAns"].Value = (i - 1).ToString();
                        }
                        Document.Save(Server.MapPath("trees/Wildwest.xml"));
                    }

                }
                PanelAns4.Visible = false;
            //alert.Text = "";
            ((Panel)FindControl("Wrap_deleteAns2")).CssClass = "hideAlert";


        }
        ((Panel)FindControl("Wrap_deleteAns2")).CssClass = "hideAlert";
        int CounterAns = Convert.ToInt16(((HiddenField)FindControl("AddingAnswerCounter")).Value);
        CounterAns--;
        ((HiddenField)FindControl("AddingAnswerCounter")).Value = CounterAns.ToString();
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();

    }
    protected void DeleteAns3_Click(object sender, EventArgs e)
    {
        //הפעלת כפתור הוספת מסיח
        addingAnswer.Enabled = true; 
        addingAnswer.Style.Add("oppacity", "1");

        XmlDocument Document = new XmlDocument();
        string gamecodeToDel = Session["GameCodeSession"].ToString(); //סשן של קוד המשחק
        Document.Load(Server.MapPath("trees/Wildwest.xml"));


        if (PanelAns4.Visible == false)//אם מסיח 4 סגור
        {
            incorrect3.Text = "";//תאפס את מסיח 3
            if (SaveChanges.Visible == true)//אם נמצאים במצב עריכה
            {
                string questionIDToDel = Session["QuestionIDSession"].ToString(); //קוד המשחק  
                XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns=3]").Item(0);//מספר השאלה למחיקה

                //מציאת כמות המסיחים
                XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer");
                int n = answersNum.Count;

                if (n > 4)//אם בשאלה המקורית יש יותר מ-4 מסיחים
                {
                    //תמחק את הענף של המסיח
                    //incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                    //תצמצם את המסיחים בהתאם
                    for (int i = 4; i < n; i++)
                    {
                        XmlNode AnswerID = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns='" + i + "']").Item(0);
                        AnswerID.Attributes["idAns"].Value = (i - 1).ToString();
                    }
                    Document.Save(Server.MapPath("trees/Wildwest.xml"));
                }
            }
            PanelAns3.Visible = false;
        }
        if (PanelAns4.Visible == true)//אם מסיח 4 פתוח
        {
            incorrect3.Text = incorrect4.Text;
            incorrect4.Text = "";
            if (SaveChanges.Visible == true)
            {
                string questionIDToDel = Session["QuestionIDSession"].ToString(); //סשן של מספר השאלה  
                XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns=3]").Item(0);

                //מציאת כמות המסיחים
                XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer");
                int n = answersNum.Count;

                if (n == 5)//אם בשאלה המקורית יש יותר מ-4 מסיחים
                {
                    //תמחק את הענף של המסיח
                    //incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                    //תצמצם את המסיחים בהתאם
                    for (int i = 4; i < n; i++)
                    {
                        XmlNode AnswerID = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns='" + i + "']").Item(0);
                        AnswerID.Attributes["idAns"].Value = (i - 1).ToString();
                    }
                    Document.Save(Server.MapPath("trees/Wildwest.xml"));
                }
                PanelAns4.Visible = false;
            }
        }
        ((Panel)FindControl("Wrap_deleteAns3")).CssClass = "hideAlert";
        int CounterAns = Convert.ToInt16(((HiddenField)FindControl("AddingAnswerCounter")).Value);
        CounterAns--;
        ((HiddenField)FindControl("AddingAnswerCounter")).Value = CounterAns.ToString();
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();

    }
    protected void DeleteAns4_Click(object sender, EventArgs e)
    {
       
        //הפעלת כפתור הוספת מסיח
        addingAnswer.Enabled = true;
        addingAnswer.Style.Add("oppacity", "1");

        XmlDocument Document = new XmlDocument();
        string gamecodeToDel = Session["GameCodeSession"].ToString(); //סשן של קוד המשחק
        Document.Load(Server.MapPath("trees/Wildwest.xml"));

        incorrect4.Text = "";
        if (SaveChanges.Visible == true)
        {
            string questionIDToDel = Session["QuestionIDSession"].ToString(); //סשן של מספר השאלה  
            XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer[@idAns=4]").Item(0);

            //מציאת כמות המסיחים
            XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDel + "']/questions/question[@id='" + questionIDToDel + "']/answers/answer");
            int n = answersNum.Count;

            if (n == 5)//אם בשאלה המקורית יש יותר מ-4 מסיחים
            {
                //תמחק את הענף של המסיח
                //incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                Document.Save(Server.MapPath("trees/Wildwest.xml"));
            }
        }
        PanelAns4.Visible = false;
        ((Panel)FindControl("Wrap_deleteAns4")).CssClass = "hideAlert";
        int CounterAns = Convert.ToInt16(((HiddenField)FindControl("AddingAnswerCounter")).Value);
        CounterAns--;
        ((HiddenField)FindControl("AddingAnswerCounter")).Value = CounterAns.ToString();
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();

    }

    //מחיקת תמונה מתיבת גזע השאלה
    protected void deleteImg0(object sender, EventArgs e)
    {
        ((ImageButton)FindControl("ImageforUpload0")).ImageUrl = "~/image/pic_upload.png";
        ImageforUpload0.Style.Add("opacity", "1");
        picTitle.Style.Add("display", "block");
        glass0.Style.Add("display", "none");
        editPic0.Style.Add("display", "none");
        deletPic0.Style.Add("display", "none");
        ImageforUpload0.Enabled = true;

        if (SaveChanges.Visible == false)// עורך במצב יצירת שאלה
        {
            ((HiddenField)FindControl("imgKeeper0")).Value = "~/image/pic_upload.png";
            ((HiddenField)FindControl("SaveNamePic0")).Value = "";
        } 

      }
     
    //מחיקת תמונה ממסיח נכון
    protected void deleteImg1(object sender, EventArgs e)
    {
            ((ImageButton)FindControl("ImageforUpload1")).ImageUrl = "~/image/pic_upload.png";
            ImageforUpload1.Style.Add("opacity", "1");
            glass1.Style.Add("display", "none");
            editPic1.Style.Add("display", "none");
            deletPic1.Style.Add("display", "none");
            ImageforUpload1.Enabled = true;
            
            ((HiddenField)FindControl("imgKeeper1")).Value = "~/image/pic_upload.png";
             correct.Enabled = true;
             correct.Style.Add("opacity", "1");


        if (SaveChanges.Visible == false)// עורך במצב יצירת שאלה
            {
                ((HiddenField)FindControl("SaveNamePic1")).Value = "";
            }
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();


    }
    //מחיקת מסיח שגוי
    protected void deleteImg2(object sender, EventArgs e)
    {
        ((ImageButton)FindControl("ImageforUpload2")).ImageUrl = "~/image/pic_upload.png";
        ImageforUpload2.Style.Add("opacity", "1");
        glass2.Style.Add("display", "none");
        editPic2.Style.Add("display", "none");
        deletPic2.Style.Add("display", "none");
        ImageforUpload2.Enabled = true;

       ((HiddenField)FindControl("imgKeeper2")).Value = "~/image/pic_upload.png";

        incorrect1.Enabled = true;
        incorrect1.Style.Add("opacity", "1");

        if (SaveChanges.Visible == false)// עורך במצב יצירת שאלה
        {
            ((HiddenField)FindControl("SaveNamePic2")).Value = "";
        }
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();


    }

    //מחיקת מסיח 1
    protected void deleteImg3(object sender, EventArgs e)
    {
        ((ImageButton)FindControl("ImageforUpload3")).ImageUrl = "~/image/pic_upload.png";
        ImageforUpload3.Style.Add("opacity", "1");
        glass3.Style.Add("display", "none");
        editPic3.Style.Add("display", "none");
        deletPic3.Style.Add("display", "none");
        ImageforUpload3.Enabled = true;

        ((HiddenField)FindControl("imgKeeper3")).Value = "~/image/pic_upload.png";

        incorrect2.Enabled = true;
        incorrect2.Style.Add("opacity", "1");

        if (SaveChanges.Visible == false)// עורך במצב יצירת שאלה
        {
            ((HiddenField)FindControl("SaveNamePic3")).Value = "";
        }
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();


    }

    //מחיקת מסיח 2
    protected void deleteImg4(object sender, EventArgs e)
    {
        ((ImageButton)FindControl("ImageforUpload4")).ImageUrl = "~/image/pic_upload.png";
        ImageforUpload4.Style.Add("opacity", "1");
        glass4.Style.Add("display", "none");
        editPic4.Style.Add("display", "none");
        deletPic4.Style.Add("display", "none");
        ImageforUpload4.Enabled = true;
        
        ((HiddenField)FindControl("imgKeeper4")).Value = "~/image/pic_upload.png";

        incorrect3.Enabled = true;
        incorrect3.Style.Add("opacity", "1");

        if (SaveChanges.Visible == false)// עורך במצב יצירת שאלה
        {
            ((HiddenField)FindControl("SaveNamePic4")).Value = "";
        }
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();


    }

    //מחיקת מסיח 3
    protected void deleteImg5(object sender, EventArgs e)
    {
        ((ImageButton)FindControl("ImageforUpload5")).ImageUrl = "~/image/pic_upload.png";
        ImageforUpload5.Style.Add("opacity", "1");
        glass5.Style.Add("display", "none");
        editPic5.Style.Add("display", "none");
        deletPic5.Style.Add("display", "none");
        ImageforUpload5.Enabled = true;
            
        ((HiddenField)FindControl("imgKeeper5")).Value = "~/image/pic_upload.png";

        incorrect4.Enabled = true;
        incorrect4.Style.Add("opacity", "1");

        if (SaveChanges.Visible == false)// עורך במצב יצירת שאלה
        {
            ((HiddenField)FindControl("SaveNamePic5")).Value = "";
        }
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();


    }

    // פונקציה המקבלת את התמונה שהועלתה , האורך והרוחב שאנו רוצים לתמונה ומחזירה את התמונה המוקטנת
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    protected void savingQuestion_Click(object sender, EventArgs e)
    {
        EditStatus.Value = "false";
        for (int j = 0; j <= 5; j++)
        {
            if (j > 2) //מסיחים אופציונאליים
            {
                if (((Panel)FindControl("PanelAns" + (j - 1))).Visible == true) //בדירה האם הפאנל פתוח
                {

                    if (((ImageButton)FindControl("ImageforUpload3")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic3")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic3")).Value = UpPic(3);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic3")).Value);
                        }
                    }
                    if (((ImageButton)FindControl("ImageforUpload4")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic4")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic4")).Value = UpPic(4);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic4")).Value);
                        }
                    }
                    if (((ImageButton)FindControl("ImageforUpload5")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic5")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic5")).Value = UpPic(5);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic5")).Value);
                        }
                    }

                }

            }
            else //מסיחים רגילים
            {
                if (((ImageButton)FindControl("ImageforUpload0")).ImageUrl != "~/image/pic_upload.png")
                {
                    if (((HiddenField)FindControl("SaveNamePic0")).Value == "")
                    {
                        ((HiddenField)FindControl("SaveNamePic0")).Value = UpPic(0);
                        Console.WriteLine(((HiddenField)FindControl("SaveNamePic0")).Value);
                    }
                }
                if (((ImageButton)FindControl("ImageforUpload1")).ImageUrl != "~/image/pic_upload.png")
                {
                    if (((HiddenField)FindControl("SaveNamePic1")).Value == "")
                    {
                        ((HiddenField)FindControl("SaveNamePic1")).Value = UpPic(1);
                        Console.WriteLine(((HiddenField)FindControl("SaveNamePic1")).Value);
                    }
                }
                if (((ImageButton)FindControl("ImageforUpload2")).ImageUrl != "~/image/pic_upload.png")
                {
                    if (((HiddenField)FindControl("SaveNamePic2")).Value == "")
                    {
                        ((HiddenField)FindControl("SaveNamePic2")).Value = UpPic(2);
                        Console.WriteLine(((HiddenField)FindControl("SaveNamePic2")).Value);
                    }
                }
            }

        }

        ((HiddenField)FindControl("savingQClickorNot")).Value = "true";
            string GameCode = Session["GameCodeSession"].ToString();
            XmlDocument Document = XmlDataSource1.GetXmlDocument();
            Document.Load(Server.MapPath("trees/Wildwest.xml"));
            XmlNodeList qNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + GameCode + "']/questions/question");
            int maxId = qNum.Count;

            //משתנה לבדיקת האם יש לפחות שאלה אחת במשחק
            XmlNode isThereFirstQ = Document.SelectSingleNode("/WildWestGame/Game[@GameCode = '" + GameCode + "']/questions").ChildNodes[0];

            if (isThereFirstQ != null) //אם קיימת שאלה
            {
                maxId++; //הקפצת ה-id
            }
            else
            {
                maxId = 1; //הגדרת ה-id ל-1
            }

            //יצירת ענף שאלה חדשה
            XmlElement newQuestion = Document.CreateElement("question");
            newQuestion.SetAttribute("id", maxId.ToString());

            //יצירת ענף לגזע שאלה
            XmlElement questionText = Document.CreateElement("questionText");
            XmlText questionText_txt = Document.CreateTextNode(question.Text);
            questionText.AppendChild(questionText_txt);
            newQuestion.AppendChild(questionText);

            Document.SelectSingleNode("//questions").AppendChild(newQuestion);

            //יצירת ענף תמונה לשאלה
            XmlElement imgQuestion = Document.CreateElement("img");
           // string fileType = ((FileUpload)FindControl("FileUpload0")).PostedFile.ContentType;
            XmlText imgQuestion_txt;
            if (((HiddenField)FindControl("SaveNamePic0")).Value!="") //בדיקה האם הקובץ שהוכנס הוא תמונה
            {

                imgQuestion_txt = Document.CreateTextNode(((HiddenField)FindControl("SaveNamePic0")).Value);
            }
            else
            {
                imgQuestion_txt = Document.CreateTextNode("");
            }

            imgQuestion.AppendChild(imgQuestion_txt);
            newQuestion.AppendChild(imgQuestion);


            //יצירת ענף למסיחים
            XmlElement answers = Document.CreateElement("answers");
            newQuestion.AppendChild(answers);

            //יצירת ענף לתשובה נכונה
            XmlElement correctAns = Document.CreateElement("answer");
            correctAns.SetAttribute("idAns", "0");
            XmlText correctAns_txt;
            if (correct.Text != "")
            {
                correctAns.SetAttribute("AnsType", "txt");
                correctAns_txt = Document.CreateTextNode(correct.Text);

            }
            else
            {
                correctAns.SetAttribute("AnsType", "img");
                correctAns_txt = Document.CreateTextNode(((HiddenField)FindControl("SaveNamePic1")).Value);
            }
            correctAns.SetAttribute("feedback", "true");
            correctAns.AppendChild(correctAns_txt);
            answers.AppendChild(correctAns);

            //יצירת ענף לתשובה שגויה
            XmlElement inCorrectAns = Document.CreateElement("answer");
            inCorrectAns.SetAttribute("idAns", "1");
            XmlText inCorrectAns_txt;
            if (incorrect1.Text != "")
            {
                inCorrectAns.SetAttribute("AnsType", "txt");
                inCorrectAns_txt = Document.CreateTextNode(incorrect1.Text);

            }
            else
            {
                inCorrectAns.SetAttribute("AnsType", "img");
                inCorrectAns_txt = Document.CreateTextNode(((HiddenField)FindControl("SaveNamePic2")).Value);
            }
            inCorrectAns.SetAttribute("feedback", "false");
            inCorrectAns.AppendChild(inCorrectAns_txt);
            answers.AppendChild(inCorrectAns);

            //יצירת ענף למסיחים שגויים
            for (int x = 2; x <= 4; x++)
            {
            if (((Panel)FindControl("PanelAns" + x)).Visible == true)
            {
                if ((((ImageButton)FindControl("ImageforUpload" + (x+1))).ImageUrl == "~/image/pic_upload.png") && (((TextBox)FindControl("incorrect" + x)).Text == ""))
                {
           
                }
                else
                {
                    XmlElement inCorrectAnswer = Document.CreateElement("answer");
                    inCorrectAnswer.SetAttribute("idAns", x.ToString());
                    if (((TextBox)FindControl("incorrect" + x)).Text != "")
                    {
                        inCorrectAnswer.SetAttribute("AnsType", "txt");
                        XmlText inCorrectAnswer_txt;
                        inCorrectAnswer_txt = Document.CreateTextNode(((TextBox)FindControl("incorrect" + x)).Text);
                        inCorrectAnswer.AppendChild(inCorrectAnswer_txt);
                    }
                    else
                    {
                            inCorrectAnswer.SetAttribute("AnsType", "img");
                            if (x == 2)
                            {
                                XmlText inCorrectAnswer_txt = Document.CreateTextNode(((HiddenField)FindControl("SaveNamePic3")).Value);
                                inCorrectAnswer.AppendChild(inCorrectAnswer_txt);

                            }
                            if (x == 3)
                            {
                                XmlText inCorrectAnswer_txt = Document.CreateTextNode(((HiddenField)FindControl("SaveNamePic4")).Value);
                                inCorrectAnswer.AppendChild(inCorrectAnswer_txt);
                            }
                            if (x == 4)
                            {
                                XmlText inCorrectAnswer_txt = Document.CreateTextNode(((HiddenField)FindControl("SaveNamePic5")).Value);
                                inCorrectAnswer.AppendChild(inCorrectAnswer_txt);
                            }
                    
                    }
                    inCorrectAnswer.SetAttribute("feedback", "false");
                    answers.AppendChild(inCorrectAnswer);
                }
            }
            }
        // שמירת העץ החדש
        Document.Save(Server.MapPath("trees/Wildwest.xml"));
        XmlDataSource1.Save();
        GridView2.DataBind();
        Restart();
    }
   
    string UpPic(int i)
    {
        HttpPostedFile file = ((FileUpload)FindControl("FileUpload" + i)).PostedFile;
        // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
        string fileName = ((FileUpload)FindControl("FileUpload" + i)).PostedFile.FileName;

        //// הסיומת של הקובץ
        string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));

        //לקיחת הזמן האמיתי למניעת כפילות בתמונות
        string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss_ffff");

        // חיבור השם החדש עם הסיומת של הקובץ
        string imageNewName = myTime + i + endOfFileName;
        //שמירה של הקובץ לספרייה בשם החדש שלו
        //((FileUpload)FindControl("FileUpload" + i)).PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);

        FileUpload uploader = (FileUpload)FindControl("FileUpload" + i);
        // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(uploader.PostedFile.InputStream);

        //קריאה לפונקציה המקטינה את התמונה
        //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
        System.Drawing.Image objImage;
        if (i > 0)
        {
            objImage = FixedSize(bmpPostedImage, 100, 70);
        }
        else
        {
            objImage = FixedSize(bmpPostedImage, 174, 123);
        }
        //הצגה של הקובץ החדש מהספרייה
        ((ImageButton)FindControl("ImageforUpload" + i)).ImageUrl = imagesLibPath + imageNewName;
        //שמירת הקובץ בגודלו החדש בתיקייה
        objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);
        return imageNewName;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void deleteQuestionRow(object sender, EventArgs e)
    {

        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        string gamecodeToDelete = Session["GameCodeSession"].ToString(); //סשן של קוד המשחק
        string questionIDToDelete = Session["QuestionIDSession"].ToString(); //סשן של מספר השאלה
        int questionNum = Convert.ToInt16(questionIDToDelete); //משתנה ששומר את מספר השאלה למחיקה

        //חיפוש מספר השאלה הגדול ביותר
        XmlNodeList qNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDelete + "']/questions/question");
        int maxIdQuestion = qNum.Count;

        //פניה לשאלה ומחיקת השאלה
        XmlNode node = Document.SelectSingleNode("/WildWestGame/Game[@GameCode='" + gamecodeToDelete + "']/questions/question[@id='" + questionIDToDelete + "']");
        node.ParentNode.RemoveChild(node);

        //לולאה שעוברת על השאלות ומשנה את האי די שלהן
        //הלולאה מתחילה בשאלה שנמחקה ומסתיימת במקסימום השאלות
       if (maxIdQuestion > 1)
       {
          for (int i = questionNum+1; i <= maxIdQuestion; i++)
            {
                XmlNode questionID = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToDelete + "']/questions/question[@id='" + i + "']").Item(0);
                questionID.Attributes["id"].Value = (i - 1).ToString();
            }
        }
     
        //שמירת העץ החדש
        XmlDataSource1.Save();
        GridView2.DataBind();

        Restart();

        ((Panel)FindControl("Wrap_deleteQuest")).CssClass = "hideAlert";
        savingQuestion.Visible = true;
        backToMyGmaes.Visible = true;
        DontSaveChanges.Visible = false;
        SaveChanges.Visible = false;

        restart.Style.Add("background-color", "#197282");
        addingAnswer.Style.Add("background-color", "#197282");
        restart.Style.Add("color", "white");
        addingAnswer.Style.Add("color", "white");
        addingAnswer.Style.Add("opacity", "0.5");
        addingAnswer.Enabled = false;
        EditStatus.Value = "false";
    }

    protected void editQuestionRow()
    {

        EditStatus.Value = "true";
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        string gamecodeToEdit = Session["GameCodeSession"].ToString(); //סשן של קוד המשחק
        string questionIDToEdit = Session["QuestionIDSession"].ToString(); //סשן של מספר השאלה  

        Restart();
        savingQuestion.Visible = false;
        backToMyGmaes.Visible = false;
        DontSaveChanges.Visible = true;
        SaveChanges.Visible = true;
        restart.Style.Add("background-color", "#E2A76B");
        addingAnswer.Style.Add("background-color", "#E2A76B");
        restart.Style.Add("color", "black");
        addingAnswer.Style.Add("color", "black");
        addingAnswer.Style.Add("opacity", "1");
        questionChars.Style.Add("color", "black");
        addingAnswer.Enabled = true;
    
        //הכנסת גזע השאלה שנבחר לתיבת הטקסט
        question.Text = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/questionText").Item(0).InnerXml;
    
        //במידה ויש תמונה לגזע השאלה
        if (Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + 
            "']/img").Item(0).InnerText !="")
        {

            ImageforUpload0.ImageUrl = "uploadedFiles/"+ Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/img").Item(0).InnerXml;
            ImageforUpload0.Style.Add("width", "100");
            ImageforUpload0.Style.Add("height", "100");
            glass0.Style.Add("display", "block");
            editPic0.Style.Add("display", "block");
            deletPic0.Style.Add("display", "block");
            picTitle.Style.Add("display", "none");
            ((HiddenField)FindControl("SaveNamePic0")).Value = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/img").Item(0).InnerXml;
        }
        else
        {
            ImageforUpload0.ImageUrl = "~/image/pic_upload.png";
            ImageforUpload1.Enabled = true;
            glass0.Style.Add("display", "none");
            editPic0.Style.Add("display", "none");
            deletPic0.Style.Add("display", "none");
            picTitle.Style.Add("display", "block");
        }

        //הכנסת התשובה הנכונה של השאלה שנבחרה לתיבת הטקסט
        XmlNode typeCheck0 = Document.SelectSingleNode("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=0]/@AnsType");
        if (typeCheck0.InnerText=="txt")
        {
            correct.Text = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=0]").Item(0).InnerXml;
            glass1.Style.Add("display", "none");
            editPic1.Style.Add("display", "none");
            deletPic1.Style.Add("display", "none");
        }
        else
        {
            ImageforUpload1.ImageUrl = "uploadedFiles/" + Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=0]").Item(0).InnerXml;
            ((HiddenField)FindControl("imgKeeper1")).Value = ImageforUpload1.ImageUrl;

            correct.Style.Add("opacity", "0.5");
            correct.Enabled = false;
            glass1.Style.Add("display", "block");
            editPic1.Style.Add("display", "block");
            deletPic1.Style.Add("display", "block");
            ((HiddenField)FindControl("SaveNamePic1")).Value= Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=0]").Item(0).InnerXml;
        }

        
        //הכנסת מסיח 1 של השאלה שנבחרה לתיבת הטקסט
        XmlNode typeCheck1 = Document.SelectSingleNode("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=1]/@AnsType");
        if (typeCheck1.InnerText == "txt")
        {
            incorrect1.Text = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=1]").Item(0).InnerXml;

            glass2.Style.Add("display", "none");
            editPic2.Style.Add("display", "none");
            deletPic2.Style.Add("display", "none");
        }
        else
        {
            ImageforUpload2.ImageUrl = "uploadedFiles/" + Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=1]").Item(0).InnerXml;
            ((HiddenField)FindControl("imgKeeper2")).Value = ImageforUpload2.ImageUrl;

            incorrect1.Style.Add("opacity", "0.5");
            incorrect1.Enabled = false;
            glass2.Style.Add("display", "block");
            editPic2.Style.Add("display", "block");
            deletPic2.Style.Add("display", "block");
            ((HiddenField)FindControl("SaveNamePic2")).Value = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=1]").Item(0).InnerXml;
        }

        //איפוס מסיחי טעות
        if (PanelAns2.Visible == true)
        {
            incorrect2.Text = "";
            PanelAns2.Visible = false;
        }
        if (PanelAns3.Visible == true)
        {
            incorrect3.Text = "";
            PanelAns3.Visible = false;
        }
        if (PanelAns4.Visible == true)
        {
            incorrect4.Text = "";
            PanelAns4.Visible = false;
        }
       
        //מציאת כמות המסיחים
        XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer");
        int n = answersNum.Count;

        ((HiddenField)FindControl("AddingAnswerCounter")).Value = (n-1).ToString();
        //אם יש יותר מ 2 מסיחים- מעבר ללולאה
        if (n >= 2)
        {
            //לולאה שתצור את המסיחים הקיימים בשאלה ותכניס את הטקסט שלהם
            for (int i = 2; i < n; i++)
            {
                ((Panel)FindControl("panelAns" + i)).Visible = true;
                XmlNode typeCheckA = Document.SelectSingleNode("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns="+i+"]/@AnsType");
                if (typeCheckA.InnerText == "txt")
                {
                    ((TextBox)FindControl("incorrect" + i)).Text = answersNum.Item(i).InnerText;
                    

                    ((ImageButton)FindControl("glass" + (i + 1))).Style.Add("display", "none");
                    ((ImageButton)FindControl("editPic" + (i + 1))).Style.Add("display", "none");
                    ((ImageButton)FindControl("deletPic" + (i + 1))).Style.Add("display", "none");
                }
                else
                {
                    ((ImageButton)FindControl("ImageforUpload" + (i + 1))).ImageUrl = "uploadedFiles/" + Document.SelectNodes
                    ("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=" + i + "]").Item(0).InnerXml;

                    //תכניס את שם התמונה למשתנה ששומר אותה 
                    ((HiddenField)FindControl("imgKeeper" + (i+1))).Value = ((ImageButton)FindControl("ImageforUpload" + (i + 1))).ImageUrl;
                    ((HiddenField)FindControl("SaveNamePic"+ (i + 1))).Value= Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToEdit + "']/questions/question[@id='" + questionIDToEdit + "']/answers/answer[@idAns=" + i + "]").Item(0).InnerXml;
                    ((TextBox)FindControl("incorrect" + i)).Style.Add("opacity", "0.5");
                    ((TextBox)FindControl("incorrect" +i)).Enabled = false;

                    ((ImageButton)FindControl("glass" + (i + 1))).Style.Add("display", "block");
                    ((ImageButton)FindControl("editPic" + (i + 1))).Style.Add("display", "block");
                    ((ImageButton)FindControl("deletPic" + (i + 1))).Style.Add("display", "block");
                }
            }
        }
       
    }

    protected void backToMyGmaes_check(object sender, EventArgs e)
    {   //תנאי שבודק שאין נתונים שלא נשמרו
        if ((question.Text == "") && (correct.Text == "") && (incorrect1.Text == "") &&
          (((ImageButton)FindControl("ImageforUpload0")).ImageUrl == "~/image/pic_upload.png") &&
          (((ImageButton)FindControl("ImageforUpload1")).ImageUrl == "~/image/pic_upload.png") &&
          (((ImageButton)FindControl("ImageforUpload2")).ImageUrl == "~/image/pic_upload.png"))
        {
            Response.Redirect("myGames.aspx");
        }
        else {
            Alert_BackToMyGamesFunc();
        }


    }

    //עבודה עם כפתורי מחיקה ועריכת שאלה
    protected void rowCommand1(object sender, GridViewCommandEventArgs e)
    {
        ImageButton i = (ImageButton)e.CommandSource;

        string theId = i.Attributes["theItemId"];
        Session["QuestionIDSession"] = theId;


        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteQuestionRow":
                AlertFunction_deleteQuest();
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editQuestionRow":
                editQuestionRow();
                break;
        }
    }

    protected void DontSaveChanges_Click(object sender, EventArgs e)
    {
        Restart();

        savingQuestion.Visible = true;
        backToMyGmaes.Visible = true;
        DontSaveChanges.Visible = false;
        SaveChanges.Visible = false;

        restart.Style.Add("background-color", "#197282");
        addingAnswer.Style.Add("background-color", "#197282");
        restart.Style.Add("color", "white");
        addingAnswer.Style.Add("color", "white");
        addingAnswer.Style.Add("opacity", "0.5");
        addingAnswer.Enabled = false;
        EditStatus.Value = "false";

    }

    protected void SaveChanges_Click(object sender, EventArgs e)
    {
        for (int j = 0; j <= 5; j++)
            {
            if (j > 2) //מסיחים אופציונאליים
            {
                if (((Panel)FindControl("PanelAns" + (j - 1))).Visible == true) //בדירה האם הפאנל פתוח
                {

                    if (((ImageButton)FindControl("ImageforUpload3")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic3")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic3")).Value = UpPic(3);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic3")).Value);
                        }
                    }
                    if (((ImageButton)FindControl("ImageforUpload4")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic4")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic4")).Value = UpPic(4);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic4")).Value);
                        }
                    }
                    if (((ImageButton)FindControl("ImageforUpload5")).ImageUrl != "~/image/pic_upload.png")
                    {
                        if (((HiddenField)FindControl("SaveNamePic5")).Value == "")
                        {
                            ((HiddenField)FindControl("SaveNamePic5")).Value = UpPic(5);
                            Console.WriteLine(((HiddenField)FindControl("SaveNamePic5")).Value);
                        }
                    }

                }

            }
            else //מסיחים רגילים
            {
                if (((ImageButton)FindControl("ImageforUpload0")).ImageUrl != "~/image/pic_upload.png")
                {
                    if (((HiddenField)FindControl("SaveNamePic0")).Value == "")
                    {
                        ((HiddenField)FindControl("SaveNamePic0")).Value = UpPic(0);
                        Console.WriteLine(((HiddenField)FindControl("SaveNamePic0")).Value);
                    }
                }
                if (((ImageButton)FindControl("ImageforUpload1")).ImageUrl != "~/image/pic_upload.png")
                {
                    if (((HiddenField)FindControl("SaveNamePic1")).Value == "")
                    {
                        ((HiddenField)FindControl("SaveNamePic1")).Value = UpPic(1);
                        Console.WriteLine(((HiddenField)FindControl("SaveNamePic1")).Value);
                    }
                }
                if (((ImageButton)FindControl("ImageforUpload2")).ImageUrl != "~/image/pic_upload.png")
                {
                    if (((HiddenField)FindControl("SaveNamePic2")).Value == "")
                    {
                        ((HiddenField)FindControl("SaveNamePic2")).Value = UpPic(2);
                        Console.WriteLine(((HiddenField)FindControl("SaveNamePic2")).Value);
                    }
                }
            }

        }

            XmlDocument Document = XmlDataSource1.GetXmlDocument();
            string gamecodeToChange = Session["GameCodeSession"].ToString(); //סשן של קוד המשחק
            string questionIDToChange = Session["QuestionIDSession"].ToString(); //סשן של מספר השאלה   

            //טעינת הנתונים
            XmlDocument myDoc = XmlDataSource1.GetXmlDocument();
            Document.Load(Server.MapPath("trees/Wildwest.xml"));

            // שמירת שינויים טקסט גזע השאלה
            XmlNode questionText = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/questionText").Item(0);
            questionText.InnerText = question.Text; //שמירת הטקסט

            //שמירת שינויים תמונה גזע השאלה
            XmlNode imgQuest = Document.SelectSingleNode("/WildWestGame/Game[@GameCode = '" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/img"); //מציאת הענף
            //בדיקה אם הוכנסה תמונה
            if(((ImageButton)FindControl("ImageforUpload0")).ImageUrl != "~/image/pic_upload.png")
            {
            imgQuest.InnerXml =((HiddenField)FindControl("SaveNamePic0")).Value;
            }
            else
            {
            imgQuest.InnerXml = ""; //במידה ולא מכניס את הטקסט
            ((HiddenField)FindControl("SaveNamePic0")).Value = "";

            }

            //שמירת שינויים תשובה נכונה
            XmlNode correctAns = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns=0]").Item(0);
             if (((ImageButton)FindControl("ImageforUpload1")).ImageUrl == "~/image/pic_upload.png") //במידה והמסיח הוא טקסט
             {
                correctAns.InnerText = correct.Text; //הכנסת הטקסט החדש
                correctAns.Attributes["AnsType"].Value = "txt"; //שינוי סוג מסיח לטקסט
                ((HiddenField)FindControl("SaveNamePic1")).Value = "";

             }
        else //במידה והמסיח הוא תמונה
            {
                correctAns.InnerXml = ((HiddenField)FindControl("SaveNamePic1")).Value; // שמור את שם התמונה
                correctAns.Attributes["AnsType"].Value = "img"; //שינוי סוג מסיח לתמונה
            }


        //שמירת שינויים מסיח ראשון- חובה
        XmlNode incorrectAns1 = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns=1]").Item(0);
        if (incorrect1.Text != "") //במידה והמסיח הוא טקסט
        {
            incorrectAns1.InnerText = incorrect1.Text; //הכנסת הטקסט החדש
            incorrectAns1.Attributes["AnsType"].Value = "txt"; //שינוי סוג מסיח לטקסט
            ((HiddenField)FindControl("SaveNamePic2")).Value = "";

        }
        else //במידה והמסיח הוא תמונה
        {
            incorrectAns1.InnerXml = ((HiddenField)FindControl("SaveNamePic2")).Value; // שמור את שם התמונה
            incorrectAns1.Attributes["AnsType"].Value = "img"; //שינוי סוג מסיח לתמונה
        }

        //מציאת כמות המסיחים
        XmlNodeList answersNum = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer");
        int n = answersNum.Count;

        //שמירת שינויים במסיחים
        for (int AnsNum = 2; AnsNum <= 4; AnsNum++)
        {
            if (((Panel)FindControl("PanelAns" + AnsNum)).Visible == true)
            {
                XmlNode incorrectAns = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns=" + AnsNum + "]").Item(0);
                if (incorrectAns == null) //במידה והמסיח לא קיים בעץ
                {
                    if ((((ImageButton)FindControl("ImageforUpload" + (AnsNum + 1))).ImageUrl == "~/image/pic_upload.png") && (((TextBox)FindControl("incorrect" + AnsNum)).Text == ""))
                    {
                        //המסיח ריק ויש להתעלם ממנו
                    }
                    else {
                        //יצירת ענף מסיח חדש
                        XmlElement inCorrectAnsNew = Document.CreateElement("answer");
                        inCorrectAnsNew.SetAttribute("idAns", AnsNum.ToString()); //מספר מסיח
                        XmlText inCorrectAnsTxt;
                        if (((TextBox)FindControl("incorrect" + AnsNum)).Text != "") //אם המסיח טקסט
                        {
                            inCorrectAnsNew.SetAttribute("AnsType", "txt"); //הגדרת סוג מסיח
                            inCorrectAnsTxt = Document.CreateTextNode(((TextBox)FindControl("incorrect" + AnsNum)).Text); //הכנסת הטקסט
                            ((HiddenField)FindControl("SaveNamePic"+(AnsNum+1))).Value = "";

                            XmlNode imgToRemove = Document.SelectSingleNode("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns='"+AnsNum+"']");
                            imgToRemove.ParentNode.RemoveChild(imgToRemove);
                        }
                        else
                        {
                            inCorrectAnsNew.SetAttribute("AnsType", "img"); //הגדרת סוג מסיח תמונה
                            inCorrectAnsTxt = Document.CreateTextNode(((HiddenField)FindControl("SaveNamePic" + (AnsNum + 1))).Value); //הכנסת שם תמונה
                        }

                        inCorrectAnsNew.SetAttribute("feedback", "false"); //הגדרת מסיח כשגוי
                        Document.SelectSingleNode("/WildWestGame/Game[@GameCode = '" + gamecodeToChange + "']/ questions/ question[@id = '" + questionIDToChange + "']/answers").AppendChild(inCorrectAnsNew); //הכנסת התשובה החדשה לעץ
                        inCorrectAnsNew.AppendChild(inCorrectAnsTxt); //הכנסת טקסט לתשובה
                    }
             
                }
                else //המסיח קיים בעץ
                {
                    //אם לא הוזן כלום בפאנל חדש של מסיח
                    if ((((ImageButton)FindControl("ImageforUpload" + (AnsNum + 1))).ImageUrl == "~/image/pic_upload.png") && (((TextBox)FindControl("incorrect" + AnsNum)).Text == ""))
                    {
                        ((HiddenField)FindControl("SaveNamePic" + (AnsNum + 1))).Value = "";

                        if (AnsNum == 2)
                        {
                            DeleteAns2_Click(DeleteAns2, e);
                            //פניה לשאלה ומחיקת השאלה
                            XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns='" + (n-1) + "']").Item(0);
                            incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                        }
                        if (AnsNum == 3)
                        {
                            DeleteAns3_Click(DeleteAns3, e);
                            //פניה לשאלה ומחיקת השאלה
                            XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns='" + (n - 1) + "']").Item(0);
                            incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                        }
                        if (AnsNum == 4)
                        {
                            DeleteAns4_Click(DeleteAns4, e);
                            //פניה לשאלה ומחיקת השאלה
                            XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns='" + (n - 1) + "']").Item(0);
                            incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                        }                     
                    }
                    else
                    {
                        //בדיקה אם המסיח טקסט או תמונה
                        if (((TextBox)FindControl("incorrect" + AnsNum)).Text != "") //אם המסיח טקסט
                        {
                            incorrectAns.InnerText = (((TextBox)FindControl("incorrect" + AnsNum)).Text); //הכנסת הטקסט החדש
                            incorrectAns.Attributes["AnsType"].Value = "txt"; //שינוי סוג מסיח לטקסט
                            ((HiddenField)FindControl("SaveNamePic" + (AnsNum + 1))).Value = "";
                        }
                        else //במידה והמסיח הוא תמונה
                        {
                            incorrectAns.InnerXml = ((HiddenField)FindControl("SaveNamePic" + (AnsNum + 1))).Value; // שמור את שם התמונה
                            incorrectAns.Attributes["AnsType"].Value = "img"; //שינוי סוג מסיח לתמונה
                        }
                    }
                }
            }
            else //המסיח סגור
            {
                XmlNode incorrectAns = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns=" + AnsNum + "]").Item(0);
                if (incorrectAns != null) //במידה והמסיח קיים בעץ
                {
                    //פניה לשאלה ומחיקת השאלה
                    XmlNode incorrectToDel = Document.SelectNodes("/WildWestGame/Game[@GameCode='" + gamecodeToChange + "']/questions/question[@id='" + questionIDToChange + "']/answers/answer[@idAns='" + AnsNum + "']").Item(0);
                    incorrectToDel.ParentNode.RemoveChild(incorrectToDel);
                }       
            }

        }
            // שמירת העץ החדש
            Document.Save(Server.MapPath("trees/Wildwest.xml"));
            XmlDataSource1.Save();
            GridView2.DataBind();


            //סידור כפתורים
            savingQuestion.Visible = true;
            backToMyGmaes.Visible = true;
            DontSaveChanges.Visible = false;
            SaveChanges.Visible = false;

           
        restart.Style.Add("background-color", "#197282");
        addingAnswer.Style.Add("background-color", "#197282");
        restart.Style.Add("color", "white");
        addingAnswer.Style.Add("color", "white");
        addingAnswer.Style.Add("opacity", "0.5");
        addingAnswer.Enabled = false;

        Restart();
        EditStatus.Value = "false";
    }

    protected void Restart()
    {
        for (int i = 0; i <= 5; i++) //לולאה שעוברת על כל התמונות ומאפסת אותן
        {
            ((HiddenField)FindControl("imgKeeper"+i)).Value = "~/image/pic_upload.png";
            ((HiddenField)FindControl("SaveNamePic" + i)).Value = "";

            ((ImageButton)FindControl("ImageforUpload" + i)).ImageUrl = "~/image/pic_upload.png";
            ((ImageButton)FindControl("ImageforUpload" + i)).Attributes.Clear();
            ((ImageButton)FindControl("ImageforUpload" + i)).Style.Add("opacity", "1");
            ((ImageButton)FindControl("ImageforUpload" + i)).Enabled = true;

            if (i == 0)
            {
                ((ImageButton)FindControl("ImageforUpload" + i)).Style.Add("width", "100");
                ((ImageButton)FindControl("ImageforUpload" + i)).Style.Add("height", "100");
            }
            else {
                ((ImageButton)FindControl("ImageforUpload" + i)).Style.Add("width", "50");
                ((ImageButton)FindControl("ImageforUpload" + i)).Style.Add("height", "50");
            }

            ((ImageButton)FindControl("glass" + i)).Style.Add("display", "none");
            ((ImageButton)FindControl("editPic" + i)).Style.Add("display", "none");
            ((ImageButton)FindControl("deletPic" + i)).Style.Add("display", "none");

        }

        for (int x = 1; x <= 4; x++) //לולאה שעוברת על כל התיבות טקסט של המסיחים ומאפסת אותן
        {
            ((TextBox)FindControl("incorrect" + x)).Text = "";
            ((TextBox)FindControl("incorrect" + x)).Style.Add("opacity", "1");
            ((TextBox)FindControl("incorrect" + x)).Enabled = true;
            if (x > 1)
            {
                if (((Panel)FindControl("PanelAns" + x)).Visible == true)
                {
                    ((Panel)FindControl("PanelAns" + x)).Visible = false;
                }
            }

        }

        //איפוס תיבת טקסט של גזע השאלה
        ((TextBox)FindControl("question")).Text = "";
        ((TextBox)FindControl("question")).Style.Add("opacity", "1");
        ((TextBox)FindControl("question")).Enabled = true;

        //איסוף תיבת טקסט של תשובה נכונה
        ((TextBox)FindControl("correct")).Text = "";
        ((TextBox)FindControl("correct")).Style.Add("opacity", "1");
        ((TextBox)FindControl("correct")).Enabled = true;

        //alert.Text = ""; //איפוס הודעת אלרט
        questionChars.Style.Add("color", "black");
        int CounterAns = Convert.ToInt16(((HiddenField)FindControl("AddingAnswerCounter")).Value);
        CounterAns=1;
        ((HiddenField)FindControl("AddingAnswerCounter")).Value = CounterAns.ToString();

     
    }
    //לא-אל תמחק מסיח 2
    protected void No_CloseAlertAns2(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteAns2")).CssClass = "hideAlert";
    }
    //חלונית למחיקת מסיח 2
    protected void AlertFunction_Ans2ToDel(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteAns2")).CssClass = "showAlert";
    }
    //לא-אל תמחק מסיח 3
    protected void No_CloseAlertAns3(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteAns3")).CssClass = "hideAlert";
    }
    //חלונית למחיקת מסיח 3
    protected void AlertFunction_Ans3ToDel(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteAns3")).CssClass = "showAlert";
    }
    //4
    protected void No_CloseAlertAns4(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteAns4")).CssClass = "hideAlert";
    }
    //חלונית למחיקת מסיח 4
    protected void AlertFunction_Ans4ToDel(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteAns4")).CssClass = "showAlert";
    }
    //לא-אל תמחק איפוס שדות
    protected void No_CloseAlertReset(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_reset")).CssClass = "hideAlert";
    }
    //חלונית לאיפוס שדות
    protected void AlertFunction_reset(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_reset")).CssClass = "showAlert";
    }
    //אל תמחק- מחיקת שאלה
    protected void No_CloseAlertDelete_quest(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_deleteQuest")).CssClass = "hideAlert";
    }
    //חלונית למחיקת שאלה
    protected void AlertFunction_deleteQuest()
    {
        ((Panel)FindControl("Wrap_deleteQuest")).CssClass = "showAlert";

    }
    //חלונית בקרה חזרה למחשקים שלי
    protected void Alert_BackToMyGamesFunc()
    {
        ((Panel)FindControl("Wrap_BackToMyGames")).CssClass = "showAlert";
    }

    //לא- אל תחזור למשחקים שלי בלי לשמור את השאלה
    protected void No_CloseAlert_BackToMyGames(object sender, EventArgs e)
    {
        ((Panel)FindControl("Wrap_BackToMyGames")).CssClass = "hideAlert";
    }
    //חזרה למשחקים שלי ללא שמירת השאלה
    protected void yes_backToMyGmaes(object sender, EventArgs e)
    {
        Response.Redirect("myGames.aspx");

    }



}






