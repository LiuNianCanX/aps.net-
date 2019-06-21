using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuitionMS.BLL;

public partial class Manage_teacherChange : System.Web.UI.Page
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
            panelDept.Visible = true;
            panelSpec.Visible = false;
            panelClass.Visible = false;
            panelStud.Visible = false;
        }
    }

    // 显示不同界面
    protected void ShowPanel(string type)
    {
        switch (type)
        {
            case "dept": {
                    panelDept.Visible = true;
                    panelSpec.Visible = false;
                    panelClass.Visible = false;
                    panelStud.Visible = false;
                    break;
                }
            case "spec":
                {
                    panelDept.Visible = false;
                    panelSpec.Visible = true;
                    panelClass.Visible = false;
                    panelStud.Visible = false;
                    break;
                }
            case "clas":
                {
                    panelDept.Visible = false;
                    panelSpec.Visible = false;
                    panelClass.Visible = true;
                    panelStud.Visible = false;
                    break;
                }
            case "stud":
                {
                    panelDept.Visible = false;
                    panelSpec.Visible = false;
                    panelClass.Visible = false;
                    panelStud.Visible = true;
                    break;
                }
        };

    }
    // 通过按钮切换界面
    protected void BtnDeptShow_Click(object sender, EventArgs e)
    {
        ShowPanel("dept");
    }

    protected void BtnSpecShow_Click(object sender, EventArgs e)
    {
        ShowPanel("spec");
    }

    protected void BtnClasShow_Click(object sender, EventArgs e)
    {
        ShowPanel("clas");

    }

    protected void BtnStudShow_Click(object sender, EventArgs e)
    {
        ShowPanel("stud");
    }

    // 调用逻辑层接口对数据进行添加、删除、修改
    // 添加学院
    protected void BtnAddDepartment_Click(object sender, EventArgs e)
    {
        bool a = teacher.addDepartment(tbDeptNo.Text.ToString().Trim(), tbDeptName.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "添加学院" + tbDeptName.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "添加学院" + tbDeptName.Text.ToString().Trim() + "失败！";
        }
    }
    // 更新学院
    protected void BtnUpdeteDepartment_Click(object sender, EventArgs e)
    {
        bool a = teacher.updateDepartment(tbDeptNo.Text.ToString().Trim(), tbDeptName.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "更新学院" + tbDeptName.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "更新学院" + tbDeptName.Text.ToString().Trim() + "失败！";
        }
    }
    // 删除学院
    protected void BtnDeleteDepartment_Click(object sender, EventArgs e)
    {
        bool a = teacher.delDepartment(tbDeptNo.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "删除学院" + tbDeptName.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "删除学院" + tbDeptName.Text.ToString().Trim() + "失败！";
        }
    }
    // 添加专业
    protected void BtnAddSpeciality_Click(object sender, EventArgs e)
    {
        bool a = teacher.addSpeciality(tbSpecNo.Text.ToString().Trim(), tbSpecName.Text.ToString().Trim(), 
            tbSpecManager.Text.ToString().Trim(), tbDeptByS.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "添加专业" + tbSpecNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "添加专业" + tbSpecNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 更新专业
    protected void BtnUpdateSpeciality_Click(object sender, EventArgs e)
    {
        bool a = teacher.updateSpe(tbSpecNo.Text.ToString().Trim(), tbSpecName.Text.ToString().Trim(),
            tbSpecManager.Text.ToString().Trim(), tbDeptByS.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "更新专业" + tbSpecNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "更新专业" + tbSpecNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 删除专业
    protected void BtnDeleteSpeciality_Click(object sender, EventArgs e)
    {
        bool a = teacher.delSpe(tbSpecNo.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "删除专业" + tbSpecNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "删除专业" + tbSpecNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 添加班级
    protected void BtnAddClass_Click(object sender, EventArgs e)
    {
        bool a = teacher.addClass(tbClasNo.Text.ToString().Trim(), tbClasName.Text.ToString().Trim()
            , tbClasManager.Text.ToString().Trim(), int.Parse(tbClasNum.Text.ToString().Trim())
            , tbDeptNo.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "添加班级" + tbClasNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "添加班级" + tbClasNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 更新班级
    protected void BtnUpdateClass_Click(object sender, EventArgs e)
    {
        bool a = teacher.updateCls(tbClasNo.Text.ToString().Trim(), tbClasName.Text.ToString().Trim()
            , tbClasManager.Text.ToString().Trim(), int.Parse(tbClasNum.Text.ToString().Trim())
            , tbDeptNo.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "更新班级" + tbClasNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "更新班级" + tbClasNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 删除班级
    protected void BtnDeleteClass_Click(object sender, EventArgs e)
    {
        bool a = teacher.delCls(tbClasNo.Text.Trim());
        if (a == true)
        {
            lblMsg.Text = "删除班级" + tbClasNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "删除班级" + tbClasNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 添加学生
    protected void BtnAddstu_Click(object sender, EventArgs e)
    {
        bool a = teacher.InsertStudent(tbStudNo.Text.ToString().Trim(),
            tbStudPwd.Text.ToString().Trim(),
            tbStudName.Text.ToString().Trim(), 
            tbStudClas.Text.ToString().Trim(), 
            tbStudBirth.Text.ToString().Trim(), 
            tbStudTel.Text.ToString().Trim(), 
            tbStudEmail.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "添加学生" + tbStudNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "添加学生" + tbStudNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 更新学生
    protected void BtnUpdatestu_Click(object sender, EventArgs e)
    {
        bool a = teacher.updateStu(tbStudNo.Text.ToString().Trim(),
            tbStudPwd.Text.ToString().Trim(),
            tbStudName.Text.ToString().Trim(),
            tbStudClas.Text.ToString().Trim(),
            tbStudBirth.Text.ToString().Trim(),
            tbStudTel.Text.ToString().Trim(),
            tbStudEmail.Text.ToString().Trim()
            );
        if (a == true)
        {
            lblMsg.Text = "更新学生" + tbStudNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "更新学生" + tbStudNo.Text.ToString().Trim() + "失败！";
        }
    }
    // 删除学生
    protected void BtnDeletestu_Click(object sender, EventArgs e)
    {
        bool a = teacher.delStu(tbStudNo.Text.ToString().Trim());
        if (a == true)
        {
            lblMsg.Text = "删除学生" + tbStudNo.Text.ToString().Trim() + "成功！";
        }
        else
        {
            lblMsg.Text = "删除学生" + tbStudNo.Text.ToString().Trim() + "失败！";
        }
    }
}