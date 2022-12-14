Partial Public Class advert
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        ElseIf Val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            Response.Redirect("pro.aspx")
        End If
        'If Val(User.Identity.Name) <> 1 Then Response.Redirect("login.aspx")
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then Call BindData()
    End Sub

    Private Sub BindData()
        tblView.Visible = True
        lblLink.Text = "Manage Widgets"
        If Request("id") <> "" Then drlType1.Text = Request("id")

        Call LoadAdvert()
    End Sub
    Private Sub drlType1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlType1.SelectedIndexChanged
        dtgrView.CurrentPageIndex = 0
        Call LoadAdvert()
    End Sub
    Private Sub LoadAdvert()
        dtgrView.PageSize = 15
        strSQL = "select ID,Name,Title,Case When right(Img,3)='swf' Then '<script>flash(" & """" & "/images/media/'+Img+'" & """" & ",50,100);</script>' Else '<img src=/images/media/'+Img+' style=width:100px;height:50px; />' End Img,Ac,Sort from tblAdvert where Type=" & drlType1.SelectedValue & " order by Sort ASC"
        Call o.BindDataGrid(strSQL, "tblAdvert", dtgrView)
    End Sub

    Private Sub btnUpdate1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate1.Click, btnUpdate2.Click
        Dim qtyAc As DropDownList
        Dim qtyName As TextBox
        Dim qtyTitle As TextBox
        Dim qtySort As TextBox
        Dim i As Integer

        For i = 0 To dtgrView.Items.Count - 1
            qtyName = CType(dtgrView.Items.Item(i).FindControl("Name"), TextBox)
            qtyTitle = CType(dtgrView.Items.Item(i).FindControl("Title"), TextBox)
            qtySort = CType(dtgrView.Items.Item(i).FindControl("Sort"), TextBox)
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            strSQL = "update tblAdvert set Title=N'" & qtyTitle.Text & "',Name=N'" & IIf(qtyName.Text = "", "#", Replace(qtyName.Text, "http://", "")) & "',Sort='" & IIf(qtySort.Text = "", "00", qtySort.Text) & "',Ac=" & qtyAc.SelectedValue & " where ID=" & Val(dtgrView.DataKeys(i))
            Call o.ExecuteSql(strSQL, False)
        Next

        o.ShowMessage("Cập nhật thành công!!")
        Call LoadAdvert()
    End Sub
    Private Sub btnDel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel1.Click, btnDel2.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True
        For i = 0 To dtgrView.Items.Count - 1
            qty = CType(dtgrView.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                iChk = False
                strSQL = "delete tblAdvert where ID=" & Val(dtgrView.DataKeys(i))
                Call o.ExecuteSql(strSQL, False)

                Try
                    System.IO.File.Delete(Server.MapPath("/images/media/" & Val(dtgrView.DataKeys(i)) & ".jpg"))
                    System.IO.File.Delete(Server.MapPath("/images/media/" & Val(dtgrView.DataKeys(i)) & ".gif"))
                    System.IO.File.Delete(Server.MapPath("/images/media/" & Val(dtgrView.DataKeys(i)) & ".swf"))
                    System.IO.File.Delete(Server.MapPath("/images/media/" & Val(dtgrView.DataKeys(i)) & ".png"))
                Catch Ex As Exception
                End Try
            End If
        Next
        If iChk = True Then
            o.ShowMessage("Bạn phải chọn trước khi nhấn xóa!")
            Exit Sub
        End If
        Call o.ShowMessage("TXóa thành công!")
        Call LoadAdvert()
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd1.Click, btnAdd2.Click
        tblView.Visible = False
        tblAdd.Visible = True
        lblID.Text = 0

        drlType.Text = drlType1.SelectedValue
        lblLink.Text = "Add a new Widgets"
    End Sub
    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddU1.Click, btnAddU2.Click
        Dim iW As Integer = 0
        Dim iH As Integer = 0
        Dim f As New ArrayList
        Dim v As New ArrayList

        Select Case drlType.SelectedValue
            Case "5"
                iW = 1980
                iH = 1080
            Case "6"
                iW = 400
                iH = 250
            Case "7"
                iW = 600
                iH = 350
            Case "8"
                iW = 400
                iH = 250
        End Select

        f.Add("Title,0,2,100") : v.Add(Title.Text) 'Tiêu đề - Kiểu nvarchar có độ rộng là 100
        f.Add("Name,0,2,300") : v.Add(Replace(txtName.Text, "http://", "")) 'Link website - Kiểu nvarchar có độ rộng là 300
        f.Add("Sort,0,2,10") : v.Add(txtSort.Text) 'Sắp xếp - Kiểu nvarchar có độ rộng là 10

        f.Add("ContentA,0,2,1000") : v.Add(txtContent.Text) 'Sắp xếp - Kiểu nvarchar có độ rộng là 10

        f.Add("W,0,1,4") : v.Add(iW) 'Chiều rộng quy định - Kiểu Int có độ rộng là 4
        f.Add("H,0,1,4") : v.Add(iH) 'Chiều cao quy định - Kiểu Int có độ rộng là 4
        f.Add("Type,0,1,4") : v.Add(drlType.SelectedValue) 'Vùng Hiển thị - Kiểu Int có độ rộng là 4
        f.Add("Ac,0,1,4") : v.Add(IIf(chkAc.Checked = True, 1, 0)) 'Trang thai tin - Kiểu Int có độ rộng là 4

        If lblID.Text <> 0 Then
            f.Add("ID,1,1,4") : v.Add(lblID.Text) 'Kiểu Int có độ rộng là 4(key)
            o.UpdateRecord("tblAdvert", f, v, False)
        Else
            o.InsertRecord("tblAdvert", f, v, False)
            strSQL = "select max(ID) as MaxID from tblAdvert"
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then lblID.Text = o.objDataReader("MaxID")
            Call o.DB_Disconnect(1)
        End If

        Call o.UploadFile(FilePro, lblID.Text, "/images/media", 5000000, "update tblAdvert set Img=", "where ID=" & lblID.Text, 1600)

        'Dim eFile As String = ""
        'Bambu.oNet.UploadPic(FilePro, 5000000, iW, iH, "/images/media/" & lblID.Text, eFile)
        'If eFile <> "" Then o.ExecuteSql("update tblAdvert set Img='" & lblID.Text + eFile & "' where ID=" & lblID.Text, False)


        Response.Redirect("advert.aspx?id=" & drlType.SelectedValue)
    End Sub

    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()

            strSQL = "select * from tblAdvert where ID=" & CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "Edit Widgets"
                drlType.Text = o.objDataReader("Type")
                txtName.Text = o.objDataReader("Name")
                txtH.Text = o.objDataReader("H")
                txtSort.Text = o.objDataReader("Sort")
                chkAc.Checked = o.objDataReader("Ac")
                Title.Text = o.objDataReader("Title")

                If o.objDataReader("Img") <> "null.gif" Then
                    lblImg.Visible = True
                    ibtnDelImgT1.Visible = True
                    lblImg.Text = IIf(Right(Trim(o.objDataReader("Img")), 3) = "swf", "<EMBED align='baseline' autostart='true' border='0' src='/images/media/", "<img src='/images/media/") & Trim(o.objDataReader("Img")) & "' style='max-width:300px;' />"
                End If
                Try
                    txtContent.Text = o.objDataReader("ContentA")
                Catch ex As Exception

                End Try
            End If
            Call o.DB_Disconnect(1)
        End If
    End Sub
    Private Sub ibtnDelImgT1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDelImgT1.Click
        Try
            System.IO.File.Delete(Server.MapPath("/images/media/" & lblID.Text & ".jpg"))
            System.IO.File.Delete(Server.MapPath("/images/media/" & lblID.Text & ".gif"))
            System.IO.File.Delete(Server.MapPath("/images/media/" & lblID.Text & ".swf"))
            System.IO.File.Delete(Server.MapPath("/images/media/" & lblID.Text & ".png"))
        Catch Ex As Exception
        End Try

        strSQL = "update tblAdvert set Img='null.gif' where ID=" & lblID.Text
        Call o.ExecuteSql(strSQL, False)

        lblImg.Visible = False
        ibtnDelImgT1.Visible = False
    End Sub

    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim iID As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim editButton As LinkButton = e.Item.Cells(5).Controls(0)
            Dim deleteButton As LinkButton = e.Item.Cells(6).Controls(0)

            deleteButton.ToolTip = "Delete (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID
            editButton.ToolTip = "Update (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID

            deleteButton.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete selected item (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ?')"

            e.Item.Cells(0).ToolTip = "ID: " & iID

            CType(e.Item.FindControl("drlAc"), DropDownList).Text = DataBinder.Eval(e.Item.DataItem, "Ac")
        End If

        Dim pageindex As Integer = dtgrView.CurrentPageIndex + 1
    End Sub
    Private Sub dtgrView_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgrView.PageIndexChanged
        dtgrView.CurrentPageIndex = e.NewPageIndex
        Call BindData()
    End Sub
    Private Sub dtgrView_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.DeleteCommand
        If (e.CommandName = "Delete") Then
            Dim iID As Integer = CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            strSQL = "Delete From tblAdvert where ID = " & iID
            Call o.ExecuteSql(strSQL, False)

            Try
                System.IO.File.Delete(Server.MapPath("/images/media/" & iID & ".jpg"))
                System.IO.File.Delete(Server.MapPath("/images/media/" & iID & ".gif"))
                System.IO.File.Delete(Server.MapPath("/images/media/" & iID & ".swf"))
                System.IO.File.Delete(Server.MapPath("/images/media/" & iID & ".png"))
            Catch Ex As Exception
            End Try

            o.ShowMessage("The item has been deleted successfully!")
            Call BindData()
        End If
    End Sub
End Class