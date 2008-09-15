<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="QuestionEdit.aspx.cs" Inherits="Web.TrueOrFalse.Admin.WebForm2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<h1>Frage berabeiten/erstellen</h1>

<table>
    <tr>
        <td>Frage</td>
        <td>
            <asp:TextBox runat="server" ID="txtQuestion" 
                Style="width:290px; height:70px"
                TextMode="MultiLine"></asp:TextBox>
        </td>        
    </tr>
    <tr>
        <td>Antwort</td>
        <td>
            <asp:TextBox runat="server" ID="txtAnswer"></asp:TextBox>
        </td>        
    </tr>
    <tr>
        <td></td>
        <td><asp:Button runat="server" ID="btnSave" Text="Erstellen" /></td>        
    </tr>
</table>


</asp:Content>
