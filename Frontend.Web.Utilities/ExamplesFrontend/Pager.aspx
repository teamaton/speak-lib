<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Pager.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmPager" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Literal runat="server" ID="ltPage">Test</asp:Literal>
    
    <ul class="pager-new clear">            
      <speakFriend:PagerControl runat="server" ID="PagerControl1">
        <PreviousPageTemplate>
            <li class="previous">
                <asp:LinkButton runat="server">Zurück</asp:LinkButton>
            </li>
        </PreviousPageTemplate>
        <PageNumberTemplate>            
            <li class="number current">
                <asp:LinkButton runat="server"><%# Container.PageNumber %></asp:LinkButton>
            </li>            
        </PageNumberTemplate>        
      </speakFriend:PagerControl>
    </ul>

</asp:Content>
