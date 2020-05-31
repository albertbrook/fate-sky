<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="UpAvatar.aspx.cs" Inherits="FateSky.UpAvatar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/user.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="main-avatar" class="bg-box">
        <div id="main-avatar-show">
            <asp:Image ID="imgAvatar" runat="server" Width="400" Height="400" CssClass="button" />
        </div>
        <div id="main-avatar-up">
            <asp:FileUpload ID="fupAvatar" runat="server" /><br />
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button" OnClick="BtnSave_Click" />
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>
