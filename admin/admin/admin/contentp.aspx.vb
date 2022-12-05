Imports System.IO
Imports Bambu
Public Class menup
    Inherits Page

    Dim _strSql As String
    Dim _o As New oData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then BindData()
    End Sub

    Private Sub BindData()
        tblView.Visible = True
        _strSql = "select IDP as ID,NameP as Name from tblProject,tblMenu where MenuIDP = IDM and AcM = 1 and AcP = 1  order by TimeP DESC"
        _o.BindDataDropList(_strSql, drlSubID)
        If Request("id") <> "" Then drlSubID.SelectedValue = Request("id")
        lblLink.Text = "Quản lý chi tiết dự án:<b style=color:red;text-transform:uppercase>" & drlSubID.SelectedItem.Text & "</b>"
        LoadMenu()
    End Sub
    Private Sub drlSubID_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drlSubID.SelectedIndexChanged
        lblLink.Text = "Quản lý chi tiết dự án:<b style=color:red;text-transform:uppercase>" & drlSubID.SelectedItem.Text & "</b>"
        dtgrView.CurrentPageIndex = 0
        LoadMenu()
    End Sub
    Private Sub LoadMenu()
        dtgrView.PageSize = 50
        _strSql = "select NameM as Name,NameMH,AutoM as ID,SortM,AcM,ImgM,Case When TypeM='1' Then 'Img' Else 'Blank' End Type from tblMenuP where ProID = '" & drlSubID.SelectedValue & "' order by SortM ASC"
        _o.BindDataGrid(_strSql, "tblMenuP", dtgrView)
    End Sub

    Private Sub btnUpdate1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate1.Click, btnUpdate2.Click
        Dim qtySort As TextBox
        Dim qtyAc As DropDownList
        Dim i As Integer
        For i = 0 To dtgrView.Items.Count - 1
            qtySort = CType(dtgrView.Items.Item(i).FindControl("txtSort"), TextBox)
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            _strSql = "update tblMenuP set SortM='" & qtySort.Text & "',AcM=" & qtyAc.SelectedValue & " where AutoM=" & Val(dtgrView.DataKeys(i))
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
                _strSql = "delete tblMenuP where AutoM='" & dtgrView.DataKeys(i) & "'"
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
        _strSql = "select IDP as ID,NameP as Name from tblProject,tblMenu where MenuIDP = IDM and AcM = 1 and AcP = 1  order by TimeP DESC"
        _o.BindDataDropList(_strSql, drlMenuID)
        drlMenuID.Text = Request("drlSubID")

        Session("IsAuthorizeds") = True
        Session("CKFinder_Permission") = "Admin"
    End Sub
    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddU1.Click, btnAddU2.Click
        Dim f As New ArrayList
        Dim v As New ArrayList
        Dim iSub As String = ""

        txtName.Text = Chuyen(Trim(txtName.Text))
        Dim iName1 As String = oNet.VnToEn(txtName.Text, True)

        f.Add("ProID,0,1,4") : v.Add(drlMenuID.SelectedValue) 'Kiểu nvarchar có độ rộng là 100
        f.Add("NameM,0,2,100") : v.Add(txtName.Text) 'Kiểu nvarchar có độ rộng là 100
        f.Add("NameMH,0,2,200") : v.Add(txtNameH.Text) 'Kiểu nvarchar có độ rộng là 100
        f.Add("Name1M,0,2,100") : v.Add(oNet.VnToEn(txtName.Text, True)) 'Kiểu nvarchar có độ rộng là 100


        If txtDescs.Text <> "" Then
            txtDescs.Text = Replace(txtDescs.Text, "<h1", "<h2")
            txtDescs.Text = Replace(txtDescs.Text, "</h1>", "</h2>")
            txtDescs.Text = Replace(txtDescs.Text, " alt=" & """" & """", " alt=" & """" & txtName.Text & """")
        End If
        f.Add("DescM,0,3,16") : v.Add(txtDescs.Text) 'Kiểu ntext độ rộng là 16

        f.Add("ContentM,0,2,500") : v.Add(txtContent.Text) 'Kiểu nvarchar có độ rộng là 100

        f.Add("TypeM,0,1,4") : v.Add(Val(Request("rbType"))) 'Kiểu Int có độ rộng là 4
        f.Add("TypeD,0,1,4") : v.Add(Val(Request("rbType1"))) 'Kiểu Int có độ rộng là 4
        f.Add("SortM,0,2,10") : v.Add(IIf(txtSortM.Text = "", "00", Replace(txtSortM.Text, "'", ""))) 'Kiểu nvarchar có độ rộng là 10
        f.Add("AcM,0,1,4") : v.Add(drlAc.Text) 'Kiểu Int có độ rộng là 4

      

        If lblID.Text <> "" Then
            f.Add("AutoM,1,1,4") : v.Add(lblID.Text) 'Kiểu nvarchar có độ rộng là 6(key)
            _o.UpdateRecord("tblMenuP", f, v, False)
        Else
            _o.InsertRecord("tblMenuP", f, v, False)
        End If

        Dim iId As Integer = 0
        _strSql = "select AutoM from tblMenuP where AutoM='" & lblID.Text & "'"
        _o.DB_Connect(_strSql, 1)
        If _o.objDataReader.Read() Then iId = _o.objDataReader("AutoM")
        _o.DB_Disconnect(1)


        _o.UploadFile(FilePro, drlMenuID.SelectedValue & "_" & iName1 & "_" & iId, "/images/contentp", 5000000, "update tblMenuP set ImgM=", "where AutoM=" & lblID.Text, 1600)


        Response.Redirect("contentp.aspx?id=" & drlMenuID.SelectedValue)
    End Sub
    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            lblLink.Text = "Sửa chi tiết dự án"

            _strSql = "select IDP as ID,NameP as Name from tblProject,tblMenu where MenuIDP = IDM and AcM = 1 and AcP = 1 and Hot = 1 order by TimeP DESC"
            _o.BindDataDropList(_strSql, drlMenuID)
            drlMenuID.Text = Request("drlSubID")

            Session("IsAuthorizeds") = True
            Session("CKFinder_Permission") = "Admin"

            _strSql = "select * from tblMenuP where AutoM=" & dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            _o.DB_Connect(_strSql, 1)
            If _o.objDataReader.Read() Then
                drlSubID.Text = _o.objDataReader("ProID")
                txtName.Text = _o.objDataReader("NameM")
                Try
                    txtNameH.Text = _o.objDataReader("NameMH")
                Catch ex As Exception

                End Try

                txtSortM.Text = _o.objDataReader("SortM")
                drlAc.Text = _o.objDataReader("AcM")
                txtDescs.Text = _o.objDataReader("DescM")

                lblType.Text = _o.objDataReader("TypeM")
                lblType1.Text = _o.objDataReader("TypeD")

                If _o.objDataReader("ImgM") <> "null.gif" Then
                    imgP1.Visible = True
                    ibtnDelImgT1.Visible = True
                    imgP1.ImageUrl = "/images/contentp/" & _o.objDataReader("ImgM")
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

        _strSql = "update tblMenuP set ImgM='null.gif' where AutoM=" & lblID.Text
        _o.ExecuteSql(_strSql, False)

        imgP1.Visible = False
        ibtnDelImgT1.Visible = False
    End Sub
    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim iId As String = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim editButton As LinkButton = e.Item.Cells(7).Controls(0)
            Dim deleteButton As LinkButton = e.Item.Cells(8).Controls(0)

            deleteButton.Attributes("onclick") = "javascript:return confirm('Ban chac chan xoa nội dung (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") nay khong?')"

            deleteButton.ToolTip = "Xoa tin (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iId
            editButton.ToolTip = "Sua tin (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iId

            e.Item.Cells(2).ToolTip = "Mã chi tiết: " & iId

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

            _strSql = "Delete From tblMenuP where AutoM = '" & iId & "'"
            _o.ExecuteSql(_strSql, False)

            Response.Write("<script>alert('Xoá thành công!');</script>")
            LoadMenu()
        End If
    End Sub

    Private Sub DellImg(ByVal iId As String)
        _strSql = "select ImgM  from tblMenuP where AutoM='" & iId & "'"
        _o.DB_Connect(_strSql, 1)
        If _o.objDataReader.Read() Then
            If _o.objDataReader("ImgM") <> "null.gif" Then
                Try
                    File.Delete(Server.MapPath("/images/contentp/" & _o.objDataReader("ImgM")))
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
End Class