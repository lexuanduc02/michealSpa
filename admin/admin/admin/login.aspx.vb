Partial Public Class login
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnlogin_Click(ByVal Sender As Object, ByVal e As EventArgs)
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        strSQL = "select IDU from tblUser where Users='" & Bambu.oNet.KillChars(Request("txtUserL")) & "' and Pass='" & Bambu.oNet.Encrypt(Request("txtPassL")) & "' "
        Call o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            Dim cookieUsername = New HttpCookie("__token__ad", Bambu.oNet.Encrypt(o.objDataReader("IDU") & "Admin123456789!@#$%^&*("))
            cookieUsername.Expires = DateTime.Now.AddHours(6)
            Response.Cookies.Add(cookieUsername)
            o.DB_Disconnect(1)

            Session("IsAuthorizeds") = True
            Session("CKFinder_Permission") = "Admin"

            Session.Timeout = 720

            Response.Redirect("project.aspx")
        Else
            Response.Write("<script>alert('Invalid login or password!');</script>")
            Exit Sub
        End If
        Call o.DB_Disconnect(1)
    End Sub
    Sub Fun_Exit(ByVal Sender As Object, ByVal e As EventArgs)
        Response.Redirect("/")
    End Sub
End Class