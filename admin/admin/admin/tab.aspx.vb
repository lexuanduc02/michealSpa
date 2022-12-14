Partial Public Class tab
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        ElseIf val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            Response.Redirect("pro.aspx")
        End If
        'If Val(User.Identity.Name) <> 1 Then Response.Redirect("login.aspx")
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then Call BindData()
    End Sub

    Private Sub BindData()
        lblLink.Text = "Manage Tabs"
        dtgrView.PageSize = 15
        strSQL = "select ID,Name,Ac,Sort,Tong from tblTab where Type = '" & drlType.SelectedValue & "' order by Sort ASC"
        Call o.BindDataGrid(strSQL, "tblTab", dtgrView)
    End Sub

    Private Sub btnDel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel1.Click, btnDel2.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True

        For i = 0 To dtgrView.Items.Count - 1
            qty = CType(dtgrView.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                iChk = False
                strSQL = "delete tblTab where ID=" & Val(dtgrView.DataKeys(i))
                Call o.ExecuteSql(strSQL, False)
            End If
        Next

        If iChk = True Then
            o.ShowMessage("You must select one item before delete!")
            Exit Sub
        End If

        Call o.ShowMessage("The item has been deleted successfully!")
        Call BindData()
    End Sub
    Private Sub btnUpdate1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate1.Click, btnUpdate2.Click
        Dim qtyName As TextBox
        Dim qtySort As TextBox
        Dim qtyAc As DropDownList
        Dim qtyTong As TextBox
        Dim i As Integer

        For i = 0 To dtgrView.Items.Count - 1
            qtyName = CType(dtgrView.Items.Item(i).FindControl("Name"), TextBox)
            qtySort = CType(dtgrView.Items.Item(i).FindControl("Sort"), TextBox)
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            qtyTong = CType(dtgrView.Items.Item(i).FindControl("Tong"), TextBox)

            Dim f As New ArrayList
            Dim v As New ArrayList

            f.Add("Tong,0,1,4") : v.Add(Val(qtyTong.Text)) 'Trang thai - Kiểu Int có độ rộng là 4
            f.Add("Name,0,2,50") : v.Add(IIf(qtyName.Text = "", "-", qtyName.Text)) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
            f.Add("Name1,0,2,50") : v.Add(Bambu.oNet.VNtoEn(qtyName.Text, True)) 'Kiểu nvarchar có độ rộng là 200
            f.Add("Sort,0,2,10") : v.Add(IIf(qtySort.Text = "", "00", qtySort.Text)) 'Sắp xếp - Kiểu nvarchar có độ rộng là 10
            f.Add("Ac,0,1,4") : v.Add(qtyAc.SelectedValue) 'Trang thai - Kiểu Int có độ rộng là 4

            f.Add("ID,1,1,4") : v.Add(Val(dtgrView.DataKeys(i))) 'Kiểu Int có độ rộng là 4(key)
            o.UpdateRecord("tblTab", f, v, False)
        Next

        o.ShowMessage("Cập nhật thành công!")
        Call BindData()
    End Sub

    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim iID As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim deleteButton As LinkButton = e.Item.Cells(5).Controls(0)

            deleteButton.ToolTip = "Delete (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & iID

            deleteButton.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete selected item (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ?')"

            e.Item.Cells(0).ToolTip = "ID: " & iID

            CType(e.Item.FindControl("drlAc"), DropDownList).Text = IIf(DataBinder.Eval(e.Item.DataItem, "Ac") = True, 1, 0)
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
            strSQL = "Delete From tblTab where ID=" & iID
            Call o.ExecuteSql(strSQL, False)

            Call o.ShowMessage("The item has been deleted successfully!")
            Call BindData()
        End If
    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Dim f As New ArrayList
        Dim v As New ArrayList

        f.Add("Tong,0,1,4") : v.Add(Val(Tong.Text)) 'Trang thai - Kiểu Int có độ rộng là 4
        f.Add("Name,0,2,50") : v.Add(IIf(txtName.Text = "", "-", txtName.Text)) 'Tiêu đề - Kiểu nvarchar có độ rộng là 200
        f.Add("Name1,0,2,50") : v.Add(Bambu.oNet.VNtoEn(txtName.Text, True)) 'Kiểu nvarchar có độ rộng là 200
        f.Add("Sort,0,2,10") : v.Add(IIf(txtSort.Text = "", "00", txtSort.Text)) 'Sắp xếp - Kiểu nvarchar có độ rộng là 10
        f.Add("Ac,0,1,4") : v.Add(drlAc.SelectedValue) 'Trang thai - Kiểu Int có độ rộng là 4
        f.Add("Type,0,1,4") : v.Add(drlType.SelectedValue) 'Trang thai - Kiểu Int có độ rộng là 4

        o.InsertRecord("tblTab", f, v, True)

        Call BindData()
    End Sub

    Private Sub drlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlType.SelectedIndexChanged
        BindData()
    End Sub
End Class