<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="DetallePagos.aspx.cs" Inherits="Acciona.Reportes.DetallePagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Consulta de Pagos - Vehiculos</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <span class="input-group-addon" style="width: 150px">Codigo:</span>
                            <%-- <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control required" Width="100%">
                                <asp:ListItem Value="0" Selected="True">Cuenta</asp:ListItem>
                                <asp:ListItem Value="1">Identificacion</asp:ListItem>
                                <asp:ListItem Value="2">Nombre</asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <asp:TextBox ID="txtBuscar" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default" OnClick="btnBuscar_Click"/>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <div class="box-name">
        <i class="fa fa-table"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <h4>Informacion del vehiculo </h4>
    <div class="box-name">
        <i class="fa fa-table"></i>
        <asp:Literal ID="LitCliente" runat="server"></asp:Literal>
    </div>
    <%-- <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <div class="col-md-6">
            <div class="input-group" style="width: 50%;">
                <span class="input-group-addon" style="width: 150px">Saldo:</span>
                <asp:TextBox ID="txtSaldo" CssClass="form-control required" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
    </div>--%>

    <div class="row" style="margin-bottom: 5px;">
        <div class="col-md-4">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon" style="width: 150px">Codigo:</span>
                <asp:TextBox ID="txtCodigo" CssClass="form-control " runat="server"></asp:TextBox>

            </div>
        </div>
        <div class="col-md-4">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon" style="width: 150px">Marca:</span>
                <asp:TextBox ID="txtMarca" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon" style="width: 150px">Modelo:</span>
                <asp:TextBox ID="txtModelo" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon" style="width: 150px">Año:</span>
                <asp:TextBox ID="txtAnio" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
        </div>
        
    </div>
    <div class="row" style="margin-bottom: 5px;">
    </div>

    <div class="box-name">
        <i class="fa fa-table"></i>
        <h4>Detalle</h4>
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="FechaEfectiva"
        EmptyDataText="No se encontró ningun dato registrado" DataMember="SP_ConsultaCodigoVehiculo">
        <Columns>
            <asp:BoundField DataField="NoMovimiento" HeaderText="No.Movimiento" />
            <asp:BoundField DataField="FechaEfectiva" HeaderText="Fecha Efectiva" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="debito" HeaderText="Debitos" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="credito" HeaderText="Creditos" DataFormatString="{0:N2}" />
        </Columns>
    </asp:GridView>
</asp:Content>
