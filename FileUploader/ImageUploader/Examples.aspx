<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Examples.aspx.cs" Inherits="ImageUploader.WebForm2" %>
<%@ Register Src="~/UserControlsExamples/ExampleStoredWithSubmitButton.ascx" TagPrefix="uc" TagName="Exampel1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
    <h1>Beispiel 1</h1>
    
    <uc:Exampel1 runat="server" ID="example1" />

</asp:Content>
