<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TuitionMSLogin.aspx.cs" Inherits="Login_TuitionMSLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统登录</title>
    <link href="css/login.css" rel="stylesheet" rev="stylesheet" type="text/css" media="all" />
    <link href="css/demo.css" rel="stylesheet" rev="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/jquery1.42.min.js"></script>
    <script type="text/javascript" src="js/jquery.SuperSlide.js"></script>

    <style type="text/css">
        .auto-style3 {
            height: 100px;
            margin: 26px auto;
            overflow: hidden;
            position: relative;
            width: 800px;
            z-index: 2;
            left: 0px;
            top: 0px;
        }

        .auto-style4 {
            margin-top: 0;
        }
    </style>

</head>

<body runat="server">

    <div class="auto-style3">
        <div class="headerLogo">
            <asp:Image ID="logo" runat="server" Height="100px" ImageUrl="~/Login/images/ic_school.png" Width="100px" />
            <asp:Image ID="logoFont" runat="server" Height="100px" ImageUrl="~/Login/images/ic_school_text.png" Width="311px" />
            <asp:Image ID="logoTitle" runat="server" CssClass="auto-style4" ImageUrl="~/Login/images/sys_name.png" />
        </div>
    </div>

    <div class="banner">

        <div class="login-aside">
            <div id="o-box-up"></div>
            <div id="o-box-down" style="table-layout: fixed;">
                <div class="error-box">
                    <asp:Label ID="lbNotice" runat="server" Text=""></asp:Label>
                </div>

                <form runat="server" class="registerform">

                            <div class="fm-item">
                                <label for="logonId" class="form-label">用户名：</label>
                                <asp:TextBox ID="tbUsername" runat="server" MaxLength="100" class="i-text"></asp:TextBox>
                                <div class="ui-form-explain">
                                </div>

                            </div>

                            <div class="fm-item">
                                <label for="logonId" class="form-label">密码：</label>
                                <asp:TextBox ID="tbPassword" runat="server" MaxLength="100" class="i-text" TextMode="Password"></asp:TextBox>
                                <div class="ui-form-explain"></div>
                            </div>


                            <div class="fm-item">
                                <label for="logonId" class="form-label"></label>
                                <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server"  Text="登录" TabIndex="4" Height="40px" Width="244px" style="border:1px solid red; background:#FD8000;1px solid red"  />
                                <div class="ui-form-explain"></div>
                            </div>

                            <div class="fm-item" style="color:white;">
                                <a href="./TuitionMSLForgetPwd.aspx" class="form-label">忘记密码：</a>
                            </div>

                </form>



            </div>

        </div>

        <div class="bd">
            <ul>
                <li style="background: url(themes/theme-pic1.jpg) #CCE1F3 center 0 no-repeat;"></li>
                <li style="background: url(themes/theme-pic2.jpg) #BCE0FF center 0 no-repeat;"></li>
            </ul>
        </div>

        <div class="hd">
            <ul></ul>
        </div>
    </div>
    <script type="text/javascript">jQuery(".banner").slide({ titCell: ".hd ul", mainCell: ".bd ul", effect: "fold", autoPlay: true, autoPage: true, trigger: "click" });</script>


    <div class="banner-shadow"></div>

    <div class="footer">
        <p>Copyright &copy; 2019.Company name All rights reserved.</p>
    </div>

</body>
</html>
