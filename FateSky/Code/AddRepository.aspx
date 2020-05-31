<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="AddRepository.aspx.cs" Inherits="FateSky.AddRepository" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/code.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="add-code-box" class="bg-box">
        <div id="add-code-demo">演示图<br /><asp:FileUpload ID="fupDemo" runat="server" /></div>
        <div id="add-code-repository">仓库名<br /><asp:TextBox ID="txtRepository" CssClass="textbox" runat="server"></asp:TextBox></div>
        <div id="add-code-url">地址<br /><asp:TextBox ID="txtUrl" CssClass="textbox" runat="server"></asp:TextBox></div>
        <div id="add-code-language">
            <asp:CheckBox ID="chkJava" Text="Java" runat="server" />
            <asp:CheckBox ID="chkPython" Text="Python" runat="server" />
            <asp:CheckBox ID="chkCSharp" Text="C#" runat="server" />
        </div>
        <div id="add-anime-btn"><asp:Button ID="btnAdd" Text="添加" runat="server" CssClass="button" OnClick="BtnAdd_Click"/></div>
    </div>
</asp:Content>
