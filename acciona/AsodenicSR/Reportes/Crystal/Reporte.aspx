<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="Acciona.Reportes.Crystal.Reporte" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <div class="container form">
        <div class="row">
            <div class="col-md-5">
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control required span12" />
            </div>
            <div class="col-md-5">
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn submit" OnClick="btnBuscar_Click" />
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $("form").validateWebForm();
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="container">
        <asp:Panel ID="pnlReporte" runat="server" Visible="false">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reportes\rldc\Report1.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
</asp:Content>

