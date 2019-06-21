using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TuitionMS.BLL;

public partial class admin_infoContentPage : System.Web.UI.Page
{
    AdminService admin = new AdminService();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        bool a=admin.addAdministrator(TextBox1.Text.ToString().Trim(), TextBox2.Text.ToString().Trim(), TextBox3.Text.ToString().Trim(), TextBox4.Text.ToString().Trim());
        if(a==true)
            lblMsg.Text = "创建"+ TextBox3.Text.ToString().Trim()+"管理员"+ TextBox1.Text.ToString().Trim()+"成功！";
       else
            lblMsg.Text = "创建" + TextBox3.Text.ToString().Trim() + "管理员" + TextBox1.Text.ToString().Trim() + "失败！";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        admin.delAdministrator(TextBox1.Text.ToString().Trim());
        lblMsg.Text = "删除管理员" + TextBox1.Text.ToString().Trim() + "成功！";
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        admin.updateAdministrator(TextBox1.Text.ToString().Trim(), TextBox2.Text.ToString().Trim(), TextBox3.Text.ToString().Trim(), TextBox4.Text.ToString().Trim());
        lblMsg.Text = "更新" + TextBox3.Text.ToString().Trim() + "管理员" + TextBox1.Text.ToString().Trim() + "信息成功！";
    }
}