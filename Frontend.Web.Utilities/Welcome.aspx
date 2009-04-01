<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmWelcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Willkommen bei Speak-Lib</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content">
    <div class="wrapper">
    <h1>Speak-Lib</h1>
        
        
        <h2>Examples</h2>
        
        <h3>Frontend</h3>
       
        <ul>
            <li><a href="/ExamplesFrontend/Pager.aspx">Pager</a></li>          
            <li><a href="/ExamplesFrontend/FileUpload/Basics.aspx">FileUpload</a></li>            
            <li><a href="/ExamplesFrontend/UserMessage.aspx">User-Message</a></li>
            <li><a href="/ExamplesFrontend/Validation.aspx">Validation</a></li>
            <li><a href="/ExamplesFrontend/Navigation.aspx">Navigation</a></li>  
        </ul>
        
        <h3>Seed-Works</h3>        
        <ul>
            <li><a href="/ExamplesSetting/Settings.aspx">Settings</a></li>
            <li><a href="/ExamplesSetting/Settings.aspx">Tagging</a></li>            
        </ul>
        
        <h3>Web</h3>
        <ul>        
            <li>
              <a href="#">Context</a>
              <ul>
                <li>Session</li>
                <li>Cache</li>
                <li>Application</li>                
                <li>Context</li>
              </ul>
            </li>
            <li><a href="#">Control-Utils</a></li>            
        </ul>               
        
        <h3>Persistence</h3>
        <ul>        
            <li><a href="#">Query</a></li>        
            <li><a href="#">Setups</a></li>
        </ul>         
    </div>
</div>

</asp:Content>
