<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Usefulness.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmUsefulness" %>
<%@ Register Src="~/_userControls/NavSeedworks.ascx" TagPrefix="uc" TagName="NavSeedwork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="sidebar">
	<div class="sidebar-box">
		<uc:NavSeedwork ID="NavAspNet1" runat="server"  />
	</div>
</div>
	
<div class="content">
    <div class="wrapper">    
    
		<h1>Usefulness</h1>

		<p>
			....
		</p>
		
	</div>
</div>

</asp:Content>
