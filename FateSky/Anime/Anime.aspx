<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Anime.aspx.cs" Inherits="FateSky.Anime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/anime.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    <div id="anime-left">
        <asp:Repeater ID="rptAnime" runat="server">
            <ItemTemplate>
                <div class="anime-info bg-box">
                    <div class="anime-info-left">
                        <div class="anime-page button"><asp:Image ID="imgPage" runat="server" Width="250" Height="357" ImageUrl=<%#Eval("Image")%> /></div>
                        <div class="anime-btn">
                            <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="button" CommandArgument=<%#Eval("Title")%> OnClick="BtnEdit_Click" />
                            <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="button" CommandArgument=<%#Eval("Title")%> OnClick="BtnDel_Click" OnClientClick="return check('确认删除吗？');" />
                        </div>                    </div>
                    <div class="anime-info-right">
                        <div class="anime-text textbox"><%#Eval("Title")%></div>
                        <div class="anime-text textbox"><%#Eval("Origin")%></div>
                        <div class="anime-time">
                            <div class="textbox"><%#Eval("Year")%></div>
                            <div class="textbox"><%#Eval("Month")%></div>
                            <span class="clear"></span>
                        </div>
                        <div class="anime-text textbox"><%#Eval("Depict")%></div>
                    </div>
                    <div class="clear"></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="anime-right">
        <div id="anime-search" class="bg-box">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="button" OnClick="BtnSearch_Click" />
        </div>
        <div id="anime-select" class="bg-box">
            <asp:ListBox ID="lstDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstDate_SelectedIndexChanged"></asp:ListBox>
        </div>
    </div>
</asp:Content>
