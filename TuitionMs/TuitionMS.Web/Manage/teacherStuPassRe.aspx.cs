using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class Manage_teacherStuPassRe_ : System.Web.UI.Page
{
    TeacherSerivce teaSrv = new TeacherSerivce();
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
                ddlDepartment.DataSource = teaSrv.GetDepartMent();
                ddlDepartment.DataBind();

            }
        }
    }
    // 获取要重置的学号
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null")
        {
            // 获得专业下拉信息
            ddlSpeciality.Items.Clear();// 先清空已下拉菜单
            ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
            ddlSpeciality.DataSource = teaSrv.GetSpeciality(ddlDepartment.SelectedValue);
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
            ddlClass.DataSource = teaSrv.GetClass(ddlSpeciality.SelectedValue);
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
            ddlStudent.DataSource = teaSrv.GetStudents(ddlClass.SelectedValue);
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

    // 调用逻辑层的接口进行修改
    protected void BtnInformDept_Click(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null")
        {
            teaSrv.ResetDeptStudentPwd(ddlDepartment.SelectedValue);
            lbMessage.Text = "重置成功";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
        
    }

    protected void BtnInformSpec_Click(object sender, EventArgs e)
    {
        if (ddlSpeciality.SelectedValue != "null")
        {
            teaSrv.ResetSpecStudentPwd(ddlSpeciality.SelectedValue);
            lbMessage.Text = "重置成功";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
    }

    protected void BtnInformClas_Click(object sender, EventArgs e)
    {
        if (ddlClass.SelectedValue != "null")
        {
            teaSrv.ResetClasStudentPwd(ddlClass.SelectedValue);
            lbMessage.Text = "重置成功";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
    }

    protected void BtnInformStud_Click(object sender, EventArgs e)
    {
        if (ddlStudent.SelectedValue != "null")
        {
            teaSrv.ResetStudentPwd(ddlStudent.SelectedValue);
            lbMessage.Text = "重置成功";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
    }
}