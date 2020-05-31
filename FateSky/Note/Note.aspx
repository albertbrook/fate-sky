<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Note.aspx.cs" Inherits="FateSky.Note" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/note.css" rel="stylesheet" />
    <script src="../JS/note.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="add-note" class="bg-box">
        <div id="add-note-control"></div>
        <div id="add-note-main">
            <div id="add-text"><asp:TextBox ID="txtText" CssClass="textbox" runat="server" TextMode="MultiLine"></asp:TextBox></div>
            <div id="add-btn"><asp:Button ID="btnAdd" CssClass="button" runat="server" Text="添加" OnClick="BtnAdd_Click" /></div>
        </div>
    </div>
    <asp:Repeater ID="rptNote" runat="server">
        <ItemTemplate>
            <div class="text">
                <div><asp:Button ID="btnDel" CssClass="button" runat="server" Text="删除" CommandArgument=<%#Eval("UpdateTime")%> OnClick="BtnDel_Click" /></div>
                <div class="textbox">
                    <%#Eval("Text")%>
                    <div class="clear"></div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
