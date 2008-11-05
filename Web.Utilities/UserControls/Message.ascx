<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Message.ascx.cs" Inherits="SpeakFriend.Web.Utilities.UserControls.Message" %>

<asp:Panel runat="server" ID="pnlUserMsg" class="user-message success" Visible="false" EnableViewState="false" >
    <h3>
        <asp:Image ID="imgSuccess" runat="server" src="/style/img/status-positiv.png" alt="Erfolg" Visible="false" />
        <asp:Image ID="imgError" runat="server" src="/style/img/status-negativ.png" alt="Fehler" Visible="false" />
        <asp:Literal ID="ltlTitle" runat="server" EnableViewState="false" />
    </h3>
   
    <p>
        <asp:Literal ID="ltlMsg" runat="server" EnableViewState="false" />
        <asp:PlaceHolder ID="plhMsgs" runat="server" EnableViewState="false" />
    </p>
</asp:Panel>

<ajaxToolkit:AnimationExtender ID="animSuccess" runat="server" TargetControlID="pnlUserMsg" Enabled="false" >
    <Animations>
        <OnLoad>
            <Sequence AnimationTarget="pnlUserMsg">
                <Color  Duration="2" fps="25" 
                    StartValue="#c9ec8e"
                    EndValue="#dddddd"
                    Property="style"
                    PropertyKey="backgroundColor"
                />
            </Sequence>
        </OnLoad>
    </Animations>
</ajaxToolkit:AnimationExtender>

<ajaxToolkit:AnimationExtender ID="animError" runat="server" TargetControlID="pnlUserMsg" Enabled="false" >
    <Animations>
        <OnLoad>
            <Sequence AnimationTarget="pnlUserMsg">
                <Color  Duration="2" fps="25" 
                    StartValue="#ffc197"
                    EndValue="#ffebce"
                    Property="style"
                    PropertyKey="backgroundColor"
                />
            </Sequence>
        </OnLoad>
    </Animations>
</ajaxToolkit:AnimationExtender>
