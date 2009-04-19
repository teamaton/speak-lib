<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileManager.ascx.cs" Inherits="SpeakFriend.Web.Utilities.UserControls.FileManager" %>

    <asp:Panel runat="server" ID="pnlSelectedItemDetails" CssClass="preview-panel form" Visible="false">
        <h3>Vorschau</h3>        
        <asp:Image runat="server" ID="imgPreview" CssClass="preview" AlternateText="Vorschau" />
        <p class="img-title"><asp:Literal runat="server" ID="ltName">Name</asp:Literal></p>
        <p><asp:HyperLink runat="server" id="hplEnlarge" CssClass="enlarge">Original anzeigen</asp:HyperLink></p>
        <h4 class="link clear">
<%--            <asp:HyperLink class="small" ID="hplMoreSettings" runat="server"><asp:Image runat="server" ID="imgExpand"/>Details <asp:Label runat="server" ID="lblText"/></asp:HyperLink>
        </h4>
        <asp:Panel ID="pnlMoreSettings" height="0" cssclass="collapsible-panel clear" runat="server">
            <asp:Label ID="Label3" AssociatedControlId="txtAltTag" runat="server">Alt Tag</asp:Label>
            <asp:TextBox runat="server" CssClass="alt-tag" ID="txtAltTag"></asp:TextBox>
            <p class="light">
                Wählen Sie einen möglichst deskriptiven Text, der angezeigt wird, sollte das von Ihnen hochgeladene Bild nicht verfügbar sein.
            </p>
        </asp:Panel>--%>
        
        <asp:LinkButton ID="btnSave" runat="server" class="primary button-small"><span>Speichern</span></asp:LinkButton>
        <asp:HyperLink ID="hplCancel" CssClass="secondary" runat="server">Abbrechen</asp:HyperLink>
    </asp:Panel>
    
    <div class="file-list">
        <div class="file-actions">
            <asp:LinkButton ID="btnSelectAll" CssClass="choose-all" runat="server">alle auswählen</asp:LinkButton>
            <asp:LinkButton ID="btnDelete" cssclass="delete" runat="server">Löschen</asp:LinkButton>
            <asp:LinkButton ID="btnList" cssclass="list" runat="server">Liste</asp:LinkButton>
            <asp:LinkButton ID="btnIcons" cssclass="tile" runat="server">Symbole</asp:LinkButton>
        </div>
        <div class="g-head">
            <div class="g-row clear">
                <span class="g-col one"><asp:CheckBox ID="cbTitle" runat="server" Enabled="false" /></span>
                <span class="g-col two"><asp:LinkButton ID="btnOrderByFileName" runat="server">Datei-Name</asp:LinkButton></span>
                <span class="g-col three"><asp:LinkButton ID="btnOrderBySize" runat="server">Größe</asp:LinkButton></span>
                <span class="g-col four"><asp:LinkButton ID="btnOrderByType" runat="server">Typ</asp:LinkButton></span>
                <span class="g-col five"><asp:LinkButton ID="btnOrderByDate" runat="server">Datum</asp:LinkButton></span>
            </div>
        </div>
        
        <div class="g-body">
            <asp:Repeater runat="server" ID="rptFiles">
                <ItemTemplate>
                    <asp:Panel runat="server" ID="pnlItem" CssClass="g-row clear"><%--current--%>
                        <span class="g-col one"><asp:CheckBox ID="cbSelectItem" runat="server" /></span>
                        <span class="g-col two"><asp:LinkButton ID="btnName" runat="server">Name</asp:LinkButton></span>
                        <span class="g-col three"><asp:LinkButton ID="btnSize" runat="server">Size</asp:LinkButton></span>
                        <span class="g-col four"><asp:LinkButton ID="btnType" runat="server">Type</asp:LinkButton></span>
                        <span class="g-col five"><asp:LinkButton ID="btnDate" runat="server">Date</asp:LinkButton></span>
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>            
        </div>
        <div class="g-footer">
            
        </div>
    </div>

<asp:Panel runat="server" ID="pnlEnlarge" style="display:none" CssClass="modal-pop-up large-image">
    <h2>Bildvorschau in Originalgröße</h2>
    <asp:Image runat="server" ID="imgOriginal" AlternateText="Originalbild" />
    <p><asp:HyperLink ID="hplClose" CssClass="close" runat="server">Schließen</asp:HyperLink></p>
</asp:Panel>

<%--<ajaxToolkit:CollapsiblePanelExtender runat="server" 
    TargetControlID="pnlMoreSettings"
    CollapsedSize="0"
    Collapsed="true"
    ExpandControlID="hplMoreSettings"
    CollapseControlID="hplMoreSettings"
    CollapsedText="einblenden..."
    ExpandedText="ausblenden..." 
    TextLabelID="lblText"
    ImageControlID="imgExpand"
    ExpandedImage="/style/img/collapse.png"
    CollapsedImage="/style/img/expand.png"
    AutoCollapse="False"
    AutoExpand="False"
    ExpandDirection="Vertical" /> --%>
    
<ajaxToolkit:ModalPopupExtender runat="server"
    TargetControlID="hplEnlarge"
    PopupControlID="pnlEnlarge" 
    BackgroundCssClass="modalBackground" 
    CancelControlID="hplClose" />