using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class StudentMS_Default : System.Web.UI.Page
{
    StudentService stuSrv = new StudentService();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbName.Text = (string)Session["Name"];
        if (Session["studentNo"] == null)
        {
            // 用户未登录，跳转回登录界面
            Response.Redirect("../Login/TuitionMSLogin.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(ddlObject.SelectedValue != "==请选择==")
        {
            if (tbFbMessage.Text.Trim() != "")
            {
                string propObject = ddlObject.SelectedValue;
                string propContent = tbFbMessage.Text;
                string propTime = DateTime.Now.ToString();
                stuSrv.SubmitFbMassage(propObject, propContent, propTime);
                // 弹出框，提示建议提交成功
                Response.Write("<script type='text/javascript'>alert('提交成功');</script>");
                // 文本框中的内容清空
                tbFbMessage.Text = "";
            }
        }
        
        
    }
}