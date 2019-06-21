<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMasterPage.master" AutoEventWireup="true" CodeFile="infoContentPage.aspx.cs" Inherits="admin_infoContentPage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSection" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="管理员编号 "></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Label2" runat="server" Text="管理员密码 "></asp:Label><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Label3" runat="server" Text="管理员类型 "></asp:Label><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Label4" runat="server" Text="管理员电话 "></asp:Label><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;
    <asp:Button ID="btnCreate" runat="server" Text="创建" OnClick="btnCreate_Click" />
    &nbsp;
    <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
    &nbsp;
    <asp:Button ID="btnUpdate" runat="server" Text="修改" OnClick="btnUpdate_Click" />
    <asp:Label ID="lblMsg" runat="server" ></asp:Label>
</asp:Content>

