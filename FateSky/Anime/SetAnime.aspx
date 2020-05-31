<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="SetAnime.aspx.cs" Inherits="FateSky.SetAnime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/anime.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="add-anime-box" class="bg-box">
        <div id="anime-page">
            <asp:Image ID="imgPage" Width="250" Height="357" runat="server" />
        </div>
        <div id="anime-info">
            <div id="add-anime-page">封面<br /><asp:FileUpload ID="fupPage" runat="server" /></div>
            <div id="add-anime-title">中文标题<br /><asp:TextBox ID="txtTitle" CssClass="textbox" runat="server"></asp:TextBox></div>
            <div id="add-anime-origin">日文标题<br /><asp:TextBox ID="txtOrigin" CssClass="textbox" runat="server"></asp:TextBox></div>
            <div id="add-anime-time">
                <div>年份<asp:DropDownList ID="dropYear" CssClass="button" runat="server"></asp:DropDownList></div>
                <div>月份<asp:DropDownList ID="dropMonth" CssClass="button" runat="server"></asp:DropDownList></div>
                <span class="clear"></span>
            </div>
            <div id="add-anime-depict">评级<asp:DropDownList ID="dropDepict" CssClass="button" runat="server"></asp:DropDownList></div>
            <div id="add-anime-btn"><asp:Button ID="btnSave" runat="server" CssClass="button" CommandArgument=<%#Eval("Title")%> OnClick="BtnSave_Click"/></div>
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>
