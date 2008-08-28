<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Frontend.Web.ImageUpload" %>
<%@ Import Namespace="SpeakFriend.FileUploader"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ImageUpload</title>
    <link rel="stylesheet" href="/style/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/style/img-upload.css" type="text/css" media="screen" />
    
</head>
<body><%--style="border: 0 solid #e0f6ba;">--%>
    <form id="uploadForm" runat="server"><%-- style="border: 0 solid #e0f6ba;">--%>
        <asp:ScriptManager runat="server" ID="scrMgrUpld" />
        <asp:Panel runat="server" ID="panFileInput">       
            <input runat="server" class="file-input" id="fileInput" type="file" onchange="uploadImage()" size="15"
                   accept="image/gif,image/png,image/jpeg"/>
        </asp:Panel>
        <asp:Panel runat="server" ID="panLoading" style="display:none;">
            <img src="/style/img/ajax-loader.gif" alt=""/>
            Bild wird hochgeladen...
        </asp:Panel>
        <asp:Panel runat="server" ID="panUploadedImage" Visible="false">                     
            <asp:Image runat="server" ID="imgUploadedImage" CssClass="uploaded-image" />
            <span class="image-title">
                <asp:Label runat="server" ID="lblFileName" />
                <img src="/style/img/check.gif" alt="Foto geladen"/>
            </span>
        </asp:Panel>
        <asp:Label runat="server" ID="lblMsg" >Die Datei muss kleiner sein als <%= string.Format("{0:0.#} MB", UploadSettings.MaxUploadSizeInMB)%>.</asp:Label>
        
        <script type="text/javascript">
            function uploadImage() {
                if (!validateFileExt()) return;
                $get('<%=panFileInput.ClientID%>').style.display = 'none';
                $get('<%=lblMsg.ClientID%>').style.display = 'none';
                $get('<%=panLoading.ClientID%>').style.display = '';
                setTimeout('document.uploadForm.submit()', 0);
            }
            function validateFileExt() {
                var filename = $get('fileInput').value;
                if (!filename) return false;
                var parts = filename.split('.');
                if (!parts || parts.length < 2) return false;
                var ext = parts[parts.length - 1].toLowerCase();
                if (ext == 'jpg' || ext == 'gif' || ext == 'png')
                    return true;
                $get('<%= lblMsg.ClientID %>').innerHTML = 'Bilder nur vom Typ JPG, PNG oder GIF!';
                $get('<%= lblMsg.ClientID %>').className = 'ifr-type'
                return false;
            }
        </script>
    </form>
</body>
</html>
