Imports System.IO
Imports Bambu

Partial Public Class menu
    Inherits Page

    Dim _strSql As String
    Dim _iTid As String = "000000"
    Dim _o As New oData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        ElseIf Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            Response.Redirect("project.aspx")
        End If
        oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Request("lv") = "" Then Response.Redirect("index.aspx")
        If Not Page.IsPostBack Then BindData()
    End Sub

    Private Sub BindData()
        tblView.Visible = True
        lblLink.Text = "Quản lý menu cấp " & Request("lv")

        If Request("lv") <> 1 Then

            drlSubID.Visible = True
            _strSql = "select IDM as ID,Case When Levels=2 Then '...'+NameM When Levels=3 Then '......'+NameM Else '1,'+NameM End Name from tblMenu where Levels<" & Request("lv") & " order by IDM ASC"
            _o.BindDataDropList(_strSql, drlSubID)
            If Request("id") <> "" Then drlSubID.Text = Request("id")
        End If
        LoadMenu()
    End Sub
    Private Sub drlSubID_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drlSubID.SelectedIndexChanged
        dtgrView.CurrentPageIndex = 0
        LoadMenu()
    End Sub
    Private Sub LoadMenu()
        If Request("lv") = 3 Then
            Dim i As Integer
            For i = 0 To drlSubID.Items.Count - 1
                If Right(drlSubID.Items(i).Value, 4) = "0000" Then
                    drlSubID.Items(i).Attributes.Add("style", "font-weight:bold;color:black;")
                    drlSubID.Items(i).Attributes.Add("disabled", "true")
                End If
            Next
        End If
        Dim wh As String = ""
        If Request("lv") <> 1 Then
            wh = " and Left(IDM," & Request("lv") * 2 - 2 & ")='" & Left(drlSubID.SelectedValue, Request("lv") * 2 - 2) & "'"
        End If
        dtgrView.PageSize = 50
        _strSql = "select IDM as ID,NameM as Name,Name1M,AutoM,SortM,AcM,'<span style=color:#999;font-size:10px> ' + case when URL is null then '" & Request.Url.Host & "/' + Name1M +'-id1' + convert(nvarchar,AutoM) +'.html' else URL + '</span>' end URL,ImgM,ImgM2,ImgM3,Case When TypeM='1' Then 'Pro' When TypeM ='2' Then 'News'  Else 'Blank' End Type from tblMenu where levels=" & Val(Request("lv")) & wh & " order by SortM ASC"
        _o.BindDataGrid(_strSql, "tblMenu", dtgrView)
    End Sub

    Private Sub btnUpdate1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate1.Click, btnUpdate2.Click
        Dim qtySort As TextBox
        Dim qtyAc As DropDownList
        Dim i As Integer
        For i = 0 To dtgrView.Items.Count - 1
            qtySort = CType(dtgrView.Items.Item(i).FindControl("txtSort"), TextBox)
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            _strSql = "update tblMenu set SortM='" & qtySort.Text & "',AcM=" & qtyAc.SelectedValue & " where IDM=" & Val(dtgrView.DataKeys(i))
            _o.ExecuteSql(_strSql, False)
        Next
        _o.ShowMessage("Cập nhật thành công!")
        LoadMenu()
    End Sub
    Private Sub btnDel1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDel1.Click, btnDel2.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True
        For i = 0 To dtgrView.Items.Count - 1
            qty = CType(dtgrView.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                DellImg(dtgrView.DataKeys(i))
                iChk = False
                _strSql = "delete tblMenu where IDM='" & dtgrView.DataKeys(i) & "'"
                _o.ExecuteSql(_strSql, False)
            End If
        Next
        If iChk = True Then
            _o.ShowMessage("Bạn phải chọn trước khia xóa!")
            Exit Sub
        End If

        _o.ShowMessage("Xoá thành công!")
        LoadMenu()
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd1.Click, btnAdd2.Click
        tblView.Visible = False
        tblAdd.Visible = True


        lblID.Text = ""

        lblType.Text = Request("drlChk")

        If Request("lv") <> 1 Then
            drlMenuID.Visible = True
            _strSql = "select IDM as ID,Case When Levels=2 Then '...'+NameM When Levels=3 Then '......'+NameM Else '1,'+NameM End Name from tblMenu where  Levels<" & Request("lv") & " order by IDM ASC"
            _o.BindDataDropList(_strSql, drlMenuID)
            drlMenuID.Text = Request("drlSubID")
        End If
        If Request("lv") = 3 Then
            Dim i As Integer
            For i = 0 To drlMenuID.Items.Count - 1
                If Right(drlMenuID.Items(i).Value, 4) = "0000" Then
                    drlMenuID.Items(i).Attributes.Add("style", "font-weight:bold;color:black;")
                    drlMenuID.Items(i).Attributes.Add("disabled", "true")
                End If
            Next
        End If

        Session("IsAuthorizeds") = True
        Session("CKFinder_Permission") = "Admin"
    End Sub
    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddU1.Click, btnAddU2.Click
        Dim f As New ArrayList
        Dim v As New ArrayList
        Dim iSub As String = ""

        txtName.Text = Chuyen(Trim(txtName.Text))
        Dim iName1 As String = oNet.VnToEn(txtName.Text, True)

        f.Add("NameM,0,2,100") : v.Add(txtName.Text) 'Kiểu nvarchar có độ rộng là 100
        f.Add("Name1M,0,2,100") : v.Add(oNet.VnToEn(txtName.Text, True)) 'Kiểu nvarchar có độ rộng là 100

        If txtURL.Text <> "" Then
            f.Add("URL,0,2,200") : v.Add(txtURL.Text)
        End If

        If txtDescs.Text <> "" Then
            txtDescs.Text = Replace(txtDescs.Text, "<h1", "<h2")
            txtDescs.Text = Replace(txtDescs.Text, "</h1>", "</h2>")
            txtDescs.Text = Replace(txtDescs.Text, " alt=" & """" & """", " alt=" & """" & txtName.Text & """")
            If chklink.Checked = True Then txtDescs.Text = RemuA(txtDescs.Text)
        End If
        f.Add("DescM,0,3,16") : v.Add(txtDescs.Text) 'Kiểu ntext độ rộng là 16

        f.Add("title,0,2,200") : v.Add(IIf(title.Text = "", txtName.Text, title.Text)) 'Kiểu nvarchar có độ rộng là 100
        f.Add("keywords,0,2,100") : v.Add(IIf(keywords.Text = "", txtName.Text, keywords.Text)) 'Kiểu nvarchar có độ rộng là 100

        If description.Text <> "" Then
            description.Text = Chuyen(description.Text)
            description.Text = Server.UrlDecode(Replace((Server.UrlEncode(description.Text)), "%0d%0a", " "))
            description.Text = Replace(description.Text, "       ", " ")
            description.Text = Replace(description.Text, "      ", " ")
            description.Text = Replace(description.Text, "     ", " ")
            description.Text = Replace(description.Text, "    ", " ")
            description.Text = Replace(description.Text, "   ", " ")
            description.Text = Replace(description.Text, "  ", " ")
        End If
        f.Add("description,0,2,300") : v.Add(IIf(description.Text = "", txtName.Text, description.Text)) 'Kiểu nvarchar có độ rộng là 100

        f.Add("ContentM,0,2,500") : v.Add(txtContent.Text) 'Kiểu nvarchar có độ rộng là 100

        f.Add("TypeM,0,1,4") : v.Add(Val(Request("rbType"))) 'Kiểu Int có độ rộng là 4
        f.Add("Levels,0,1,4") : v.Add(Request("lv")) 'Kiểu Int có độ rộng là 4
        f.Add("ChkM,0,1,4") : v.Add(0) 'Kiểu Int có độ rộng là 4
        f.Add("SortM,0,2,10") : v.Add(IIf(txtSortM.Text = "", "00", Replace(txtSortM.Text, "'", ""))) 'Kiểu nvarchar có độ rộng là 10

        f.Add("AcM,0,1,4") : v.Add(drlAc.Text) 'Kiểu Int có độ rộng là 4

        f.Add("HomeM,0,1,4") : v.Add(IIf(HomeM.Checked = True, 1, 0)) 'Kiểu Int có độ rộng là 4

        f.Add("TopM,0,1,4") : v.Add(IIf(TopM.Checked = True, 1, 0)) 'Kiểu Int có độ rộng là 4
        f.Add("HotM,0,1,4") : v.Add(IIf(HotM.Checked = True, 1, 0)) 'Kiểu Int có độ rộng là 4
        f.Add("BottomM,0,1,4") : v.Add(IIf(BottomM.Checked = True, 1, 0))

        If lblID.Text <> "" Then
            If drlMenuID.Text <> drlSubID.Text Then
                _strSql = "update tblMenu set IDM='" & Left(MenuCode(), Request("lv") * 2) & "'+right(IDM," & Len(_iTid) - (Request("lv") * 2) & ") where left(IDM," & Request("lv") * 2 & ")=" & Left(lblID.Text, Request("lv") * 2)
                _o.ExecuteSql(_strSql, False)

                _strSql = "update tblPro set MenuIDP='" & Left(MenuCode(), Request("lv") * 2) & "'+right(MenuIDP," & Len(_iTid) - (Request("lv") * 2) & ") where left(MenuIDP," & Request("lv") * 2 & ")=" & Left(lblID.Text, Request("lv") * 2)
                _o.ExecuteSql(_strSql, False)

                _strSql = "update tblNews set MenuIDN='" & Left(MenuCode(), Request("lv") * 2) & "'+right(MenuIDN," & Len(_iTid) - (Request("lv") * 2) & ") where left(MenuIDN," & Request("lv") * 2 & ")=" & Left(lblID.Text, Request("lv") * 2)
                _o.ExecuteSql(_strSql, False)

                f.Add("IDM,1,2,6") : v.Add(MenuCode()) 'Kiểu nvarchar có độ rộng là 6(key)
            Else
                f.Add("IDM,1,2,6") : v.Add(lblID.Text) 'Kiểu nvarchar có độ rộng là 6(key)
            End If
            _o.UpdateRecord("tblMenu", f, v, False)
        Else
            lblID.Text = MenuCode()
            f.Add("IDM,1,2,6") : v.Add(lblID.Text) 'Kiểu nvarchar có độ rộng là 6(key)
            _o.InsertRecord("tblMenu", f, v, False)
        End If

        Dim iId As Integer = 0
        _strSql = "select AutoM from tblMenu where IDM='" & lblID.Text & "'"
        _o.DB_Connect(_strSql, 1)
        If _o.objDataReader.Read() Then iId = _o.objDataReader("AutoM")
        _o.DB_Disconnect(1)


        _o.UploadFile(FilePro, iName1 & "_" & iId, "/images/menu", 5000000, "update tblMenu set ImgM=", "where IDM=" & lblID.Text, 100)
        _o.UploadFile(FileUpload2, iName1 & "_" & iId & "_3", "/images/menu", 5000000, "update tblMenu set ImgM3=", "where IDM=" & lblID.Text, 600)


        Response.Redirect("menu.aspx?id=" & drlMenuID.SelectedValue & "&lv=" & Request("lv"))
    End Sub
    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            lblLink.Text = "Sửa menu cấp " & Request("lv")

            If Request("lv") <> 1 Then
                drlMenuID.Visible = True
                _strSql = "select IDM as ID,Case When Levels=2 Then '...'+NameM When Levels=3 Then '......'+NameM Else '1,'+NameM End Name from tblMenu where  Levels<" & Request("lv") & " order by IDM ASC"
                _o.BindDataDropList(_strSql, drlMenuID)
                drlMenuID.Text = Request("drlSubID")
            End If
            If Request("lv") = 3 Then
                Dim i As Integer
                For i = 0 To drlMenuID.Items.Count - 1
                    If Right(drlMenuID.Items(i).Value, 4) = "0000" Then
                        drlMenuID.Items(i).Attributes.Add("style", "font-weight:bold;color:black;")
                        drlMenuID.Items(i).Attributes.Add("disabled", "true")
                    End If
                Next
            End If

            Session("IsAuthorizeds") = True
            Session("CKFinder_Permission") = "Admin"

            _strSql = "select * from tblMenu where IDM=" & dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            _o.DB_Connect(_strSql, 1)
            If _o.objDataReader.Read() Then
                drlSubID.Text = Left(_o.objDataReader("IDM"), 2 * Request("lv") - 2) & Left(_iTid, Len(_iTid) - (2 * Request("lv") - 2))
                txtName.Text = _o.objDataReader("NameM")
                Try
                    txtURL.Text = _o.objDataReader("URL")
                Catch ex As Exception

                End Try

                txtSortM.Text = _o.objDataReader("SortM")
                drlAc.Text = _o.objDataReader("AcM")
                TopM.Checked = _o.objDataReader("TopM")
                HomeM.Checked = _o.objDataReader("HomeM")
                BottomM.Checked = IIf(IsDBNull(_o.objDataReader("BottomM")), 0, _o.objDataReader("BottomM"))
                HotM.Checked = _o.objDataReader("HotM")
                txtDescs.Text = _o.objDataReader("DescM")
                lblType.Text = _o.objDataReader("TypeM")

                title.Text = _o.objDataReader("title")
                keywords.Text = _o.objDataReader("keywords")
                description.Text = _o.objDataReader("description")

                If _o.objDataReader("ImgM") <> "null.gif" Then
                    imgP1.Visible = True
                    ibtnDelImgT1.Visible = True
                    imgP1.ImageUrl = "/images/menu/" & _o.objDataReader("ImgM")
                End If

                If _o.objDataReader("ImgM2") <> "null.gif" Then
                    Image1.Visible = True
                    ImageButton1.Visible = True
                    Image1.ImageUrl = "/images/menu/" & _o.objDataReader("ImgM2")
                End If

                If _o.objDataReader("ImgM3") <> "null.gif" Then
                    Image2.Visible = True
                    ImageButton2.Visible = True
                    Image2.ImageUrl = "/images/menu/" & _o.objDataReader("ImgM3")
                End If

                Try
                    txtContent.Text = _o.objDataReader("ContentM")
                Catch ex As Exception

                End Try
            End If
            _o.DB_Disconnect(1)
        End If
    End Sub
    Private Sub ibtnDelImgT1_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibtnDelImgT1.Click
        DellImg(lblID.Text)

        _strSql = "update tblMenu set ImgM='null.gif' where IDM=" & lblID.Text
        _o.ExecuteSql(_strSql, False)

        imgP1.Visible = False
        ibtnDelImgT1.Visible = False
    End Sub
    Private Sub ImageButton1_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImageButton1.Click
        _strSql = "select ImgM2 from tblMenu where IDM='" & lblID.Text & "'"
        _o.DB_Connect(_strSql, 1)
        If _o.objDataReader.Read() Then
            If _o.objDataReader("ImgM2") <> "null.gif" Then
                Try
                    File.Delete(Server.MapPath("/images/menu/" & _o.objDataReader("ImgM2")))
                Catch ex As Exception
                End Try
            End If
        End If
        _o.DB_Disconnect(1)

        _strSql = "update tblMenu set ImgM2='null.gif' where IDM=" & lblID.Text
        _o.ExecuteSql(_strSql, False)

        Image1.Visible = False
        ImageButton1.Visible = False
    End Sub
    Private Sub ImageButton2_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImageButton2.Click
        _strSql = "select ImgM3 from tblMenu where IDM='" & lblID.Text & "'"
        _o.DB_Connect(_strSql, 1)
        If _o.objDataReader.Read() Then
            If _o.objDataReader("ImgM3") <> "null.gif" Then
                Try
                    File.Delete(Server.MapPath("/images/menu/" & _o.objDataReader("ImgM3")))
                Catch ex As Exception
                End Try
            End If
        End If
        _o.DB_Disconnect(1)

        _strSql = "update tblMenu set ImgM3='null.gif' where IDM=" & lblID.Text
        _o.ExecuteSql(_strSql, False)

        Image2.Visible = False
        ImageButton2.Visible = False
    End Sub
    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim iId As String = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim editButton As LinkButton = e.Item.Cells(7).Controls(0)
            Dim deleteButton As LinkButton = e.Item.Cells(8).Controls(0)

            If Right(iId, 4) <> "0000" Then
                deleteButton.Attributes("onclick") = "javascript:return confirm('Ban chac chan xoa menu (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") nay khong?')"
            Else
                deleteButton.Attributes("onclick") = "javascript:return confirm('Nếu bạn xoá menu (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") thì toàn bộ menu cấp 2 thuộc menu (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") sẽ bị xoá theo. Bạn chắc chắn muốn xoá menu (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") không?')"
            End If

            deleteButton.ToolTip = "Xoa tin (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iId
            editButton.ToolTip = "Sua tin (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iId

            e.Item.Cells(2).ToolTip = "Mã menu: " & iId

            CType(e.Item.FindControl("drlAc"), DropDownList).Text = DataBinder.Eval(e.Item.DataItem, "AcM")
        End If


    End Sub
    Private Sub dtgrView_PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles dtgrView.PageIndexChanged
        dtgrView.CurrentPageIndex = e.NewPageIndex
        LoadMenu()
    End Sub
    Private Sub dtgrView_DeleteCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles dtgrView.DeleteCommand
        If (e.CommandName = "Delete") Then
            Dim iId As String = dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            DellImg(iId)

            If Right(iId, 4) = "0000" Then
                _strSql = "Delete From tblMenu where left(IDM,2) = '" & Left(iId, 2) & "'"
            ElseIf Right(iId, 2) = "00" Then
                _strSql = "Delete From tblMenu where left(IDM,4) = '" & Left(iId, 4) & "'"
            Else
                _strSql = "Delete From tblMenu where IDM = '" & iId & "'"
            End If
            _o.ExecuteSql(_strSql, False)

            Response.Write("<script>alert('Xoá thành công!');</script>")
            LoadMenu()
        End If
    End Sub

    Protected Function MenuCode() As String
        MenuCode = ""
        _strSql = "select Max(substring(IDM," & Request("lv") * 2 - 1 & ",2)) as ID from tblMenu where left(IDM," & Request("lv") * 2 - 2 & ")='" & Left(drlMenuID.Text, Request("lv") * 2 - 2) & "'"
        _o.DB_Connect(_strSql, 1)
        If (_o.objDataReader.Read()) Then
            Try
                MenuCode = Left(drlMenuID.Text, Request("lv") * 2 - 2) & IIf(_o.objDataReader("ID") >= 9, Val(_o.objDataReader("ID")) + 1, "0" & Val(_o.objDataReader("ID")) + 1) & Left(_iTid, Len(_iTid) - (2 * Request("lv")))
            Catch ex As Exception
                MenuCode = "01" & Left(_iTid, Len(_iTid) - (2))
            End Try
        End If
        _o.DB_Disconnect(1)
    End Function

    Private Sub DellImg(ByVal iId As String)
        _strSql = "select ImgM,ImgM2,ImgM3 from tblMenu where IDM='" & iId & "'"
        _o.DB_Connect(_strSql, 1)
        If _o.objDataReader.Read() Then
            If _o.objDataReader("ImgM") <> "null.gif" Then
                Try
                    File.Delete(Server.MapPath("/images/menu/" & _o.objDataReader("ImgM")))
                Catch ex As Exception
                End Try
            End If
            If _o.objDataReader("ImgM2") <> "null.gif" Then
                Try
                    File.Delete(Server.MapPath("/images/menu/" & _o.objDataReader("ImgM2")))
                Catch ex As Exception
                End Try
            End If
            If _o.objDataReader("ImgM3") <> "null.gif" Then
                Try
                    File.Delete(Server.MapPath("/images/menu/" & _o.objDataReader("ImgM3")))
                Catch ex As Exception
                End Try
            End If
        End If
        _o.DB_Disconnect(1)
    End Sub

    Private Function Chuyen(ByVal str As String) As String
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
            For i As Integer = 0 To 100
                If InStr(str, "<a ") > 0 Then
                    str = Replace(str, Mid(str, InStr(str, "<a "), InStr(Mid(str, InStr(str, "<a ")), ">")), "")
                Else
                    Exit For
                End If
                i += i
            Next
            RemuA = Replace(str, "</a>", "")
        End If
    End Function
End Class