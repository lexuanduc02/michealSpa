<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<script src="/scripts/admin/jquery-1.9.1.min.js"></script>
<script src="../scripts/fancybox/jquery.fancybox.js" type="text/javascript"></script>
<link href="../scripts/fancybox/jquery.fancybox.css" rel="stylesheet" />
<link href="../../styles/admin.css" rel="stylesheet" />
<script type="text/javascript">
    $(function () {
        setInterval("resetSession()", 1000 * 30);
    });
    function resetSession() {
        $.get("/admin/services/resetSession.ashx", function (data) { });
    }
</script>
<script src="../scripts/admin/inbox.js"></script>
<table class="head" cellpadding="0" cellspacing="0">
    <tr>
        <td rowspan="2"><img src="icon/logo.png" style="height:auto; padding: 5px 0;" /></td>
        <td class="head1">
            <p style="margin:10px 0 5px 0;">Hello, <b>admin</b>&nbsp;|&nbsp;<a class="dtw" onclick="javascript: return confirm('Are you sure you want to logout?');" href="index.aspx?do=logout">Log out</a></p>
        </td>
    </tr>
    <tr>
        <td class="head2">
            <h1>Welcome to Control Panel</h1>
        </td>
    </tr>
</table>