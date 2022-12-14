Partial Public Class news
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        End If

        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then Call BindData()
    End Sub

    Private Sub BindData()
        tblView.Visible = True
        lblLink.Text = "Manage news"

        If Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            strSQL = "select IDM as ID,Case When Levels=2 Then '......'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where uID = '" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__id").Value)) & "' and Left(MenuIDN,4) = Left(IDM,4) )) + N' news )' When Levels=3 Then '............'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where uID = '" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__id").Value)) & "' and Left(MenuIDN,6) = Left(IDM,6) )) + N' news )' Else NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where uID = '" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__id").Value)) & "' and Left(MenuIDN,2) = Left(IDM,2) )) + N' news )'  End Name from tblMenu,tblPermissions where Left(IDM,6)=Left(MenuID,6) and IDM='" & Request("drlSubID") & "' and UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and TypeM in (2,6) order by ID ASC"
        Else
            strSQL = "select IDM as ID,Case When Levels=2 Then '......'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where Left(MenuIDN,4) = Left(IDM,4) )) + N' news )' When Levels=3 Then '............'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where Left(MenuIDN,6) = Left(IDM,6) )) + N' news )'  Else NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where Left(MenuIDN,2) = Left(IDM,2) )) + N' news )'  End Name from tblMenu where TypeM in (2,6) order by ID ASC"
        End If

        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            btnAdd1.Visible = True
            btnAdd2.Visible = True
            btnUpdate1.Visible = True
        Else
            btnAdd1.Visible = False
            btnAdd2.Visible = False
            btnUpdate1.Visible = False
        End If
        o.DB_Disconnect(1)
        o.BindDataDropList(strSQL, drlSubID)

        strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,2)='" & Left(drlSubID.SelectedValue, 2) & "'"
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            btnAdd1.Visible = o.objDataReader("AddNews")
            btnAdd2.Visible = o.objDataReader("AddNews")
            btnDel1.Visible = o.objDataReader("DelNews")
            btnDel2.Visible = o.objDataReader("DelNews")
            btnUpdate1.Visible = o.objDataReader("ChkNews")
        End If
        o.DB_Disconnect(1)

        If Request("id") <> "" Then drlSubID.Text = Request("id")
        strSQL = "Select * from tblTab where Type = 0 order by Sort"
        o.BindDataDropList(strSQL, drlType)
        drlType.Items.Insert(0, "All")
        drlType.Items(0).Value = ""

        Call loadNews()
    End Sub
    Private Sub drlSubID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlSubID.SelectedIndexChanged
        drlType.SelectedIndex = 0
        dtgrView.CurrentPageIndex = 0
        strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,6)='" & Left(drlSubID.SelectedValue, 6) & "'"
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            btnAdd1.Visible = o.objDataReader("AddNews")
            btnAdd2.Visible = o.objDataReader("AddNews")
            btnDel1.Visible = o.objDataReader("DelNews")
            btnDel2.Visible = o.objDataReader("DelNews")
            btnUpdate1.Visible = o.objDataReader("ChkNews")
        End If
        o.DB_Disconnect(1)
        Call loadNews()
    End Sub
    Private Sub drlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlType.SelectedIndexChanged
        drlSubID.SelectedIndex = 0
        dtgrView.PageSize = 20
        If drlType.SelectedValue <> "" Then
            strSQL = "select IDN as ID,NameN as Name,TimeN,AcN,ImgN,Users from tblNews,tblUser where uID=tblUser.IDU and MenuIDN='" & drlSubID.SelectedValue & "' and TabN like  '%-" & drlType.SelectedValue & "-%' order by TimeN DESC"
        Else
            strSQL = "select IDN as ID,NameN as Name,TimeN,AcN,ImgN,Users from tblNews,tblUser where uID=tblUser.IDU and MenuIDN='" & drlSubID.SelectedValue & "' order by TimeN DESC"
        End If
        Call o.BindDataGrid(strSQL, "tblNews", dtgrView)
    End Sub
    Private Sub loadNews()
        dtgrView.PageSize = 20

        If Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            strSQL = "select IDN as ID,NameN as Name,TimeN,AcN,ImgN,Users,Viewer from tblNews,tblUser where uID=tblUser.IDU and MenuIDN='" & drlSubID.SelectedValue & "' and uID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " order by TimeN DESC"
        Else
            strSQL = "select IDN as ID,NameN as Name,TimeN,AcN,ImgN,Users,Viewer from tblNews,tblUser where uID=tblUser.IDU and MenuIDN='" & drlSubID.SelectedValue & "' order by TimeN DESC"
        End If
        Call o.BindDataGrid(strSQL, "tblNews", dtgrView)
    End Sub

    Private Sub btnUpdate1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate1.Click, btnUpdate2.Click
        Dim qtyAc As DropDownList
        Dim i As Integer
        For i = 0 To dtgrView.Items.Count - 1
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            strSQL = "update tblNews set AcN=" & qtyAc.SelectedValue & " where IDN=" & Val(dtgrView.DataKeys(i))
            Call o.ExecuteSql(strSQL, False)
        Next

        Call o.ShowMessage("The news has been updated successfully!")
        Call loadNews()
    End Sub
    Private Sub btnDel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel1.Click, btnDel2.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True
        For i = 0 To dtgrView.Items.Count - 1
            qty = CType(dtgrView.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                iChk = False
                Call DellImg(Val(dtgrView.DataKeys(i)))
                strSQL = "delete tblNews where IDN=" & Val(dtgrView.DataKeys(i))
                Call o.ExecuteSql(strSQL, False)
            End If
        Next
        If iChk = True Then
            o.ShowMessage("You must select one item before delete!")
            Exit Sub
        End If
        o.ShowMessage("The item has been deleted successfully!")
        Call loadNews()
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd1.Click, btnAdd2.Click
        tblView.Visible = False
        tblAdd.Visible = True
        lblLink.Text = "Add a new News"
        lblID.Text = 0

        strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,6)='" & Left(Request("drlSubID"), 6) & "'"
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            chkAc.Visible = o.objDataReader("ChkNews")
            chkNews.Visible = o.objDataReader("ChkNews")
        End If
        o.DB_Disconnect(1)

        If Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            strSQL = "select IDM as ID,Case When Levels=2 Then '......'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where uID = '" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__id").Value)) & "' and Left(MenuIDN,4) = Left(IDM,4) )) + N' news )' When Levels=3 Then '............'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where uID = '" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__id").Value)) & "' and Left(MenuIDN,6) = Left(IDM,6) )) + N' news )' Else NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where uID = '" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__id").Value)) & "' and Left(MenuIDN,2) = Left(IDM,2) )) + N' news )'  End Name from tblMenu,tblPermissions where Left(IDM,6)=Left(MenuID,6) and IDM='" & Request("drlSubID") & "' and UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and TypeM in (2,6) order by ID ASC"
        Else
            strSQL = "select IDM as ID,Case When Levels=2 Then '......'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where Left(MenuIDN,4) = Left(IDM,4) )) + N' news )' When Levels=3 Then '............'+NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where Left(MenuIDN,6) = Left(IDM,6) )) + N' news )'  Else NameM + '(' + convert(nvarchar, (Select count(IDN) from tblNews where Left(MenuIDN,2) = Left(IDM,2) )) + N' news )'  End Name from tblMenu where TypeM in (2,6) order by ID ASC"
        End If
        Call o.BindDataDropList(strSQL, drlMenuID)
        drlMenuID.Text = Request("drlSubID")

        strSQL = "select ID,Name from tblTab where Ac = 1 and Type = 0 order by Sort"
        o.BindDataList(strSQL, "tblTab", dtlTab)

        Session("IsAuthorizeds") = True
        Session("CKFinder_Permission") = "Admin"
    End Sub
    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddU1.Click, btnAddU2.Click
        Dim f As New ArrayList
        Dim v As New ArrayList

        Dim i As Integer
        Dim qtyTab As CheckBox
        Dim strTab As String = ""
        For i = 0 To dtlTab.Items.Count - 1
            qtyTab = CType(dtlTab.Items.Item(i).FindControl("chkTab"), CheckBox)
            If qtyTab.Checked = True Then
                strTab &= ",-" & dtlTab.DataKeys(i) & "-"
            End If
        Next
        If strTab <> "" Then strTab = Mid(strTab, 2)
        f.Add("TabN,0,2,50") : v.Add(strTab) 'Kiểu nvarchar có độ rộng là 50

        txtName.Text = chuyen(Trim(txtName.Text))
        Dim iName1 As String = Bambu.oNet.VNtoEn(txtName.Text, True)

        f.Add("MenuIDN,0,2,6") : v.Add(drlMenuID.SelectedValue) 'Kiểu nvarchar có độ rộng là 6

        f.Add("NameN,0,2,200") : v.Add(txtName.Text) 'Kiểu nvarchar có độ rộng là 200
        f.Add("Name1N,0,2,200") : v.Add(IIf(txtURL.Text = "", iName1, txtURL.Text)) 'Kiểu nvarchar có độ rộng là 100
        f.Add("URL,0,2,200") : v.Add(IIf(txtURL.Text = "", iName1, txtURL.Text)) 'Kiểu nvarchar có độ rộng là 100

        If txtContent.Text <> "" Then
            txtContent.Text = chuyen(txtContent.Text)
            txtContent.Text = Server.UrlDecode(Replace((Server.UrlEncode(Trim(txtContent.Text))), "%0d%0a", " "))
            txtContent.Text = Replace(txtContent.Text, "     ", " ")
            txtContent.Text = Replace(txtContent.Text, "    ", " ")
            txtContent.Text = Replace(txtContent.Text, "   ", " ")
            txtContent.Text = Replace(txtContent.Text, "  ", " ")
        End If
        f.Add("ContentN,0,2,500") : v.Add(Left(txtContent.Text, 500)) 'Kiểu nvarchar có độ rộng là 200

        If txtDescs.Text <> "" Then
            txtDescs.Text = Replace(txtDescs.Text, "<h1", "<h2")
            txtDescs.Text = Replace(txtDescs.Text, "</h1>", "</h2>")
            txtDescs.Text = Replace(txtDescs.Text, " alt=" & """" & """", " alt=" & """" & txtName.Text & """")
            If chklink.Checked = True Then txtDescs.Text = RemuA(txtDescs.Text)
        End If
        f.Add("DescN,0,3,16") : v.Add(txtDescs.Text) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16

        f.Add("meta_title,0,2,500") : v.Add(IIf(txtMetaTitle.Text = "", txtName.Text, txtMetaTitle.Text)) 'Kiểu nvarchar có độ rộng là 200
        f.Add("meta_keywords,0,2,500") : v.Add(txtMetaKeywords.Text) 'Kiểu nvarchar có độ rộng là 200
        f.Add("meta_description,0,2,500") : v.Add(txtMetaDescription.Text) 'Kiểu nvarchar có độ rộng là 200

        If lblID.Text <> 0 Then
            If Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> "1" Then
                strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,2)='" & Left(drlMenuID.SelectedValue, 2) & "'"
                o.DB_Connect(strSQL, 1)
                If o.objDataReader.Read() Then
                    If o.objDataReader("ChkNews") = 1 Then
                        'f.Add("NewsHot,0,1,4") : v.Add(IIf(chkNewsHot.Checked = True, 1, 0)) 'Tin hot - Kiểu Int có độ rộng là 4
                        f.Add("News,0,1,4") : v.Add(IIf(chkNews.Checked = True, 1, 0)) 'Tin mới - Kiểu Int có độ rộng là 4
                        'f.Add("Hot,0,1,4") : v.Add(IIf(chkHot.Checked = True, 1, 0)) 'Tin mới - Kiểu Int có độ rộng là 4
                        f.Add("AcN,0,1,4") : v.Add(IIf(chkAc.Checked = True, 1, 0)) 'Trang thai tin - Kiểu Int có độ rộng là 4
                    End If
                End If
                o.DB_Disconnect(1)
            Else
                'f.Add("duyet,0,1,4") : v.Add(IIf(chkDuyet.Checked = True, 1, 0)) 'Trang thai tin - Kiểu Int có độ rộng là 4
                'f.Add("NewsHot,0,1,4") : v.Add(IIf(chkNewsHot.Checked = True, 1, 0)) 'Tin hot - Kiểu Int có độ rộng là 4
                f.Add("News,0,1,4") : v.Add(IIf(chkNews.Checked = True, 1, 0)) 'Tin mới - Kiểu Int có độ rộng là 4
                'f.Add("Hot,0,1,4") : v.Add(IIf(chkHot.Checked = True, 1, 0)) 'Tin mới - Kiểu Int có độ rộng là 4
                f.Add("AcN,0,1,4") : v.Add(IIf(chkAc.Checked = True, 1, 0)) 'Trang thai tin - Kiểu Int có độ rộng là 4
            End If
            f.Add("IDN,1,1,4") : v.Add(lblID.Text) 'Kiểu Int có độ rộng là 4(key)
            o.UpdateRecord("tblNews", f, v, True)
        Else
            'If Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> "1" Then
            '    strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,2)='" & Left(drlMenuID.SelectedValue, 2) & "'"
            '    o.DB_Connect(strSQL, 1)
            '    If o.objDataReader.Read() Then
            '        If o.objDataReader("ChkNews") = 1 Then
            '            f.Add("duyet,0,1,4") : v.Add(1) 'Trang thai tin - Kiểu Int có độ rộng là 4
            '        Else
            '            f.Add("duyet,0,1,4") : v.Add(0) 'Trang thai tin - Kiểu Int có độ rộng là 4
            '        End If
            '    End If
            '    o.DB_Disconnect(1)
            'Else
            '    f.Add("duyet,0,1,4") : v.Add(1) 'Trang thai tin - Kiểu Int có độ rộng là 4
            'End If

            'f.Add("NewsHot,0,1,4") : v.Add(IIf(chkNewsHot.Checked = True, 1, 0)) 'Tin hot - Kiểu Int có độ rộng là 4
            f.Add("News,0,1,4") : v.Add(IIf(chkNews.Checked = True, 1, 0)) 'Tin mới - Kiểu Int có độ rộng là 4
            'f.Add("Hot,0,1,4") : v.Add(IIf(chkHot.Checked = True, 1, 0)) 'Tin mới - Kiểu Int có độ rộng là 4
            f.Add("AcN,0,1,4") : v.Add(IIf(chkAc.Checked = True, 1, 0)) 'Trang thai tin - Kiểu Int có độ rộng là 4
            f.Add("uID,0,1,4") : v.Add(Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)))

            o.InsertRecord("tblNews", f, v, False)
            strSQL = "select max(IDN) as MaxID from tblNews"
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then lblID.Text = o.objDataReader("MaxID")
            Call o.DB_Disconnect(1)

            'Dim iSEO As Integer = 0
            'strSQL = "select top 1 ID from tblIntro where Ac=0 order by ID ASC"
            'Call o.DB_Connect(strSQL, 1)
            'If o.objDataReader.Read() Then iSEO = o.objDataReader("ID")
            'Call o.DB_Disconnect(1)
            'If iSEO = 0 Then
            '    strSQL = "update tblIntro set Ac=0"
            '    Call o.ExecuteSql(strSQL, False)
            '    strSQL = "select top 1 ID from tblIntro where Ac=0 order by ID ASC"
            '    Call o.DB_Connect(strSQL, 1)
            '    If o.objDataReader.Read() Then iSEO = o.objDataReader("ID")
            '    Call o.DB_Disconnect(1)
            'End If

            'strSQL = "update tblIntro set Ac=1 where ID=" & iSEO
            'Call o.ExecuteSql(strSQL, False)
            'strSQL = "update tblNews set SEON=" & iSEO & " where IDN=" & lblID.Text
            'Call o.ExecuteSql(strSQL, False)
        End If
        'Call o.UploadFile(FilePro, iName1 & "_" & lblID.Text, "/images/news", 2000000, "update tblNews set ImgN=", "where IDN=" & lblID.Text, 500)
        'Call o.UploadFile(FilePro, iName1 & "-" & lblID.Text, "/images/news", 2000000, "update tblNews set Img1N=", "where IDN=" & lblID.Text, 800)

        Dim eFile As String = ""
        Bambu.oNet.UploadPic(FilePro, 5000000, 110, 110, "/images/news/sm_" & iName1 & "_" & lblID.Text, eFile)
        Bambu.oNet.UploadPic(FilePro, 5000000, 300, 300, "/images/news/" & iName1 & "_" & lblID.Text, eFile)
        Bambu.oNet.UploadPic(FilePro, 5000000, 800, 800, "/images/news/" & iName1 & "-" & lblID.Text, eFile)
        If eFile <> "" Then o.ExecuteSql("update tblNews set ImgN='" & iName1 & "_" & lblID.Text + eFile & "', Img1N='" & iName1 & "-" & lblID.Text + eFile & "' where IDN=" & lblID.Text, False)


        If KeyN.Text <> "" Then iKey(KeyN.Text)
        Response.Redirect("news.aspx?id=" & drlMenuID.SelectedValue)
    End Sub
    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            Dim strTT As String = ""

            Session("IsAuthorizeds") = True
            Session("CKFinder_Permission") = "Admin"

            strSQL = "select ID,Name from tblTab where Ac = 1 and Type = 0 order by Sort"
            o.BindDataList(strSQL, "tblTab", dtlTab)

            strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,6)='" & Left(Request("drlSubID"), 6) & "'"
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                chkAc.Visible = o.objDataReader("ChkNews")
                'chkNewsHot.Visible = o.objDataReader("ChkNews")
                chkNews.Visible = o.objDataReader("ChkNews")
            End If
            o.DB_Disconnect(1)

            If Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
                strSQL = "select IDM as ID,Case When Levels=2 Then '......'+NameM + '_c2' When Levels=3 Then '............'+NameM+ '_c3' When Levels=4 Then '.........'+NameM Else NameM+'_c1'  End Name from tblMenu,tblPermissions where Left(IDM,6)=Left(MenuID,6) and IDM='" & Request("drlSubID") & "' and UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and TypeM in (2,6) order by ID ASC"
            Else
                strSQL = "select IDM as ID,Case When Levels=2 Then '......'+NameM + '_c2' When Levels=3 Then '............'+NameM+ '_c3' When Levels=4 Then '.........'+NameM Else NameM+'_c1'  End Name from tblMenu where TypeM in (2,6) order by ID ASC"
            End If
            Call o.BindDataDropList(strSQL, drlMenuID)

            strSQL = "select * from tblNews where IDN=" & CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "Sửa tin tức: " & o.objDataReader("NameN")
                drlMenuID.Text = o.objDataReader("MenuIDN")

                txtName.Text = o.objDataReader("NameN")

                txtContent.Text = o.objDataReader("ContentN")
                txtDescs.Text = o.objDataReader("DescN")
                chkNews.Checked = o.objDataReader("News")
                chkAc.Checked = o.objDataReader("AcN")

                txtURL.Text = o.objDataReader("Name1N")

                If o.objDataReader("ImgN") <> "null.gif" Then
                    imgP1.Visible = True
                    ibtnDelImgT1.Visible = True
                    imgP1.ImageUrl = "/images/news/" & o.objDataReader("ImgN")
                End If

                Try
                    txtMetaTitle.Text = o.objDataReader("meta_title")
                    txtMetaKeywords.Text = o.objDataReader("meta_keywords")
                    txtMetaDescription.Text = o.objDataReader("meta_description")
                Catch ex As Exception

                End Try

            End If
            Call o.DB_Disconnect(1)

            'If strTT <> "" Then
            '    strSQL = "select IDTT from tblTT where IDTT in(" & Replace(strTT, "-", "") & ")"
            '    o.DB_Connect(strSQL, 1)
            '    Do While o.objDataReader.Read()
            '        lblChk.Text &= "<script type='text/javascript'>document.getElementById(" & o.objDataReader("IDTT") & ").checked=true;</script>"
            '    Loop
            '    o.DB_Disconnect(1)
            'End If

        ElseIf (e.CommandName = "eTop") Then
            strSQL = "update tblNews set TimeN=getdate() where IDN=" & dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            Call o.ExecuteSql(strSQL, False)
            Call loadNews()
        End If
    End Sub
    Private Sub ibtnDelImgT1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDelImgT1.Click
        Call DellImg(lblID.Text)
        strSQL = "update tblNews set ImgN='null.gif' where IDN=" & lblID.Text
        Call o.ExecuteSql(strSQL, False)

        imgP1.Visible = False
        ibtnDelImgT1.Visible = False
    End Sub

    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim iID As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim topButton As LinkButton = e.Item.Cells(7).Controls(0)
            Dim editButton As LinkButton = e.Item.Cells(8).Controls(0)
            Dim deleteButton As LinkButton = e.Item.Cells(9).Controls(0)

            topButton.ToolTip = "Up (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID
            deleteButton.ToolTip = "Delete (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID
            editButton.ToolTip = "Update (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID

            deleteButton.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete selected item (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ?')"

            e.Item.Cells(0).ToolTip = "ID: " & iID

            strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,2)='" & Left(drlSubID.SelectedValue, 2) & "'"
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                editButton.Visible = o.objDataReader("EditNews")
                deleteButton.Visible = o.objDataReader("DelNews")
                'CType(e.Item.FindControl("drlAc"), DropDownList).Enabled = o.objDataReader("ChkNews")
            End If
            o.DB_Disconnect(1)
            CType(e.Item.FindControl("drlAc"), DropDownList).Text = DataBinder.Eval(e.Item.DataItem, "AcN")
        End If

        Dim pageindex As Integer = dtgrView.CurrentPageIndex + 1
    End Sub
    Private Sub dtgrView_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgrView.PageIndexChanged
        dtgrView.CurrentPageIndex = e.NewPageIndex
        Call loadNews()
    End Sub
    Private Sub dtgrView_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.DeleteCommand
        If (e.CommandName = "Delete") Then
            Dim iID As Integer = CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call DellImg(iID)
            strSQL = "Delete From tblNews where IDN=" & iID
            Call o.ExecuteSql(strSQL, False)

            Response.Write("<script>alert('The item has been deleted successfully!');</script>")
            Call loadNews()
        End If
    End Sub
    Private Sub drlMenuID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlMenuID.SelectedIndexChanged
        strSQL = "select AddNews,EditNews,DelNews,ChkNews from tblPermissions where UserID=" & Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) & " and Left(MenuID,2)='" & Left(drlMenuID.SelectedValue, 2) & "'"
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            chkAc.Visible = o.objDataReader("ChkNews")
            'chkNewsHot.Visible = o.objDataReader("ChkNews")
            chkNews.Visible = o.objDataReader("ChkNews")
        End If
        o.DB_Disconnect(1)
    End Sub
    Function iKey(ByVal iName As String) As String
        iKey = ""
        If iName = "" Then Exit Function

        Dim Users As String() = iName.Split(",")
        Dim i As Integer = 0
        For i = 0 To Users.Length - 1
            Dim f As New ArrayList
            Dim v As New ArrayList

            f.Add("Name,0,2,300") : v.Add(Trim(Users(i).ToString())) 'Kiểu nvarchar có độ rộng là 200
            f.Add("Name1,0,2,300") : v.Add(Bambu.oNet.VNtoEn(Trim(Users(i).ToString()), True)) 'Kiểu nvarchar có độ rộng là 200

            Try
                o.InsertRecord("tblKey", f, v, False)
            Catch ex As Exception
            End Try
        Next
        iKey = iName
    End Function
    Private Sub DellImg(ByVal iID As Integer)
        strSQL = "select ImgN,Img1N from tblNews where IDN=" & iID
        Call o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            If o.objDataReader("ImgN") <> "null.gif" Then
                Try
                    System.IO.File.Delete(Server.MapPath("/images/news/" & o.objDataReader("ImgN")))
                    System.IO.File.Delete(Server.MapPath("/images/news/sm_" & o.objDataReader("ImgN")))
                Catch Ex As Exception
                End Try
            End If
            If o.objDataReader("Img1N") <> "null.gif" Then
                Try
                    System.IO.File.Delete(Server.MapPath("/images/news/" & o.objDataReader("Img1N")))
                Catch Ex As Exception
                End Try
            End If
        End If
        Call o.DB_Disconnect(1)
    End Sub

    Private Function chuyen(ByVal str As String) As String
        chuyen = str
        chuyen = Replace(chuyen, "ằ", "ằ")
        chuyen = Replace(chuyen, "ắ", "ắ")
        chuyen = Replace(chuyen, "ẳ", "ẳ")
        chuyen = Replace(chuyen, "ẵ", "ẵ")
        chuyen = Replace(chuyen, "ặ", "ặ")

        chuyen = Replace(chuyen, "ầ", "ầ")
        chuyen = Replace(chuyen, "ấ", "ấ")
        chuyen = Replace(chuyen, "ẩ", "ẩ")
        chuyen = Replace(chuyen, "ẫ", "ẫ")
        chuyen = Replace(chuyen, "ậ", "ậ")

        chuyen = Replace(chuyen, "à", "à")
        chuyen = Replace(chuyen, "á", "á")
        chuyen = Replace(chuyen, "ả", "ả")
        chuyen = Replace(chuyen, "ã", "ã")
        chuyen = Replace(chuyen, "ạ", "ạ")

        chuyen = Replace(chuyen, "ề", "ề")
        chuyen = Replace(chuyen, "ế", "ế")
        chuyen = Replace(chuyen, "ể", "ể")
        chuyen = Replace(chuyen, "ễ", "ễ")
        chuyen = Replace(chuyen, "ệ", "ệ")

        chuyen = Replace(chuyen, "è", "è")
        chuyen = Replace(chuyen, "é", "é")
        chuyen = Replace(chuyen, "ẻ", "ẻ")
        chuyen = Replace(chuyen, "ẽ", "ẽ")
        chuyen = Replace(chuyen, "ẹ", "ẹ")

        chuyen = Replace(chuyen, "ì", "ì")
        chuyen = Replace(chuyen, "í", "í")
        chuyen = Replace(chuyen, "ỉ", "ỉ")
        chuyen = Replace(chuyen, "ĩ", "ĩ")
        chuyen = Replace(chuyen, "ị", "ị")

        chuyen = Replace(chuyen, "ỳ", "ỳ")
        chuyen = Replace(chuyen, "ý", "ý")
        chuyen = Replace(chuyen, "ỷ", "ỷ")
        chuyen = Replace(chuyen, "ỹ", "ỹ")
        chuyen = Replace(chuyen, "ỵ", "ỵ")

        chuyen = Replace(chuyen, "ồ", "ồ")
        chuyen = Replace(chuyen, "ố", "ố")
        chuyen = Replace(chuyen, "ổ", "ổ")
        chuyen = Replace(chuyen, "ỗ", "ỗ")
        chuyen = Replace(chuyen, "ộ", "ộ")

        chuyen = Replace(chuyen, "ờ", "ờ")
        chuyen = Replace(chuyen, "ớ", "ớ")
        chuyen = Replace(chuyen, "ở", "ở")
        chuyen = Replace(chuyen, "ỡ", "ỡ")
        chuyen = Replace(chuyen, "ợ", "ợ")

        chuyen = Replace(chuyen, "ò", "ò")
        chuyen = Replace(chuyen, "ó", "ó")
        chuyen = Replace(chuyen, "ó", "ó")
        chuyen = Replace(chuyen, "ỏ", "ỏ")
        chuyen = Replace(chuyen, "õ", "õ")
        chuyen = Replace(chuyen, "ọ", "ọ")

        chuyen = Replace(chuyen, "ừ", "ừ")
        chuyen = Replace(chuyen, "ứ", "ứ")
        chuyen = Replace(chuyen, "ử", "ử")
        chuyen = Replace(chuyen, "ữ", "ữ")
        chuyen = Replace(chuyen, "ự", "ự")

        chuyen = Replace(chuyen, "ù", "ù")
        chuyen = Replace(chuyen, "ú", "ú")
        chuyen = Replace(chuyen, "ủ", "ủ")
        chuyen = Replace(chuyen, "ũ", "ũ")
        chuyen = Replace(chuyen, "ụ", "ụ")

        chuyen = Replace(chuyen, "Ằ", "Ằ")
        chuyen = Replace(chuyen, "Ắ", "Ắ")
        chuyen = Replace(chuyen, "Ẳ", "Ẳ")
        chuyen = Replace(chuyen, "Ẵ", "Ẵ")
        chuyen = Replace(chuyen, "Ặ", "Ặ")

        chuyen = Replace(chuyen, "Ầ", "Ầ")
        chuyen = Replace(chuyen, "Ấ", "Ấ")
        chuyen = Replace(chuyen, "Ẩ", "Ẩ")
        chuyen = Replace(chuyen, "Ẫ", "Ẫ")
        chuyen = Replace(chuyen, "Ậ", "Ậ")

        chuyen = Replace(chuyen, "À", "À")
        chuyen = Replace(chuyen, "Á", "Á")
        chuyen = Replace(chuyen, "Ả", "Ả")
        chuyen = Replace(chuyen, "Ã", "Ã")
        chuyen = Replace(chuyen, "Ạ", "Ạ")

        chuyen = Replace(chuyen, "Ề", "Ề")
        chuyen = Replace(chuyen, "Ế", "Ế")
        chuyen = Replace(chuyen, "Ể", "Ể")
        chuyen = Replace(chuyen, "Ễ", "Ễ")
        chuyen = Replace(chuyen, "Ệ", "Ệ")

        chuyen = Replace(chuyen, "È", "È")
        chuyen = Replace(chuyen, "É", "É")
        chuyen = Replace(chuyen, "Ẻ", "Ẻ")
        chuyen = Replace(chuyen, "Ẽ", "Ẽ")
        chuyen = Replace(chuyen, "Ẹ", "Ẹ")

        chuyen = Replace(chuyen, "Ì", "Ì")
        chuyen = Replace(chuyen, "Í", "Í")
        chuyen = Replace(chuyen, "Ỉ", "Ỉ")
        chuyen = Replace(chuyen, "Ĩ", "Ĩ")
        chuyen = Replace(chuyen, "Ị", "Ị")

        chuyen = Replace(chuyen, "Ỳ", "Ỳ")
        chuyen = Replace(chuyen, "Ý", "Ý")
        chuyen = Replace(chuyen, "Ỷ", "Ỷ")
        chuyen = Replace(chuyen, "Ỹ", "Ỹ")
        chuyen = Replace(chuyen, "Ỵ", "Ỵ")

        chuyen = Replace(chuyen, "Ồ", "Ồ")
        chuyen = Replace(chuyen, "Ố", "Ố")
        chuyen = Replace(chuyen, "Ổ", "Ổ")
        chuyen = Replace(chuyen, "Ỗ", "Ỗ")
        chuyen = Replace(chuyen, "Ộ", "Ộ")

        chuyen = Replace(chuyen, "Ờ", "Ờ")
        chuyen = Replace(chuyen, "Ớ", "Ớ")
        chuyen = Replace(chuyen, "Ở", "Ở")
        chuyen = Replace(chuyen, "Ỡ", "Ỡ")
        chuyen = Replace(chuyen, "Ợ", "Ợ")

        chuyen = Replace(chuyen, "Ò", "Ò")
        chuyen = Replace(chuyen, "Ó", "Ó")
        chuyen = Replace(chuyen, "Ỏ", "Ỏ")
        chuyen = Replace(chuyen, "Õ", "Õ")
        chuyen = Replace(chuyen, "Ọ", "Ọ")

        chuyen = Replace(chuyen, "Ừ", "Ừ")
        chuyen = Replace(chuyen, "Ứ", "Ứ")
        chuyen = Replace(chuyen, "Ử", "Ử")
        chuyen = Replace(chuyen, "Ữ", "Ữ")
        chuyen = Replace(chuyen, "Ự", "Ự")

        chuyen = Replace(chuyen, "Ù", "Ù")
        chuyen = Replace(chuyen, "Ú", "Ú")
        chuyen = Replace(chuyen, "Ủ", "Ủ")
        chuyen = Replace(chuyen, "Ũ", "Ũ")
        chuyen = Replace(chuyen, "Ụ", "Ụ")

        chuyen = Replace(chuyen, "–", "-")
        chuyen = Replace(chuyen, "•", ",")
        chuyen = Replace(chuyen, "[", "(")
        chuyen = Replace(chuyen, "]", ")")
        chuyen = Replace(chuyen, "๏", " ")
        chuyen = Replace(chuyen, "~", "-")
    End Function
    Private Function RemuA(ByVal str As String) As String
        RemuA = ""
        If str <> "" Then
            Dim i As Integer = 0
            For i = 0 To 100
                If InStr(str, "<a ") > 0 Then
                    str = Replace(str, Mid(str, InStr(str, "<a "), InStr(Mid(str, InStr(str, "<a ")), ">")), "")
                Else
                    Exit For
                End If
            Next
            RemuA = Replace(str, "</a>", "")
        End If
    End Function

    Private Sub dtlTab_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlTab.ItemDataBound
        If lblID.Text <> 0 Then
            strSQL = "select TabN from tblNews where TabN like'%-" & DataBinder.Eval(e.Item.DataItem, "ID") & "-%' and IDN=" & lblID.Text
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then CType(e.Item.FindControl("chkTab"), CheckBox).Checked = True
            o.DB_Disconnect(1)
        End If
    End Sub
End Class