<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="advert.aspx.vb" Inherits="admin.advert" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage Widgets</title>
    <script type="text/javascript" src="../scripts/admin/mBanner.js"></script>
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
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" />
                    
                    <asp:DropDownList CssClass="textbox drl" ID="drlType1" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="5" Text="Home Slider (Full HD)" />
                        <asp:ListItem Value="7" Text="Home lage banner (width:600px - Height:auto"  />
                        <asp:ListItem Value="8" Text="Đối tác (width:200px - Height:auto" />
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
                        
                        <asp:TemplateColumn HeaderText="Picture"> 
                            <ItemTemplate> 
                                <div><%#Eval("Img") %></div>
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Name / Link" ItemStyle-Width="100%"> 
                            <ItemTemplate>
                                <asp:TextBox CssClass="textbox" ID="Title" runat="server" Text='<%#Eval("Title") %>' Width="98%" MaxLength="100" />
                                <asp:TextBox CssClass="textbox" ID="Name" runat="server" Text='<%#Eval("Name") %>' Width="98%" MaxLength="300" />
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Display&nbsp;order">
                            <ItemTemplate> 
                                <asp:TextBox CssClass="textbox" ID="Sort" runat="server" Text='<%#Eval("Sort") %>' Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                         
                        <asp:TemplateColumn HeaderText="Published"> 
                            <ItemTemplate>
                                <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                                    <asp:ListItem Value="1" Text="Published" /><asp:ListItem Value="0" Text="Unpublished" />
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
                    <asp:Button CssClass="button" ID="btnAddU1" runat="server" Text="Ghi nhận" />
                    
                </td>
            </tr>
            <tr>
                <td class="td1"><div style="width:95px;">Name:</div></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="Title" Width="98%" runat="server" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td class="td1"><div style="width:95px;">Link:</div></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtName" Width="98%" runat="server" MaxLength="300" />
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" ControlToValidate="txtName" errormessage="*" display="Dynamic" />
                </td>
            </tr>
             <tr>
                <td class="td1"><div style="width:95px;">Description:</div></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtContent" Width="98%" TextMode="MultiLine" Rows="6" runat="server" MaxLength="1000" />
                </td>
            </tr>
            <tr>
                <td class="td1">Position:</td>
                <td class="td2">
                    <asp:DropDownList CssClass="textbox drl" ID="drlType" runat="server">
                        <asp:ListItem Value="5" Text="Home Slider (Full HD)" ></asp:ListItem>
                        <asp:ListItem Value="7" Text="Home lage banner (width:600px - Height:auto"  />
                        <asp:ListItem Value="8" Text="Đối tác (width:200px - Height:auto" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtH" runat="server" CssClass="textbox" Width="30px" />px)
                    <span style="color:Red;">(Require with Flash *.swf)</span>
                </td>
            </tr>
            <tr>
                <td class="td1">Sắp xếp:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtSort" Width="150px" runat="server" />
                    <span style="color:Red;">(Ex: 01, 02, 03, ... | hoặc 01.1, 01.2, 01.3, ...)</span>
                </td>
            </tr>
            <tr>
                <td class="td1">Hiển thị:</td>
                <td class="td2">
                    <asp:CheckBox ForeColor="red" ID="chkAc" runat="server" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="td1">Hình ảnh:</td>
                <td class="td2">
                    <asp:FileUpload ID="FilePro" runat="server" Width="98%" />
                    <asp:Label ID="lblImg" runat="server" />
                    <asp:ImageButton ID="ibtnDelImgT1" runat="server" ImageUrl="icon/delimg.gif" OnClientClick="javascript:return confirm('Are you sure you want to delete this picture?')" Visible="false" />
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
