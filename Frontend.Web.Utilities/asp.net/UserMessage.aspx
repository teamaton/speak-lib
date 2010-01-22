<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="UserMessage.aspx.cs" Inherits="SpeakFriend.Web.Utilities.WebForm2" %>
<%@ Register Src="~/_userControls/NavAspNet.ascx" TagPrefix="uc" TagName="NavAspNet" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="sidebar">
    <div class="sidebar-box">
		<uc:NavAspNet ID="NavAspNet1" runat="server"  />
    </div>
</div>

<div class="content">
    <div class="wrapper">    
    
		<h1>UserMessage</h1>

		<p>
			....
		</p>
		
	</div>
</div>

</asp:Content>
