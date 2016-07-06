<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="ReporteEvaluaciones.aspx.cs" Inherits="Acciona.Reportes.ReporteEvaluaciones" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <div class="container form">
        <div class="row">
            <div class="col-md-4">
                <label for="txtFechaInicio">Fecha de Inicio</label>
                <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="form-control required" />
                <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaInicio" Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="col-md-4">
                <label for="txtFechaFin">Fecha Fin</label>
                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="form-control required" />
                <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaFin" Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="col-md-4">
                <label for="ddlSucursal">Sucursal</label>
                <asp:DropDownList runat="server" ID="ddlSucursal" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <label for="ddlMetodologia">Metodologia</label>
                <asp:DropDownList runat="server" ID="ddlMetodogolia" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label for="ddlNivelRiesgo">Nivel de Riesgo</label>
                <asp:DropDownList runat="server" ID="ddlNivelRiesgo" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label for="ddlPromotor">Promotor</label>
                <asp:DropDownList runat="server" ID="ddlPromotor" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 pull-right">
                <%-- <asp:Button Text="Buscar" runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-default submit" />--%>
                <asp:Button Text="Generar Reporte" runat="server" ID="btnGenerar" OnClick="btnGenerar_Click" CssClass="btn btn-default submit" />
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
        <asp:Panel ID="pnlResultados" runat="server" CssClass="row">
            <asp:GridView ID="grvResultados" runat="server" AutoGenerateColumns="False" SkinID="sGridPrincipal">
                <Columns>
                    <asp:BoundField DataField="NoIdentificacion" HeaderText="No. Identificacion" SortExpression="NoIdentificacion" />
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" SortExpression="NombreCompleto" />
                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha" SortExpression="FechaRegistro" />
                    <asp:BoundField DataField="Nombre" HeaderText="Sucursal" SortExpression="Nombre" />
                    <asp:BoundField DataField="Promotor" HeaderText="Promotor" SortExpression="Promotor" />
                    <asp:BoundField DataField="Metodologia" HeaderText="Metodologia" SortExpression="Metodologia" />
                    <asp:BoundField DataField="NivelRiesgo" HeaderText="Nivel Riesgo" SortExpression="NivelRiesgo" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlReporte" runat="server" CssClass="row" Visible="false">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="span12" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reportes\rldc\Evaluaciones.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
</asp:Content>