<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="gallery.aspx.vb" Inherits="admin.gallery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Quản lý thư viện ảnh dự án landing page</title>
     <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
    <script src="../scripts/admin/jquery-1.9.1.min.js"></script>
    <script src="../scripts/admin/inbox.js"></script>
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
    <asp:Label ID="lblIDImg" runat="server" Text="0" Visible="false" />
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-right">
        <a href="#" onclick="javascript:parent.jQuery.fancybox.close();" class="fancybox-close-small" title="Close"></a>
        <h1 class="main-title" style="margin-bottom:0;"><asp:Label ID="lblLink" runat="server" /></h1>
        <table id="tblAdd" runat="server" style="width:100%;" cellpadding="0" cellspacing="1">
            <tr class="tr-view" style="display:none;">
                <td class ="td1" style="background:#3d77cb;color:#fff;vertical-align:middle"><strong>Dự án:</strong></td>
                <td style="height:35px; padding-left:5px;color:#fff;" >
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xoá chọn" OnClientClick="javascript:return confirm('Bạn có muốn xóa tất cả tin đã chọn không?')" Visible ="false"  />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" Visible ="false"  />
                    <asp:DropDownList cssClass="textbox drl" ID="drlSubID" DataValueField ="ID" DataTextField ="Name" runat="server" Visible="false" Width="250px" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="td1"><p style="margin:0;width:110px;">Tiêu đề ảnh:</p></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtName" Width="450px" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td1"><p style="width:80px;">Chọn ảnh:</p></td>
                <td class="td2">
                    <p style="border-bottom: 1px solid #f5f5f5;color:blue; padding-bottom: 3px;margin:0 0 5px 0;">Chức năng cho phép upload nhiều ảnh / 1 lần ( < 15 ảnh  ) - Không giới hạn số lần up ảnh</p>
                    <asp:FileUpload ID="FileU" AllowMultiple="true" runat="server"/>
                    <asp:Button CssClass="button btnW" ID="btnAdd1" runat="server" Text="Ghi  nhận" />
                </td>
            </tr>
             <tr><td colspan="2">
                <asp:DataGrid id="dtgrView" DataKeyField="ID" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" AllowSorting="True" BorderStyle="None" cellpadding="3" Width="100%" HorizontalAlign="Center" AllowPaging="True"> 
                     <HeaderStyle Font-Bold="True" BackColor="#F4F4F4" cssclass="header" />
                     <Columns>    
                        <asp:TemplateColumn HeaderText="&lt;input value=&quot;0&quot; id=&quot;all_check_box&quot; onclick=&quot;CheckAll(this,'inbox');&quot; type=&quot;checkbox&quot; /&gt;">
                            <ItemTemplate><asp:CheckBox ID="inbox" runat="server" /></ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Banner" ItemStyle-Width="20%"> 
                            <ItemTemplate> 
                                <div><%#Eval("Img") %></div>
                            </ItemTemplate> 
                        </asp:TemplateColumn> 
                         <asp:TemplateColumn HeaderText="Tiêu đề" ItemStyle-Width="70%"> 
                            <ItemTemplate> 
                                <asp:TextBox CssClass="textbox" ID="Name" runat="server" Text='<%#Eval("Name") %>' Width="400px" /><br />
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Trạng&nbsp;thái" ItemStyle-Width ="10%"> 
                            <ItemTemplate>
                                <asp:DropDownList cssClass="textbox drl" ID="drlAc" runat="server">
                                    <asp:ListItem Value="1" Text="Hiển thị" />
                                    <asp:ListItem Value="0" Text="Ẩn" />
                                </asp:DropDownList>
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/up.png&quot;&gt;' HeaderText="Top" ItemStyle-HorizontalAlign ="Center" CommandName="eTop" HeaderStyle-Width="10px" />
                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_delete.gif&quot;&gt;' HeaderText="&nbsp;Xoá&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="Delete" HeaderStyle-Width="10px" />
                     </Columns>
                     <PagerStyle Mode="NumericPages" BackColor="#F4F4F4" HorizontalAlign="Left" cssclass="tr-footer" />
                </asp:DataGrid>
            </td></tr>
             <tr class="tr-view">
                <td class="td-view-left" colspan ="2">
                    <asp:Button CssClass="button" ID="btnDel2" runat="server" Text="Xoá chọn" OnClientClick="javascript:return confirm('Bạn có muốn xóa tất cả tin đã chọn không?')" />
                    <asp:Button CssClass="button" ID="btnUpdate2" runat="server" Text="Cập nhật" />
                </td>
            </tr>
        </table>
    </td>
</tr>
</table>
</form>
</body>
</html>

