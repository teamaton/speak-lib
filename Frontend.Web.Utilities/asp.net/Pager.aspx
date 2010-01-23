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
			In Verbindung mit IPager.
		</p>
		
		<h2>Styles/Skins</h2>
		<p>Wir sollten vielleicht verschiedene Styles/Skins anbieten..</p>
		
		<pre class="brush: xml;">
		
&lt;speakFriend:PagerControl&nbsp;runat=&quot;server&quot;&nbsp;ID=&quot;PagerControl1&quot;&gt;
	&lt;PreviousPageTemplate&gt;
		&lt;li&nbsp;class=&quot;previous&quot;&gt;
			&lt;asp:LinkButton&nbsp;ID=&quot;LinkButton1&quot;&nbsp;runat=&quot;server&quot;&gt;Zur&uuml;ck&lt;/asp:LinkButton&gt;
		&lt;/li&gt;
	&lt;/PreviousPageTemplate&gt;
	&lt;DisabledPreviousPageTemplate&gt;
		&lt;li&nbsp;class=&quot;previous&nbsp;disabled&quot;&gt;
			&lt;a&gt;Zur&uuml;ck&lt;/a&gt;
		&lt;/li&gt;
	&lt;/DisabledPreviousPageTemplate&gt;
	&lt;PageNumberTemplate&gt;
		&lt;li&nbsp;class=&quot;number&quot;&gt;
			&lt;asp:LinkButton&nbsp;ID=&quot;LinkButton2&quot;&nbsp;runat=&quot;server&quot;&gt;&lt;%#&nbsp;Container.PageNumber&nbsp;%&gt;&lt;/asp:LinkButton&gt;
		&lt;/li&gt;
	&lt;/PageNumberTemplate&gt;&nbsp;
	&lt;CurrentPageNumberTemplate&gt;
		&lt;li&nbsp;class=&quot;number&nbsp;current&quot;&gt;
			&lt;a&gt;&lt;%#&nbsp;Container.PageNumber&nbsp;%&gt;&lt;/a&gt;
		&lt;/li&gt;
	&lt;/CurrentPageNumberTemplate&gt;&nbsp;
	&lt;NextPageTemplate&gt;
		&lt;li&nbsp;class=&quot;next&quot;&gt;
			&lt;asp:LinkButton&nbsp;ID=&quot;LinkButton3&quot;&nbsp;runat=&quot;server&quot;&gt;Vor&lt;/asp:LinkButton&gt;
		&lt;/li&gt;
	&lt;/NextPageTemplate&gt;&nbsp;
	&lt;DisabledNextPageTemplate&gt;
		&lt;li&nbsp;class=&quot;next&nbsp;disabled&quot;&gt;
			&lt;a&gt;Vor&lt;/a&gt;
		&lt;/li&gt;
	&lt;/DisabledNextPageTemplate&gt;
	&lt;SpacerTemplate&gt;
		&lt;li&nbsp;class=&quot;gap&quot;&gt;
			&lt;a&gt;...&lt;/a&gt;
		&lt;/li&gt;
	&lt;/SpacerTemplate&gt;
&lt;/speakFriend:PagerControl&gt;
		
		</pre>
		
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
