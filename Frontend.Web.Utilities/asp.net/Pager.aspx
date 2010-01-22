<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Pager.aspx.cs" Inherits="SpeakFriend.Web.Utilities.frmPager" %>
<%@ Register Src="~/_userControls/NavAspNet.ascx" TagPrefix="uc" TagName="NavAspNet" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="sidebar">
	<div class="sidebar-box">
		<uc:NavAspNet ID="NavAspNet1" runat="server"  />
	</div>
</div>

<div class="content">
    <div class="wrapper">    
    
		<h1>Pager</h1>

		<p>
			....
		</p>
		
		<ul class="pager-new clear">            
      <speakFriend:PagerControl runat="server" ID="PagerControl1">
        <PreviousPageTemplate>
            <li class="previous">
                <asp:LinkButton ID="LinkButton1" runat="server">Zurück</asp:LinkButton>
            </li>
        </PreviousPageTemplate>
        <DisabledPreviousPageTemplate>
            <li class="previous disabled">
                <a>Zurück</a>
            </li>
        </DisabledPreviousPageTemplate>
        <PageNumberTemplate>            
            <li class="number">
                <asp:LinkButton ID="LinkButton2" runat="server"><%# Container.PageNumber %></asp:LinkButton>
            </li>            
        </PageNumberTemplate> 
        <CurrentPageNumberTemplate>
            <li class="number current">
                <a><%# Container.PageNumber %></a>
            </li>
        </CurrentPageNumberTemplate> 
        <NextPageTemplate>
            <li class="next">
                <asp:LinkButton ID="LinkButton3" runat="server">Vor</asp:LinkButton>
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
		
	</div>
</div>
      


</asp:Content>
