<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FileManager.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmFileManager" %>
<%@ Register Src="~/_UserControls/FileManager.ascx" TagPrefix="uc" TagName="FileManager"  %>
<%@ Register Src="~/_userControls/NavAspNet.ascx" TagPrefix="uc" TagName="NavAspNet" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="sidebar">
	<div class="sidebar-box">
		<uc:NavAspNet ID="NavAspNet1" runat="server"  />
	</div>
</div>
	
<div class="content">
    <div class="wrapper">    
    
		<h1>FileManager</h1>

		<p>
			....
		</p>

		<uc:FileManager ID="FileManager1" runat="server"></uc:FileManager>	    
		
	</div>
</div>

</asp:Content>