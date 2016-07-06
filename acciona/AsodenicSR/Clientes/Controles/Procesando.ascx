<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Procesando.ascx.cs" Inherits="Procesando" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress" Style="display: none;">
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div style="position: relative; top: 30%; text-align: center;">
                <%--<asp:Label ID="Label1" runat="server" Text="x" onclick="AbortPostBack()"></asp:Label>--%>
                <asp:Image ID="imgProcesando" runat="server" SkinID="imgProcesando" />
                <%--                <img src="../../imagenes/loading.gif" style="vertical-align: middle" alt="Processing" />--%>
                <asp:Label ID="lblProcesando" runat="server" Text="Procesando..."></asp:Label>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div style="position: relative; top: 30%; text-align: center;">
               <asp:Label ID="Label1" runat="server" Text="x" onclick="AbortPostBack()"></asp:Label>
                <asp:Image ID="imgProcesando" runat="server" SkinID="imgProcesando" />
                <%--                <img src="../../imagenes/loading.gif" style="vertical-align: middle" alt="Processing" />
                <asp:Label ID="lblProcesando" runat="server" Text="Procesando..."></asp:Label>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Panel>
<cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
    BackgroundCssClass="modalBackgroundProgress" PopupControlID="panelUpdateProgress" />
<div id="modalBackground" class="modalBackgroundProgress" style="position: fixed; left: 0px; top: 0px; z-index: 10000; display: none">
    <div id="progress" style="text-align: center; position: absolute; top: 50%; left: 50%; width: 200px; margin-left: -100px;">
        <asp:Image ID="imgProcesando1" runat="server" SkinID="imgProcesando" />
        <asp:Label ID="lblProcesando1" runat="server" Text="Procesando..."></asp:Label>
    </div>
</div>

<script type="text/javascript" >
    var ModalProgress = '<%= ModalProgress.ClientID %>';         
</script>
