<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajax-img-upload.aspx.cs" Inherits="SpeakFriend.Web.Utilities.fwx.ajax_img_upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
		<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="file" />
			<script type="text/javascript">
				$(function () {
					$('input:file').change(function () {
						alert('Changed to: ' + $(this).val());
						$('<form action="upload" />').append($(this)).submit();
					});
				});
			</script>
    </div>
    </form>
</body>
</html>
