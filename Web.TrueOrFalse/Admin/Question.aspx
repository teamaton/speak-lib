<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="Web.TrueOrFalse.Admin.WebForm1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<table>
    <tr>
        <td>Text</td>
        <td>Antwort</td>        
        <td>Rating</td>  
        <td></td>              
        <td></td>
    </tr>
    
    <asp:Repeater runat="server" ID="rptQuestions">
        <ItemTemplate>
            <tr>
                <td><asp:Label runat="server" ID="lblText"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblAnswer"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblRating"></asp:Label></td>
                <td><asp:LinkButton runat="server" ID="btnEdit" Text="edit"></asp:LinkButton></td>
                <td><asp:LinkButton runat="server" ID="btnDelete" Text="delete"></asp:LinkButton></td>                
            </tr>            
        </ItemTemplate>
    </asp:Repeater>
    
</table>

</asp:Content>
