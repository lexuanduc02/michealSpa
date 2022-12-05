Imports System.Web
Imports System.Web.Services

Public Class resetSession
    Implements System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        HttpContext.Current.Session("IsAuthorizeds") = True
        HttpContext.Current.Session("CKFinder_Permission") = "Admin"

        HttpContext.Current.Session.Timeout = 720
        context.Response.ContentType = "text/plain"
        context.Response.Write("")

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class