Imports System.Web
Imports System.Web.Services

Public Class Images
    Implements System.Web.IHttpHandler
    Dim o As New Bambu.oData
    Dim strSQl As String
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ConnectionString

        Dim iStr As New StringBuilder
        iStr.AppendLine("<div class='box-ajax'>")
        strSQl = "Select NameP from tblProject where IDP = '" & context.Request.QueryString("pid").ToString() & "'"
        o.DB_Connect(strSQl, 1)
        If o.objDataReader.Read Then
            iStr.AppendLine("<h2  class ='h2t'>" & o.objDataReader("NameP") & "</h2>")
        End If
        o.DB_Disconnect(1)
        iStr.AppendLine("<div class='row row-xs'>")
        strSQl = "Select * from tblGallery where ProID = '" & context.Request.QueryString("pid").ToString() & "'"
        o.DB_Connect(strSQl, 1)
        While o.objDataReader.Read
            iStr.AppendLine("<div class='col-sm-4 col-xs'><a data-fancybox='gallery' class='thumb-gallery' href='/images/gallery/" & o.objDataReader("Img1") & "'><img src='/images/gallery/" & o.objDataReader("Img1") & "'></a></div>")
        End While
        o.DB_Disconnect(1)
        iStr.AppendLine("</div>")
        iStr.AppendLine("</div>")
        context.Response.ContentType = "text/plain"
        context.Response.Write(iStr)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class