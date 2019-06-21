using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_onLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 此页面用于中转删除Session
        Session["Name"] = null;
        Session["studentNo"] = null;
        Session["adminNo"] = null;
        // 跳转至登录界面
        Response.Redirect("../Login/TuitionMSLogin.aspx");

    }
}