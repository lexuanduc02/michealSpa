Imports System.IO

Partial Public Class website
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        ElseIf Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            Response.Redirect("pro.aspx")
        End If

        Session("IsAuthorizeds") = True
        Session("CKFinder_Permission") = "Admin"

        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then
            strSQL = "select * from tblTotal where ID=1"
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                Contact.Text = o.objDataReader("Contact")
                Tel.Text = o.objDataReader("Tel")
                Footer.Text = o.objDataReader("Footer")
                Head.Text = o.objDataReader("Head")
                txtSl.Text = o.objDataReader("Slogan")
                Try

                    txtMeta_Title.Text = o.objDataReader("meta_title")
                    txtMeta_Keywords.Text = o.objDataReader("meta_keywords")
                    txtMeta_Description.Text = o.objDataReader("meta_description")
                    txtScript.Text = o.objDataReader("code_analytic")

                    txtscFacebook.Text = o.objDataReader("social_fb")
                    txtscGoogle.Text = o.objDataReader("social_gplus")
                    txtscInstagram.Text = o.objDataReader("social_insta")
                    txtscPinterest.Text = o.objDataReader("social_pin")
                    txtscTwitter.Text = o.objDataReader("social_tw")
                    txtscYoutube.Text = o.objDataReader("social_yt")

                    txtFacebookN.Text = o.objDataReader("name_fb")
                    txtFacebookL.Text = o.objDataReader("link_fb")
                    txtFacebookID.Text = o.objDataReader("id_fb")

                    Email.Text = o.objDataReader("Email")
                    Email1.Text = o.objDataReader("Email1")
                    lblPassWord.Text = IIf(IsDBNull(o.objDataReader("Pass")), "", Bambu.oNet.Decrypt(o.objDataReader("Pass")))
                    drlEmail.SelectedValue = o.objDataReader("ChkEmail")
                    txtSMTP.Text = IIf(IsDBNull(o.objDataReader("smtp")), "", o.objDataReader("smtp"))

                Catch ex As Exception

                End Try
            End If
            Call o.DB_Disconnect(1)
        End If
    End Sub

    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddU1.Click, btnAddU2.Click
        Dim f As New ArrayList
        Dim v As New ArrayList

        f.Add("Email,0,2,100") : v.Add(Email.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200

        f.Add("Email1,0,2,100") : v.Add(Email1.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
        f.Add("Pass,0,2,100") : v.Add(Bambu.oNet.Encrypt(Request("txtPass")))
        f.Add("ChkEmail,0,1,4") : v.Add(drlEmail.SelectedValue)
        f.Add("smtp,0,2,100") : v.Add(txtSMTP.Text)

        f.Add("Tel,0,2,100") : v.Add(Tel.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
        f.Add("meta_title,0,2,500") : v.Add(txtMeta_Title.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
        f.Add("meta_keywords,0,2,500") : v.Add(txtMeta_Keywords.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
        f.Add("meta_description,0,2,500") : v.Add(txtMeta_Description.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200

        f.Add("Hotline,0,2,100") : v.Add("") 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
        f.Add("Footer,0,3,16") : v.Add(Footer.Text) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16
        f.Add("Head,0,3,16") : v.Add(Head.Text) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16
        f.Add("Contact,0,3,16") : v.Add(Contact.Text) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16
        f.Add("Slogan,0,2,300") : v.Add(txtSl.Text)
        f.Add("code_analytic,0,3,16") : v.Add(txtScript.Text)
        f.Add("social_fb,0,2,200") : v.Add(txtscFacebook.Text)
        f.Add("social_tw,0,2,200") : v.Add(txtscTwitter.Text)
        f.Add("social_gplus,0,2,200") : v.Add(txtscGoogle.Text)
        f.Add("social_pin,0,2,200") : v.Add(txtscPinterest.Text)
        f.Add("social_insta,0,2,200") : v.Add(txtscInstagram.Text)
        f.Add("social_yt,0,2,200") : v.Add(txtscYoutube.Text)

        f.Add("name_fb,0,2,100") : v.Add(txtFacebookN.Text)
        f.Add("link_fb,0,2,100") : v.Add(txtFacebookL.Text)
        f.Add("id_fb,0,2,50") : v.Add(txtFacebookID.Text)


        f.Add("ID,1,1,4") : v.Add(1) 'Kiểu Int có độ rộng là 4(key)
        o.UpdateRecord("tblTotal", f, v, False)

        Call o.UploadFile(FilePro, "logo", "/images/icon", 500000, "", "")
        Call o.UploadFile(FileLogoM, "logoM", "/images/icon", 500000, "", "")
        Call o.UploadFile(FileFavicon, "favicon", "/images/icon", 500000, "", "")
        Call o.UploadFile(FileBgForm, "sgd", "/images/bg", 5000000, "", "")
        Call o.UploadFile(FileBgFooter, "bg-footer", "/images/bg", 5000000, "", "")

        o.ShowMessage("The configuration has been updated successfully!")
    End Sub
End Class