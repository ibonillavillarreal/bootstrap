<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="RecuperacionCartera.aspx.cs" Inherits="Acciona.Reportes.RecuperacionCartera" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="row">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Recuperacion de Cartera</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
            <div class="col-md-4">
                <div class="input-group" style="width: 80%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Colector:</span>
                    <asp:DropDownList ID="ddlColector" runat="server" CssClass="form-control required input-sm" Width="100%" required="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-2">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 60px">No Serie:</span>
                    <asp:TextBox ID="txtSerie" CssClass="form-control " runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="input-group" style="width: 80%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Fecha Inicio:</span>
                    <asp:TextBox ID="txtFechaInicio" CssClass="form-control " runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaInicio" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>


            <div class="col-md-4">
                <div class="input-group" style="width: 80%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Fecha Fin:</span>
                    <asp:TextBox ID="txtFechaFin" CssClass="form-control " runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaFin" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>

            <div class="col-md-2">
                <div class="input-group" style="width: 100%;">
                    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default " OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="container">
        <asp:Panel ID="pnlReporte" runat="server" Visible="false">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reportes\rldc\RecuperacionCartera.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
</asp:Content>
