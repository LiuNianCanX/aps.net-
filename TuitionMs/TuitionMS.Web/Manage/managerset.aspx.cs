using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class StudentMS_and_FinanceMS_FinMsSetStandard : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                ddlDepartment.Items.Clear();// 先清空已下拉菜单
                ddlDepartment.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlSpeciality.Items.Clear();// 先清空已下拉菜单
                ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                ddlTerm.Items.Clear();// 先清空已下拉菜单
                ddlTerm.Items.Add(new ListItem("==请选择==", "null"));//设置第一行

                // 获得该生未缴费的学期,并显示到下拉菜单表中
                ddlDepartment.DataSource = finSrv.GetDepartMent();
                ddlDepartment.DataBind();
                // 获得当前的学年
                ddlTerm.DataSource = finSrv.GetTerm();
                ddlTerm.DataBind();
            }
        }
    }
    // 选择学院时触发修改选择专业
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 通过已经选择的学院选择专业
        ddlSpeciality.Items.Clear();// 先清空已下拉菜单
        ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));
        if(ddlDepartment.SelectedValue != "null")
        {
            lbMessage.Text = "";
            bindSpecility();
        }
        else
        {
            lbMessage.Text = "请选择学期";
            
        }
        
    }

    // 刷新专业号
    protected void bindSpecility()
    {
        // 获得未创建标准的专业
        ddlSpeciality.Items.Clear();// 先清空已下拉菜单
        ddlSpeciality.Items.Add(new ListItem("==请选择==", "null"));
        ddlSpeciality.DataSource = finSrv.GetSpeciality(ddlDepartment.SelectedValue);
        ddlSpeciality.DataBind();
    }
    // 刷新学期
    protected void bindTerm()
    {
        // 获得未创建标准的专业
        ddlTerm.Items.Clear();// 先清空已下拉菜单
        ddlTerm.Items.Add(new ListItem("==请选择==", "null"));
        ddlTerm.DataSource = finSrv.GetNoExistTerm(ddlSpeciality.SelectedValue);
        ddlTerm.DataBind();
    }

    // 提交缴费表
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // 判断是否所有的选项都有值
        if(ddlSpeciality.SelectedValue != "null" && ddlDepartment.SelectedValue != "null" && ddlTerm.SelectedValue != "null"  )
        {
            // 获取当前的值并将其写入到学费表中 term  specNo tuition  premiun accom  book other 
            finSrv.GreateTuition(ddlTerm.SelectedValue, ddlSpeciality.SelectedValue, 
                int.Parse(tbTuition.Text), int.Parse(tbPremiun.Text), 
                int.Parse(tbAccom.Text), int.Parse(tbBook.Text), int.Parse(tbOther.Text));
            // 弹出框，提示标准创建已经成功
            Response.Write("<script type='text/javascript'>alert('创建标准成功');</script>");
        }
        else
        {
            lbMessage.Text = "请正确选择专业,学院，和学期";
        }
    }

    protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlSpeciality.SelectedValue != "null")
        {
            bindTerm();
            lbMessage.Text = "";
        }
        else
        {
            lbMessage.Text = "请选择专业";
        }
    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}