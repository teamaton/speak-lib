<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Basics.aspx.cs" Inherits="SpeakFriend.Web.Utilities.FileUploadExample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Speak-Friend FileUpload Walkthrough</h1>

<h2>Step 1: Basics</h2>

<p>The Speak-Friend FileUpload controls are highly customizable controls that allow users to upload
files asynchronously. Uploaded files are automatically stored in a temporary directory on the server
and are always deleted when they are not used any more, e.g. when the user's session expires.</p>
<p>Use the FileUpload below to upload a file. After uploading you can still delete it and choose another
file instead. When you are finished, click on the submit button</p>

<asp:MultiView runat="server" ID="mvUploadFileDemo" ActiveViewIndex="0">
    <asp:View runat="server" ID="vwUpload">
        <speakFriend:FileUploadFrame 
            runat="server" 
            ID="FileUploadFrame1" 
            ContentUrl="UploadContent/DefaultContent.aspx"            
        />
        <asp:Button runat="server" Id="btnSubmit" Text="Submit"/>
    </asp:View>
     <asp:View runat="server" ID="vwNoFile">
        You selected no file.
    </asp:View> 
    <asp:View runat="server" ID="vwSubmitted">
        You submitted the file <asp:Label runat="server" ID="lblFileName"></asp:Label>
        which is temporarily stored in <asp:Label runat="server" ID="lblTempPath"></asp:Label>
    </asp:View> 

</asp:MultiView>

<h3>How to use the FileUpload</h3>

<p>To integrate the FileUpload into your page, simply add a FileUploadFrame control:</p>
<pre>
&lt;speakFriend:FileUploadFrame
    runat=&quot;server&quot;
    ID=&quot;FileUploadFrame1&quot;
    ContentUrl=&quot;UploadContent/DefaultContent.aspx&quot;
/&gt;
</pre>

<p>The <code>ContentUrl</code> points to a second page that contains the actual content of the FileUpload.
That page is where you can customize the FileUpload. It is shown inside an iframe to make asynchronous 
upload possible. The content page used in this example contains the following code inside the body tag:</p>
<pre>
&lt;form id=&quot;uploadForm&quot; runat=&quot;server&quot;&gt;
    &lt;asp:ScriptManager ID=&quot;ScriptManager1&quot; runat=&quot;server&quot; /&gt;
    
    &lt;speakFriend:FileUploadContent ID=&quot;FileUploadContent1&quot; runat=&quot;server&quot;&gt;
    &lt;/speakFriend:FileUploadContent&gt; 
    
&lt;/form&gt;
</pre>

</asp:Content>
