Public Class advertp
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


        strSQL = "select IDP as ID,NameP as Name from tblProject,tblMenu where MenuIDP = IDM and AcM = 1 and AcP = 1  order by TimeP DESC"
        o.BindDataDropList(strSQL, drlSubID)
        If Request("id") <> "" Then drlSubID.SelectedValue = Request("id")
        lblLink.Text = "Quản lý slider dự án:<b style=color:red;text-transform:uppercase>" & drlSubID.SelectedItem.Text & "</b>"
        Call LoadAdvert()
    End Sub
    Private Sub drlType1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlType1.SelectedIndexChanged
        dtgrView.CurrentPageIndex = 0
        Call LoadAdvert()
    End Sub
    Private Sub LoadAdvert()
        dtgrView.PageSize = 15

        strSQL = "select ID,Name,Title,Case When right(Img,3)='swf' Then '<script>flash(" & """" & "/images/media/duan/'+Img+'" & """" & ",50,100);</script>' Else '<img src=/images/media/duan/'+Img+' style=width:100px;height:50px; />' End Img,Ac,Sort from tblAdvertP where ProID=" & drlSubID.SelectedValue & " and Type = 1 order by Sort ASC"
        Call o.BindDataGrid(strSQL, "tblAdvertP", dtgrView)
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
            strSQL = "update tblAdvertP set Title=N'" & qtyTitle.Text & "',Name=N'" & IIf(qtyName.Text = "", "#", Replace(qtyName.Text, "http://", "")) & "',Sort='" & IIf(qtySort.Text = "", "00", qtySort.Text) & "',Ac=" & qtyAc.SelectedValue & " where ID=" & Val(dtgrView.DataKeys(i))
            Call o.ExecuteSql(strSQL, False)
        Next

        o.ShowMessage("The item has been updated successfully!!")
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
                strSQL = "delete tblAdvertP where ID=" & Val(dtgrView.DataKeys(i))
                Call o.ExecuteSql(strSQL, False)

                Try
                    System.IO.File.Delete(Server.MapPath("/images/media/duan/" & Val(dtgrView.DataKeys(i)) & ".jpg"))
                    System.IO.File.Delete(Server.MapPath("/images/media/duan/" & Val(dtgrView.DataKeys(i)) & ".gif"))
                    System.IO.File.Delete(Server.MapPath("/images/media/duan/" & Val(dtgrView.DataKeys(i)) & ".swf"))
                    System.IO.File.Delete(Server.MapPath("/images/media/duan/" & Val(dtgrView.DataKeys(i)) & ".png"))
                Catch Ex As Exception
                End Try
            End If
        Next
        If iChk = True Then
            o.ShowMessage("You must select one item before delete!")
            Exit Sub
        End If
        Call o.ShowMessage("The item has been deleted successfully!")
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

        Dim f As New ArrayList
        Dim v As New ArrayList

        f.Add("Title,0,2,100") : v.Add(Title.Text) 'Sắp xếp - Kiểu nvarchar có độ rộng là 10
        f.Add("Name,0,2,300") : v.Add(Replace(txtName.Text, "http://", "")) 'Link website - Kiểu nvarchar có độ rộng là 300
        f.Add("Sort,0,2,10") : v.Add(txtSort.Text) 'Sắp xếp - Kiểu nvarchar có độ rộng là 10

        f.Add("ContentA,0,2,1000") : v.Add(txtContent.Text) 'Sắp xếp - Kiểu nvarchar có độ rộng là 10

        f.Add("Type,0,1,4") : v.Add(1) 'Vùng Hiển thị - Kiểu Int có độ rộng là 4
        f.Add("ProID,0,1,4") : v.Add(drlSubID.SelectedValue) 'Vùng Hiển thị - Kiểu Int có độ rộng là 4

        f.Add("Ac,0,1,4") : v.Add(IIf(chkAc.Checked = True, 1, 0)) 'Trang thai tin - Kiểu Int có độ rộng là 4

        If lblID.Text <> 0 Then
            f.Add("ID,1,1,4") : v.Add(lblID.Text) 'Kiểu Int có độ rộng là 4(key)
            o.UpdateRecord("tblAdvertP", f, v, False)
        Else
            o.InsertRecord("tblAdvertP", f, v, False)
            strSQL = "select max(ID) as MaxID from tblAdvertP"
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then lblID.Text = o.objDataReader("MaxID")
            Call o.DB_Disconnect(1)
        End If



        Dim eFile As String = ""
        Bambu.oNet.UploadPic(FilePro, 5000000, 1920, 800, "/images/media/duan/" & lblID.Text, eFile)
        If eFile <> "" Then o.ExecuteSql("update tblAdvertP set Img='" & lblID.Text + eFile & "' where ID=" & lblID.Text, False)

        Response.Redirect("advertp.aspx?id=" & drlSubID.SelectedValue)
    End Sub

    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()

            strSQL = "select * from tblAdvertP where ID=" & CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "Edit Widgets"
                txtName.Text = o.objDataReader("Name")
                txtSort.Text = o.objDataReader("Sort")
                chkAc.Checked = o.objDataReader("Ac")
                Title.Text = o.objDataReader("Title")

                If o.objDataReader("Img") <> "null.gif" Then
                    lblImg.Visible = True
                    ibtnDelImgT1.Visible = True
                    lblImg.Text = IIf(Right(Trim(o.objDataReader("Img")), 3) = "swf", "<EMBED align='baseline' autostart='true' border='0' src='/images/media/duan/", "<img src='/images/media/duan/") & Trim(o.objDataReader("Img")) & "' style='max-width:300px;' />"
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
            System.IO.File.Delete(Server.MapPath("/images/media/duan/" & lblID.Text & ".jpg"))
            System.IO.File.Delete(Server.MapPath("/images/media/duan/" & lblID.Text & ".gif"))
            System.IO.File.Delete(Server.MapPath("/images/media/duan/" & lblID.Text & ".swf"))
            System.IO.File.Delete(Server.MapPath("/images/media/duan/" & lblID.Text & ".png"))
        Catch Ex As Exception
        End Try

        strSQL = "update tblAdvertP set Img='null.gif' where ID=" & lblID.Text
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
            strSQL = "Delete From tblAdvertP where ID = " & iID
            Call o.ExecuteSql(strSQL, False)

            Try
                System.IO.File.Delete(Server.MapPath("/images/media/duan/" & iID & ".jpg"))
                System.IO.File.Delete(Server.MapPath("/images/media/duan/" & iID & ".gif"))
                System.IO.File.Delete(Server.MapPath("/images/media/duan/" & iID & ".swf"))
                System.IO.File.Delete(Server.MapPath("/images/media/duan/" & iID & ".png"))
            Catch Ex As Exception
            End Try

            o.ShowMessage("The item has been deleted successfully!")
            Call BindData()
        End If
    End Sub

    Private Sub drlSubID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drlSubID.SelectedIndexChanged
        lblLink.Text = "Quản lý slider dự án:<b style=color:red;text-transform:uppercase>" & drlSubID.SelectedItem.Text & "</b>"
        Call LoadAdvert()
    End Sub
End Class