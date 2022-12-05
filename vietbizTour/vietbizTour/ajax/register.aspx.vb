Public Class register
    Inherits System.Web.UI.Page
    Dim o As New Bambu.oData
    Dim strSQL As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bambu.oData.sConnect = ConfigurationManager.ConnectionStrings("sConnection").ConnectionString
        If Request("email") = "" Then
            Response.Redirect("/")
        Else
            Try
                Dim f As New ArrayList
                Dim v As New ArrayList

                f.Add("NameC,0,2,100") : v.Add(Request("hoten").Trim()) 'Kiểu nvarchar có độ rộng là 200
                f.Add("EmailC,0,2,100") : v.Add(Request("email").Trim()) 'Kiểu nvarchar có độ rộng là 200
                f.Add("TelC,0,2,20") : v.Add(Request("dienthoai").Trim()) 'Kiểu nvarchar có độ rộng là 200
                f.Add("MobileC,0,2,20") : v.Add(Request("dienthoai").Trim()) 'Kiểu nvarchar có độ rộng là 200
                f.Add("ContentC,0,3,16") : v.Add(Request("noidung").Trim()) 'Nội dung chi tiết - Kiểu ntext độ rộng là 16
                f.Add("AcC,0,1,4") : v.Add(0) 'Trang thai tin - Kiểu Int có độ rộng là 4
                o.InsertRecord("tblContact", f, v, False)
            Catch ex As Exception
                HttpContext.Current.Response.Status = "301 moved permanently"
                Response.Redirect("/")
            Finally
                Dim iMs As String = "Dear Admin,<br><br>"
                iMs &= "<table align='center' border='0' cellspacing='0' cellpadding='0' style='line-height:100%; width:100%;font-family:arial;max-width:800px;font-size:14px;'><tr><td style='line-height:30px;'><h2 >THÔNG TIN KHÁCH HÀNG LIÊN HỆ</h2><p style='margin:0;'>(Thời gian <strong>" & Format(System.DateTime.Now, "H:mm - dd/MM/yyyy") & "</strong>)</p></td></tr><tr><td style='padding:10px 0; width:100%'><table cellspacing='0' cellpadding='0' style='line-height:24px;'><tr><td style='width:120px; padding-bottom:5px;'>Họ và tên :</td><td><strong style='font-size:16px;'>" & Request("hoten").Trim() & "</strong></td></tr><tr><td style='padding-bottom:5px;'>Điện thoại :</td><td>" & Request("dienthoai").Trim() & "</td></tr><tr><td style='padding-bottom:5px;'>E-mail :</td><td><a href='mailto:" & Request("email").Trim() & "' target='_blank'>" & Request("email").Trim() & "</a></td></tr><tr><td style='font-style:italic; text-decoration:underline;padding-bottom:5px;'>Nội dung :</td><td>" & Request("noidung").Trim() & "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr>"
                iMs &= "</table>"

                SendEmail(iMs, Request("email").Trim())
            End Try
        End If
    End Sub
    Protected Sub SendEmail(itext As String, imail As String)
        Dim tMs As String = "[TAPDOANDIAOC.NET] - Khách hàng gửi liên hệ từ website " & Request.Url.Host()
        Dim iType As Integer = 1
        Dim iEmail As String = ""
        Dim iPass As String = ""
        Dim iSmtp As String = ""
        Dim sEmail As String = ""
        strSQL = "select Email,Email1,Pass,ChkEmail,smtp from tblTotal where ID=1"
        Call o.DB_Connect(strSQL, 1)
        If o.objDataReader.Read() Then
            iType = o.objDataReader("ChkEmail")
            iEmail = o.objDataReader("Email")
            sEmail = o.objDataReader("Email1")
            iPass = o.objDataReader("Pass")
            iSmtp = o.objDataReader("smtp")
        End If
        Call o.DB_Disconnect(1)

        Try
            If iType = 1 Then
                Bambu.oMail.SendGmail(sEmail, iEmail, "", "", tMs, itext, "smtp.gmail.com", sEmail, Bambu.oNet.Decrypt(iPass), False, imail)
            Else
                Bambu.oMail.SendMailPop3(sEmail, iEmail, "", "", tMs, itext, iSmtp, sEmail, Bambu.oNet.Decrypt(iPass), False, imail)
            End If

        Catch ex As Exception

        End Try
       
        Response.Write("<script>alert('Yêu cầu của quý khách đã được gửi tới " & Request.Url.Host & "\nChúng tôi sẽ sớm phản hồi tới quý khách.\n\nChân thành cảm ơn!');window.open('/','_self');</script>")
    End Sub
End Class