<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="EditPwd.aspx.cs" Inherits="FateSky.EditPwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/user.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="main-pwd" class="bg-box">
        <div>
            原密码：<br />
            <asp:TextBox ID="txtOldPwd" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            新密码：<br />
            <asp:TextBox ID="txtNewPwd" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            确认密码：<br />
            <asp:TextBox ID="txtChkPwd" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox>
        </div>
        <div><asp:Button ID="btnSave" runat="server" Text="保存" OnClick="BtnSave_Click" CssClass="button" /></div>
        <span class="clear"></span>
    </div>
</asp:Content>
