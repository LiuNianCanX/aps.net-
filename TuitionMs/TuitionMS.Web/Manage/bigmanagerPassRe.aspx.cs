using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class Manage_bigmanagerPassRe : System.Web.UI.Page
{
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

    protected void ddlPageSise_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvManager.PageSize = int.Parse(ddlPageSise.SelectedValue);
        gvManager.DataBind();
    }

    protected void gvManager_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //写分页信息
        lblMag.Text = "当前页为第" + (gvManager.PageIndex + 1).ToString() + "页，共有" + gvManager.PageCount.ToString() + "页";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                LinkButton lnkbtnDelete = (LinkButton)e.Row.Cells[5].Controls[0];
                lnkbtnDelete.OnClientClick = "return confirm('确认删除" + e.Row.Cells[2].Text + "管理员:" + e.Row.Cells[0].Text + " ！');";//Java script 脚本
            }
            catch
            {

            }
        }
    }
}