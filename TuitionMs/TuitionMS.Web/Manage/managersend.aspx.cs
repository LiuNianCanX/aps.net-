using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;
public partial class Manage_managersend : System.Web.UI.Page
{
    private FinanceService finSrv = new FinanceService();
    private static string subject;  // 发信主题
    private static string content;  // 发信内容
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["adminNo"] = "t0001"; // 设定死Session["adminNo"] 仅做测试用
        if (Session["adminNo"] == null)
        {
            // 用户未登录，跳转回登录界面
            Response.Redirect("../Login/TuitionMSLogin.aspx");
        }
        else
        {
            lbName.Text = (string)Session["Name"];
            if (!Page.IsPostBack)
            {
                ddlDepartment.Items.Clear();// 先清空已下拉菜单
                ddlDepartment.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlSpeciality.Items.Clear();// 先清空已下拉菜单
                ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlClass.Items.Clear();// 先清空已下拉菜单
                ddlClass.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlStudent.Items.Clear();// 先清空已下拉菜单
                ddlStudent.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                                                                      // 获得学院
                ddlDepartment.DataSource = finSrv.GetDepartMent();
                ddlDepartment.DataBind();

                subject = "贵州大学缴费通知";
                content = "你好！为了确保您今后的各项权利，请及时缴纳学费";
            }

        }
    }



    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null")
        {
            // 获得专业下拉信息
            ddlSpeciality.Items.Clear();// 先清空已下拉菜单
            ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
            ddlSpeciality.DataSource = finSrv.GetSpeciality(ddlDepartment.SelectedValue);
            ddlSpeciality.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
    }

    protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null" && ddlSpeciality.SelectedValue != "null")
        {
            // 获得班级
            ddlClass.Items.Clear();// 先清空已下拉菜单
            ddlClass.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
            ddlClass.DataSource = finSrv.GetClass(ddlSpeciality.SelectedValue);
            ddlClass.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择学院和专业";
        }
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null" && ddlSpeciality.SelectedValue != "null" && ddlClass.SelectedValue != "null")
        {
            // 获得学生信息
            ddlStudent.Items.Clear();// 先清空已下拉菜单
            ddlStudent.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
            ddlStudent.DataSource = finSrv.GetStudents(ddlClass.SelectedValue);
            ddlStudent.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择学院和专业、班级";
        }
    }

    protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    // 根据专业发送邮件
    protected void BtnInformSpec_Click(object sender, EventArgs e)
    {
        if (ddlSpeciality.SelectedValue != "null")
        {
            // 发送邮件
            EmailSender email = new EmailSender();
            List<TuitionMS.DAL.Students> students = finSrv.GetSpecStudent(ddlSpeciality.SelectedValue);
            foreach (TuitionMS.DAL.Students s in students)
            {
                if (s.studEmail != "")
                {
                    // 设置邮箱
                    email.SetEmailSender(s.studEmail);
                    // 发送邮件
                    email.Send(subject, s.studName + content);

                }
            }
            lbMessage.Text = "通知成功！";
        }
        else
        {
            lbMessage.Text = "请选择学院和专业";
        }

    }
    // 根据班级发送文件
    protected void BtnInformClas_Click(object sender, EventArgs e)
    {
        if (ddlClass.SelectedValue != "null")
        {
            // 发送邮件
            EmailSender email = new EmailSender();
            List<TuitionMS.DAL.Students> students = finSrv.GetClasStudent(ddlClass.SelectedValue);
            foreach (TuitionMS.DAL.Students s in students)
            {
                if (s.studEmail != "")
                {
                    // 设置邮箱
                    email.SetEmailSender(s.studEmail);
                    // 发送邮件
                    email.Send(subject, s.studName + content);

                }
            }
            lbMessage.Text = "通知成功！！";
        }
        else
        {
            lbMessage.Text = "请选择班级";
        }
    }
    // 根据学生发送文件
    protected void BtnInformStud_Click(object sender, EventArgs e)
    {
        if (ddlStudent.SelectedValue != "null")
        {
            // 发送邮件
            EmailSender email = new EmailSender();
            List<TuitionMS.DAL.Students> students = finSrv.GetStudent(ddlStudent.SelectedValue);
            foreach (TuitionMS.DAL.Students s in students)
            {
                if (s.studEmail != "")
                {
                    // 设置邮箱
                    email.SetEmailSender(s.studEmail);
                    // 发送邮件
                    email.Send(subject, s.studName + content);

                }
            }
            lbMessage.Text = "通知成功！！";
        }
        else
        {
            lbMessage.Text = "请选择学生";
        }
       
    }

    // 根据学院发送邮件
    protected void BtnInformDept_Click(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null")
        {
            // 发送邮件
            EmailSender email = new EmailSender();
            List<TuitionMS.DAL.Students> students = finSrv.GetDeptStudent(ddlDepartment.SelectedValue);
            foreach (TuitionMS.DAL.Students s in students)
            {
                if (s.studEmail != "")
                {
                    // 设置邮箱
                    email.SetEmailSender(s.studEmail);
                    // 发送邮件
                    email.Send(subject, s.studName + content);

                }
            }
            lbMessage.Text = "通知成功！！";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
    }
}