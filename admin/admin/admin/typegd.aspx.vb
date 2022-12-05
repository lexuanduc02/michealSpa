Public Class TypeGd
    Inherits System.Web.UI.Page

    Dim strSQL As String
    Dim o As New Bambu.oData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("__token__ad") Is Nothing Then
            Response.Redirect("login.aspx")
        ElseIf val(Bambu.oNet.Decrypt(Request.Cookies("__token__ad").Value)) <> 1 Then
            Response.Redirect("pro.aspx")
        End If
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ToString()
        If Not Page.IsPostBack Then
            Call BindData()
        End If
    End Sub

    Private Sub BindData()
        tblView.Visible = True
        lblLink.Text = "Directions"

        dtgrView.PageSize = 300
        strSQL = "select * from tblTypeGd  order by DisplayOrder ASC"
        Call o.BindDataGrid(strSQL, "ID", dtgrView)
    End Sub
    Private Sub drlSubID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlSubID.SelectedIndexChanged
        Call BindData()
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd1.Click, btnAdd2.Click
        tblView.Visible = False
        tblAdd.Visible = True
        lblID.Text = 0

        lblLink.Text = "Add a new Direction"
    End Sub
    Private Sub btnDel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel1.Click, btnDel2.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True
        For i = 0 To dtgrView.Items.Count - 1
            qty = CType(dtgrView.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                iChk = False
                strSQL = "delete tblTypeGd where ID=" & Val(dtgrView.DataKeys(i))
                Call o.ExecuteSql(strSQL, False)
            End If
        Next
        If iChk = True Then
            Response.Write("<script>alert('You must select one item before delete!');</script>")
            Exit Sub
        End If
        Response.Write("<script>alert('The item has been deleted successfully!');</script>")
        Call BindData()
    End Sub
    Private Sub btnUpdate1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate1.Click, btnUpdate2.Click
        Dim qtyName As TextBox
        Dim qtyPo As TextBox
        Dim i As Integer
        For i = 0 To dtgrView.Items.Count - 1
            qtyName = CType(dtgrView.Items.Item(i).FindControl("Name"), TextBox)
            qtyPo = CType(dtgrView.Items.Item(i).FindControl("Po"), TextBox)


            strSQL = "update tblTypeGd set Name=N'" & IIf(qtyName.Text = "", "-", qtyName.Text) & "',DisplayOrder='" & IIf(qtyPo.Text = "", "00", qtyPo.Text) & "' where ID=" & Val(dtgrView.DataKeys(i))
            Call o.ExecuteSql(strSQL, False)
        Next
        Response.Write("<script>alert('The item has been updated successfully!');</script>")
        Call BindData()
    End Sub
    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddU1.Click, btnAddU2.Click

        Dim f As New ArrayList
        Dim v As New ArrayList

        f.Add("Name,0,2,100") : v.Add(txtName.Text)
        f.Add("Name1,0,2,100") : v.Add(Bambu.oNet.VnToEn(txtName.Text, True))
        f.Add("DisplayOrder,0,2,10") : v.Add(txtPo.Text)
        f.Add("Published,0,0,1") : v.Add(chkAc.Checked)

        If lblID.Text <> 0 Then
            f.Add("ID,1,1,4") : v.Add(lblID.Text)
            o.UpdateRecord("tblTypeGd", f, v, False)
        Else
            o.InsertRecord("tblTypeGd", f, v, False)
        End If
        Response.Redirect("typegd.aspx")
    End Sub

    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()

            strSQL = "select * from tblTypeGd where ID=" & CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "Edit item: " & o.objDataReader("Name")
                txtName.Text = o.objDataReader("Name")
                txtPo.Text = o.objDataReader("DisplayOrder")
                chkAc.Checked = o.objDataReader("Published")
            End If
            Call o.DB_Disconnect(1)
        End If
    End Sub
    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim strIDcell As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim editButton As LinkButton = e.Item.Cells(4).Controls(0)
            Dim deleteButton As LinkButton = e.Item.Cells(5).Controls(0)

            deleteButton.ToolTip = "Delete (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & DataBinder.Eval(e.Item.DataItem, "ID")
            editButton.ToolTip = "Edit (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & DataBinder.Eval(e.Item.DataItem, "ID")

            deleteButton.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete selected item (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ?')"


            e.Item.Cells(0).ToolTip = "ID: " & DataBinder.Eval(e.Item.DataItem, "ID")
            e.Item.Cells(1).ToolTip = "View item: " & DataBinder.Eval(e.Item.DataItem, "Name")

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
            strSQL = "Delete From tblTypeGd where ID = " & iID
            Call o.ExecuteSql(strSQL, False)

            Response.Write("<script>alert('The item has been deleted successfully!');</script>")
            Call BindData()
        End If
    End Sub
End Class