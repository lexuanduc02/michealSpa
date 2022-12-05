<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="user.aspx.vb" Inherits="admin.user" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Quản lý thành viên</title>
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
        <table id="tblView" runat="server" visible="false" style="width:100%;" cellpadding="0" cellspacing="0">
            <tr><td colspan="2" style="text-align:right; padding-bottom:5px;">
                <asp:TextBox CssClass="textbox" ID="txtUserS" Width="250px" runat="server" MaxLength="30" />
                <asp:Button CssClass="button1" ID="btnSearch" runat="server" Text="Tìm kiếm" />
            </td></tr>
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" />
                    
                    <asp:DropDownList CssClass="textbox drl" ID="drlSubID" runat="server" Width="200px" AutoPostBack="true">
                        <asp:ListItem Value="" Text="Tất cả thành viên" />
                        <asp:ListItem Value=" and AcU=0" Text="Tài khoản bị khoá" />
                        <asp:ListItem Value=" and Type=1" Text="Tài khoản quản trị" />
                    </asp:DropDownList>
                </td>
                <td style="padding-right:5px; text-align:right;">
                    <asp:Button CssClass="button" ID="btnAdd1" runat="server" Text="Thêm mới" />
                    
                </td>
            </tr>
            <tr><td colspan="2">
                <asp:DataGrid id="dtgrView" DataKeyField="ID" runat="server"  CssClass="table table-striped table-bordered" AutoGenerateColumns="False" AllowSorting="True" BorderStyle="None" cellpadding="3" Width="100%" HorizontalAlign="Center" AllowPaging="True"> 
                     <HeaderStyle Font-Bold="True" BackColor="#F4F4F4" cssclass="header" />
                       
                     <Columns>
                        <asp:TemplateColumn HeaderText="&lt;input value=&quot;0&quot; id=&quot;all_check_box&quot; onclick=&quot;CheckAll(this,'inbox');&quot; type=&quot;checkbox&quot; /&gt;">
                            <ItemTemplate><asp:CheckBox ID="inbox" runat="server" /></ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:BoundColumn DataField="Name" HeaderText="Tải khoản" ItemStyle-Width="40%" />
                        <asp:BoundColumn DataField="Email"  HeaderText="Email" ItemStyle-Width="100px" />
                        <asp:BoundColumn DataField="Mobile"  HeaderText="Điện thoại" ItemStyle-Width="100px" />
                        <asp:BoundColumn DataField="TimeU" DataFormatString="{0:d}" HeaderText="Ngày ĐK" ItemStyle-Width="100%" />
                       <asp:TemplateColumn HeaderText="Trạng&nbsp;thái"> 
                            <ItemTemplate>
                                <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                                    <asp:ListItem Value="1" Text="Mở khoá" /><asp:ListItem Value="0" Text="Khoá" />
                                </asp:DropDownList>
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                        
                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_edit.gif&quot;&gt;' HeaderText="&nbsp;Sửa&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="edit" HeaderStyle-Width="10px" />
                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_delete.gif&quot;&gt;' HeaderText="&nbsp;Delete&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="Delete" HeaderStyle-Width="10px" />
                     </Columns>
                     <PagerStyle Mode="NumericPages" BackColor="#F4F4F4" HorizontalAlign="Left" cssclass="tr-footer" />
                     
                </asp:DataGrid>
            </td></tr>
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel2" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate2" runat="server" Text="Cập nhật" />
                </td>
                <td style="padding-right:5px; text-align:right;">
                    <asp:Button CssClass="button" ID="btnAdd2" runat="server" Text="Thêm mới" />
                    
                </td>
            </tr>
        </table>
        
        <table id="tblAdd" runat="server" visible="false" style="width:100%;" cellpadding="0" cellspacing="1">
            <tr style="background-color:#3C8DBC;">
                <td colspan="2" style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnAddU1" runat="server" Text="Ghi nhận" Width="100px" />
                    
                </td>
            </tr>           
            <tr>
                <td class="td1"><div style="width:95px;">Tải khoản:</div></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtUser" Width="250px" runat="server" MaxLength="30" />
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" ControlToValidate="txtUser" errormessage="*" display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td1">Mật khẩu:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPass" Width="250px" runat="server" MaxLength="30" />
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="txtPass" errormessage="*" display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td1">Email:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtEmail" Width="250px" runat="server" MaxLength="100" />
                    <asp:Label ID="lblEmailR" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td class="td1">Họ tên:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtFullName" Width="250px" runat="server" MaxLength="50" />
                </td>
            </tr>             
            <tr>
                <td class="td1">Điện thoại:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtTel" Width="250px" runat="server" MaxLength="20" />
                </td>
            </tr>                   
            <tr>
                <td class="td1">Trạng thái:</td>
                <td class="td2">
                    <asp:CheckBox ID="chkAc" runat="server" Text="Mở/khóa tài khoản" />&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="AcU" Visible="false" runat="server" Text="Kích hoạt tài khoản" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkType" runat="server" Text="Tài khoản quản trị dự án"  />
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
