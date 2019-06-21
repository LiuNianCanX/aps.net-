using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TuitionMS.BLL;

public partial class admin_showProposeContentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AdminService admin = new AdminService();
        /*
        string prop=admin.showPorpose(0);
        for (int i=1; prop!="";i++)
        {
            Label lable1 = new Label();
            this.Page.Form.Controls.Add(lable1);
            lable1.Text = prop;
           prop = admin.showPorpose(i);
        }
        */
    }
}