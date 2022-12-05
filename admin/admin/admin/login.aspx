<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="admin.login" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Administrator</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../styles/login.css" rel="stylesheet" />
    <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
</head>
<body class="login-page">
    <div class="login-box">
        <div class="login-logo">
            <img src="icon/logo.png" style="vertical-align:middle;display:inline-block;float:left;" alt="" />
            <span style="display: inline-block; float: left;margin:5px 0 0 15px;">Administrator</span>
            <div style ="clear:both;"></div>
        </div>
        <div class="login-box-body">
            <form id="form1" runat="server">
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" placeholder="Username" runat="server" ID="txtUserL" />
                </div>
                <div class="form-group">    
                    <asp:TextBox runat="server" ID="txtPassL" placeholder="Password" CssClass="form-control" TextMode="password" />
                </div>
                <div class="form-group" style="border-top: 1px solid #ddd; padding-top: 15px; text-align: right; margin-bottom: 5px;">
                    <a href="#" style="color: #188dd9; display: inline-block; float: left;">Forgot password?</a>
                    <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="btnlogin_Click" Text="Login" />
                </div>
            </form>
            <asp:Label runat="server" Style="color: red;" ID="lblinvalid" Text="" />
        </div>
    </div>
</body>
</html>

