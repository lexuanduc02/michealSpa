Public Partial Class gallery
    Inherits System.Web.UI.Page
    Dim o As New Bambu.oData
    Dim strSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Not Page.IsPostBack Then Call BindData()
    End Sub
    Protected Sub BindData()
        strSql = "select IDP as ID,NameP as Name from tblProject,tblMenu where MenuIDP = IDM and AcM = 1 and AcP = 1  order by TimeP DESC"
        o.BindDataDropList(strSql, drlSubID)
        If Request("id") <> "" Then drlSubID.SelectedValue = Request("id")
        lblLink.Text = "Quản lý thư viện ảnh:<b style=color:red;text-transform:uppercase>" & drlSubID.SelectedItem.Text & "</b>"
        Call LoadAdvert()
    End Sub
    Protected Sub LoadAdvert()
        dtgrView.PageSize = 15
        strSql = "select Name,ID,Case When right(Img,3)='swf' Then '<script>flash(" & """" & "/images/gallery/'+Img+'" & """" & ",50,100);</script>' Else '<img src=/images/gallery/'+Img+' style=width:70px;height:auto; />' End Img,Ac from tblGallery where ProID='" & drlSubID.SelectedValue & "' order by Time DESC"
        Call o.BindDataGrid(strSql, "tblAdvert", dtgrView)
    End Sub
    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd1.Click


        Dim i As Integer = 0

        Dim f As New ArrayList
        Dim v As New ArrayList

        If FileU.HasFiles Then

            For Each uploadedFile As HttpPostedFile In FileU.PostedFiles

                f.Clear()
                v.Clear()

                f.Add("ProID,0,2,6") : v.Add(Request("id"))
                f.Add("Name,0,2,200") : v.Add(IIf(txtName.Text = "", "Tiêu đề ảnh", txtName.Text))
                f.Add("Ac,0,1,4") : v.Add(1)

                o.InsertRecord("tblGallery", f, v, False)

                strSql = "select cast(coalesce(Max(ID),0) as int) as ID from tblGallery where ProID = '" & Val(Request("id")) & "' "
                o.DB_Connect(strSql, 1)
                If o.objDataReader.Read() Then lblIDImg.Text = o.objDataReader("ID")
                o.DB_Disconnect(1)

                o.UploadFileAllowMultiple(uploadedFile, "1_" & lblIDImg.Text, "/images/gallery", 9000000, "update tblGallery set Img1=", "where ID=" & lblIDImg.Text, 1920)
                Dim path As String = HttpContext.Current.Server.MapPath("/images/gallery/1_" & lblIDImg.Text + "." + uploadedFile.FileName.Split("."c)(1))
                o.UploadFileSmallAllowMultiple(path, lblIDImg.Text + "." + uploadedFile.FileName.Split("."c)(1), "/images/gallery/", "update tblGallery set Img=", "where ID=" & lblIDImg.Text, 350, 350)

                i = i + 1
            Next
        End If
        o.ShowMessage("Upload thành công " & i & " hình ảnh")

        LoadAdvert()

    End Sub

    Private Sub dtgrView_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.DeleteCommand
        If (e.CommandName = "Delete") Then
            Dim iID As Integer = CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            strSql = "Delete From tblGallery where ID = " & iID
            Call o.ExecuteSql(strSql, False)
            Try
                System.IO.File.Delete(Server.MapPath("/images/gallery/" & iID & ".jpg"))
                System.IO.File.Delete(Server.MapPath("/images/gallery/" & iID & ".gif"))
                System.IO.File.Delete(Server.MapPath("/images/gallery/" & iID & ".png"))

                System.IO.File.Delete(Server.MapPath("/images/gallery/1_" & iID & ".jpg"))
                System.IO.File.Delete(Server.MapPath("/images/gallery/1_" & iID & ".gif"))
                System.IO.File.Delete(Server.MapPath("/images/gallery/1_" & iID & ".png"))

            Catch Ex As Exception
            End Try
            o.ShowMessage("Xoá thành công!")
            Call LoadAdvert()
        End If
    End Sub

    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "eTop") Then
            strSql = "update tblGallery set Time=getdate() where ID=" & dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            Call o.ExecuteSql(strSql, False)
            Call LoadAdvert()
        End If
    End Sub

    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim iID As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim deleteButton As LinkButton = e.Item.Cells(5).Controls(0)

            deleteButton.ToolTip = "Xóa ảnh (" & DataBinder.Eval(e.Item.DataItem, "Img") & ") ID #:" & iID

            deleteButton.Attributes("onclick") = "javascript:return confirm('Ban chac chan xoa anh nay khong?')"

            e.Item.Cells(0).ToolTip = "Mã ảnh: " & iID

            CType(e.Item.FindControl("drlAc"), DropDownList).Text = DataBinder.Eval(e.Item.DataItem, "Ac")
        End If

        Dim pageindex As Integer = dtgrView.CurrentPageIndex + 1
    End Sub

    Private Sub drlSubID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlSubID.SelectedIndexChanged
        Response.Redirect("gallery.aspx?id=" & drlSubID.SelectedValue)
    End Sub

    Private Sub btnUpdate2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate2.Click, btnUpdate1.Click
        Dim qtyAc As DropDownList
        Dim qtyName As TextBox
        Dim i As Integer
        For i = 0 To dtgrView.Items.Count - 1
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            qtyName = CType(dtgrView.Items.Item(i).FindControl("Name"), TextBox)
            strSql = "update tblGallery set Ac=" & IIf(qtyAc.SelectedValue = True, 1, 0) & ", Name = N'" & IIf(qtyName.Text = "", "Title", qtyName.Text) & "' where ID=" & Val(dtgrView.DataKeys(i))
            Call o.ExecuteSql(strSql, False)
        Next
        o.ShowMessage("Cập nhật thành công!")
        Call LoadAdvert()
    End Sub

    Private Sub btnDel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel2.Click, btnDel1.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True
        For i = 0 To dtgrView.Items.Count - 1
            qty = CType(dtgrView.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                iChk = False
                strSql = "delete tblGallery where ID=" & Val(dtgrView.DataKeys(i))
                Call o.ExecuteSql(strSql, False)

                Try
                    System.IO.File.Delete(Server.MapPath("/images/gallery/" & Val(dtgrView.DataKeys(i)) & ".png"))
                    System.IO.File.Delete(Server.MapPath("/images/gallery/" & Val(dtgrView.DataKeys(i)) & ".jpg"))
                    System.IO.File.Delete(Server.MapPath("/images/gallery/" & Val(dtgrView.DataKeys(i)) & ".gif"))


                    System.IO.File.Delete(Server.MapPath("/images/gallery/1_" & Val(dtgrView.DataKeys(i)) & ".png"))
                    System.IO.File.Delete(Server.MapPath("/images/gallery/1_" & Val(dtgrView.DataKeys(i)) & ".jpg"))
                    System.IO.File.Delete(Server.MapPath("/images/gallery/1_" & Val(dtgrView.DataKeys(i)) & ".gif"))
                Catch Ex As Exception
                End Try
            End If
        Next
        If iChk = True Then
            o.ShowMessage("Ban phai chon truoc khi nhan Xoa!")
            Exit Sub
        End If
        Call o.ShowMessage("Xáo thành công!")
        Call LoadAdvert()
    End Sub

    Private Sub dtgrView_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgrView.PageIndexChanged
        dtgrView.CurrentPageIndex = e.NewPageIndex
        Call LoadAdvert()
    End Sub
End Class