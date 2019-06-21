<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentseges.aspx.cs" Inherits="StudentMS_Default" %>

<!DOCTYPE >
<html>
<head>
    <title>意见与建议</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="css/jquery.gritter.css" />
    <link rel="stylesheet" href="css/matrix-style.css" />
    <link rel="stylesheet" href="css/matrix-media.css" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700,800' rel='stylesheet' type='text/css'>
    <style type="text/css">
        .auto-style1 {
            background: none repeat scroll 0 0 #F9F9F9;
            border-left: 1px solid #CDCDCD;
            border-top: 1px solid #CDCDCD;
            border-right: 1px solid #CDCDCD;
            clear: both;
            margin-top: 16px;
            margin-bottom: 16px;
            position: relative;
            left: 0px;
            top: 0px;
            height: 318px;
        }
    </style>
</head>
<body>

    <!--Header-part-->
    <div id="header">
        <h1><a href="dashboard.html">MatAdmin</a></h1>
    </div>
    <!--close-Header-part-->

    <!--top-Header-menu-->
    <div id="user-nav" class="navbar navbar-inverse">
        <ul class="nav">
            <li class="dropdown" id="profile-messages"><a title="" href="#" data-toggle="dropdown" data-target="#profile-messages" class="dropdown-toggle"><i class="icon icon-user"></i><span class="text">
                <asp:Label ID="lbName" runat="server" Text="Label"></asp:Label></span><b class="caret"></b></a>
                <ul class="dropdown-menu">
                    <li><a href="#"><i class="icon-user"></i>我的资料</a></li>
                    <li class="divider"></li>
                    <li><a href="#"><i class="icon-check"></i>我的任务</a></li>
                    <li class="divider"></li>
                    <li><a href="onLoad.aspx"><i class="icon-key"></i>退出</a></li>
                </ul>
            </li>
            <li class="dropdown" id="menu-messages"><a href="#" data-toggle="dropdown" data-target="#menu-messages" class="dropdown-toggle"><i class="icon icon-envelope"></i><span class="text">消息</span> <span class="label label-important">5</span> <b class="caret"></b></a>
                <ul class="dropdown-menu">
                    <li><a class="sAdd" title="" href="#"><i class="icon-plus"></i>新消息</a></li>
                    <li class="divider"></li>
                    <li><a class="sInbox" title="" href="#"><i class="icon-envelope"></i>收件箱</a></li>
                    <li class="divider"></li>
                    <li><a class="sOutbox" title="" href="#"><i class="icon-arrow-up"></i>发件箱</a></li>
                    <li class="divider"></li>
                    <li><a class="sTrash" title="" href="#"><i class="icon-trash"></i>发送</a></li>
                </ul>
            </li>
            <li class=""><a title="" href="#"><i class="icon icon-cog"></i><span class="text">设置</span></a></li>
            <li class=""><a title="" href="onLoad.aspx"><i class="icon icon-share-alt"></i><span class="text">退出</span></a></li>
        </ul>
    </div>

    <!--start-top-serch-->
    <div id="search">
        <input type="text" placeholder="输入搜索内容..." />
        <button type="submit" class="tip-bottom" title="Search"><i class="icon-search icon-white"></i></button>
    </div>
    <!--close-top-serch-->

    <!--sidebar-menu-->
    <div id="sidebar">
        <a href="#" class="visible-phone"><i class="icon icon-fullscreen"></i>Full width</a>
        <ul>
            <li><a href="studentperson.aspx"><i class="icon icon-signal"></i><span>学生个人信息</span></a> </li>
            <li><a href="studentpaym.aspx"><i class="icon icon-inbox"></i><span>缴纳学费</span></a> </li>
            <li><a href="studentrecord.aspx"><i class="icon icon-th"></i><span>缴费记录</span></a></li>
            <li class="active" style="left: 0px; top: 0px"><a href="#"><i class="icon icon-pencil"></i><span>意见与建议</span></a></li>
        </ul>
    </div>

    <!--main-container-part-->
    <div id="content">
        <div id="content-header">
            <div id="breadcrumb"><a href="studentpaym.aspx" title="Go to Home" class="tip-bottom"><i class="icon-home"></i>学费缴纳</a> <a href="#" class="current">意见与建议</a> </div>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="auto-style1">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-hand-right"></i></span>
                            <h5>Different notifications</h5>
                        </div>
                        <div class="widget-content notify-ui">
                            <form id="form1" runat="server">
                                <div>
                                    反馈采用匿名的方式，请放心发言！！！！
            <div>
                请选择建议的对象:<asp:DropDownList ID="ddlObject" runat="server">
                    <asp:ListItem>==请选择==</asp:ListItem>
                    <asp:ListItem Value="teacher">教务人员</asp:ListItem>
                    <asp:ListItem Value="finance">财务人员</asp:ListItem>
                    <asp:ListItem Value="admin">系统管理员</asp:ListItem>
                </asp:DropDownList>
            </div>
                                    <div>
                                        <asp:TextBox ID="tbFbMessage" runat="server" Height="183px" Width="882px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="提交反馈" OnClick="btnSubmit_Click" />


                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--end-main-container-part-->


    <!--Footer-part-->
    <div class="row-fluid">
        <div id="footer" class="span12">Copyright &copy; 2018.Company name All rights reserved.<a target="_blank" href="http://sc.chinaz.com/moban/">&#x7F51;&#x9875;&#x6A21;&#x677F;</a></div>
    </div>
    <!--End-Footer-part-->
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.ui.custom.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.gritter.min.js"></script>
    <script src="js/jquery.peity.min.js"></script>
    <script src="js/matrix.js"></script>
    <script src="js/matrix.interface.js"></script>
    <script src="js/matrix.popover.js"></script>
</body>
</html>
