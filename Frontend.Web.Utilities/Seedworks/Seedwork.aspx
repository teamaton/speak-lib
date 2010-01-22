<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Seedwork.aspx.cs" Inherits="SpeakFriend.Web.Utilities.WebForm1" %>
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
    
		<h1>Seedwork?</h1>

		<h2>Summary</h2>
		<p style="font-style:italic">
		In opposite to a framework, a seedwork is meant to be duplicated and copied, in a manner of obsessive redundancy. It serves as "seed" 
		to build something new. A seedwork serves as starter and is meant to be change. 
		</p>

		<h2>Usage</h2>
		<h2>Break the API!</h2>
		<h2>Provide Feedback</h2>
		
	</div>
</div>





</asp:Content>
