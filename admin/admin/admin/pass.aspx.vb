Public Partial Class pass
    Inherits System.Web.UI.Page
    Dim o As New Bambu.oData
    Dim strsql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then Call BindData()
    End Sub
    Protected Sub BindData()
        lblLink.Text = "Change password"

        strsql = "select IDU,Pass,Email from tblUser where IDU=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value))
        Call o.DB_Connect(strsql, 1)
        If o.objDataReader.Read() Then
            hd.Value = Bambu.oNet.Decrypt(o.objDataReader("Pass"))
        End If
        Call o.DB_Disconnect(1)

    End Sub

    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddU1.Click, btnAddU2.Click

        If txtPassC1.Text <> "" Then
            strsql = "update tblUser set Pass='" & Bambu.oNet.Encrypt(txtPassC1.Text) & "' where IDU='" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & "'"
            o.DB_Connect(strsql, 1)
            o.DB_Disconnect(1)
        End If
        Response.Write("<script>alert('The password has been updated successfully!!');</script>")
    End Sub
End Class