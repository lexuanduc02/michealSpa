Imports System.Web.SessionState
Imports System.Web.Optimization

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started

        'BundleConfig.RegisterBundles(BundleTable.Bundles)
        'BundleTable.EnableOptimizations = True

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        If Not HttpContext.Current.Request.Url.ToString().ToLower().Contains("localhost") Then
            If HttpContext.Current.Request.Url.ToString().ToLower().Contains("www.") Then
                HttpContext.Current.Response.Status = "301 moved permanently"
                Dim newUrl = Request.Url.Scheme + "://" + Request.Url.Host.Replace("www.", "") + Request.RawUrl
                HttpContext.Current.Response.AddHeader("location", newUrl)
            End If
        End If
        If Not HttpContext.Current.Request.Url.ToString().ToLower().Contains("localhost") Then
            If Not HttpContext.Current.Request.Url.Scheme.Equals("https") Then
                HttpContext.Current.Response.Status = "301 moved permanently"
                Dim newUrl = "https://" + Request.Url.Host + Request.RawUrl
                HttpContext.Current.Response.AddHeader("location", newUrl)
            End If
        End If
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        If Not HttpContext.Current.Request.Url.ToString().ToLower().Contains("localhost") Then
            Dim serverError = TryCast(Server.GetLastError(), HttpException)

            If serverError IsNot Nothing Then
                Dim errorCode As Integer = serverError.GetHttpCode()

                If 404 = errorCode Then
                    Server.ClearError()
                    Server.Transfer("/404.aspx")
                Else
                    Response.RedirectPermanent("/")
                End If
            End If
        End If
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class