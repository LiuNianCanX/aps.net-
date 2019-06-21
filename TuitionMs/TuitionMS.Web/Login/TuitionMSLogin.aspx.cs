using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class Login_TuitionMSLogin : System.Web.UI.Page
{
    StudentService studentService = new StudentService();
    AdminService adminService = new AdminService();

    protected void Page_Load(object sender, EventArgs e)
    {
        // 创建session 并通过session判断用户登录状态
        if (Session["studentNo"] != null)
        {
            // 跳转至学生管理页面
            Response.Redirect("../Manage/studentpaym.aspx");
        }
        else if (Session["adminNo"] != null)
        {
            if ((string)Session["Name"] == "admin")
            {
                // 跳转至管理员页面 bigmanager_FTChange
                Response.Redirect("../Manage/bigmanager_FTChange.aspx");
            }
            else if ((string)Session["Name"] == "teacher")
            {
                // 跳转至教务管理页面 teacherChange.aspx
                Response.Redirect("../Manage/teacherChange.aspx");
            }
            else if ((string)Session["Name"] == "finance")
            {
                Response.Redirect("../Manage/managerset.aspx");
                // 跳转至财务管理页面
            }
            else
            {
                lbNotice.Text = "Session错误,请重新登录";
                Session["adminNo"] = null;
                Session["studentNo"] = null;
                Session["Name"] = null;
            }
        }
        else
        {

        }

    }

    // 登录
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // 检查用户输入的内容
        if (tbUsername.Text.Trim() == "" || tbPassword.Text.Trim() == "")
        {
            lbNotice.Text = "用户名或密码不能为空";
        }
        else
        {
            // 检查学生用户
            string studentMessage = studentService.CheckLogin(tbUsername.Text, tbPassword.Text);
            string adminMessage = adminService.CheckLogin(tbUsername.Text, tbPassword.Text);
            if (studentMessage != "")
            {

                // 填写Session
                string[] studentMessages = studentMessage.Split(' ');
                Session["studentNo"] = studentMessages[0];
                Session["Name"] = studentMessages[1];
                // 跳转至学生页面
                Response.Redirect("../Manage/studentpaym.aspx");
                lbNotice.Text = "跳转至学生页面";
            }
            // 检查管理用户
            else if (adminMessage != "")
            {
                string[] adminMessages = adminMessage.Split(' ');
                Session["adminNo"] = adminMessages[0];
                Session["Name"] = adminMessages[1];

                if (adminMessages[1] == "teacher")
                {
                    // 跳转至教务管理人员页面
                    Response.Redirect("../Manage/teacherChange.aspx");
                    lbNotice.Text = "跳转至教务管理人员页面";

                }
                else if (adminMessages[1] == "finance")
                {
                    // 跳转至财务管理人员页面
                    Response.Redirect("../Manage/managerset.aspx");
                    lbNotice.Text = "跳转至财务管理人员页面";
                }
                else
                {
                    // 跳转至系统管理员页面
                    lbNotice.Text = "跳转至系统管理员页面";
                    Response.Redirect("../Manage/bigmanager_FTChange.aspx");
                }
            }
            else
            {
                lbNotice.Text = "用户名或密码错误，请重新尝试！";
            }
        }

    }


    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Write("12323");
    }
}