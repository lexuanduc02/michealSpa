<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="tab.aspx.vb" Inherits="admin.tab" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage Tabs</title>
    <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
</head>
<body>
<form id="form1" runat="server">
<!--#include file="include/iclHead.aspx"-->
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-left"><!--#include file="include/iclLeft.aspx"--></td>
    <td class="main-content-right">
        <h1 class="main-title"><asp:Label ID="lblLink" runat="server" /></h1>
        <div style="padding-bottom:10px;">
            <asp:DropDownList CssClass="textbox drl" ID="drlType" runat="server" AutoPostBack ="true" >
                <asp:ListItem Value="1" Text="Product Tabs" />
                <asp:ListItem Value="0" Text="News Tabs" />
            </asp:DropDownList>
            <asp:TextBox CssClass="textbox" ID="txtName" Width="300px" runat="server" MaxLength="50" onfocus="javascript:if(this.value=='Name'){this.value='';};" onblur="javascript:if(this.value==''){this.value='Name';};" Text="Name" />
            <asp:TextBox CssClass="textbox" ID="Tong" Width="50px" runat="server" onfocus="javascript:if(this.value=='Size'){this.value='';};" onblur="javascript:if(this.value==''){this.value='Size';};" Text="Size" />
            <asp:TextBox CssClass="textbox" ID="txtSort" Width="100px" runat="server" MaxLength="10" onfocus="javascript:if(this.value=='Sắp xếp'){this.value='';};" onblur="javascript:if(this.value==''){this.value='Sắp xếp';};" Text="Sắp xếp" />
            <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                <asp:ListItem Value="1" Text="Published" />
                <asp:ListItem Value="0" Text="Unpublished" />
            </asp:DropDownList>
            <asp:Button CssClass="button btnW" ID="btnInsert" runat="server" Text="Thêm mới" Width="70px" />
        </div>
        <table style="width:100%;" cellpadding="0" cellspacing="0">
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" Width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" Width="100px" />
                </td>
                <td style="padding-right:5px; text-align:right;">
                    
                </td>
            </tr>
            <tr><td colspan="2">
                <asp:DataGrid id="dtgrView" DataKeyField="ID" runat="server"  CssClass="table table-striped table-bordered" AutoGenerateColumns="False" AllowSorting="True" BorderStyle="None" cellpadding="3" Width="100%" HorizontalAlign="Center" AllowPaging="True"> 
                     <HeaderStyle Font-Bold="True" BackColor="#F4F4F4" cssclass="header" />
                       
                     <Columns>    
                        <asp:TemplateColumn HeaderText="&lt;input value=&quot;0&quot; id=&quot;all_check_box&quot; onclick=&quot;CheckAll(this,'inbox');&quot; type=&quot;checkbox&quot; /&gt;">
                            <ItemTemplate><asp:CheckBox ID="inbox" runat="server" /></ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn HeaderText="Name" ItemStyle-Width="100%">
                            <ItemTemplate><asp:TextBox CssClass="textbox" ID="Name" runat="server" Text='<%#Eval("Name") %>' Width="250px" MaxLength="50" /></ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Size">
                            <ItemTemplate><asp:TextBox CssClass="textbox" ID="Tong" runat="server" Text='<%#Eval("Tong") %>' Width="50px" MaxLength="10" /></ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn HeaderText="Display&nbsp;order" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:TextBox CssClass="textbox" ID="Sort" runat="server" Text='<%#Eval("Sort") %>' Width="50px" MaxLength="10" /></ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Published"> 
                            <ItemTemplate>
                                <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                                    <asp:ListItem Value="1" Text="Published" /><asp:ListItem Value="0" Text="Unpublished" />
                                </asp:DropDownList>
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                                                
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
