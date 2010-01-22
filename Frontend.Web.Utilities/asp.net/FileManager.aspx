<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FileManager.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmFileManager" %>

<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="uc" TagName="FileManager"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:FileManager ID="FileManager1" runat="server"></uc:FileManager>
</asp:Content>