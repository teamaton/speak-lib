<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="SpeakFriend.Web.Utilities.WebForm3" %>
<%@ Register Src="~/_userControls/NavAspNet.ascx" TagPrefix="uc" TagName="NavAspNet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="sidebar">
    <div class="sidebar-box">
		<uc:NavAspNet runat="server"  />
    </div>
</div>

<div class="content">
    <div class="wrapper">    
    
	    <h1>ASP.NET</h1>
	    
	    <p>
			....
	    </p>
        	
</div>


</asp:Content>
