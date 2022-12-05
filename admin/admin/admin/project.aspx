<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="project.aspx.vb" Inherits="admin.project" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Quản lý dự án</title>
    <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
    <link href="../styles/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .tien-ich {margin:0;padding:0;list-style:none;margin-top:5px;}
        .tien-ich li {display:block;width:150px;text-align:left;margin-bottom:5px;}
    </style>
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:Label ID="lblID" runat="server" Text="0" Visible="false" />
<asp:Label ID="lblM" runat="server" Text="" Visible="false" />
<!--#include file="include/iclHead.aspx"-->
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-left"><!--#include file="include/iclLeft.aspx"--></td>
    <td class="main-content-right">
        <h1 class="main-title"><asp:Label ID="lblLink" runat="server" /></h1>
        <table id="tblView" runat="server" visible="false" style="width:100%;" cellpadding="0" cellspacing="0">
            <tr><td colspan="2" style="text-align:right; padding-bottom:5px;">
                <asp:TextBox CssClass="textbox" ID="key" Width="250px" runat="server" />
                <asp:Button CssClass="button btnBlue" ID="btnSearch" runat="server" Text="Search" />
            </td></tr>
            <tr style="background-color:#3C8DBC;">
                <td style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnDel1" runat="server" Text="Xóa chọn" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" width="120px" />
                    <asp:Button CssClass="button" ID="btnUpdate1" runat="server" Text="Cập nhật" />
                    <asp:DropDownList CssClass="textbox drl" ID="drlSubID" runat="server" DataValueField="ID" DataTextField="Name" Width="200px" AutoPostBack="true" />
                    <asp:DropDownList CssClass="textbox drl" ID="drlTP" runat="server" Visible="false" DataValueField="ID" DataTextField="Name" Width="150px" AutoPostBack="true" />
                    <%--<asp:DropDownList CssClass="textbox drl" ID="drlQ" runat="server" DataValueField="ID" DataTextField="Name" Width="150px" AutoPostBack="true" />--%>
                </td>
                <td style="padding-right: 5px;
                        text-align: right;">
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
                            <ItemTemplate><img src="/images/service/<%#Eval("ImgP")%>" style="height:80px;width:auto" alt="project" /></ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:BoundColumn DataField="Name" HeaderText="Tên&nbsp;dự&nbsp;án" ItemStyle-Width="40%" />
                        <asp:BoundColumn DataField="NameM" HeaderText="Chuyên&nbsp;mục" Visible="false" ItemStyle-Width="10%" />
                         <asp:TemplateColumn HeaderText="Thông tin" ItemStyle-Width="30%">
                             <ItemTemplate>
                                 <p>Giá: <strong><%#Eval("Price") %></strong> - Thời lượng: <strong><%#Eval("completionTimeP") %></strong></p>
                             </ItemTemplate>
                         </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Trạng thái" ItemStyle-Width="120px"> 
                            <ItemTemplate>
                                <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                                    <asp:ListItem Value="1" Text="Hiển thị" /><asp:ListItem Value="0" Text="Ẩn" />
                                </asp:DropDownList>
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Kiểu&nbsp;dự&nbsp;án" ItemStyle-Width="120px" Visible="false"> 
                            <ItemTemplate>
                                <%# IIF(Eval("Hot") = 1,"<b style=color:red>Landing Page</b>","Thường") %>
                            </ItemTemplate> 
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Chi&nbsp;tiết" ItemStyle-Width="20%" > 
                            <ItemTemplate>
                                <ul class="tien-ich">
                                    <li>
                                        <a data-fancybox data-type="iframe" data-src="contentp.aspx?id=<%#Eval("ID") %>" href="javascript:;"><img src="/admin/icon/icon_pro.png" alt="" /> Nội dung chi tiết</a>
                                    </li>
                                    <li>
                                        <a  data-fancybox data-type="iframe" data-src="advertp.aspx?id=<%#Eval("ID") %>" href="javascript:;" ><img src="/admin/icon/icon_news.png" alt="" /> Banner slider</a>
                                    </li>
                                    <li>
                                        <a data-fancybox data-type="iframe" data-src="gallery.aspx?id=<%#Eval("ID") %>" href="javascript:;"><img src="/admin/icon/icon_news.png" alt="" /> Thư viện ảnh</a>
                                    </li>
                                    <li>
                                        <a  data-fancybox data-type="iframe" data-src="advertp1.aspx?id=<%#Eval("ID") %>" href="javascript:;" ><img src="/admin/icon/icon_comment.png" alt="" /> Tiện ích dự án</a>
                                    </li>
                                </ul>
                            </ItemTemplate> 
                        </asp:TemplateColumn> 

                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/up.png&quot;&gt;' HeaderText="Top" ItemStyle-HorizontalAlign ="Center" CommandName="eTop" HeaderStyle-Width="10px" />
                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_edit.gif&quot;&gt;' HeaderText="&nbsp;Sửa&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="edit" HeaderStyle-Width="10px" />
                        <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_delete.gif&quot;&gt;' HeaderText="&nbsp;Xóa&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="Delete" HeaderStyle-Width="10px" />
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
                    <link href="../styles/bootstrap.min.css" rel="stylesheet" />
                    <asp:Button CssClass="button" ID="btnAddU1" ValidationGroup="grAdd" runat="server" Text="Ghi nhận" />
                    
                </td>
            </tr>
             <tr>
                <td class="td1"><div style="width:105px;">Danh mục:</div></td>
                <td class="td2">
                    <asp:DropDownList CssClass="textbox drl border" ID="drlMenuID" runat="server" DataValueField="ID" DataTextField="Name" Width="200px" AutoPostBack="true" />
                    <asp:RequiredFieldValidator runat="server" id="rfv_drlMenuID" ValidationGroup="grAdd" ControlToValidate="drlMenuID" InitialValue="" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td1"><div style="width:105px;">Tên:</div></td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtName" Width="98%" runat="server" MaxLength="150" />
                    <asp:RequiredFieldValidator runat="server" id="rfv_txtName" ValidationGroup="grAdd" ControlToValidate="txtName" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td1">Meta title:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtMetaTitle" runat="server" Width="98%" />
                    <asp:RequiredFieldValidator runat="server" id="rfv_txtMetaTitle" ValidationGroup="grAdd" ControlToValidate="txtMetaTitle" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                </td>
            </tr> 
            <tr>
                <td class="td1">Meta keywords:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtMetaKeywords" runat="server" Width="98%" Rows="2" TextMode="MultiLine" />
                </td>
            </tr> 
            <tr>
                <td class="td1">Meta description:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtMetaDescription" runat="server" Width="98%" Rows="3" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator runat="server" id="rfv_txtMetaDescription" ValidationGroup="grAdd" ControlToValidate="txtMetaDescription" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" />
                </td>
            </tr>                      
           
            <tr style="display: none">
                <td class="td1"><div style="width:105px;">Vị trí:</div></td>
                <td class="td2">
                    <asp:UpdatePanel ID="udPnVitri" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drlThanhPho"  DataTextField="Name" CssClass="textbox drl border" Width="200px" DataValueField="ID" runat="server" AutoPostBack="true"></asp:DropDownList>
                            <asp:DropDownList ID="drlQuanHuyen" DataTextField="Name" CssClass="textbox drl border" Visible="false" Width="200px" DataValueField="ID" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
             <tr>
                <td class="td1">Link video giới thiệu:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtVideo" placeholder="VD: https://www.youtube.com/watch?v=h5gojIuL1q4&feature=youtu.be&autoplay=1" Width="98%" runat="server" /> 
                    
                </td>
            </tr>
            <tr>
                <td class="td1">Giá:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPrice" placeholder="vd: 170k/lần" Width="150px" runat="server" /> 
                    
                </td>
            </tr>
             <tr style="display: none">
                <td class="td1">Diện tích:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtDienTich" placeholder="vd: 120m2" Width="150px" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td1">Thời lượng:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtThoiLuong" placeholder="vd: 60 phút" Width="150px" runat="server" />
                </td>
            </tr>
            <tr style="display:none;">
                <td class="td1">Số phòng tắm:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPhongTam" Width="250px" runat="server" /> 
                </td>
            </tr>
            <tr style="display:none;">
                <td class="td1">Số phòng ngủ:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="txtPhongNgu" Width="250px" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td class="td1">Mô tả ngắn:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtContent" runat="server" Width="98%" Rows="5" TextMode="MultiLine" /></td>
            </tr>
            <tr style="display:none;">
                <td class="td1">Thông tin liên hệ:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtLienhe" runat="server" Width="98%" Rows="3" TextMode="MultiLine" /></td>
            </tr> 
            <tr>
                <td class="td1">Trạng thái:</td>
                <td class="td2">
                    <asp:CheckBox ID="chkAc" runat="server"  Text="Hiển thị" Checked="true" /><br />
                    <asp:CheckBox ID="chkHot" runat="server" Text="Hiển thị dạng Landing Page"  />
                </td>
            </tr>                  
            <tr style="display:none;">
                <td class="td1">Chi tiết dự án ( Dự án thường ):</td>
                <td class="td2">
                    <CKEditor:CKEditorControl ID="txtDescs" BasePath="/Content/ckeditor/" runat="server" Height="200px" Width="99%" />
                </td>
            </tr>
             <tr>
                <td class="td1">Thông tin chi tiết:</td>
                <td class="td2">
                    <CKEditor:CKEditorControl ID="txtTextHome" BasePath="/Content/ckeditor/" runat="server" Height="700px" Width="99%" />
                </td>
            </tr>
            <tr style="display:none;">
                <td class="td1">Chân trang landing page:</td>
                <td class="td2">
                    <CKEditor:CKEditorControl ID="txtFooter" BasePath="/Content/ckeditor/" runat="server" Height="150px" Width="99%" />
                </td>
            </tr>
             <tr style="display:none">
                <td class="td1">Link facebook landing page:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" placeholder="VD:https://www.facebook.com/novaland.jsc/" ID="Facebook" runat="server" Width="98%"  />
                </td>
            </tr>
             <tr style="display:none">
                <td class="td1">Link youtube landing page:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox" ID="Youtube" placeholder="VD:https://www.youtube.com/watch?v=h5gojIuL1q4&feature=youtu.be&autoplay=1" runat="server" Width="98%"  />
                </td>
            </tr>
            <tr>
                <td class="td1">Ảnh đại diện:</td>
                <td class="td2">
                    <asp:FileUpload ID="FilePro" runat="server" Width="98%" />
                    <div style="color:Red;">(Chỉ hỗ trợ ảnh có định dạng *.jpg, *.gif và dùng lượng < 1M - Size: 1200px x 900px)</div>
                    <asp:Image ID="imgP1" Width="90px" runat="server" Visible="false" />
                    <asp:ImageButton ID="ibtnDelImgT1" runat="server" ImageUrl="icon/delimg.gif" OnClientClick="javascript:return confirm('Bạn có muốn Xóa ảnh này không?')" Visible="false" />
                </td>
            </tr>

            <tr>
                <td class="td1">Logo du an:</td>
                <td class="td2">
                    <asp:FileUpload ID="FilePro1" runat="server" Width="98%" />
                    <div style="color:Red;">(Chỉ hỗ trợ ảnh có định dạng *.png và dùng lượng < 1M - Size: < 300px)</div>
                    <asp:Image ID="imgP2" Width="90px" runat="server" Visible="false" />
                    <asp:ImageButton ID="ibtnDelImgT2" runat="server" ImageUrl="icon/delimg.gif" OnClientClick="javascript:return confirm('Bạn có muốn Xóa ảnh này không?')" Visible="false" />
                </td>
            </tr>
            <tr style="display:none;">
                <td class="td1">Bg form dk landing page(<span class="red">.jpg</span>):</td>
                <td class="td2">
                    <asp:FileUpload ID="FileBgFooter" runat="server" Width="98%" />
                    <img src="/images/bg/img-contact.jpg" alt="" class="imgW" />                </td>
            </tr>
            <tr>
                <td class="td1">&nbsp;</td>
                <td class="td1" style="text-align:left;">
                    <asp:Button CssClass="button" ID="btnAddU2" ValidationGroup="grAdd" runat="server" Text="Ghi nhận" Width="100px" />
                    
                </td>
            </tr>
        </table>
        <asp:Panel ID="pel_UploadImg" runat="server" Visible="false"> 
            <table style="width:100%; margin-top:15px;" cellpadding="0" cellspacing="0" id="Table2" runat="server">
                <tr style="display:none">
                    <td class="td1" style="border-bottom:1px #fff solid;"><div style="width:100px;"></div>Chọn màu: </td>
                    <td class="td2" style="border-bottom:1px #fff solid;display:none">
                        <asp:DropDownList ID="drpMS" runat="server" DataTextField="NameMS" DataValueField="IDMS" AutoPostBack="true" Visible ="false" ></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ValidationGroup="fileImg" ControlToValidate="drpMS" InitialValue ="0" errormessage="* Chưa có màu sắc nào được chọn" display="Dynamic" />
                    </td>
                </tr>
                <tr style="background-color:#3C8DBC;">
                    <td colspan="2" style="height:35px; padding-left:5px;">
                        <asp:DropDownList CssClass="textbox drl" ID="drlTypeI" runat="server" Width="200px" AutoPostBack="true" >
                            <asp:ListItem Text ="Product pictures" Value = "1"></asp:ListItem>
                        </asp:DropDownList>
                        <a href="project.aspx" style="font-weight:bold; color:#fff;"><< back to project list</a>
                    </td>
                </tr>
                <tr>
                    <td class="td1" style="border-bottom:1px #fff solid;">Chọn&nbsp;ảnh: </td>
                    <td class="td2" style="border-bottom:1px #fff solid;">
                        <asp:FileUpload Font-Size="11px" ID="fileImg" AllowMultiple="true" runat="server" />
                        <div><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" ValidationGroup="fileImg" ControlToValidate="fileImg" ForeColor="Red" errormessage=" <br/>* This is a required field" display="Dynamic" /></div>
                    </td>
                </tr>
                <tr>
                    <td class="td1"></td>
                    <td class="td2">
                        <span style="color:red;">Chú ý: Up ảnh kích thước < 1M, có thể chọn nhiều ảnh cùng lúc bằng cách dùng phím Ctrl </span>
                    </td>
                </tr>
                <tr class="hidden">
                    <td class="td1" style="border-bottom:1px #fff solid;">Trạng thái: </td>
                    <td class="td2" style="border-bottom:1px #fff solid;"><asp:CheckBox ID="chkAcP" runat="server" Text="Hiển thị" Checked="true" /></td>
                </tr>
                <tr>
                    <td class="td1">&nbsp;</td>
                    <td class="td2"><asp:Button ID="btnUpdateImg" runat="server" Text="Add project picture" ValidationGroup="fileImg" CssClass="button btnBlue" /></td>
                </tr>                  
            </table>     
            <table cellpadding="0" cellspacing="0" style="margin-top:5px;">
                <tr><td><asp:Literal ID="lblMn" runat="server" Text="" Visible="false"></asp:Literal> </td></tr>
            </table>       
            <table style="width:100%; margin-top:5px;" cellpadding="0" cellspacing="0" id="Table1" runat="server">
                <tr style="background-color:#3C8DBC;">
                    <td style="height:35px; padding-left:5px; color:White; font-family:Tahoma; font-weight:bold;">
                        Manage product pictures
                        <asp:Button CssClass="button" ID="Button1" runat="server" Visible="false" Text="Xoá" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" Width="40px" />
                        <asp:Button CssClass="button" ID="Button2" runat="server" Text="Cập nhật" Visible='false' Width="100px" />
                    </td>
                    <td style="padding-right:5px; text-align:right;">
                    </td>
                </tr>
                <tr><td colspan="2">
                    <asp:DataGrid id="gridProImg" DataKeyField="ID" runat="server"  CssClass="table table-striped table-bordered" AutoGenerateColumns="False" AllowSorting="True" BorderStyle="None" cellpadding="3" Width="100%" HorizontalAlign="Center" AllowPaging="True"> 
                         <HeaderStyle Font-Bold="True" BackColor="#F4F4F4" cssclass="header" />
                           
                         <Columns>    
                            <asp:TemplateColumn HeaderText="&lt;input value=&quot;0&quot; id=&quot;all_check_box&quot; onclick=&quot;CheckAll(this,'inbox');&quot; type=&quot;checkbox&quot; /&gt;">
                                <ItemTemplate><asp:CheckBox ID="inbox" runat="server" /></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Picture" ItemStyle-Width="100%">
                                <ItemTemplate><img src="/images/project/<%#Eval("Img") %>" style=" max-height:35px; max-width:75px;" alt="" /></ItemTemplate>
                            </asp:TemplateColumn>                        
                            
                            <asp:TemplateColumn HeaderText="Mô tả" ItemStyle-Width="100%" Visible="false">
                                <ItemTemplate><asp:TextBox CssClass="textbox" ID="Name" runat="server" Text='<%#Eval("ID") %>' Width="375px" MaxLength="200" /></ItemTemplate>
                            </asp:TemplateColumn>
                                                    
                            <asp:TemplateColumn HeaderText="URL" Visible="false">
                                <ItemTemplate><asp:TextBox CssClass="textbox" ID="url" runat="server" Text='<%#Eval("ID") %>' Width="200px" MaxLength="200" /></ItemTemplate>
                            </asp:TemplateColumn>
                                                     
                            <asp:TemplateColumn HeaderText="Published"> 
                                <ItemTemplate>
                                    <asp:DropDownList CssClass="textbox drl border" ID="drlAc" runat="server">
                                        <asp:ListItem Value="1" Text="Published" />
                                        <asp:ListItem Value="0" Text="Unpublished" />
                                    </asp:DropDownList>
                                </ItemTemplate> 
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Edit"> 
                                <ItemTemplate><asp:FileUpload CssClass="textbox" ID="FilePro" Width="550px" runat="server" /></ItemTemplate> 
                            </asp:TemplateColumn>                        
                            
                            <asp:ButtonColumn Text='&lt;img border=&quot;0&quot; src=&quot;icon/icon_delete.gif&quot;&gt;' HeaderText="&nbsp;Delete&nbsp;" ItemStyle-HorizontalAlign ="Center" CommandName="Delete" HeaderStyle-Width="10px" />
                         </Columns>
                         <PagerStyle Mode="NumericPages" BackColor="#F4F4F4" HorizontalAlign="Left" cssclass="tr-footer" />
                         
                    </asp:DataGrid>
                </td></tr>
                <tr style="background-color:#3C8DBC;">
                    <td style="height:35px; padding-left:5px;">
                        <asp:Button CssClass="button" ID="Button3" runat="server" Visible="false" Text="Xóa" OnClientClick="javascript:return confirm('Are you sure you want to delete selected item?')" Width="40px" />
                        <asp:Button CssClass="button" ID="Button4" runat="server" Text="Update" Width="100px" />
                    </td>
                    <td style="padding-right:5px; text-align:right;">
                    </td>
                </tr>
            </table>   
        </asp:Panel>      
    </td>
</tr>
</table>
<!--#include file="include/iclFooter.aspx"-->
</form>
</body>
</html>
