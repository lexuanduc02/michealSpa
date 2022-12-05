<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="admin.index1" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administrator</title>
    <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
</head>
<body>
<form id="form1" runat="server">
<!--#include file="include/iclHead.aspx"-->
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-left"><!--#include file="include/iclLeft.aspx"--></td>
    <td class="main-content-right">
        <h1 style="color:#3162a6; font-size:18px;border-bottom: #95b3de 1px solid;">Xin chào, admin</h1>
        <asp:Panel ID="Panel_pass" runat="server" Visible="false">
            <div class="contentdisplay">
            <div style="padding: 2px; text-align: center; margin-left: 26px; margin-right: 26px;">
                <br /><asp:Label cssClass="corange" runat="server" id="lblpass" Visible="false" />
            </div><br />
            <table style="WIDTH: 100%; border:0;" cellpadding="0" cellspacing="0">
                <tr>
                  <td style="WIDTH: 150px; TEXT-ALIGN: right"><span class="V12">Mật khẩu cũ:&nbsp;&nbsp;</span> </td>
                  <td style="WIDTH: 350px">
                      <asp:TextBox ID="txtPassOld" runat="server" CssClass="textbox" Width="190px" TextMode="password"></asp:TextBox>
                      <asp:RequiredFieldValidator runat="server" id="chkpass1" ControlToValidate="txtPassOld" cssClass="cred2" errormessage="*<br />Yêu cầu nhập mật khẩu cũ!" display="Dynamic" />
                  </td></tr>
                <tr>
                  <td style="HEIGHT: 7px" colspan="2"></td></tr>
                <tr>
                  <td style="WIDTH: 150px; TEXT-ALIGN: right"><span class="V12">Mật khẩu mới:&nbsp;&nbsp;</span> </td>
                  <td>
                    <asp:TextBox ID="txtPassNew" runat="server" CssClass="textbox" Width="190px" TextMode="password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" id="chkpass2" ControlToValidate="txtPassNew" cssClass="cred2" errormessage="*<br />Yêu cầu nhập mật khẩu mới!" display="Dynamic" />
                  </td></tr>
                <tr>
                  <td style="HEIGHT: 7px" colspan="2"></td></tr>
                <tr>
                  <td style="WIDTH: 150px; TEXT-ALIGN: right"><span class="V12">Xác nhận lại:&nbsp;&nbsp;</span> </td>
                  <td>
                    <asp:TextBox ID="txtPassReNew" runat="server" CssClass="textbox" Width="190px" TextMode="password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" id="chkpass3" ControlToValidate="txtPassReNew" cssClass="cred2" errormessage="*<br />Yêu cầu xác nhận lại mật khẩu!" display="Dynamic" />
                  </td></tr>
                <tr>
                  <td style="HEIGHT: 7px" colspan="2"></td></tr>
                <tr>
                  <td style="BACKGROUND: #f7f7f7; WIDTH: 100px; TEXT-ALIGN: right; height: 24px;"></td>
                  <td style="BACKGROUND: #f7f7f7; TEXT-ALIGN: left; height: 24px;" >
                    <asp:Button CssClass="button" ID="btnpass" runat="server" width="70" Text="Thay đổi" />
                  </td></tr>
                <tr>
                  <td colspan="2" char="T12" style="line-height:150%;"><br /><span style="MARGIN-LEFT: 5px; color:Red;">
                        <b>Một số lưu ý khi thay đổi mật khẩu</b></span><br />
                    <span class="T11" style="MARGIN-LEFT: 10px">
                        1. Nên thay đổi mật khẩu thường xuyên hoặc login lần đầu tiên để bảo mật thông tin của bạn.</span><br />
                    <span class="T11" style="MARGIN-LEFT: 10px">
                        2. Mật khẩu không dung ký tự đặc biệt, không dung dấu cách.</span><br />
                    <span class="T11" style="MARGIN-LEFT: 10px">
                        3. Mật khẩu đặt nên đặt dễ nhớ đối với bạn.</span><br /><br /></td>
                </tr>
            </table>
            </div>
        </asp:Panel> 
        <asp:Panel ID="Panel_SQL" runat="server" Visible="false">
            <div class="contentdisplay">
                <table style="width:100%;" cellpadding="0" cellspacing="1">
                    <tr>
                        <td class="td1"><div style="width:120px;">User:</div></td>
                        <td class="td2">
                            <asp:TextBox CssClass="textbox" ID="txtUserL" Width="300px" runat="server" />
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" ControlToValidate="txtUserL" cssClass="cred2" errormessage="* not null" display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">Pass:</td>
                        <td class="td2">
                            <asp:TextBox CssClass="textbox" ID="txtPassL" Width="300px" runat="server" TextMode="password" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td1"><div style="width:120px;">SQL:</div></td>
                        <td class="td2">
                            <asp:TextBox CssClass="textbox" ID="txtSQL" Width="300px" runat="server" Rows="9" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color:#ffffff;">&nbsp;</td>
                        <td style="vertical-align:top;padding-bottom:10px;background-color:#ffffff;">
                            <asp:Button runat="server" Text="Cập nhật" id="btnSQL"  Width="180px" CssClass="button" />
                            &nbsp;<input type="reset" class="button" name="cmdReset" value="Điền lại mẫu" style="width:180px;" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel> 
    </td>
</tr>
</table>
<!--#include file="include/iclFooter.aspx"-->
</form>
</body>
</html>
