<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiUpload.ascx.cs" Inherits="Frontend.Web.MultiUpload" %>

<asp:Repeater runat="server" ID="rptUploadRows">
    <ItemTemplate>
        <tr runat="server" id="trUploadRow">
            <td class="upload-box">
                <a class="trash" runat="server" id="aRemoveRow">
                    <img src="/style/img/trash.gif" onmouseover="src='/style/img/trash-on.gif'"
                         onmouseout="src='/style/img/trash.gif'" title="Foto entfernen" alt="Foto entfernen"/>
                </a>
                <div class="iframe">
                    <iframe runat="server" id="imageUpload" src="/UserControls/Upload.aspx" frameborder="0"
                            width="240px" height="40px"
                            style="" /><%--border: 0 solid #e0f6ba; width:240px; height:40px;--%>
                </div>
            </td>
            <td class="last">
                <asp:TextBox class="img-description" ID="txDescription" runat="server" TextMode="MultiLine" />
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>

<tr runat="server" id="trAddRowRow">
    <td class="upload-more">
        <script type="text/javascript">
            var nextRowId = 1;
            var uploadRows = new Array (<%=GetUploadRowIds()%>);
            var iframes = new Array (<%=GetIframeIds()%>);
                        
            function AddUploadRow() {            
                $get(uploadRows[nextRowId]).style.display = '';
                nextRowId++;
                
                if(nextRowId >= uploadRows.length)
                    $get('<%=trAddRowRow.ClientID%>').style.display = 'none';                                   
            }
            
            function RemoveUploadRow(rowId) {
                var row = $get(uploadRows[rowId]);
                var iframe = $get(iframes[rowId])
                
                row.style.display = 'none';
                                                                              
                iframe.src = "../UserControls/Upload.aspx"
                            + "?oldId=" + rowId
                            + "&Id=" + uploadRows.length
                            + "&umId=<%=UploadManagerId%>";
                                
                row.parentNode.insertBefore(
                    row, $get(uploadRows[uploadRows.length-1]).nextSibling);
                    
                uploadRows.push(row.id);
                iframes.push(iframe);
                
                if(nextRowId < uploadRows.length)
                    $get('<%=trAddRowRow.ClientID%>').style.display = ''; 
            }
             
        </script>
        <a class="right" href="javascript:AddUploadRow()">Ein weiteres Foto auswählen...</a>
    </td>
    <td class="last"></td>
</tr>  