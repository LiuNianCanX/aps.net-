using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class Manage_bigmanagerSeges : System.Web.UI.Page
{
    AdminService adSrv = new AdminService();
    protected void Page_Load(object sender, EventArgs e)
    {
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

    // 修改数据,根据后台逻辑层
    private void GvBind()
    {
        gv.DataSource = adSrv.GetFbByStud();
        gv.DataBind();
    }
    // 页面修改
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
        GvBind();
    }
    // 全选
    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        // 获取GridView标题行中的chkAll对象
        CheckBox chkAll = (CheckBox)sender;
        foreach (GridViewRow gvRow in gv.Rows)
        {
            // 获取GridView数据行中的chkItemduixiang 
            CheckBox chkItem = (CheckBox)gvRow.FindControl("chkItem");
            chkItem.Checked = chkAll.Checked;
        }
    }
    // 将选中的信息删除
    protected void BtnDelChecked_Click(object sender, EventArgs e)
    {
        // 遍历行
        foreach (GridViewRow gvRow in gv.Rows)
        {
            // 获取GridView数据行中的chkItemduixiang 
            CheckBox chkItem = (CheckBox)gvRow.FindControl("chkItem");
            // 删除行
            if (chkItem.Checked == true)
            {
                string propTime = ((Label)gvRow.Cells[1].Controls[0].FindControl("lbTime")).Text;
                // 调用方法将该行删除
                adSrv.DelPropose(propTime);
            }
        }
        // 删除完成，进行信息的更新
        GvBind();
    }
    // 刷新
    protected void BtnFresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }
}