using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class StudentMS_and_FinanceMS_FinMsSetFree : System.Web.UI.Page
{
    private FinanceService finSrv = new FinanceService();
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
            if (!Page.IsPostBack)
            {
                ddlDepartment.Items.Clear();// 先清空已下拉菜单
                ddlDepartment.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlSpeciality.Items.Clear();// 先清空已下拉菜单
                ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlTerm.Items.Clear();// 先清空已下拉菜单
                ddlTerm.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlClass.Items.Clear();// 先清空已下拉菜单
                ddlClass.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlStudent.Items.Clear();// 先清空已下拉菜单
                ddlStudent.Items.Add(new ListItem("==请选择==", "null"));//设置第一行

                // 获得该生未缴费的学期,并显示到下拉菜单表中
                ddlDepartment.DataSource = finSrv.GetDepartMent();
                ddlDepartment.DataBind();
            }
        }
    }

    protected void ddlterm_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlDepartment.SelectedValue != "null")
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
        if(ddlDepartment.SelectedValue != "null" && ddlSpeciality.SelectedValue != "null")
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

    // 提交信息
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null" && ddlSpeciality.SelectedValue != "null" 
            && ddlClass.SelectedValue != "null" && ddlStudent.SelectedValue != "null")
        {
            // 查询数据库，对指定学生减免情况进行修改
                finSrv.SetStudTuition(ddlTerm.SelectedValue, ddlStudent.SelectedValue, int.Parse(tbTuiWaiver.Text), int.Parse(tbLoans.Text));
            // 弹出框，提示修改已经成功
            Response.Write("<script type='text/javascript'>alert('修改成功');</script>");
                lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请将选项选完";
        }
    }

    protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 获得学期信息
        if(ddlStudent.SelectedValue != "null")
        {
            ddlTerm.Items.Clear();// 先清空已下拉菜单
            ddlTerm.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
            ddlTerm.DataSource = finSrv.GetStudTuition(ddlStudent.SelectedValue);
            ddlTerm.DataBind();
        }
        else
        {
            lbMessage.Text = "请选择学生";
        }
    }
}