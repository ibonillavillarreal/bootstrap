<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="EditarRecibos.aspx.cs" Inherits="Acciona.Administracion.EditarRecibos" ResponseEncoding="ISO-8859-1"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="row">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Editar Recibos</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->


        <div class="form-group">
            <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                <div class="col-md-4">
                    <div class="input-group" style="width: 100%;">
                        <span class="input-group-addon input-sm" style="width: 70px">Serie:</span>
                        <asp:TextBox ID="txtSerieb" CssClass="form-control " runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="input-group" style="width: 100%;">
                        <span class="input-group-addon input-sm" style="width: 70px">NoRecibo:</span>
                        <asp:TextBox ID="txtRecibob" CssClass="form-control " runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="input-group" style="width: 100%;">
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default " OnClick="btnBuscar_Click" />
                    </div>

                </div>

            </div>

        </div>


    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <div role="navigation" class="navbar navbar-default">
        <!-- Brand and toggle get grouped for better mobile display -->

        <!-- Collection of nav links, forms, and other content for toggling -->
        <div class="navbar-form navbar-left" role="search">
            <div class="btn-toolbar" role="toolbar">
                <div class="btn-group">
                    <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click" />
                </div>

            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="container">
        <div class="box-name">
            <i class="fa fa-table"></i>
            <asp:Literal Text="" ID="litmensaje" runat="server" />
        </div>

        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">Cliente:</span>
            <asp:TextBox ID="txtCliente" CssClass="form-control " ReadOnly="true" runat="server"></asp:TextBox>
        </div>
        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">No.Serie:</span>
            <asp:TextBox ID="txtSerie" CssClass="form-control " runat="server"></asp:TextBox>
        </div>
        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">No. Referencia:</span>
            <asp:TextBox ID="txtNoRecibo" CssClass="form-control " runat="server"></asp:TextBox>
        </div>
        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">Monto Recibido:</span>
            <asp:TextBox ID="txtMonto" CssClass="form-control " runat="server"></asp:TextBox>
        </div>

        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">Fecha Efectiva:</span>
            <asp:TextBox ID="txtFechaEfectiva" CssClass="form-control " runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="cetxtFechaEfectiva" runat="server" Enabled="True" TargetControlID="txtFechaEfectiva" Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
        </div>
    </div>
</asp:Content>
