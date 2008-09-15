<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="QuestionCreate.aspx.cs" Inherits="Web.TrueOrFalse.WebForm2" %>
<%@ Register Src="~/UserControls/Login.ascx" TagPrefix="uc" TagName="Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Frage erstellen</h1>

<p>Um Fragen erstellen zu dürfen, mußt Du angemeldet sein.</p>

<uc:Login runat="server" />


</asp:Content>
