<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pass.aspx.vb" Inherits="admin.pass" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Change password</title>
    <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
</head>
<body>
<form id="form1" runat="server">
<asp:Label ID="lblID" runat="server" Text="0" Visible="false" />
<!--#include file="include/iclHead.aspx"-->
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-left"><!--#include file="include/iclLeft.aspx"--></td>
    <td class="main-content-right">
        <h1 class="main-title"><asp:Label ID="lblLink" runat="server" /></h1>
        <table id="tblAdd" runat="server" style="width:100%;" cellpadding="0" cellspacing="1">
            <tr style="background-color:#3C8DBC;display:none" >
                <td colspan="2" class="td-view-left">
                    <asp:Button CssClass="button" ID="btnAddU1" runat="server" Text="Ghi nhận" />
                </td>
            </tr>
            <tr>
                <td class="td1"><p style="width:120px;padding:0;margin:0">Old password:</p></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPassC" runat="server" Width="350px" TextMode="password" />
                    <input type="text" class="textbox" id ="hd" name="hd" runat="server" style="visibility:hidden; width:1px;" />
                    <asp:RequiredFieldValidator runat="server" id="rfvName" SetFocusOnError="true" ControlToValidate="txtPassC" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                    <asp:CompareValidator id="CompareValidator3" runat="server" ControlToValidate="txtPassC"  ForeColor="Red" ControlToCompare="hd" ErrorMessage="<br/>* Old password is not correct!" Display="dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td1"><p style="width:120px;padding:0;margin:0">New password:</p></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPassC1" runat="server" Width="350px" TextMode="password" MaxLength="30" />
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtPassC1" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                </td>
            </tr>
             <tr>
                <td class="td1"><p style="width:120px;padding:0;margin:0">Confirm new password:</p></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPassCR1" runat="server" Width="350px" TextMode="password" MaxLength="30" />
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" SetFocusOnError="true" ControlToValidate="txtPassCR1" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                    <asp:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="txtPassCR1"  ForeColor="Red" ControlToCompare="txtPassC1" ErrorMessage="<br/>* Confirm password is not correct!" Display="dynamic" />
                </td>
            </tr> 
            <tr>
                <td class="td1">&nbsp;</td>
                <td class="td1" style="text-align:left;">
                    <asp:Button CssClass="button" ID="btnAddU2" runat="server" Text="Ghi nhận" Width="100px" />
                </td>
            </tr>
        </table>
    </td>
</tr>
</table>
<!--#include file="include/iclFooter.aspx"-->
</form>
</body>
</html>
