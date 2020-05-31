<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Code.aspx.cs" Inherits="FateSky.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/code.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnAddRepo" CssClass="button" runat="server" Text="添加仓库" OnClick="BtnAddRepo_Click" />
    <asp:GridView ID="gvwCode" CssClass="bg-box" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvwCode_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="演示图">
                <ItemTemplate>
                    <asp:Image ID="imgDemo" ImageUrl=<%#Eval("Image")%> runat="server" Width="200px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Repository" HeaderText="仓库名" >
                <HeaderStyle Width="200px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="Url" HeaderText="地址" DataTextField="Url" >
                <HeaderStyle Width="400px" />
            </asp:HyperLinkField>
        </Columns>
    </asp:GridView>
</asp:Content>
