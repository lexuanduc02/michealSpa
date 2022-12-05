Public Class _Default
    Inherits System.Web.UI.Page
    Dim o As New Bambu.oData
    Dim strSQL As String
    Dim ipage As String
    Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
        If Me.Request.Headers("X-MicrosoftAjax") <> "Delta=true" Then
            Dim reg As New System.Text.RegularExpressions.Regex("<script[^>]*>[\w|\t|\r|\W]*?</script>")
            Dim sb As New System.Text.StringBuilder()
            Dim sw As New System.IO.StringWriter(sb)
            Dim hw As New HtmlTextWriter(sw)
            MyBase.Render(hw)
            Dim html As String = sb.ToString()
            Dim mymatch As System.Text.RegularExpressions.MatchCollection = reg.Matches(html)
            html = reg.Replace(html, String.Empty)
            reg = New System.Text.RegularExpressions.Regex("(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}")
            html = reg.Replace(html, String.Empty)
            reg = New System.Text.RegularExpressions.Regex("</body>")
            Dim str As String = String.Empty
            For Each match As System.Text.RegularExpressions.Match In mymatch
                str += match.ToString()
            Next
            html = reg.Replace(html, str & "</body>")
            writer.Write(html)
        Else
            MyBase.Render(writer)
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Filter = New IO.Compression.GZipStream(Response.Filter, IO.Compression.CompressionMode.Compress)
        'Response.AddHeader("content-encoding", "gzip")
        Response.Cache.SetCacheability(HttpCacheability.Public)
        Response.AppendHeader("Cache-Control", "public")
        Response.AppendHeader("Pragma", "public")
        Response.AppendHeader("Expires", "7.00:00:00")

        Dim url As String = HttpContext.Current.Request.RawUrl.ToString()
        If url.Contains("utm_source=zalo") Then
            url = Mid(url, 1, InStr(url, "?utm_source=zalo") - 1)
            Response.Status = "301 Moved Permanently"
            Response.Redirect(url)
        End If

        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ConnectionString
        If Not Page.IsPostBack Then

            If Val(Request("page")) <> 0 Then
                ipage = " | Page  " & Request("page")
            End If
           
            If Request("id") = "" Then
                LoadHome()
                LoadMain()
            Else
                If Val(Request("id")) <> 0 Then
                    LoadID()
                Else
                    LoadID2()
                End If

            End If
            LoadMain()
        End If

    End Sub
    Protected Sub LoadProject()

        LoadM(Request("id").Substring(1))

        strSQL = "Select LogoP,NameP,ContentP,MetaTitle,MetaKeywords,MetaDescription,ImgP,VideoP,Facebook,Youtube from tblProject where IDP = '" & Request("id").Substring(1) & "' "
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read Then
            lblLogo.Text = o.objDataReader("LogoP")
            lblVideoP.Text = o.objDataReader("VideoP")
            Youtube.Text = o.objDataReader("Youtube")
            Bambu.oNet.SetSeo(o.objDataReader("MetaTitle"), o.objDataReader("MetaKeywords"), o.objDataReader("MetaDescription"), "")
            Bambu.oNet.Meta_Open_Graph(" WebPage", Request.Url.Host(), Page.Title, "https://" & Request.Url.Host() & Request.RawUrl(), "https://" & Request.Url.Host() & "/images/project/" & o.objDataReader("ImgP"), o.objDataReader("MetaDescription"))
        End If
        o.DB_Disconnect(1)

        strSQL = "Select NameM,Name1M,AutoM from tblMenuP where TypeD = 1 and AcM = 1 and ProID = '" & Request("id").Substring(1) & "' order by SortM"
        o.BindData(strSQL, "ID", rptMenuLink)

        strSQL = "Select NameM,Name1M,AutoM from tblMenuP where TypeD = 2 and AcM = 1 and ProID = '" & Request("id").Substring(1) & "' order by SortM"
        o.BindData(strSQL, "ID", rptMenuLink2)

        strSQL = "Select * from tblAdvertP where Ac = 1 and ProID = '" & Request("id").Substring(1) & "' and Type = 1 order by Sort"
        o.BindData(strSQL, "ID", rptSliderP)

        strSQL = "Select Footer from tblTotal where ID = '1' "
        o.BindData(strSQL, "ID", rptFooterP)

        strSQL = "Select TextHome from tblProject where IDP = '" & Request("id").Substring(1) & "' "
        o.BindData(strSQL, "ID", rptTextHome)

        LoadMenuPr(1, rptSectionAbout)
        LoadMenuPr(2, rptSectionAbout2)

        strSQL = "Select * from tblAdvertP where ProID = '" & Request("id").Substring(1) & "' and Type = 2 and Ac = 1 order by Sort "
        o.BindData(strSQL, "ID", rptUtilitiesTienIch)

    End Sub
    Protected Sub LoadMenuPr(ByVal i As Integer, ByVal rpt As Repeater)
        strSQL = "Select AutoM,NameM,ContentM,ImgM,NameMH,DescM from tblMenuP where TypeD = '" & i & "' and ProID = '" & Val(Request("id").Substring(1)) & "'"
        o.BindData(strSQL, "ID", rpt)
    End Sub
    Protected Sub LoadRight()
    End Sub
    Protected Sub LoadID2()
        If Request("id") = "admin" Then
            Response.Redirect("/admin/login.aspx")
        ElseIf Request("id") = "tim-kiem" Then
            lblTitle.Text = "Tìm kiếm"
            Bambu.oNet.SetSeo("Tìm kiếm", "", "")
            Dim iwh As String = ""
            iwh = " and ( ' '+NameP+' '+Replace(Name1P,'-',' ')+' ' like N'%" & Replace(Bambu.oNet.KillChars(Request("key")), " ", "%' and ' '+NameP+' '+Replace(Name1P,'-',' ')+' ' like N'%") & "%' )"
            strSQL = "Select NameP,Name1P,IDP,ImgP,Price, completionTimeP,ContentP from tblProject,tblMenu where MenuIDp = IDM and AcM = 1 " & iwh & " and AcP = 1  order by TimeP DESC "
            o.BindDataPage(strSQL, "ID", rptPro, 6, Val(Request("page")), 4, lblPage, False)
        End If
    End Sub
    Protected Sub LoadID()
        LoadM(Request("id").Substring(1))
        LoadLinkN(idm.Text)

        If Left(Request("id"), 1) = 1 Then
            LoadPage()
            LoadRight()
        Else
            LoadDT()
            LoadRight()
        End If
    End Sub
    Protected Sub LoadPage()

        Dim count As Integer = 0
        Dim iWh As String = ""
        Dim MenuID As String = "MenuID"
        Select Case type.Text
            Case "1" : MenuID = "MenuIDP"
            Case "2" : MenuID = "MenuIDN"
        End Select
        If Right(idm.Text, 4) = "0000" Then
            iWh = " and left(" & MenuID & ",2)='" & Left(idm.Text, 2) & "'"
        ElseIf Right(idm.Text, 2) = "00" Then
            iWh = " and left(" & MenuID & ",4)='" & Left(idm.Text, 4) & "'"
        Else
            iWh = " and left(" & MenuID & ",6)='" & Left(idm.Text, 6) & "'"
        End If
        If type.Text = 1 Then
            strSQL = "Select NameP,Name1P,IDP,ImgP,Price,ContentP,ImgP, completionTimeP from tblProject,tblMenu where MenuIDp = IDM and AcM = 1 " & iWh & " and AcP = 1  order by TimeP DESC "
            o.BindDataPage(strSQL, "ID", rptPro, 6, Val(Request("page")), 4, lblPage, False)
        ElseIf type.Text = 2 Then
            strSQL = "Select  AutoM,IDN,Name1N,NameN,ImgN,ContentN,TimeN from tblNews,tblMenu where MenuIDN = IDM and AcN <> 0 " & iWh & " order by TimeN DESC"
            o.BindDataPage(strSQL, "ID", rptNews, 12, Val(Request("page")), 4, lblPage, False)
        ElseIf type.Text = 0 Then
            strSQL = "Select NameM,DescM,title,description,keywords,ImgM from tblMenu where IDM = '" & idm.Text & "'"
            o.BindData(strSQL, "ID", rptIntroM)
        ElseIf type.Text = 99 Then
            strSQL = "Select NameM,DescM,title,description,keywords,ImgM from tblMenu where IDM = '" & idm.Text & "'"
            o.BindData(strSQL, "ID", rptIntroM)
            pnDk.Visible = True
        End If

    End Sub

    Protected Sub LoadM(ByVal auto As Integer)
        Try
            If Left(Request("id"), 1) = 1 Then
                strSQL = "select IDM,TypeM,Levels,title,description,keywords,NameM from tblMenu where AutoM='" & auto & "'"
            ElseIf Left(Request("id"), 1) = 2 Then
                strSQL = "select IDM,TypeM,Levels,title,description,keywords,NameM from tblMenu,tblProject where MenuIDP =IDM and IDP='" & auto & "'"
            ElseIf Left(Request("id"), 1) = 3 Then
                strSQL = "select IDM,TypeM,Levels,title,description,keywords,NameM from tblMenu,tblNews where MenuIDN =IDM and IDN='" & auto & "'"
            End If
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                idm.Text = o.objDataReader("IDM")
                type.Text = o.objDataReader("TypeM")
                slv.Text = o.objDataReader("Levels")
                lblTitle.Text = o.objDataReader("NameM")

                If Left(Request("id"), 1) = 1 Then
                    Bambu.oNet.SetSeo(IIf(o.objDataReader("title") = "", o.objDataReader("NameM") & ipage, o.objDataReader("title") & ipage), IIf(o.objDataReader("keywords") = "", o.objDataReader("NameM"), o.objDataReader("keywords")), IIf(o.objDataReader("description") = "", o.objDataReader("NameM") & ipage, o.objDataReader("description") & ipage), "")
                    Bambu.oNet.Meta_Open_Graph(" WebPage", Request.Url.Host(), Page.Title, "" & Request.Url.Scheme & "://" & Request.Url.Host() & Request.RawUrl(), "", o.objDataReader("description"))
                End If

            End If
            o.DB_Disconnect(1)
        Catch ex As Exception
            Response.Redirect("/")
        End Try

    End Sub
    Private Sub LoadLinkN(ByVal iIDM As String)

       If Right(iIDM, 4) = "0000" Then
            strSQL = "select IDM,NameM,Name1M,description,keywords,title,'/'+Name1M +'-1'+convert(nvarchar,AutoM) as URL from tblMenu where IDM='" & iIDM & "'"
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "<li><a href='/'><i class='fa fa-home fa-fw' ></i>Trang chủ</a></li><li><a href='" & o.objDataReader("URL") & ".html'>" & o.objDataReader("NameM") & "</a></li>"
            End If
            Call o.DB_Disconnect(1)
        ElseIf Right(iIDM, 2) = "00" Then
            strSQL = "select m1.IDM,m1.NameM as NameM1, '/'+m1.Name1M +'-id1'+convert(nvarchar,m1.AutoM) as URL,m2.IDM as IDM2, '/'+m2.Name1M +'id1'+convert(nvarchar,m2.AutoM) as URL2,m2.Name1M as Name1M2,m2.NameM as NameM2,m2.TypeM as TypeM2,m2.description as mDessc2,m2.keywords as keywords2,m2.title as title2 from tblMenu m2,tblMenu m1 where left(m2.IDM,2)+'0000'=m1.IDM and m2.IDM='" & iIDM & "'"
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "<li><a href='/'><i class='fa fa-home fa-fw' ></i>Trang chủ</a></li>"
                lblLink.Text &= "<li><a href='" & o.objDataReader("URL") & ".html'>" & o.objDataReader("NameM1") & "</a></li>"
                lblLink.Text &= "<li><a href='" & o.objDataReader("URL2") & ".html'>" & o.objDataReader("NameM2") & "</a></li>"
            End If
            Call o.DB_Disconnect(1)
        Else
            strSQL = "select m1.IDM,m1.NameM as NameM1,'/'+m1.Name1M +'-id1'+convert(nvarchar,m1.AutoM) as URL,m2.IDM as IDM2,'/'+m2.Name1M +'-id1'+convert(nvarchar,m2.AutoM) as URL2,m2.IDM as IDM2,m2.Name1M as Name1M2,m2.NameM as NameM2, '/'+m3.Name1M +'-id1'+convert(nvarchar,m3.AutoM) as URL3,m3.IDM as IDM3,m3.Name1M as Name1M3,m3.NameM as NameM3,m3.TypeM as TypeM3,m3.description as mDessc3,m3.keywords as keywords3,m3.title as title3 from tblMenu m3,tblMenu m2,tblMenu m1 where left(m3.IDM,4)+'00'=m2.IDM and left(m3.IDM,2)+'0000'=m1.IDM and m3.IDM='" & iIDM & "'"
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "<li><a href='/'><i class='fa fa-home fa-fw' ></i>Trang chủ</a></li>"
                lblLink.Text &= "<li><a href='" & o.objDataReader("URL") & ".html'>" & o.objDataReader("NameM1") & "</a></li>"
                lblLink.Text &= "<li><a href='" & o.objDataReader("URL2") & ".html'>" & o.objDataReader("NameM2") & "</a></li>"
                lblLink.Text &= "<li><a href='" & o.objDataReader("URL3") & ".html'>" & o.objDataReader("NameM3") & "</a></li>"
            End If
            Call o.DB_Disconnect(1)
        End If
    End Sub

    Protected Sub LoadDT()
        If type.Text = 2 Then
            strSQL = "Select * from tblNews where IDN = '" & Request("id").Substring(1) & "'"
            o.BindData(strSQL, "ID", rptNewsDt)

        ElseIf type.Text = 1 Then

            Dim hot As Integer = 0
            strSQL = "Select Hot from tblProject where IDP = '" & Request("id").Substring(1) & "'"
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read Then
                If o.objDataReader("Hot") = 1 Then
                    hot = 1
                End If
            End If
            o.DB_Disconnect(1)
            If hot Then
                pnMain.Visible = False
                pnProject.Visible = True
                LoadProject()
            Else
                strSQL = "Select tblProject.*,(Select Name from tblCountry where Id= TinhThanhID) as CityN,(Select Name from tblDistrict where Id= QuanHuyenID) as HuyenN from tblProject where IDP =  '" & Request("id").Substring(1) & "'"
                o.BindData(strSQL, "ID", rptProD2)
            End If
        End If
    End Sub
    Protected Sub LoadHome()
        strSQL = "Select meta_title,meta_keywords,meta_description from tblTotal where ID = 1"
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read Then
            Bambu.oNet.SetSeo(o.objDataReader("meta_title"), o.objDataReader("meta_keywords"), o.objDataReader("meta_description"), "")
            Bambu.oNet.Meta_Open_Graph(" WebPage", Request.Url.Host(), Page.Title, "" & Request.Url.Scheme & "://" & Request.Url.Host() & Request.RawUrl(), "https://badiland.com.vn/images/image/profile2.png", o.objDataReader("meta_description"))
        End If
        o.DB_Disconnect(1)
        LoadAdv(5, rptSlider)
        LoadAdv(7, rptPrDD)
        LoadAdv(8, rptreviews)

        strSQL = "Select Head from tblTotal where ID = 1"
        o.BindData(strSQL, "ID", rptAbout)

        strSQL = "Select top 6 NameP,Name1P,IDP,ImgP,Price,ContentP, completionTimeP from tblProject,tblMenu where MenuIDp = IDM and AcM = 1 and AcP = 1  order by TimeP DESC "
        o.BindData(strSQL, "ID", rptProHot)

        strSQL = "Select top 8 IDN,NameN,Name1N,ImgN,ContentN from tblNews,tblMenu where MenuIDN = IDM and AcM = 1 and AcN = 1  order by TimeN DESC"
        o.BindData(strSQL, "ID", rptNewsHot)

    End Sub
    Protected Sub LoadMain()
        strSQL = "Select  NameM,AutoM,IDM,TypeM,ImgM,case when URL is not null then URL else '/'+Name1M+'-id1'+ convert(nvarchar,AutoM) +'.html'  end URL,(select count(s.IDM) from tblMenu s where Left(s.IDM,2) = Left (tblMenu.IDM,2) and Levels <> 1 ) as TotalM  from tblMenu where AcM <> 0 and Levels = 1  order by SortM ASC"
        o.BindData(strSQL, "AutoM", rptNavMain)

        'strSQL = "Select IDM,TypeM,NameM,case when URL is not null then URL else '/'+Name1M+'-id1'+ convert(nvarchar,AutoM) +'.html'  end URL from tblMenu where AcM  =1 and BottomM = 1 order by SortM"
        'o.BindData(strSQL, "ID", rptNavF)

        strSQL = "Select Footer,Contact from tblTotal where ID = 1"
        o.BindData(strSQL, "ID", rptFooter)

        strSQL = "Select Tel,Slogan,Email,link_fb,name_fb,id_fb from tblTotal where ID = 1"
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read Then
            lblHotline.Text = o.objDataReader("Tel")
            lblSlogan.Text = o.objDataReader("Slogan")
            lblFbN.Text = o.objDataReader("name_fb")
            lblFbL.Text = o.objDataReader("link_fb")
            lblFbID.Text = o.objDataReader("id_fb")
            lblEmail.Text = o.objDataReader("Email")
        End If
        o.DB_Disconnect(1)

    End Sub

    Private Sub rptNavMain_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptNavMain.ItemDataBound
        If DataBinder.Eval(e.Item.DataItem, "TotalM") <> 0 Then
            CType(e.Item.FindControl("subnavmain"), HtmlControl).Visible = True

            strSQL = "Select NameM,AutoM,IDM,TypeM,case when URL is not null then URL else '/'+Name1M+'-id1'+ convert(nvarchar,AutoM) +'.html'  end URL from tblMenu where AcM <> 0 and Levels = 2 and Left(IDM,2) = '" & Left(DataBinder.Eval(e.Item.DataItem, "IDM"), 2) & "' order by SortM ASC"
            o.BindData(strSQL, "ID", CType(e.Item.FindControl("rptSub"), Repeater))

        End If
    End Sub
    Public Sub LoadAdv(ByVal i As Integer, ByVal iname As Repeater)
        strSQL = "select * from tblAdvert where Ac <> 0 and Type = " & i & " order by Sort"
        o.BindData(strSQL, "tblAdvert", iname)
    End Sub

    Private Sub rptNewsDt_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptNewsDt.ItemDataBound
        Dim wh As String = " and Left(IDM," & slv.Text * 2 & ")='" & Left(DataBinder.Eval(e.Item.DataItem, "MenuIDN"), slv.Text * 2) & "'"
        strSQL = "Select top 6 IDN,NameN,Name1N,ImgN,ContentN from tblMenu, tblNews where MenuIDN = IDM " & wh & " and IDN <> " & DataBinder.Eval(e.Item.DataItem, "IDN") & "  order by TimeN DESC"
        o.BindData(strSQL, "tblNews", rptNews)

        strSQL = "Select top 6 NameP,Name1P,IDP,ImgP,Price,ContentP,ImgP,completionTimeP,(Select Name from tblCountry where Id= TinhThanhID) as CityN,(Select Name from tblDistrict where Id= QuanHuyenID) as HuyenN from tblProject,tblMenu where MenuIDp = IDM and AcM = 1 and AcP = 1  order by TimeP DESC "
        o.BindData(strSQL, "ID", e.Item.FindControl("rptSuggestService"))

        Bambu.oNet.SetSeo(DataBinder.Eval(e.Item.DataItem, "NameN"), DataBinder.Eval(e.Item.DataItem, "NameN"), Left(DataBinder.Eval(e.Item.DataItem, "ContentN"), 300), "")
        Bambu.oNet.Meta_Open_Graph("WebPage", Request.Url.Host(), Page.Title, "http://" & Request.Url.Host() & Request.RawUrl(), "http://" & Request.Url.Host() & "/images/news/" & DataBinder.Eval(e.Item.DataItem, "ImgN"), Left(DataBinder.Eval(e.Item.DataItem, "ContentN"), 200))
    End Sub

    Private Sub rptProD2_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProD2.ItemDataBound

        Dim wh As String = " and Left(IDM," & slv.Text * 2 & ")='" & Left(DataBinder.Eval(e.Item.DataItem, "MenuIDP"), slv.Text * 2) & "'"
        strSQL = "Select top 6 NameP,Name1P,IDP,ImgP,Price,ContentP, completionTimeP from tblProject,tblMenu where MenuIDp = IDM and AcM = 1 " & wh & "  and AcP = 1  order by TimeP DESC "
        o.BindData(strSQL, "ID", rptPro)

        Bambu.oNet.SetSeo(DataBinder.Eval(e.Item.DataItem, "MetaTitle"), DataBinder.Eval(e.Item.DataItem, "MetaKeywords"), Left(DataBinder.Eval(e.Item.DataItem, "MetaDescription"), 200), "")
        Bambu.oNet.Meta_Open_Graph("WebPage", Request.Url.Host(), Page.Title, "http://" & Request.Url.Host() & Request.RawUrl(), "http://" & Request.Url.Host() & "/images/project/" & DataBinder.Eval(e.Item.DataItem, "Img1P"), Left(DataBinder.Eval(e.Item.DataItem, "MetaDescription"), 200))
    End Sub
End Class