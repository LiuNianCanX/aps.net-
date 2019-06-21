using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class TuiTionStuMSPayedTest_aspx : System.Web.UI.Page
{
    private StudentService stuSrv = new StudentService();
    protected void Page_Load(object sender, EventArgs e)
    {
        // 获得姓名
        lbName.Text = (string)Session["Name"];
        //Session["studentNo"] = "1600101001";    // 仅做测试用，到时候需要注释掉
        if (Session["studentNo"] == null)
        {
            // 用户未登录，跳转回登录界面
            Response.Redirect("../Login/TuitionMSLogin.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                ddlPayedTerm.Items.Clear();// 先清空已下拉菜单
                ddlPayedTerm.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                // 获得该生未缴费的学期,并显示到下拉菜单表中
                ddlPayedTerm.DataSource = stuSrv.GetPayedTermByStudNo((string)Session["studentNo"]);
                // 显示数据
                ddlPayedTerm.DataBind();
            }
        }
    }

    // 更新数据
    protected void DdlPayedTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 当前选择的值为空时不进行跳转
        if (ddlPayedTerm.SelectedValue == "null")
            return;
        // 获得当前已选中的学期
        string termNo = ddlPayedTerm.SelectedValue;
        var result = stuSrv.GetPayedStudTuitionByTermAndSpecNo(termNo, (string)Session["StudentNo"]);
        // 将当前学费显示出来
        dvPayedFee.DataSource = result;
        dvPayedFee.DataBind();
    }
}