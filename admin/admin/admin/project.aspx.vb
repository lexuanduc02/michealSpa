Partial Public Class project
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        End If

        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then
            If Val(Request("pro")) = "0" Then
                Call BindData()
            Else
                Call LoadImgUpdate()
            End If
        End If
    End Sub

    Private Sub BindData()
        tblView.Visible = True
        lblLink.Text = "Projects"

        strSQL = "select IDM as ID,Case When Levels=2 Then '...'+NameM  + ' (' + convert(nvarchar, (Select count(IDP) from tblProject where Left(MenuIDP,4) = Left(IDM,4) )) + N' bản ghi)' When Levels=3 Then '......'+NameM + ' (' + convert(nvarchar, (Select count(IDP) from tblProject where Left(MenuIDP,6) = Left(IDM,6) )) + N' bản ghi)' When Levels=4 Then '.........'+NameM Else NameM + ' (' + convert(nvarchar, (Select count(IDP) from tblProject where Left(MenuIDP,2) = Left(IDM,2) )) + N' bản ghi)' End Name from tblMenu where TypeM=1 order by ID ASC"
        Call o.BindDataDropList(strSQL, drlSubID)
        drlSubID.Items.Insert(0, "Tất cả dự án")
        drlSubID.Items(0).Value = ""

        'drlTP.Items.Insert(0, "TP Hồ Chí Minh")
        'drlTP.Items(0).Value = "2"

        'strSQL = "select ID,Name from tblDistrict where ProvinceId = '2' and Published = 1 order by DisplayOrder"
        'Call o.BindDataDropList(strSQL, drlQ)
        'drlQ.Items.Insert(0, "Tất cả quận huyện")
        'drlQ.Items(0).Value = ""

        If Request("id") <> "" Then drlSubID.Text = Request("id")
        'If Request("q") <> "" Then drlQ.SelectedValue = Request("q")

        If Val(Request("p")) > 0 Then dtgrView.CurrentPageIndex = Val(Request("p"))
        Call loadPro()
    End Sub
    Private Sub loadPro()
        dtgrView.PageSize = 50

        If Request("s") = "" Then
            Dim iwh As String = ""
            If Request("id") <> "" Then
                iwh &= " and MenuIDP = '" & Bambu.oNet.KillChars(Request("id")) & "' "
            End If
            'If Request("q") <> "" Then
            '    iwh &= " and QuanHuyenId = '" & Bambu.oNet.KillChars(Request("q")) & "' "
            'End If
            strSQL = "select IDP as ID,NameP as Name,NameM,TimeP,AcP,ImgP,MenuIDP,Price,completionTimeP,Hot,(Select Name from tblDistrict where Id = QuanHuyenId ) as QuanHuyen from tblProject,tblMenu where MenuIDP = IDM and AcM = 1  " & iwh & "  order by TimeP DESC"
        Else
            key.Text = Request("s")
            Dim iwh As String = " and  NameP+' '+Replace(Name1P,'-',' ') like N'%" & Bambu.oNet.KillChars(Replace(Trim(Request("s")), "'", "")) & "%'"
            strSQL = "select IDP as ID,NameP as Name,NameM,TimeP,AcP,ImgP,MenuIDP,Hot,Price,completionTimeP,PhongTam,PhongNgu,(Select Name from tblDistrict where Id = QuanHuyenId ) as QuanHuyen from tblProject,tblMenu where MenuIDP = IDM  and AcM = 1  " & iwh & "  order by TimeP DESC"
        End If
        Call o.BindDataGrid(strSQL, "tblProject", dtgrView)
    End Sub
    Private Sub drlSubID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlSubID.SelectedIndexChanged
        Response.Redirect("project.aspx?id=" & drlSubID.SelectedValue & "&q=" & Request("q"))
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect("project.aspx?s=" & key.Text)
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

                strSQL = "delete tblProject where IDP=" & Val(dtgrView.DataKeys(i))
                Call o.ExecuteSql(strSQL, False)
            End If
        Next

        If iChk = True Then
            Call o.ShowMessage("You must select one item before delete!")
            Exit Sub
        End If

        Call o.ShowMessage("The item has been deleted successfully!")
        Call loadPro()
    End Sub
    Private Sub btnUpdate1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate1.Click, btnUpdate2.Click

        Dim qtyAc As DropDownList
        Dim i As Integer

        For i = 0 To dtgrView.Items.Count - 1
          
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            strSQL = "update tblProject set AcP=" & qtyAc.SelectedValue & " where IDP=" & Val(dtgrView.DataKeys(i))
            Call o.ExecuteSql(strSQL, False)
        Next

        Call o.ShowMessage("Cập nhật thành công!")

        Call loadPro()
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd1.Click, btnAdd2.Click
        tblView.Visible = False
        tblAdd.Visible = True
        lblLink.Text = "Thêm dịch vụ mới"
        lblID.Text = 0

        Call LoadInsert()
    End Sub
    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddU1.Click, btnAddU2.Click
        Dim f As New ArrayList
        Dim v As New ArrayList


        f.Add("MenuIDP,0,2,8") : v.Add(drlMenuID.SelectedValue) 'Kiểu nvarchar có độ rộng là 6
        txtName.Text = chuyen(Trim(txtName.Text))
        Dim iName1 As String = Bambu.oNet.VNtoEn(txtName.Text, True)

        f.Add("NameP,0,2,150") : v.Add(txtName.Text) 'Kiểu nvarchar có độ rộng là 200
        f.Add("Name1P,0,2,150") : v.Add(iName1) 'Kiểu nvarchar có độ rộng là 200


        f.Add("TinhThanhId,0,1,4") : v.Add(drlThanhPho.SelectedValue)
        'f.Add("QuanHuyenId,0,1,4") : v.Add(drlQuanHuyen.SelectedValue)
        f.Add("Price,0,2,50") : v.Add(txtPrice.Text)
        f.Add("completionTimeP,0,2,50") : v.Add(txtThoiLuong.Text)
        'f.Add("DienTich,0,2,100") : v.Add(txtDienTich.Text)
        'f.Add("PhongTam,0,2,100") : v.Add(txtPhongTam.Text)
        'f.Add("PhongNgu,0,2,100") : v.Add(txtPhongNgu.Text)
        f.Add("ContentP,0,2,300") : v.Add(txtContent.Text) 'Kiểu nvarchar có độ rộng là 200
        f.Add("LienHe,0,2,300") : v.Add(txtLienhe.Text) 'Kiểu nvarchar có độ rộng là 200
        If txtDescs.Text <> "" Then
            txtDescs.Text = Replace(txtDescs.Text, "<h1", "<h2")
            txtDescs.Text = Replace(txtDescs.Text, "</h1>", "</h2>")
            txtDescs.Text = Replace(txtDescs.Text, " alt=" & """" & """", " alt=" & """" & txtName.Text & """")
        End If
        f.Add("DescP,0,3,16") : v.Add(txtDescs.Text) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16

        If txtFooter.Text <> "" Then
            txtFooter.Text = Replace(txtFooter.Text, "<h1", "<h2")
            txtFooter.Text = Replace(txtFooter.Text, "</h1>", "</h2>")
            txtFooter.Text = Replace(txtFooter.Text, " alt=" & """" & """", " alt=" & """" & txtName.Text & """")
        End If
        f.Add("FooterP,0,3,16") : v.Add(txtFooter.Text) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16
        f.Add("TextHome,0,3,16") : v.Add(txtTextHome.Text) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16

        f.Add("AcP,0,1,4") : v.Add(IIf(chkAc.Checked = True, 1, 0)) 'Kiểu Int có độ rộng là 4
        f.Add("Hot,0,1,4") : v.Add(IIf(chkHot.Checked = True, 1, 0)) 'Kiểu Int có độ rộng là 4

        f.Add("MetaTitle,0,2,500") : v.Add(IIf(txtMetaTitle.Text = "", txtName.Text, txtMetaTitle.Text)) 'Kiểu nvarchar có độ rộng là 200
        f.Add("MetaKeywords,0,2,500") : v.Add(txtMetaKeywords.Text) 'Kiểu nvarchar có độ rộng là 200
        f.Add("MetaDescription,0,2,500") : v.Add(IIf(txtMetaDescription.Text = "", txtContent.Text, txtMetaDescription.Text)) 'Kiểu nvarchar có độ rộng là 200
        f.Add("VideoP,0,2,100") : v.Add(txtVideo.Text)

        f.Add("Facebook,0,2,200") : v.Add(Facebook.Text)
        f.Add("Youtube,0,2,200") : v.Add(Youtube.Text)

        If lblID.Text <> 0 Then
            f.Add("IDP,1,1,4") : v.Add(lblID.Text) 'Kiểu Int có độ rộng là 4(key)
            o.UpdateRecord("tblProject", f, v, False)
        Else
            dtgrView.CurrentPageIndex = 0
            o.InsertRecord("tblProject", f, v, False)

            strSQL = "select max(IDP) as MaxID from tblProject"
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then lblID.Text = o.objDataReader("MaxID")
            o.DB_Disconnect(1)
        End If

        o.UploadFile(FilePro, iName1 & "-j" & lblID.Text, "/images/service", 2000000, "update tblProject set ImgP=", "where IDP=" & lblID.Text, 600)
        o.UploadFile(FilePro, iName1 & "_j" & lblID.Text, "/images/service", 2000000, "update tblProject set Img1P=", "where IDP=" & lblID.Text, 1600)

        o.UploadFile(FilePro1, "logo_" & lblID.Text, "/images/service", 2000000, "update tblProject set LogoP=", "where IDP=" & lblID.Text, 250)
        o.UploadFile(FileBgFooter, "img-contact", "/images/bg", 2000000, "", "", 1600)

        Response.Redirect("project.aspx?id=" & Request("id") & "&q=" & Request("q") & "&p=" & dtgrView.CurrentPageIndex)
    End Sub

    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()

            Call LoadInsert()
            Dim TinhThanhId As Integer = 0
            Dim QuanHuyenId As Integer = 0
            strSQL = "select * from tblProject where IDP=" & CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "Edit : " & o.objDataReader("NameP")
                drlMenuID.Text = o.objDataReader("MenuIDP")

                txtPrice.Text = o.objDataReader("Price")
                txtThoiLuong.Text = o.objDataReader("completionTimeP")
                txtName.Text = o.objDataReader("NameP")
                txtDescs.Text = o.objDataReader("DescP")
                txtFooter.Text = o.objDataReader("FooterP")
                txtContent.Text = o.objDataReader("ContentP")
                chkAc.Checked = o.objDataReader("AcP")
                chkHot.Checked = o.objDataReader("Hot")
                txtVideo.Text = o.objDataReader("VideoP")
                Try
                    Facebook.Text = o.objDataReader("Facebook")
                    Youtube.Text = o.objDataReader("Youtube")
                Catch ex As Exception

                End Try

                Try
                    If o.objDataReader("ImgP") <> "null.gif" Then
                        imgP1.Visible = True
                        ibtnDelImgT1.Visible = True
                        'imgP1.ImageUrl = "/images/project/" & o.objDataReader("ImgP")
                        imgP1.ImageUrl = "/images/service/" & o.objDataReader("ImgP")
                    End If
                Catch ex As Exception

                End Try

                Try
                    If o.objDataReader("LogoP") <> "null.gif" Then
                        imgP2.Visible = True
                        ibtnDelImgT2.Visible = True
                        imgP2.ImageUrl = "/images/service/" & o.objDataReader("LogoP")
                    End If
                Catch ex As Exception

                End Try

              
                Try
                    TinhThanhId = o.objDataReader("TinhThanhId")
                    QuanHuyenId = o.objDataReader("QuanHuyenId")

                    txtMetaTitle.Text = o.objDataReader("MetaTitle")
                    txtMetaKeywords.Text = o.objDataReader("MetaKeywords")
                    txtMetaDescription.Text = o.objDataReader("MetaDescription")
                    txtDienTich.Text = o.objDataReader("DienTich")
                    txtPhongTam.Text = o.objDataReader("PhongTam")
                    txtPhongNgu.Text = o.objDataReader("PhongNgu")
                    txtLienhe.Text = o.objDataReader("LienHe")
                    txtTextHome.Text = o.objDataReader("TextHome")
                Catch ex As Exception

                End Try
            End If
            Call o.DB_Disconnect(1)

            'Try
            '    If TinhThanhId <> 0 Then
            '        drlThanhPho.SelectedValue = TinhThanhId

            '        strSQL = "Select * from tblDistrict where ProvinceId = '" & TinhThanhId & "' and Published = 'True' order by DisplayOrder"
            '        o.BindDataDropList(strSQL, drlQuanHuyen)

            '        If drlQuanHuyen.Items.Count <> 0 Then
            '            drlQuanHuyen.Visible = True
            '            drlQuanHuyen.SelectedValue = QuanHuyenId
            '        End If
            '    End If

            'Catch ex As Exception

            'End Try


        ElseIf (e.CommandName = "eTop") Then
            strSQL = "update tblProject set TimeP=getdate() where IDP=" & dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            Call o.ExecuteSql(strSQL, False)
            Call loadPro()
        End If
    End Sub
    Private Sub ibtnDelImgT1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDelImgT1.Click
        Call DellImg(lblID.Text)

        strSQL = "update tblProject set ImgP='null.gif',Img1P='null.gif' where IDP=" & lblID.Text
        Call o.ExecuteSql(strSQL, False)

        imgP1.Visible = False
        ibtnDelImgT1.Visible = False
    End Sub

    Private Sub ibtnDelImgT2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDelImgT2.Click
        DellImg2(lblID.Text)
        strSQL = "update tblProject set LogoP='null.gif' where IDP=" & lblID.Text
        Call o.ExecuteSql(strSQL, False)

        imgP2.Visible = False
        ibtnDelImgT2.Visible = False
    End Sub
    Private Sub LoadInsert()

        Session("IsAuthorizeds") = True
        Session("CKFinder_Permission") = "Admin"

        strSQL = "select IDM as ID,Case When Levels=2 Then '...'+NameM When Levels=3 Then '......'+NameM When Levels=4 Then '.........'+NameM Else NameM End Name from tblMenu where TypeM=1  order by ID ASC"
        o.BindDataDropList(strSQL, drlMenuID)

        drlMenuID.Items.Insert(0, "Chọn chuyên mục")
        drlMenuID.Items(0).Value = ""
        drlMenuID.Text = Request("drlSubID")

        strSQL = "Select * from tblCountry order by DisplayOrder"
        o.BindDataDropList(strSQL, drlThanhPho)
        drlThanhPho.Items.Insert(0, "Chọn Tinh / TP")
        drlThanhPho.Items(0).Value = "0"

        strSQL = "Select * from tblDistrict where Published = 'True' and ProvinceId = '" & drlThanhPho.SelectedValue & "' order by DisplayOrder"
        o.BindDataDropList(strSQL, drlQuanHuyen)
        If drlQuanHuyen.Items.Count <> 0 Then
            drlQuanHuyen.Visible = True
        Else
            drlQuanHuyen.Visible = False
        End If

    End Sub
    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim iID As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim topButton As LinkButton = e.Item.Cells(8).Controls(0)
            Dim editButton As LinkButton = e.Item.Cells(9).Controls(0)
            Dim deleteButton As LinkButton = e.Item.Cells(10).Controls(0)

            topButton.ToolTip = "Update time (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID
            deleteButton.ToolTip = "Delete (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID
            editButton.ToolTip = "Edit (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID

            deleteButton.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete selected item (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ?')"

            'If DataBinder.Eval(e.Item.DataItem, "Hot") = 0 Then
            '    e.Item.Cells(7).Text = "Tin thường"
            'End If
            e.Item.Cells(0).ToolTip = "ID: " & iID
            CType(e.Item.FindControl("drlAc"), DropDownList).Text = DataBinder.Eval(e.Item.DataItem, "AcP")
        End If

        Dim pageindex As Integer = dtgrView.CurrentPageIndex + 1
    End Sub
    Private Sub dtgrView_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgrView.PageIndexChanged
        dtgrView.CurrentPageIndex = e.NewPageIndex
        Call loadPro()
    End Sub
    Private Sub dtgrView_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.DeleteCommand
        If (e.CommandName = "Delete") Then
            Dim iID As Integer = CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call DellImg(iID)

            strSQL = "Delete From tblProject where IDP = " & iID
            Call o.ExecuteSql(strSQL, False)

            Response.Write("<script>alert('The item has been deleted successfully!');</script>")
            Call loadPro()
        End If
    End Sub
    Private Sub DellImg(ByVal iID As Integer)
        strSQL = "select ImgP,Img1P from tblProject where IDP=" & iID
        Call o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            If o.objDataReader("ImgP") <> "null.gif" Then
                Try
                    'System.IO.File.Delete(Server.MapPath("/images/project/" & o.objDataReader("ImgP")))
                    System.IO.File.Delete(Server.MapPath("/images/service/" & o.objDataReader("ImgP")))
                Catch Ex As Exception
                End Try
            End If
            If o.objDataReader("Img1P") <> "null.gif" Then
                Try
                    'System.IO.File.Delete(Server.MapPath("/images/project/" & o.objDataReader("Img1P")))
                    System.IO.File.Delete(Server.MapPath("/images/service/" & o.objDataReader("Img1P")))
                Catch Ex As Exception
                End Try
            End If
        End If
        Call o.DB_Disconnect(1)
    End Sub

    Private Sub DellImg2(ByVal iID As Integer)
        strSQL = "select LogoP from tblProject where IDP=" & iID
        Call o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            If o.objDataReader("LogoP") <> "null.gif" Then
                Try
                    'System.IO.File.Delete(Server.MapPath("/images/project/" & o.objDataReader("LogoP")))
                    System.IO.File.Delete(Server.MapPath("/images/service/" & o.objDataReader("LogoP")))
                Catch Ex As Exception
                End Try
            End If
        End If
        Call o.DB_Disconnect(1)
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

    'Private Sub drlThanhPho_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drlThanhPho.SelectedIndexChanged
    '    strSQL = "Select * from tblDistrict where Published = 'True' and ProvinceId = '" & drlThanhPho.SelectedValue & "' order by DisplayOrder"
    '    o.BindDataDropList(strSQL, drlQuanHuyen)
    '    If drlQuanHuyen.Items.Count <> 0 Then
    '        drlQuanHuyen.Visible = True
    '    Else
    '        drlQuanHuyen.Visible = False
    '    End If
    'End Sub

    'Private Sub drlQ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drlQ.SelectedIndexChanged
    '    Response.Redirect("project.aspx?id=" & Request("id") & "&q=" & drlQ.SelectedValue)
    'End Sub

    Private Sub LoadImgUpdate()
        pel_UploadImg.Visible = True
        Dim pname As String = ""
        strSQL = "select NameP,MenuIDP from tblProject where IDP=" & Val(Request("pro"))
        o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            pname = o.objDataReader("NameP")
            lblMn.Text = o.objDataReader("MenuIDP")
        End If
        o.DB_Disconnect(1)
        lblLink.Text = "Add a new picture for : " & pname

        Call LoadIMg()
    End Sub
    Private Sub LoadIMg()
        strSQL = "select * from tblImg where ProID=" & Val(Request("pro")) & " and Type = '" & drlTypeI.SelectedValue & "' order by ID ASC"
        Call o.BindDataGrid(strSQL, "ID", gridProImg)
    End Sub
    Private Sub drpMS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpMS.SelectedIndexChanged
        Call LoadIMg()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, Button3.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True

        For i = 0 To gridProImg.Items.Count - 1
            qty = CType(gridProImg.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                iChk = False
                strSQL = "delete tblImg where ID=" & Val(gridProImg.DataKeys(i))
                o.ExecuteSql(strSQL, False)
            End If
        Next

        If iChk = True Then
            o.ShowMessage("You must select one item before delete!")
            Exit Sub
        End If

        o.ShowMessage("The item has been deleted successfully!")
        Call LoadImgUpdate()
    End Sub
    Private Sub btnUpdateImg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateImg.Click
        Dim iId As New List(Of String)
        If fileImg.HasFiles Then
            For Each uploadedFile As HttpPostedFile In fileImg.PostedFiles

                Dim f As New ArrayList
                Dim v As New ArrayList

                f.Add("ProID,0,1,4") : v.Add(Val(Request("pro"))) 'Kiểu Int có độ rộng là 4
                f.Add("Name,0,2,300") : v.Add("") 'Kiểu nvarchar có độ rộng là 100
                f.Add("Type,0,1,4") : v.Add(drlTypeI.SelectedValue) 'Kiểu Int có độ rộng là 4

                f.Add("AcP,0,1,4") : v.Add(IIf(chkAcP.Checked = True, 1, 0)) 'Kiểu Int có độ rộng là 4

                o.InsertRecord("tblImg", f, v, False)

                strSQL = "select cast(coalesce(Max(ID),0) as int) as ID from tblImg"
                o.DB_Connect(strSQL, 1)
                If o.objDataReader.Read() Then lblID.Text = o.objDataReader("ID")
                iId.Add(lblID.Text)
                o.DB_Disconnect(1)

                Dim eFile As String = ""
                'Bambu.oNet.UploadPicS(uploadedFile, 50000000, 1100, 1100, "/images/project/lager_" & lblID.Text, eFile)
                Bambu.oNet.UploadPicS(uploadedFile, 50000000, 1100, 1100, "/images/service/lager_" & lblID.Text, eFile)
                If eFile <> "" Then o.ExecuteSql("update tblImg set Img1='lager_" & lblID.Text & eFile & "', Img='lager_" & lblID.Text & eFile & "' where ID=" & lblID.Text, False)

            Next
        End If

        o.ShowMessage("The picture has been inserted successfully!")
        Call LoadImgUpdate()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click, Button4.Click
        Dim qtyName As TextBox

        Dim qtyAc As DropDownList
        Dim qtyImgT As FileUpload
        Dim i As Integer

        For i = 0 To gridProImg.Items.Count - 1
            qtyName = CType(gridProImg.Items.Item(i).FindControl("Name"), TextBox)
            qtyAc = CType(gridProImg.Items.Item(i).FindControl("drlAc"), DropDownList)
            qtyImgT = CType(gridProImg.Items.Item(i).FindControl("FilePro"), FileUpload)

            Dim f As New ArrayList
            Dim v As New ArrayList

            f.Add("Name,0,2,200") : v.Add(qtyName.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
            f.Add("AcP,0,1,4") : v.Add(qtyAc.SelectedValue) 'Trang thai - Kiểu Int có độ rộng là 4

            f.Add("ID,1,1,4") : v.Add(Val(gridProImg.DataKeys(i))) 'Kiểu Int có độ rộng là 4(key)
            o.UpdateRecord("tblImg", f, v, False)

            Dim eFile As String = ""
            Bambu.oNet.UploadPic(CType(gridProImg.Items.Item(i).FindControl("FilePro"), FileUpload), 5000000, 1100, 1100, "/images/service/lager_" & Val(gridProImg.DataKeys(i)), eFile)
            'Bambu.oNet.UploadPic(CType(gridProImg.Items.Item(i).FindControl("FilePro"), FileUpload), 5000000, 1100, 1100, "/images/project/lager_" & Val(gridProImg.DataKeys(i)), eFile)
            If eFile <> "" Then o.ExecuteSql("update tblImg set Img='lager__" & Val(gridProImg.DataKeys(i)) & eFile & "', Img1='lager_" & Val(gridProImg.DataKeys(i)) & eFile & "' where ID=" & Val(gridProImg.DataKeys(i)), False)

        Next
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>alert('The picture has been updated successfully!');window.open('project.aspx?pro=" & Val(Request("pro")) & "','_parent');</script>")
    End Sub
    Private Sub gridProImg_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridProImg.ItemDataBound
        Dim iID As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim deleteButton As LinkButton = e.Item.Cells(6).Controls(0)

            deleteButton.ToolTip = "Delete (" & DataBinder.Eval(e.Item.DataItem, "ID") & ") ID #:" & iID

            deleteButton.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete selected item (" & DataBinder.Eval(e.Item.DataItem, "ID") & ") ?')"

            e.Item.Cells(0).ToolTip = "ID: " & iID

            CType(e.Item.FindControl("drlAc"), DropDownList).Text = DataBinder.Eval(e.Item.DataItem, "AcP")
        End If

        Dim pageindex As Integer = gridProImg.CurrentPageIndex + 1
    End Sub
    Private Sub gridProImg_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles gridProImg.PageIndexChanged
        gridProImg.CurrentPageIndex = e.NewPageIndex
        Call LoadImgUpdate()
    End Sub
    Private Sub gridProImg_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridProImg.DeleteCommand
        Dim iID As Integer = CInt(gridProImg.DataKeys(e.Item.ItemIndex).ToString())
        If (e.CommandName = "Delete") Then
            Try
                'System.IO.File.Delete(Server.MapPath("/images/project/" & Val(iID) & ".jpg"))
                'System.IO.File.Delete(Server.MapPath("/images/project/" & Val(iID) & ".gif"))
                System.IO.File.Delete(Server.MapPath("/images/service/" & Val(iID) & ".jpg"))
                System.IO.File.Delete(Server.MapPath("/images/service/" & Val(iID) & ".gif"))

            Catch Ex As Exception
            End Try

            strSQL = "Delete From tblImg where ID=" & iID
            o.ExecuteSql(strSQL, True)

            Call LoadIMg()
        End If
    End Sub
End Class