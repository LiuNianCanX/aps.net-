using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class Login_TuitionMSLForgetPwd : System.Web.UI.Page
{
    StudentService studentService = new StudentService();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    // 找回密码
    protected void btnFind_Click(object sender, EventArgs e)
    {
        
        if (tbUsername.Text.Trim() == "" || tbEmail.Text.Trim() == "")
        {
            // 弹框，提示需要写入非空的值
            lbNotice.Text = "用户名或邮箱不能为空";
        }
        else
        {
            // 从数据库中查询该生的邮箱.
            if(studentService.IsEmailExits( tbUsername.Text.Trim(), tbEmail.Text.Trim() ) )
            {
                // 重置学生密码为123456
                studentService.ResetPassword(tbUsername.Text.Trim(), tbEmail.Text.Trim());
                // 发送信息通知该生
                EmailSender emailSender = new EmailSender(tbEmail.Text.Trim());
                emailSender.Send("贵州大学学费管理系统密码重置","您的密码已经重置为123456，请重新登录，并尽快修改密码！");
                lbNotice.Text = "修改成功";
                Response.Write("<script type='text/javascript'>alert('修改成功');function(){location.href='TuitionMSLogin.aspx'};</script>");
            }
            else
            {
                lbNotice.Text = "用户名错误或该生未填邮箱";
            }
        }

    }
}