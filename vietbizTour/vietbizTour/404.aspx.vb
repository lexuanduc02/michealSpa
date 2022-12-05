Public Class _404
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.TrySkipIisCustomErrors = True
        Response.StatusCode = 404
        Response.StatusDescription = "Page not found"
    End Sub

End Class