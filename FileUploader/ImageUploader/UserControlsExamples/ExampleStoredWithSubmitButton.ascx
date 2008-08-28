<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExampleStoredWithSubmitButton.ascx.cs" Inherits="ImageUploader.ExampleStoredWithSubmitButton" %>
<%@ Register Src="~/UserControls/MultiUpload.ascx" TagPrefix="uc" TagName="MultiUpload" %>

<p>
    The images will be stored only 
</p>

<asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
<div>

<table class="form upload">
    <tr>
        <th>
            <h4 class="pre-help">Foto Auswählen</h4>
         </th>
        <th>
            <h4 class="pre-help">Beschreibung</h4>
        </th>
    </tr>
    <uc:MultiUpload runat="server" ID="ucMultiUpload" UploadManagerId="mgrTabImg" />    
</table>    

<asp:Button runat="server" ID="btnSave" />
<asp:Button runat=server ID="foo" />