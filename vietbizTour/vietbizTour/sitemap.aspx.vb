Public Partial Class sitemap
    Inherits System.Web.UI.Page
    Dim o As New Bambu.oData
    Dim strsql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ConnectionString.ToString()
        If Not Page.IsPostBack Then
            Response.Clear()
            Response.ContentType = "text/xml"
            Dim writer As New System.Xml.XmlTextWriter(Response.OutputStream, Encoding.UTF8)
            writer.WriteStartDocument()
            writer.WriteStartElement("urlset")
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9")

            ' Load link menu
            strsql = "Select AutoM,Name1M from tblMenu where AcM <> 0 and IDM <> '010000'"
            o.DB_Connect(strsql, 1)
            Do While o.objDataReader.Read()
                writer.WriteStartElement("url")
                writer.WriteElementString("loc", "https://" & HttpContext.Current.Request.Url.Host & "/" & o.objDataReader("Name1M") & "-id1" & o.objDataReader("AutoM") & ".html")
                writer.WriteElementString("changefreq", "daily")
                writer.WriteElementString("priority", "1.0")
                writer.WriteEndElement()
            Loop
            o.DB_Disconnect(1)

            ' Load News 
            strsql = "Select  AutoM,IDN,Name1N from tblMenu,tblNews where MenuIDN = IDM and AcN <> 0 order by TimeN DESC"
            o.DB_Connect(strsql, 1)
            Do While o.objDataReader.Read()
                writer.WriteStartElement("url")
                writer.WriteElementString("loc", "https://" & HttpContext.Current.Request.Url.Host & "/" & o.objDataReader("Name1N") & "-id3" & o.objDataReader("IDN") & ".html")
                writer.WriteElementString("changefreq", "daily")
                writer.WriteElementString("priority", "1.0")
                writer.WriteEndElement()
            Loop
            o.DB_Disconnect(1)

            ' Load Pro 
            strsql = "Select AutoM,IDP,Name1P from tblMenu,tblProject where MenuIDP = IDM and AcP <> 0 order by TimeP DESC"
            o.DB_Connect(strsql, 1)
            Do While o.objDataReader.Read()
                writer.WriteStartElement("url")
                writer.WriteElementString("loc", "https://" & HttpContext.Current.Request.Url.Host & "/" & o.objDataReader("Name1P") & "-id2" & o.objDataReader("IDP") & ".html")
                writer.WriteElementString("changefreq", "daily")
                writer.WriteElementString("priority", "1.0")
                writer.WriteEndElement()
            Loop
            o.DB_Disconnect(1)

            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Flush()
            Response.End()
        End If
    End Sub

End Class