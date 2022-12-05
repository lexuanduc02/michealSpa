<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="districts.aspx.vb" Inherits="admin.district" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Provinces</title>
     <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
     <link href="../styles/admin.css" rel="stylesheet" />
    <link href="../scripts/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <script src="../scripts/fancybox/jquery.fancybox.js" type="text/javascript"></script>
        <style type="text/css">
        .fancybox-close-small:after {
            content: '(Đóng lại)';
            top:0;
            right:0;
            width:100px;
            font-size:15px;color:red
        }
        .fancybox-close-small:hover:after {
          border-radius:0;
          color:red;
          background: none; }
    </style>
</head>
<body>
<form id="form1" runat="server">
<asp:Label ID="lblID" runat="server" Text="0" Visible="false" />
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-right">
        <a href="#" onclick="javascript:parent.jQuery.fancybox.close();" class="fancybox-close-small" title="Close"></a>
        <h1 class="main-title"><asp:Label ID="lblLink" runat="server" /></h1>
        <table id="tblView" runat="server" visible="false" style="width:100%;" cellpadding="0" cellspacing="0">
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" Width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" Width="100px" />
                    <asp:DropDownList CssClass="textbox drl" ID="drlSubID" runat="server" Width="200px" AutoPostBack="true" Visible="false">
                    </asp:DropDownList>
                </td>
                <td style="padding-right:5px; text-align:right;">
                    <asp:Button CssClass="button" ID="btnAdd1" runat="server" Text="Thêm mới" Width="100px" />
                    
                </td>
            </tr>
            <tr><td colspan="2">
                <asp:DataGrid id="dtgrView" DataKeyField="ID" runat="server"  CssClass="table table-striped table-bordered" AutoGenerateColumns="False" AllowSorting="True" BorderStyle="None" cellpadding="3" Width="100%" HorizontalAlign="Center" AllowPaging="True"> 
                     <HeaderStyle Font-Bold="True" BackColor="#F4F4F4" cssclass="header" />
                       
                     <Columns>    
                        <asp:TemplateColumn HeaderText="&lt;input value=&quot;0&quot; id=&quot;all_check_box&quot; onclick=&quot;CheckAll(this,'inbox');&quot; type=&quot;checkbox&quot; /&gt;">
                            <ItemTemplate>
                                <asp:CheckBox ID="inbox" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Name" ItemStyle-Width="50%">
                            <ItemTemplate> 
                                <asp:TextBox CssClass="textbox" ID="Name" runat="server" Text='<%#Eval("Name") %>' Width="90%" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Display&nbsp;order">
                            <ItemTemplate> 
                                <asp:TextBox CssClass="textbox" ID="Po" runat="server" Text='<%#Eval("DisplayOrder") %>' Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Published" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"> 
                            <ItemTemplate>
                                <img src="icon/<%# IIf(Eval("Published") = True, "true.gif", "false.gif")%>" alt="" />
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
                    <asp:Button CssClass="button" ID="btnDel2" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" Width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate2" runat="server" Text="Cập nhật" Width="100px" />
                </td>
                <td style="padding-right:5px; text-align:right;">
                    <asp:Button CssClass="button" ID="btnAdd2" runat="server" Text="Thêm mới" Width="100px" />
                    
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
                <td class="td1"><div style="width:95px;">Name:</div></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtName" Width="98%" runat="server" />
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" ControlToValidate="txtName" errormessage="*" display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td1">Sắp xếp:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPo" Width="150px" runat="server" />
                    <span style="color:Red;">(Ex : 01, 02, 03, ... | or 01.1, 01.2, 01.3, ...)</span>
                </td>
            </tr>
            <tr>
                <td class="td1">Hiển thị:</td>
                <td class="td2">
                    <asp:CheckBox ForeColor="red" ID="chkAc" runat="server"  Checked="true" />
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
</form>
</body>
</html>