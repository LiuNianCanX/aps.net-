using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;
using System.Text.RegularExpressions;//字符串分割

public partial class Manage_teacherPerson : System.Web.UI.Page
{
    TeacherSerivce teacher = new TeacherSerivce();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["adminNo"] = "t0001"; // 仅用于测试，在使用时注释掉
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
    // 使用表格显示
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string info = teacher.searchStuInfo(txtSearchKey.Text.Trim());
        string[] sArray = Regex.Split(info, ",", RegexOptions.IgnoreCase);
        TableRow row = new TableRow();  //建立一个行对象       
        //建立9个单元格对象 
        TableCell stuNo = new TableCell();
        TableCell stuName = new TableCell();
        TableCell deptName = new TableCell();
        TableCell specName = new TableCell();
        TableCell clsName = new TableCell();
        TableCell stuSex = new TableCell();
        TableCell clasInstructor = new TableCell();
        TableCell studTel = new TableCell();
        TableCell studEmail = new TableCell();
        //设置9个单元格的显示内容   
        stuNo.Text = sArray[0];
        stuName.Text = sArray[1];
        deptName.Text = sArray[2];
        specName.Text = sArray[3];
        clsName.Text = sArray[4];
        stuSex.Text = sArray[5];
        clasInstructor.Text = sArray[6];
        studTel.Text = sArray[7];
        studEmail.Text = sArray[8];
        //添加9个单元格到行对象
        row.Cells.Add(stuNo);
        row.Cells.Add(stuName);
        row.Cells.Add(deptName);
        row.Cells.Add(specName);
        row.Cells.Add(clsName);
        row.Cells.Add(stuSex);
        row.Cells.Add(clasInstructor);
        row.Cells.Add(studTel);
        row.Cells.Add(studEmail);
        TableSearchResult.Rows.Add(row);  //添加行对象到表格对象               
    }
}