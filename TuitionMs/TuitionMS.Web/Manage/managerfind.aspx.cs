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
    private static string typeBind;
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
                typeBind = "";
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

    // 显示GridView的位置
    private void GridTermShow(bool b)
    {
        if(b)
        {
            gvTermShow.Visible = true;
            gvOtherShow.Visible = false;
        }
        else
        {
            gvTermShow.Visible = false;
            gvOtherShow.Visible = true;
        }
    }
    // 更新学期显示表
    protected void GridTermBind()
    {
        // 获取所有的学费统计表
        gvTermShow.DataSource = finSrv.GetAllTuition();
        gvTermShow.DataBind();
    }
    // 更新其他统计信息显示表
    protected void GridOtherBind(string type)
    {
        switch(type)
        {
            case "dept":
                gvOtherShow.DataSource = finSrv.GetDepartmentTuition(ddlDepartment.SelectedItem.Text);
                gvOtherShow.DataBind();
                break;
            case "spec":
                gvOtherShow.DataSource = finSrv.GetSpecilityTuition(ddlSpeciality.SelectedItem.Text);
                gvOtherShow.DataBind();
                break;
            case "clas":
                gvOtherShow.DataSource = finSrv.GetClassTuition(ddlClass.SelectedItem.Text);
                gvOtherShow.DataBind();
                break;
            case "term":
                gvOtherShow.DataSource = finSrv.GetTermTuition(ddlTerm.SelectedValue);
                gvOtherShow.DataBind();
                break;
            case "studNo":
                gvOtherShow.DataSource = finSrv.GetStudentTuitionByStudNo(tbSearch.Text.Trim());
                gvOtherShow.DataBind();
                break;
            case "studName":
                gvOtherShow.DataSource = finSrv.GetStudentTuitionByName(tbSearch.Text.Trim());
                gvOtherShow.DataBind(); break;
        }
    }

    // 查看学期显示表
    protected void BtnAllStd_Click(object sender, EventArgs e)
    {
        // 显示学期表
        // 隐藏其他表
        GridTermShow(true);
        // 更新数据
        GridTermBind();
    }
    // 按院系查询
    protected void BtnDeptStd_Click(object sender, EventArgs e)
    {
        if(ddlDepartment.SelectedValue != "null")
        {
            GridOtherBind("dept");
            typeBind = "dept";
            GridTermShow(false);
        }
        else
        {
            lbMessage.Text = "请选择院系";
        }
    }
    // 按专业查询
    protected void BtnSpecStd_Click(object sender, EventArgs e)
    {
        if (ddlSpeciality.SelectedValue != "null")
        {
            GridOtherBind("spec");
            typeBind = "spec";
            GridTermShow(false);
        }
        else
        {
            lbMessage.Text = "请选择专业";
        }
    }
    // 按班级查询
    protected void BtnClasStd_Click(object sender, EventArgs e)
    {
        if (ddlClass.SelectedValue != "null")
        {
            GridOtherBind("clas");
            typeBind = "clas";
            GridTermShow(false);
        }
        else
        {
            lbMessage.Text = "请选择班级";
        }
    }
    // 按学期查询
    protected void BtnTermStd_Click(object sender, EventArgs e)
    {
        if (ddlTerm.SelectedValue != "null")
        {
            GridOtherBind("term");
            typeBind = "term";
            GridTermShow(false);
        }
        else
        {
            lbMessage.Text = "请选择班级";
        }
    }
    // 按学号查询
    protected void BtnStudNoStd_Click(object sender, EventArgs e)
    {
        if (tbSearch.Text.Trim() != "")
        {
            GridOtherBind("studNo");
            typeBind = "studNo";
            GridTermShow(false);
        }
        else
        {
            lbMessage.Text = "";
        }
    }
    // 按名字查询
    protected void BtnStudNameStd_Click(object sender, EventArgs e)
    {
        if (tbSearch.Text.Trim() != "")
        {
            GridOtherBind("studName");
            typeBind = "studName";
            GridTermShow(false);
        }
        else
        {
            lbMessage.Text = "";
        }
    }

    protected void DdlClass_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DdlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    // 全选
    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        // 获取GridView标题行中的chkAll对象
        CheckBox chkAll = (CheckBox)sender;
        foreach(GridViewRow gvRow in gvTermShow.Rows)
        {
            // 获取GridView数据行中的chkItemduixiang 
            CheckBox chkItem = (CheckBox)gvRow.FindControl("chkItem");
            chkItem.Checked = chkAll.Checked;
        }
    }

    // 删除被选中的列
    protected void BtnDelChecked_Click(object sender, EventArgs e)
    {
        // 遍历行
        foreach (GridViewRow gvRow in gvTermShow.Rows)
        {
            // 获取GridView数据行中的chkItemduixiang 
            CheckBox chkItem = (CheckBox)gvRow.FindControl("chkItem");
            // 删除行
            if(chkItem.Checked == true)
            {
                string termNo = ((Label)gvRow.Cells[1].Controls[0].FindControl("label1")).Text;
                string specName = ((Label)gvRow.Cells[2].Controls[0].FindControl("label2")).Text;
                // 调用方法将该行删除
                finSrv.DelTuition(termNo, specName);
            }
        }
        // 删除完成，进行信息的更新
        GridTermBind();
    }

    // 行的内容进行编辑
    protected void GvTermShow_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTermShow.EditIndex = e.NewEditIndex;
        GridTermBind();
    }

    // 获得行的各种信息，并将其进行修改
    protected void GvTermShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string termNo = ((TextBox)gvTermShow.Rows[e.RowIndex].Cells[1].Controls[0].FindControl("TextBox1")).Text;
        string specName = ((TextBox)gvTermShow.Rows[e.RowIndex].Cells[2].Controls[0].FindControl("TextBox2")).Text;
        string tuition = ((TextBox)gvTermShow.Rows[e.RowIndex].Cells[3].Controls[0].FindControl("TextBox3")).Text;
        string premiun = ((TextBox)gvTermShow.Rows[e.RowIndex].Cells[4].Controls[0].FindControl("TextBox4")).Text;
        string accom = ((TextBox)gvTermShow.Rows[e.RowIndex].Cells[5].Controls[0].FindControl("TextBox5")).Text;
        string book = ((TextBox)gvTermShow.Rows[e.RowIndex].Cells[6].Controls[0].FindControl("TextBox6")).Text;
        string other = ((TextBox)gvTermShow.Rows[e.RowIndex].Cells[7].Controls[0].FindControl("TextBox7")).Text;
        // 调用逻辑层接口实现修改行信息
        finSrv.SetTuition(termNo, specName, int.Parse(tuition), int.Parse(premiun), int.Parse(accom), 
                        int.Parse(book), int.Parse(other));
        gvTermShow.EditIndex = -1;
        GridTermBind();
    }

    // 取消编辑
    protected void GvTermShow_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTermShow.EditIndex = -1;
        GridTermBind();
    }

    // 页面修改
    protected void GvTermShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex < 0)
        {
            TextBox pageNum = (TextBox)gvTermShow.BottomPagerRow.FindControl("txtNewPageIndex");
            int Pa = int.Parse(pageNum.Text);
            if (Pa <= 0)
            {
                gvTermShow.PageIndex = 0;
            }
            else
            {
                gvTermShow.PageIndex = Pa - 1;
            }
        }
        else
        {
            gvTermShow.PageIndex = e.NewPageIndex;
        }
        GridTermBind();
    }

    // 分页显示其他信息表
    protected void GvOtherShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex < 0)
        {
            TextBox pageNum = (TextBox)gvOtherShow.BottomPagerRow.FindControl("txtNewPageIndex");
            int Pa = int.Parse(pageNum.Text);
            if (Pa <= 0)
            {
                gvOtherShow.PageIndex = 0;
            }
            else
            {
                gvOtherShow.PageIndex = Pa - 1;
            }
        }
        else
        {
            gvOtherShow.PageIndex = e.NewPageIndex;
        }
        if(typeBind != "")
        {
            GridOtherBind(typeBind);

        }

    }




}