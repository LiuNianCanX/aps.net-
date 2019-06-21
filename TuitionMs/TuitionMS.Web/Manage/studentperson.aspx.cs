using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class StudentMS_StuMaMessage : System.Web.UI.Page
{
    private StudentService stuSrv = new StudentService();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["studentNo"] = "1600101001";    // 仅做测试用，到时候需要注释掉
        // 第一次加载,将个人的内容填入表格中
        if (Session["studentNo"] == null)
        {
            // 用户未登录，跳转回登录界面
            Response.Redirect("../Login/TuitionMSLogin.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                // 获得姓名
                lbName.Text = (string)Session["Name"];
                string[] result = stuSrv.GetStudentMessage((string)Session["studentNo"]);
                // 将值填入到表格之中
                lbStudNo.Text = (string)Session["studentNo"];
                lbStudName.Text = result[0];
                lbStudClass.Text = result[1];
                tbEmail.Text = result[2];
                tbBirth.Text = result[3];
                tbTel.Text = result[4];
                tbSex.Text = result[5];

                // 仅显示个人信息
                panelMessage.Visible = true;
                PanelSetPw.Visible = false;
            }
        }
            
    }

    // 修改信息按钮
    protected void btnSet_Click(object sender, EventArgs e)
    {
        if(btnSet.Text == "修改信息")
        {
            // 开放tb进行修改
            tbEmail.Enabled = true;
            tbBirth.Enabled = true;
            tbTel.Enabled = true;
            tbSex.Enabled = true;
            btnSet.Text = "保存修改";
        }
        else
        {
            // 保存修改
            stuSrv.SetStudentMessage((string)Session["studentNo"], tbEmail.Text, tbTel.Text, tbSex.Text, tbBirth.Text.Trim());
            // 关闭tb进行修改
            tbEmail.Enabled = false;
            tbBirth.Enabled = false;
            tbTel.Enabled = false;
            tbSex.Enabled = false;
            btnSet.Text = "修改信息";

        }
    }

    // 确认修改
    protected void BtnChangePwd_Click(object sender, EventArgs e)
    {
        // 获得密码信息，并进行数据库的查询
        if(stuSrv.IsPassword((string)Session["studentNo"],txtOldPwd.Text))
        {
            // 修改密码
            stuSrv.SetStudentPassword((string)Session["studentNo"], txtOldPwd.Text, txtPwd.Text);
            // 若该密码存在，且合法，则进行修改
            // 隐去修改界面，显示个人信息界面
            // 仅显示个人信息
            panelMessage.Visible = true;
            PanelSetPw.Visible = false;
            // 弹出框，提示修改成功
            Response.Write("<script type='text/javascript'>alert('密码修改成功');</script>");
        }
        else
        {
            lblMsg.Text = "原密码错误，请重新输入";
        }
    }

    // 修改密码
    protected void btnChangePw_Click(object sender, EventArgs e)
    {
        // 隐去个人信息，显示密码修改
        panelMessage.Visible = false;
        PanelSetPw.Visible = true;
    }
}