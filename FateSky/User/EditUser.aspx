<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="FateSky.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/user.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="main-user" class="bg-box">
        <div id="main-img">
            <asp:ImageButton ID="ibtnAvatar" runat="server" Width="400" Height="400" CssClass="button" OnClick="IbtnAvatar_Click" />
        </div>
        <div id="main-info">
            <div>UID：<asp:Literal ID="ltlUid" runat="server"></asp:Literal></div>
            <div>用户名<br /><asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox></div>
            <div>签名<br /><asp:TextBox ID="txtMotto" runat="server" CssClass="textbox" TextMode="MultiLine"></asp:TextBox></div>
            <div>
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="BtnSave_Click" CssClass="button" />
                <asp:Button ID="btnPwd" runat="server" Text="修改密码" OnClick="BtnPwd_Click" CssClass="button" />
            </div>
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>
