using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class StudentMS_TuiTionStuMSPay : System.Web.UI.Page
{
    private StudentService stuSrv = new StudentService();
    private static string specNo;  // 专业号
    private static string studNo;  // 学生学号
    private static int tuitionAll;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session["studentNo"] = "1600101001";    // 仅做测试用，到时候需要注释掉
        if (Session["studentNo"] == null)
        {
            // 用户未登录，跳转回登录界面
            Response.Redirect("../Login/TuitionMSLogin.aspx");
        }
        else
        {
            // 获得姓名
            lbName.Text = (string)Session["Name"];
            studNo = (string)Session["studentNo"];
            if (!Page.IsPostBack)
            {
                // 初始化数据
                tuitionAll = -1;
                // 显示查询界面
                Panelshow.Visible = true;
                // 显示支付界面
                Panelpay.Visible = false;
                ddlTerm.Items.Clear();// 先清空已下拉菜单
                ddlTerm.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
                // 获得该生要缴费的学期,并显示到下拉菜单表中
                ddlTerm.DataSource = stuSrv.GetNoPayTermByStudNo(studNo);
                // 显示数据
                ddlTerm.DataBind();
            }

        }
    }

    // 显示当前学生的缴费信息
    protected void DdlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 当前选择的值为空时不进行跳转
        if (ddlTerm.SelectedValue == "null")
            return;

        // 获得当前已选中的学期
        string termNo = ddlTerm.SelectedValue;
        // 使用学生学号通过班级获得专业号
        specNo = stuSrv.GetSpecNoByStudNo(studNo);
        // 通过专业号查询相应的专业学费
        var result = stuSrv.GetPayStudTuitionByTermAndSpecNo(termNo, specNo, studNo);
        // 将当前学费显示出来
        dvFee.DataSource = result;
        dvFee.DataBind();
        // 获取当前的应缴纳的总学费
        tuitionAll = stuSrv.GetPayAllByTermNoAndSpecNo(termNo,specNo,studNo);
        lbTotal.Text = ""+tuitionAll;
     }

    protected void BtnPay_Click(object sender, EventArgs e)
    {
        if(tuitionAll != -1 && ddlTerm.SelectedValue != "")
        {
            // 显示支付页面，隐藏查询页面
            Panelshow.Visible = false;
            Panelpay.Visible = true;
        }
    }

    protected void BtnPayed_Click(object sender, EventArgs e)
    {
        // 获得当前已选中的学期
        string termNo = ddlTerm.SelectedValue;
        // 读取框中获得缴费值
        stuSrv.PayStudTuition(termNo, specNo, studNo);
        // 弹出框，提示支付已经成功
        Response.Write("<script type='text/javascript'>alert('支付成功');</script>");
        // 显示查询页面，隐藏支付页面
        Panelshow.Visible = true;
        Panelpay.Visible = false;
        // 总金额恢复到未结算的转态
        tuitionAll = -1;
        // 更新下拉列表
        ddlTerm.Items.Clear();// 先清空已下拉菜单
        ddlTerm.Items.Add(new ListItem("==请选择==", "null"));//设置第一行
        ddlTerm.DataSource = stuSrv.GetNoPayTermByStudNo(studNo);
        ddlTerm.DataBind();
        // 清空当前的学费
        lbTotal.Text = "";
        // 清空dv目前显示的信息
        dvFee.DataSource = null;
        dvFee.DataBind();
    }
}