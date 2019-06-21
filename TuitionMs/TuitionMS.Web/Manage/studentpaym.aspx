<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentpaym.aspx.cs" Inherits="StudentMS_TuiTionStuMSPay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>缴纳学费</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/bootstrap-responsive.min.css" />
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
            left: 4px;
            top: 9px;
            width: 1150px;
            height: 436px;
        }

        .auto-style3 {
            left: 0px;
            top: 0px;
            height: 551px;
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
                <asp:Label ID="lbName" runat="server" Text=""></asp:Label></span><b class="caret"></b></a>
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
        <a href="#" class="visible-phone"><i class="icon icon-inbox"></i>Widgets</a>
        <ul>
            <li><a href="studentperson.aspx"><i class="icon icon-signal"></i><span>学生个人信息</span></a> </li>
            <li class="active"><a href="studentpaym.aspx"><i class="icon icon-inbox"></i><span>缴纳学费</span></a> </li>
            <li><a href="studentrecord.aspx"><i class="icon icon-th"></i><span>缴费记录</span></a></li>
            <li><a href="studentseges.aspx"><i class="icon icon-pencil"></i><span>意见与建议</span></a></li>
        </ul>
    </div>
    <!--sidebar-menu-->

    <!--main-container-part-->
    <div id="content" class="auto-style3">
        <div id="content-header">
            <div id="breadcrumb"><a href="#" title="Go to Home" class="tip-bottom"><i class="icon-home"></i>学费缴纳 </div>
        </div>
        <div class="container-fluid">
            <div class="auto-style1">
                <div class="widget-title">
                    <span class="icon"><i class="icon-file"></i></span>
                    <h5>学费缴纳表</h5>
                </div>
                <div class="widget-content nopadding">
                    <form id="form1" runat="server">
                        <div>
                            <!--显示查询页面-->
                            <asp:Panel ID="Panelshow" runat="server">
                                <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="True" DataTextField="termName" DataValueField="termNo" OnSelectedIndexChanged="DdlTerm_SelectedIndexChanged" AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <br />
                                <asp:DetailsView Style="width: 100%;" ID="dvFee" runat="server" AutoGenerateRows="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Height="50px" Width="1147px">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <EditRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <Fields>
                                        <asp:BoundField DataField="Tuition" HeaderText="学费" />
                                        <asp:BoundField DataField="Premiun" HeaderText="保险费" />
                                        <asp:BoundField DataField="Accom" HeaderText="住宿费" />
                                        <asp:BoundField DataField="Book" HeaderText="书本费" />
                                        <asp:BoundField DataField="Other" HeaderText="其他费用" />
                                        <asp:BoundField DataField="Sloans" HeaderText="贷款" />
                                        <asp:BoundField DataField="STuiWaiver" HeaderText="学费减免" />
                                    </Fields>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                </asp:DetailsView>
                                <div>
                                    <span>费用总额: ￥<asp:Label ID="lbTotal" runat="server" Text="Label"></asp:Label></span>
                                    <asp:Button ID="btnPay" runat="server" Text="支付" OnClick="BtnPay_Click" CssClass="btn btn-success" />
                                </div>

                            </asp:Panel>
                            <!--显示支付页面-->
                            <asp:Panel ID="Panelpay" runat="server">
                                <div style="text-align: center">
                                    <h2 style="display: inline-block">请使用支付宝或微信扫码支付</h2>

                                </div>
                                <div style="text-align: center">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="img/twoCode.jpg" Height="202px" Width="217px" />
                                </div>
                                <div style="text-align: center">
                                    <asp:Button ID="btnPayed" runat="server" OnClick="BtnPayed_Click" Text="支付完成" CssClass="btn btn-success" />
                                </div>
                            </asp:Panel>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!--main-container-part-->

    <!--Footer-part-->
    <div class="row-fluid">
        <div id="footer" class="span12">Copyright &copy; 2018.Company name All rights reserved.<a target="_blank" href="http://sc.chinaz.com/moban/">&#x7F51;&#x9875;&#x6A21;&#x677F;</a></div>
    </div>
    <!--end-Footer-part-->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.ui.custom.js"></script>
    <script src="js/matrix.js"></script>
</body>
</html>
