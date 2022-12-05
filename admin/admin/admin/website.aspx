<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="website.aspx.vb" Inherits="admin.website" ValidateRequest="false" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage Configuration</title>
    <link href="icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
</head>
<body>
<form id="form1" runat="server">
<!--#include file="include/iclHead.aspx"-->
<table class="main-content" cellpadding="0" cellspacing="0">
<tr>
    <td class="main-content-left"><!--#include file="include/iclLeft.aspx"--></td>
    <td class="main-content-right">
        <h1 class="main-title"><asp:Label ID="lblLink" runat="server" Text="Manage configuration" /></h1>
        <table style="width:100%;" cellpadding="0" cellspacing="1">
            <tr style="background-color:#3C8DBC;">
                <td colspan="2" style="height:35px; padding-left:5px;">
                    <asp:Button CssClass="button" ID="btnAddU1" runat="server" Text="Ghi nhận" />
                </td>
            </tr>
            <tr>
                <td class="td1"><div style="width:95px;">Email nhận tin:</div></td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="Email" runat="server" Width="500px" MaxLength="100" /></td>
            </tr>
             <tr>
                <td class="td1"><div style="width:95px;">Email gửi tin</div></td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="Email1" runat="server" Width="500px" MaxLength="100" /></td>
            </tr>
             <tr>
                <td class="td1"><div>Pass email:</div></td>
                <td class="td2">
                    <asp:Label ID="lblPassWord" Visible="false" runat="server"></asp:Label>
                    <input type="password" name="txtPass" id="txtPass" class ="textbox" value="<%=lblPassWord.text%>" />
                </td>
            </tr>
            <tr>
                <td class="td1"><div>Kiểu Email:</div></td>
                <td class="td2">
                    <asp:DropDownList ID="drlEmail" CssClass ="textbox" Width ="100px" runat ="server" >
                        <asp:ListItem Text = "Gmail"  Value = "1"></asp:ListItem>
                        <asp:ListItem Text = "PoP3"  Value = "0"></asp:ListItem>
                    </asp:DropDownList><span>- SMTP</span><asp:TextBox ID="txtSMTP" runat="server" CssClass="textbox" Width="190px" TextMode="singleLine"  ></asp:TextBox><span>(VD: SMTP của Gmail là: smtp.gmail.com)</span>
                </td>
            </tr> 
            <tr>
                <td class="td1">Hotline:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="Tel" runat="server" Width="250px" MaxLength="50" /></td>
            </tr>
             <tr>
                <td class="td1">Slogan:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtSl" runat="server" Width="98%" MaxLength="300" /></td>
            </tr> 
            <tr>
                <td class="td1">Meta Title:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtMeta_Title" runat="server" Width="98%" MaxLength="500" /></td>
            </tr>
            <tr>
                <td class="td1">Meta Keywords:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtMeta_Keywords" runat="server" TextMode="MultiLine" Rows="3" Width="98%" MaxLength="500" /></td>
            </tr>
            <tr>
                <td class="td1">Meta Description:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtMeta_Description" runat="server" TextMode="MultiLine" Rows="5" Width="98%" MaxLength="500" /></td>
            </tr>
             <tr>
                <td class="td1">Script:</td>
                <td class="td2"><asp:TextBox CssClass="textbox" ID="txtScript" runat="server" TextMode="MultiLine" Rows="5" Width="98%" MaxLength="2000" /></td>
            </tr>
           
            <tr>
                <td class="td1">Social Network:</td>
                <td class="td2">
                    <asp:TextBox CssClass="textbox mgb3" ID="txtscFacebook" placeholder="Facebook" runat="server" Width="250px" MaxLength="200" /><br />
                    <asp:TextBox CssClass="textbox mgb3" ID="txtscTwitter" placeholder="Twitter"  runat="server" Width="250px" MaxLength="200" /><br />
                    <asp:TextBox CssClass="textbox mgb3" ID="txtscGoogle" placeholder="Google +" runat="server" Width="250px" MaxLength="200" /><br />
                    <asp:TextBox CssClass="textbox mgb3" ID="txtscPinterest" placeholder="Pinterest +" runat="server" Width="250px" MaxLength="200" /><br />
                    <asp:TextBox CssClass="textbox mgb3" ID="txtscInstagram" placeholder="Instagram" runat="server" Width="250px" MaxLength="200" /><br />
                    <asp:TextBox CssClass="textbox" ID="txtscYoutube" placeholder="Youtube" runat="server" Width="250px" MaxLength="200" />
                </td>
            </tr>   
            <tr>
                <td class="td1">Cấu hình Fanpage:</td>
                <td class="td2">
                    <asp:TextBox ID="txtFacebookN" CssClass="textbox mgb3" Placeholder="Tên fanpage" Width="90%" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtFacebookL" CssClass="textbox mgb3" Placeholder="Link fanpage" Width="90%" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtFacebookID" CssClass="textbox mgb3" Placeholder="ID fanpage" Width="250px" runat="server"></asp:TextBox><br />
                </td>
            </tr>             
            
            <tr>
                <td class="td1">About us:</td>
                <td class="td2">
                    <CKEditor:CKEditorControl ID="Head" BasePath="/Content/ckeditor/" runat="server" Height="200px" Width="98%" />
                </td>
            </tr>        
            <tr>
                <td class="td1">Footer:</td>
                <td class="td2">
                    <CKEditor:CKEditorControl ID="Footer" BasePath="/Content/ckeditor/" runat="server" Height="200px" Width="98%" />
                </td>
            </tr>
            <tr>
                <td class="td1">Contact us:</td>
                <td class="td2">
                    <CKEditor:CKEditorControl ID="Contact" BasePath="/Content/ckeditor/" runat="server" Height="200px" Width="98%" />
                </td>
            </tr>   
            <tr>
                <td class="td1">Favicon(<span class="red">.png</span>):</td>
                <td class="td2">
                    <asp:FileUpload ID="FileFavicon" runat="server" Width="98%" />
                    <img src="/images/icon/favicon.png" alt="" class="imgW" /> 
                </td>
            </tr>    
            <tr>
                <td class="td1">Logo(<span class="red">.png</span>):</td>
                <td class="td2">
                    <asp:FileUpload ID="FilePro" runat="server" Width="98%" />
                    <img src="/images/icon/logo.png" alt="" class="imgW" /> 
                </td>
            </tr> 
             <tr>
                <td class="td1">Logo Mobile(<span class="red">.png</span>):</td>
                <td class="td2">
                    <asp:FileUpload ID="FileLogoM" runat="server" Width="98%" />
                    <img src="/images/icon/logoM.png" alt="" class="imgW" /> 
                </td>
            </tr> 
            <tr>
                <td class="td1">Bg form dk(<span class="red">.jpg</span>):</td>
                <td class="td2">
                    <asp:FileUpload ID="FileBgForm" runat="server" Width="98%" />
                    <img src="/images/bg/sgd.jpg" alt="" class="imgW" />                </td>
            </tr> 
            <tr>
                <td class="td1">Bg footer(<span class="red">.jpg</span>):</td>
                <td class="td2">
                    <asp:FileUpload ID="FileBgFooter" runat="server" Width="98%" />
                    <img src="/images/bg/bg-footer.jpg" alt="" class="imgW" />                </td>
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
