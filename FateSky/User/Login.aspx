<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FateSky.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title>登入</title>
	<link href="../CSS/default.css" rel="stylesheet" />
    <link href="../CSS/login.css" rel="stylesheet" />
    <script src="../JS/jquery-3.5.1.js"></script>
    <script src="../JS/login.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
		<div id="login-box">
            <div><asp:TextBox ID="txtUser" runat="server"></asp:TextBox></div>
            <div><asp:TextBox ID="txtPwd" runat="server"></asp:TextBox></div>
            <div><asp:CheckBox ID="chkRem" runat="server" /></div>
            <div>
                <asp:Button ID="btnSignIn" runat="server" OnClick="BtnSignIn_Click" />
                <asp:Button ID="btnSignOut" runat="server" />
                <div class="clear"></div>
            </div>
		</div>
    </form>
</body>
</html>
