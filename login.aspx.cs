using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void EnterEditor_Click(object sender, ImageClickEventArgs e)
    {
        if ((UserName.Text != "admin") || (Password.Text != "telem"))
        {
            alert_login.Text = "שם משתמש או סיסמא שגויים";

        }
        if ((UserName.Text == "admin") && (Password.Text == "telem"))
        {
            Response.Redirect("myGames.aspx");
        }
    }

}