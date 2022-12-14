Imports System.Data.SqlClient

Partial Public Class user
    Inherits System.Web.UI.Page

    Dim o As New Bambu.oData
    Dim strSQL As String

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
        tblAdd.Visible = False
        lblLink.Text = "Quản lý thành viên"
        drlSubID.Text = IIf(Request("drlMenuID") <> "", Request("drlMenuID"), Request("drlSubID"))

        dtgrView.PageSize = 15
        strSQL = "select IDU as ID,Users as Name,Email,Mobile,TimeU,AcU from tblUser where IDU<>1 " & drlSubID.SelectedValue & " order by TimeU DESC"
        o.BindDataGrid(strSQL, "ID", dtgrView)
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd1.Click, btnAdd2.Click
        tblView.Visible = False
        tblAdd.Visible = True
        lblID.Text = 0

        lblLink.Text = "Thêm mới thành viên"
    End Sub
    Private Sub btnDel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel1.Click, btnDel2.Click
        Dim qty As CheckBox
        Dim i As Integer
        Dim iChk As Boolean = True
        For i = 0 To dtgrView.Items.Count - 1
            qty = CType(dtgrView.Items.Item(i).FindControl("inbox"), CheckBox)
            If qty.Checked = True Then
                iChk = False
                strSQL = "delete tblUser where ID=" & Val(dtgrView.DataKeys(i))
                o.ExecuteSql(strSQL, False)
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
        Dim qtyAc As DropDownList
        Dim i As Integer
        For i = 0 To dtgrView.Items.Count - 1
            qtyAc = CType(dtgrView.Items.Item(i).FindControl("drlAc"), DropDownList)
            strSQL = "update tblUser set AcU" & IIf(qtyAc.SelectedValue = True, 1, 0) & " where IDU=" & Val(dtgrView.DataKeys(i))
            o.ExecuteSql(strSQL, False)
        Next
        Response.Write("<script>alert('Cập nhật thành công!');</script>")
        Call BindData()
    End Sub
    Private Sub drlSubID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drlSubID.SelectedIndexChanged
        Call BindData()
    End Sub
    Private Sub btnAddU1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddU1.Click, btnAddU2.Click
        Dim f As New ArrayList
        Dim v As New ArrayList

        If lblID.Text = 0 Then
            strSQL = "Select Users from tblUser where Users='" & Bambu.oNet.KillChars(Trim(txtUser.Text)) & "' and IDU<>" & lblID.Text
            o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                Response.Write("<script>alert('Tài khoản này đã được sử dụng\n\nBạn vui lòng đăng ký với tên khác!');</script>")
                txtUser.Focus()
                Exit Sub
            End If
            o.DB_Disconnect(1)
        End If

        f.Add("Users,0,2,100") : v.Add(Trim(txtUser.Text)) 'Kiểu nvarchar có độ rộng là 100
        f.Add("Pass,0,2,100") : v.Add(Bambu.oNet.Encrypt(Trim(txtPass.Text))) 'Kiểu nvarchar có độ rộng là 100
        f.Add("Email,0,2,100") : v.Add(txtEmail.Text) 'Kiểu nvarchar có độ rộng là 100

        f.Add("Fullname,0,2,100") : v.Add(txtFullName.Text) 'Kiểu nvarchar có độ rộng là 100

        f.Add("Mobile,0,2,50") : v.Add(txtTel.Text) 'Kiểu nvarchar có độ rộng là 10

        f.Add("Type,0,1,4") : v.Add(IIf(chkType.Checked = True, 1, 0))
        f.Add("AcU,0,1,4") : v.Add(1)

        If lblID.Text <> 0 Then
            f.Add("IDU,1,1,4") : v.Add(lblID.Text) 'Kiểu Int có độ rộng là 4(key)
            o.UpdateRecord("tblUser", f, v, True)
        Else
            o.InsertRecord("tblUser", f, v, True)
        End If
        Response.Redirect("user.aspx")
    End Sub
    Private Sub dtgrView_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgrView.ItemCommand
        If (e.CommandName = "edit") Then
            tblView.Visible = False
            tblAdd.Visible = True
            lblID.Text = dtgrView.DataKeys(e.Item.ItemIndex).ToString()


            strSQL = "select * from tblUser where IDU=" & CInt(dtgrView.DataKeys(e.Item.ItemIndex).ToString())
            Call o.DB_Connect(strSQL, 1)
            If o.objDataReader.Read() Then
                lblLink.Text = "Sửa thành viên: " & o.objDataReader("Users")
                txtUser.Text = o.objDataReader("Users")
                txtPass.Text = Bambu.oNet.Decrypt(o.objDataReader("Pass"))
                txtEmail.Text = o.objDataReader("Email")

                txtFullName.Text = o.objDataReader("Fullname")
                txtTel.Text = Replace(o.objDataReader("Mobile"), "&nbsp;", "")


                chkType.Checked = o.objDataReader("Type")
                chkAc.Checked = o.objDataReader("AcU")


            End If
            o.DB_Disconnect(1)

        ElseIf (e.CommandName = "eTop") Then
            strSQL = "update tblUser set TimeU=getdate() where IDU = " & dtgrView.DataKeys(e.Item.ItemIndex).ToString()
            o.ExecuteSql(strSQL, False)
            Call BindData()
        End If
    End Sub
    Private Sub dtgrView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgrView.ItemDataBound
        Dim strIDcell As Integer = DataBinder.Eval(e.Item.DataItem, "ID")
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim editButton As LinkButton = e.Item.Cells(6).Controls(0)
            Dim deleteButton As LinkButton = e.Item.Cells(7).Controls(0)

            deleteButton.ToolTip = "Xoa user (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & DataBinder.Eval(e.Item.DataItem, "ID")
            editButton.ToolTip = "Update (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ID #:" & DataBinder.Eval(e.Item.DataItem, "ID")

            deleteButton.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete selected item (" & DataBinder.Eval(e.Item.DataItem, "Name") & ") ?')"

            e.Item.Cells(0).ToolTip = "Mã user: " & DataBinder.Eval(e.Item.DataItem, "ID")

            CType(e.Item.FindControl("drlAc"), DropDownList).Text = DataBinder.Eval(e.Item.DataItem, "AcU")
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
            strSQL = "Delete From tblUser where IDU = " & iID
            o.ExecuteSql(strSQL, False)

            Response.Write("<script>alert('The item has been deleted successfully!');</script>")
            Call BindData()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strSQL = "select IDU as ID,Users as Name,Email,AcU,TimeU from tblUser where IDU<>1 and Users like N'%" & txtUserS.Text & "%' order by TimeU DESC"
        o.BindDataGrid(strSQL, "ID", dtgrView)
    End Sub

End Class