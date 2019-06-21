using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class Manage_teacherseges_send : System.Web.UI.Page
{
    TeacherSerivce teacher = new TeacherSerivce();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["adminNo"] = "t0001"; // 仅用于测试，在使用时注释掉
        if (Session["adminNo"] == null)
        {
            // 用户未登录，跳转回登录界面
            Response.Redirect("../Login/TuitionMSLogin.aspx");
        }
        else
        {
            lbName.Text = (string)Session["Name"];
        }
    }

    protected void btnPropose_Click(object sender, EventArgs e)
    {
        bool a = teacher.propose("admin", tbFbMessage.Text.ToString().Trim(), DateTime.Now);
        if (a == true)
        {
            lblMsg.Text = "建议提交成功！";
        }
    }
}