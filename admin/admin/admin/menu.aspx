<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="menu.aspx.vb" Inherits="admin.menu" ValidateRequest="false" EnableViewStateMac="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Quản lý menu</title>
    <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
</head>
<body>
<form id="form1" runat="server">
<asp:Label ID="lblID" runat="server" Visible="false" />
<asp:Label ID="lblM" runat="server" Text="" Visible="false" />
<!--#include file="include/iclHead.aspx"-->
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-left"><!--#include file="include/iclLeft.aspx"--></td>
    <td class="main-content-right">
        <h1 class="main-title"><asp:Label ID="lblLink" runat="server" /></h1>
        <table id="tblView" runat="server" visible="false"  style="width:100%;" cellpadding="0" cellspacing="0">
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xoá chọn" OnClientClick="javascript:return confirm('Bạn có muốn xóa tất cả tin đã chọn không?')" Visible="false" />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" />

                    <asp:DropDownList CssClass="textbox drl" ID="drlSubID" runat="server" DataValueField="ID" DataTextField="Name" Width="200px" AutoPostBack="true" Visible="false" />
                </td>
                <td style="padding-right:5px; text-align:right;">
                    <asp:Button CssClass="button" ID="btnAdd1" runat="server" Text="Thêm mới" />
                    
                </td>
            </tr>
            <tr><td colspan="2">
                <asp:DataGrid id="dtgrView" DataKeyField="ID" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" AllowSorting="True" BorderStyle="None" cellpadding="3" Width="100%" HorizontalAlign="Center" AllowPaging="True"> 
                     <HeaderStyle Font-Bold="True" BackColor="#F4F4F4" cssclass="header" />
                     
                         <Columns>   
                            <asp:TemplateColumn HeaderText="&lt;input value=&quot;0&quot; id=&quot;all_check_box&quot; onclick=&quot;CheckAll(this,'inbox');&quot; type=&quot;checkbox&quot; /&gt;" Visible="false">
                                <ItemTemplate><asp:CheckBox ID="inbox" runat="server" /></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn Visible="false" HeaderText="Icon">
                                <ItemTemplate><img src="/images/menu/<%#Eval("ImgM") %>" style="height:35px; width:auto;" alt="menu" /></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn Visible="false" HeaderText="Ảnh">
                                <ItemTemplate><img src="/images/menu/<%#Eval("ImgM3") %>" style="height:35px; width:auto;" alt="menu" /></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tiêu đề" ItemStyle-Width="100%">
                                <ItemTemplate><%#Eval("Name")%><br />
                                <%#Eval("URL") %>    
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:BoundColumn DataField="Type" HeaderText="Loại" ItemStyle-Width="20px" />
                            
                            <asp:TemplateColumn HeaderText="Sẵp&nbsp;xếp">
                                <ItemTemplate><asp:TextBox CssClass="textbox" ID="txtSort" runat="server" Text='<%#Eval("SortM") %>' Width="50px" MaxLength="10" /></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Trạng&nbsp;thái"> 
                                <ItemTemplate>
                                    <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                                        <asp:ListItem Value="1" Text="Hiển thị" />
                                        <asp:ListItem Value="2" Text="Hiển thị cả chân trang" />
                                        <asp:ListItem Value="0" Text="Ẩn" />
                                    </asp:DropDownList>
                                </ItemTemplate> 
                            </asp:TemplateColumn>
                            
                            <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_edit.gif&quot;&gt;' HeaderText="&nbsp;Sửa&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="edit" HeaderStyle-Width="10px" />
                            <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_delete.gif&quot;&gt;' HeaderText="&nbsp;Xóa&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="Delete" HeaderStyle-Width="10px" />
                         </Columns>
                     <PagerStyle Mode="NumericPages" BackColor="#F4F4F4" HorizontalAlign="Left" cssclass="tr-footer" />
                     
                </asp:DataGrid>
            </td></tr>
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel2" runat="server" Text="Xoá chọn" OnClientClick="javascript:return confirm('Bạn có muốn xóa tất cả tin đã chọn không?')" Visible="false" />
                    <asp:Button CssClass="button" ID="btnUpdate2" runat="server" Text="Cập nhật" />
                </td>
                <td style="padding-right:5px; text-align:right;">
                    <asp:Button CssClass="button" ID="btnAdd2" runat="server" Text="Thêm mới" />
                    
                </td>
            </tr>
        </table>
        
        <table id="tblAdd" runat="server" visible="false" style="width:100%;" cellpadding="0" cellspacing="1">
            <tr style="background-color:#3C8DBC;">
                <td colspan="2" style="height:35px; padding-left:15px; padding-right:15px;">
                    <span style="float:right;"></span>
                    <asp:Button CssClass="button" ID="btnAddU1" runat="server" Text="Ghi nhận" />
                   
                    <asp:DropDownList CssClass="textbox drl" ID="drlMenuID" runat="server" DataValueField="ID" DataTextField="Name" Width="200px" Visible="false" />
                </td>
            </tr>
            <tr>
                <td class="td1">Tiêu đề:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtName" Width="650px" runat="server" MaxLength="100" />
                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" ControlToValidate="txtName" errormessage="*" display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td1">URL:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtURL" Width="650px" runat="server" MaxLength="200" />
                </td>
            </tr>
            <tr style="display:none;">
                <td class="td1">Tiêu đề Home:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="NameMH" Width="650px" runat="server" MaxLength="100" /></td>
            </tr>
            <tr>
                <td class="td1">Mô tả ngắn:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtContent" runat="server" Width="650px" Rows="3" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td class="td1">Title:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="title" Width="650px" runat="server" MaxLength="200" /></td>
            </tr>
            <tr>
                <td class="td1">Keywords:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="keywords" Width="650px" runat="server" MaxLength="100" /></td>
            </tr>
            <tr>
                <td class="td1">Description:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="description" runat="server" Width="650px" Rows="3" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td class="td1">Sắp xếp:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtSortM" Width="150px" runat="server" />
                    <span style="color:Red;">(Sắp xếp tăng dần, VD: 01, 02, 03, ... | hoặc 01.1, 01.2, 01.3, ...)</span>
                </td>
            </tr>
            <tr>
                <td class="td1">Vị trí:</td>
                <td class="td2">
                    <asp:CheckBox ID="HomeM" runat="server"  Text="Active trang chủ " /><br />
                    <asp:CheckBox ID="BottomM" runat="server"  Text="Menu chân trang" />
                    <asp:CheckBox ID="TopM" Visible="false" runat="server"  Text="Menu Top " />
                    <asp:CheckBox ID="HotM" runat="server"  Visible="false" Text="Active trang chủ " />
                </td>
            </tr>            
            <tr>
                <td class="td1">Trạng thái:</td>
                <td class="td2">
                    <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                        <asp:ListItem Value="1" Text="Hiển thị" />
                        <asp:ListItem Value="2" Text="Hiển thị cả chân trang" />
                        <asp:ListItem Value="0" Text="Ẩn" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td1">Loại menu:</td>
                <td class="td2" style="line-height:180%;"> 
		            <input name="rbType" value="1" id="rb_1" tabindex="1" onclick="swap_posticon('pi_1')" type="radio" />
		            <label for="rb_1" id="pi_1">Pro: Dự án</label><br />
		            
		            <input name="rbType" value="2" id="rb_2" tabindex="1" onclick="swap_posticon('pi_2')" type="radio" />
		            <label for="rb_2" id="pi_2">News: Tin tức</label><br />
		            
		            <input name="rbType" value="0" id="rb_0" tabindex="1" onclick="swap_posticon('pi_0')" type="radio" />
		            <label for="rb_0" id="pi_0">Blank: Nội dung</label>
                    
                    <asp:Label ID="lblType" runat="server" Text="0" Visible="false" />
                    <%="<script type='text/javascript'>document.getElementById('rb_" & lblType.Text & "').checked=true;</script>"%>
                </td>
            </tr>           
            <tr>
                <td class="td1">Nội dung:</td>
                <td class="td2">
                    <div style="display:none;"><asp:CheckBox ID="chklink" runat="server" Text="Xóa link trong nội dung bài viết" /></div>
                    <CKEditor:CKEditorControl ID="txtDescs" BasePath="/Content/ckeditor/" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td1">Icon:</td>
                <td class="td2">
                    <asp:FileUpload ID="FilePro" runat="server" Width="650px" />
                    <div style="color:Red;">(Chỉ hỗ trợ ảnh có định dạng *.jpg, *.gif và dung lượng < 200kb)</div>
                    <asp:Image ID="imgP1" Width="60px" runat="server" Visible="false" />
                    <asp:ImageButton ID="ibtnDelImgT1" runat="server" ImageUrl="icon/delimg.gif" OnClientClick="javascript:return confirm('Bạn có muốn xoá ảnh này không?')" Visible="false" />
                </td>
            </tr>
            <tr id="tr_img2" runat ="server" visible ="false" >
                <td class="td1">Banner trang chủ:</td>
                <td class="td2">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="650px" />
                    <div style="color:Red;">(Chỉ hỗ trợ ảnh có định dạng *.jpg, *.gif và dung lượng < 200kb - Kích thước 370x380px)</div>
                    <asp:Image ID="Image1" Width="60px" runat="server" Visible="false" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="icon/delimg.gif" OnClientClick="javascript:return confirm('Bạn có muốn xoá ảnh này không?')" Visible="false" />
                </td>
            </tr>   
            <tr>
                <td class="td1">Ảnh đại diện:</td>
                <td class="td2">
                    <asp:FileUpload ID="FileUpload2" runat="server" Width="650px" />
                    <div style="color:Red;">(Chỉ hỗ trợ ảnh có định dạng *.jpg, *.gif và dung lượng < 200kb - Kích thước 370x380px)</div>
                    <asp:Image ID="Image2" Width="60px" runat="server" Visible="false" />
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="icon/delimg.gif" OnClientClick="javascript:return confirm('Bạn có muốn xoá ảnh này không?')" Visible="false" />
                </td>
            </tr>                      
            <tr>
                <td class="td1"><div style="width:85px;">&nbsp;</div></td>
                <td class="td1" style="text-align:left;">
                    <asp:Button CssClass="button" ID="btnAddU2" runat="server" Text="Ghi nhận" Width="100px" />
                    <input type="reset" class="button" name="cmdReset" value="Mặc định" style="width:100px;"/>
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
