<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="news.aspx.vb" Inherits="admin.news" ValidateRequest="false" EnableViewStateMac="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage News</title>
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
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" Width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" />
                    <asp:DropDownList CssClass="textbox drl" ID="drlSubID" runat="server" DataValueField="ID" DataTextField="Name" Width="200px" AutoPostBack="true" />
                    <%If Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) = 1 Then%>
                    <asp:DropDownList CssClass="textbox drl" ID="drlType" DataTextField ="Name" DataValueField ="ID" runat="server" Width="200px" AutoPostBack="true"></asp:DropDownList>
                    <%end if %>
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
                            <ItemTemplate><img src="/images/news/<%#Eval("ImgN") %>" style="height:35px; width:45px;" alt="" /></ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:BoundColumn DataField="Name" HeaderText="Name" ItemStyle-Width="100%" />
                        <asp:BoundColumn DataField="Users" HeaderText="Users" />
                        <asp:BoundColumn DataField="Viewer" HeaderText="View" />
                        <asp:BoundColumn DataField="TimeN" DataFormatString="{0:d}" HeaderText="Created&nbsp;on" ItemStyle-Width="20px" />
                         
                        <asp:TemplateColumn HeaderText="Published"> 
                            <ItemTemplate>
                                <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                                    <asp:ListItem Value="1" Text="Published" /><asp:ListItem Value="0" Text="Unpublished" />
                                </asp:DropDownList>
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                        
                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/up.png&quot;&gt;' HeaderText="Top" ItemStyle-HorizontalAlign ="Center" CommandName="eTop" HeaderStyle-Width="10px" />
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
                    
                    <asp:DropDownList CssClass="textbox drl" ID="drlMenuID" runat="server" DataValueField="ID" DataTextField="Name" Width="200px" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td class="td1"><div style="width:95px;">Name:</div></td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtName" Width="98%" runat="server" onkeyup="chk('txtName')" />
                    <asp:RequiredFieldValidator runat="server" id="rfvName" SetFocusOnError="true" ControlToValidate="txtName" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                </td>
            </tr>
             <tr class="hidden">
                <td class="td1"><div style="width:105px;">URL:</div></td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtURL" Width="640px" runat="server" MaxLength="150" /></td>
            </tr>            
            <tr class="hidden">
                <td class="td1">Tags :</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="KeyN" Width="98%" runat="server" MaxLength="300" />
                    <span style="font-size:11px;color:Red;display:block;padding-top:3px;">(Các tags cách nhau bằng dấu ",")</span>
                </td>
            </tr>
            <tr>
                <td class="td1">Hiển thị:</td>
                <td class="td2">
                    <asp:CheckBox ForeColor="red" ID="chkAc" runat="server" Checked="true" />
                    <asp:CheckBox ID="chkNews" Visible ="false"  runat="server" Text="Tin tức mới" />
                </td>
            </tr>
            <tr>
                <td class="td1">Short description:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtContent" runat="server" Width="98%" Rows="5" TextMode="MultiLine" /></td>
            </tr>
              <tr>
                <td class="td1">Full description:</td>
                <td class="td2">
                    <div class="hidden"><asp:CheckBox ID="chklink" runat="server" Text="Xóa link trong nội dung bài viết" /></div>
                    <CKEditor:CKEditorControl ID="txtDescs" BasePath="/Content/ckeditor/" runat="server" Height="300px" Width="98%" />
                </td>
            </tr>

            <tr class="hidden">
                <td class="td1">Thuộc tab:</td>
                <td class="td2">
                <asp:DataList id="dtlTab" DataKeyField="ID" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkTab" runat="server" Text='<%#Eval("Name") %>' /> &nbsp; &nbsp;
                    </ItemTemplate>
                </asp:DataList>
                </td>
            </tr> 
          
            <tr>
                <td class="td1">Meta title:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtMetaTitle" runat="server" Width="98%" /></td>
            </tr> 
            <tr>
                <td class="td1">Meta keywords:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtMetaKeywords" runat="server" Width="98%" Rows="5" TextMode="MultiLine" /></td>
            </tr> 
            <tr>
                <td class="td1">Meta description:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtMetaDescription" runat="server" Width="98%" Rows="5" TextMode="MultiLine" /></td>
            </tr>     

            <tr>
                <td class="td1">Hình ảnh:</td>
                <td class="td2">
                    <asp:FileUpload ID="FilePro" runat="server" Width="98%" />
                    <asp:Image ID="imgP1" Width="90px" runat="server" Visible="false" />
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
<asp:Label ID="lblChk" runat="server" ></asp:Label>
</form>
</body>
</html>
