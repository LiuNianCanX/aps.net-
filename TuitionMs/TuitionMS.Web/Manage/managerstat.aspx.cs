using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class StudentMS_and_FinanceMS_FinMsSearch : System.Web.UI.Page
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
                lbName.Text = (string)Session["Name"];
                ddlDepartment.Items.Clear();// 先清空已下拉菜单
                ddlDepartment.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlSpeciality.Items.Clear();// 先清空已下拉菜单
                ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlTerm.Items.Clear();// 先清空已下拉菜单
                ddlTerm.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlClass.Items.Clear();// 先清空已下拉菜单
                ddlClass.Items.Add(new ListItem("==请选择==", "null"));//设置第一行

                // 获得学院信息，并进行初始化
                ddlDepartment.DataSource = finSrv.GetDepartMent();
                ddlDepartment.DataBind();
                // 获得学年信息
                ddlTerm.DataSource = finSrv.GetTerm();
                ddlTerm.DataBind();
            }
        }
    }
    // 当下拉学院改变时,添加专业信息
    protected void DdlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null")
        {
            // 获得专业下拉信息
            ddlSpeciality.Items.Clear();// 先清空已下拉菜单
            ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
            ddlSpeciality.DataSource = finSrv.GetSpeciality(ddlDepartment.SelectedValue);
            ddlSpeciality.DataBind();
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
    }
    // 当下拉专业改变时,添加班级信息
    protected void DdlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "null" && ddlSpeciality.SelectedValue != "null")
        {
            // 获得班级
            ddlClass.Items.Clear();// 先清空已下拉菜单
            ddlClass.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
            ddlClass.DataSource = finSrv.GetClass(ddlSpeciality.SelectedValue);
            ddlClass.DataBind();
        }
        else
        {
            lbMessage.Text = "请选择学院和专业";
        }
    }

    protected void DdlClass_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DdlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void BtnDeptSta_Click(object sender, EventArgs e)
    {
        // 获得学院统计情况
        if(ddlDepartment.SelectedValue != "null")
        {
            // 调用逻辑查询统计情况
            gv.DataSource = finSrv.GetDepartmentStatistics(ddlDepartment.SelectedItem.Text);
            gv.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }

    }

    protected void BtnSpecSta_Click(object sender, EventArgs e)
    {
        // 获得专业统计情况
        if(ddlSpeciality.SelectedValue != "null")
        {
            // 调用逻辑查询统计情况
            gv.DataSource = finSrv.GetSpecilityStatistics(ddlSpeciality.SelectedItem.Text);
            gv.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择专业";
        }
    }

    protected void btnClasSta_Click(object sender, EventArgs e)
    {
        // 获取班级统计的情况
        if (ddlClass.SelectedValue != "null")
        {
            // 调用逻辑查询统计情况
            gv.DataSource = finSrv.GetClassStatistics(ddlClass.SelectedItem.Text);
            gv.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择班级";
        }
    }

    protected void btnTermSta_Click(object sender, EventArgs e)
    {
        // 获取学年统计的情况
        if (ddlTerm.SelectedValue != "null")
        {
            // 调用逻辑查询统计情况
            gv.DataSource = finSrv.GetTermStatistics(ddlTerm.SelectedValue);
            gv.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择学期";
        }
    }
    // 未缴费的统计情况
    protected void btnDeptNoPaySta_Click(object sender, EventArgs e)
    {
        // 获得学院未缴纳费用统计情况
        if (ddlDepartment.SelectedValue != "null")
        {
            // 调用逻辑查询统计情况
            gv.DataSource = finSrv.GetDepartmentNoPay(ddlDepartment.SelectedItem.Text);
            gv.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择学院";
        }
    }

    protected void btnSpecNoPaySta_Click(object sender, EventArgs e)
    {
        // 获得专业未缴纳费用统计情况
        if (ddlSpeciality.SelectedValue != "null")
        {
            // 调用逻辑查询统计情况
            gv.DataSource = finSrv.GetSpecilityNoPay(ddlSpeciality.SelectedItem.Text);
            gv.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择专业";
        }
    }



    protected void btnClassNoPaySta_Click(object sender, EventArgs e)
    {
        // 获得班级未缴纳费用统计情况
        if (ddlClass.SelectedValue != "null")
        {
            // 调用逻辑查询统计情况
            gv.DataSource = finSrv.GetClassNoPay(ddlClass.SelectedItem.Text);
            gv.DataBind();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择班级";
        }
    }

    protected void Gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex < 0)
        {
            TextBox pageNum = (TextBox)gv.BottomPagerRow.FindControl("txtNewPageIndex");
            int Pa = int.Parse(pageNum.Text);
            if (Pa <= 0)
            {
                gv.PageIndex = 0;
            }
            else
            {
                gv.PageIndex = Pa - 1;
            }
        }
        else
        {
            gv.PageIndex = e.NewPageIndex;
        }
        gv.DataBind();
    }
}