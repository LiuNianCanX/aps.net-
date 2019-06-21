using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;
public partial class Manage_managerseges_send : System.Web.UI.Page
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
            lbName.Text = (string)Session["Name"];
            
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // 对系统提交建议
        if(tbFbMessage.Text.Trim() != "")
        {
            finSrv.SubmitFbMassage("admin", tbFbMessage.Text, DateTime.Now.ToString());
            // 弹出框，提示支付已经成功
            Response.Write("<script type='text/javascript'>alert('提交成功，感谢您的建议！');</script>");
            // 文本框中的内容清空
            tbFbMessage.Text = "";
        }
        
    }
}