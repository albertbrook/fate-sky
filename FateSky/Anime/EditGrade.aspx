<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="EditGrade.aspx.cs" Inherits="FateSky.EditGrade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/anime.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="grade-box" class="bg-box">
        <div>level 1<asp:TextBox ID="txtOne" runat="server" CssClass="textbox"></asp:TextBox></div>
        <div>level 2<asp:TextBox ID="txtTwo" runat="server" CssClass="textbox"></asp:TextBox></div>
        <div>level 3<asp:TextBox ID="txtThree" runat="server" CssClass="textbox"></asp:TextBox></div>
        <div>level 4<asp:TextBox ID="txtFour" runat="server" CssClass="textbox"></asp:TextBox></div>
        <div>level 5<asp:TextBox ID="txtFive" runat="server" CssClass="textbox"></asp:TextBox></div>
        <div>level 6<asp:TextBox ID="txtSix" runat="server" CssClass="textbox"></asp:TextBox></div>
        <div id="save-btn"><asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button" OnClick="BtnSave_Click" /></div>
    </div>
</asp:Content>
