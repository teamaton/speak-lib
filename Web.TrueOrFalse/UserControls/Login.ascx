<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Web.TrueOrFalse.UserControls.Login" %>

<table>
    <tr>
        <td colspan="2"><asp:Label runat="server" ID="lblUserMessage"></asp:Label></td>
    </tr>
    <tr>
        <td>Email</td>
        <td><asp:TextBox runat="server" ID="txtEmail"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Passwort</td>
        <td><asp:TextBox runat="server" ID="txtPassword"></asp:TextBox></td>
    </tr>
    <tr>
        <td></td>
        <td><asp:Button runat="server" ID="btnLogin" Text="Anmelden" /></td>
    </tr>
</table>
