<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="SpeakFriend.Web.Utilities.FileUploadExample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<speakFriend:FileUploadFrame runat="server" ContentUrl="UploadContent.aspx" />
</asp:Content>
