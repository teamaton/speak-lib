<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
	CodeBehind="SeoLinkButton.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmSeoLinkButton" %>
	
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>SeoLinkButton Demonstration</h1>

<div id="divMsg" style="border: solid 1px red;">
	JavaScript is currently disabled.
</div>

<h2>Hidden text below (click button to show)</h2>
<div style="border: solid 1px cyan; height:50px;">
	<asp:Label runat="server" ID="lblHidden" Visible="false" Font-Size="Large" >This is the hidden text.</asp:Label>
</div>
<asp:Button runat="server" ID="btnHideText" Text="Hide text" OnClick="btnHide_OnClick" />

<h2>Regular LinkButton</h2>
<asp:LinkButton runat="server" ID="LinkButton1" OnClick="OnClick" Text="Show hidden text"
	OnClientClick="alertPostback()" />

<h2>Search engine optimized SeoLinkButton</h2>
<h3>--- Works the same as the link button when JavaScript is enabled. Disable JavaScript to see the difference!---</h3>
<speakFriend:SeoLinkButton runat="server" ID="SeoLinkButton1" OnClick="OnClick" Text="Show hidden text"
	OnClientClick="alertPostback()" />

<script type="text/javascript">
	function alertPostback() {
		alert('Doing postback');
	}
	function hideDivMsg(s, a) {
		$get('divMsg').style.display = "none";
	}
	hideDivMsg();
</script>
</asp:Content>
