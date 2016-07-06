<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contratos.aspx.cs" Inherits="Acciona.Credito.Contratos" ValidateRequest="false" %>

<!DOCTYPE html>
<script src="Scripts/nicedit/nicEdit.js" type="text/javascript"></script>
        <script type="text/javascript">
            bkLib.onDomLoaded(function () {
                new nicEditor({ fullPanel: true, iconsPath: 'Scripts/nicedit/nicEditorIcons.gif' }).panelInstance('docArea');
            });
        </script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table border="1">
            <tr>
                 <td>
                     <textarea cols="100" rows="20" id="docArea" runat="server"></textarea>
               </td>
            </tr>
            <tr>
                 <td>
                   <asp:Button ID="btnLoad" runat="server" Text="Load Doc" Width="70px" OnClick="btnLoad_Click" />
                         
                   <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                 </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
