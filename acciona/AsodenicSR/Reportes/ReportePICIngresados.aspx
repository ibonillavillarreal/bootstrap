<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="ReportePICIngresados.aspx.cs" Inherits="Acciona.Reportes.ReportePICIngresados" %>

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
                <label for="ddlNivelRiesgo">Estado:</label>
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                    <asp:ListItem Value="1">En Proceso</asp:ListItem>
                    <asp:ListItem Value="2">Finalizado</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label for="ddlPromotor">Promotor</label>
                <asp:DropDownList runat="server" ID="ddlPromotor" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label for="ddlPromotor">Detallado/Resumido</label>
                <asp:DropDownList runat="server" ID="ddlTipoReporte" CssClass="form-control">
                    <asp:ListItem Text="Detallado" Value="Detallado" />
                    <asp:ListItem Text="Resumido" Value="Resumido" />
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
    <asp:Panel ID="pnlReporte" runat="server" Visible="false">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        </rsweb:ReportViewer>
    </asp:Panel>
</asp:Content>