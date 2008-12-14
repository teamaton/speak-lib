<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadContent.aspx.cs" Inherits="SpeakFriend.Web.Utilities.ExamplesFrontend.UploadContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="uploadForm" runat="server">    
        <asp:ScriptManager runat="server" />
        
        <speakFriend:FileUploadContent runat="server">
        
        </speakFriend:FileUploadContent>
        
    </form>   
</body>
</html>
