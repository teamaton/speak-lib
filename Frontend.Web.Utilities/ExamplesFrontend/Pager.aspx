<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Pager.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmPager" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <ul class="pager-new clear">            
      <speakFriend:PagerControl runat="server" ID="PagerControl1">
        <PreviousPageTemplate>
            <li class="previous">
                <asp:LinkButton runat="server">Zurück</asp:LinkButton>
            </li>
        </PreviousPageTemplate>
        <DisabledPreviousPageTemplate>
            <li class="previous disabled">
                <a>Zurück</a>
            </li>
        </DisabledPreviousPageTemplate>
        <PageNumberTemplate>            
            <li class="number">
                <asp:LinkButton runat="server"><%# Container.PageNumber %></asp:LinkButton>
            </li>            
        </PageNumberTemplate> 
        <CurrentPageNumberTemplate>
            <li class="number current">
                <a><%# Container.PageNumber %></a>
            </li>
        </CurrentPageNumberTemplate> 
        <NextPageTemplate>
            <li class="next">
                <asp:LinkButton runat="server">Vor</asp:LinkButton>
            </li>
        </NextPageTemplate> 
        <DisabledNextPageTemplate>
            <li class="next disabled">
                <a>Vor</a>
            </li>
        </DisabledNextPageTemplate>
        <SpacerTemplate>
            <li class="gap">
                <a>...</a>
            </li>
        </SpacerTemplate>     
      </speakFriend:PagerControl>
    </ul>

</asp:Content>
