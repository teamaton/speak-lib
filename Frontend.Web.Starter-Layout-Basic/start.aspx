<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="start.aspx.cs" Inherits="Frontend.Web.Starter_Layout_Basic.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <title>Startseite</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="sidebar">
    <div class="sidebar-box">
        <h2>Ein Sidebar-Block</h2>
        <p>
            Hier können Texte rein, wenn keine Seitennavigation vonnöten ist.
        </p>    
    </div>
    <div class="sidebar-box">
        <h2>Ein Sidebar-Block</h2>
        <p>
            Hier können Texte rein, wenn keine Seitennavigation vonnöten ist.
        </p>    
    </div>
</div>

<div class="content">
    <div class="wrapper">
        <h1>Willkommen auf der Startseite des speak-friend Starters</h1>
        <p>Dies ist der Haupt-Content-Bereich.</p>
        <p>Hier kann man Texte, Bilder und sonstige Inhalte präsentieren.</p>
    </div>
</div>

</asp:Content>
