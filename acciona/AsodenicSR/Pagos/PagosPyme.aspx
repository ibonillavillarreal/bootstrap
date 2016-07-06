<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="PagosPyme.aspx.cs" Inherits="Acciona.Pagos.PagosPyme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Pagos (Cuotas)</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form form" role="search">
                <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <span class="input-group-addon" style="width: 150px">Parametros:</span>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control required" Width="100%">
                                 <asp:ListItem Value="0" Selected="True">Placa</asp:ListItem>
                                <asp:ListItem Value="1">Identificacion</asp:ListItem>
                                <asp:ListItem Value="2">Codigo</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn" OnClick="btnBuscar_Click" />
                        <%--<asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default " runat="server" OnClick="btImprimir_Click" />--%>
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
        <div id="navbarCollapses" class="collapse navbar-collapse">
            <div class="navbar-form navbar-left" role="search">
                <div class="btn-toolbar" role="toolbar">
                    <div class="form-group">
                        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
                        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click" />
                        <%--<asp:ImageButton ID="imbImprimir" SkinID="imbImprimir" runat="server" CssClass="btn btn-default" OnClick="imbImprimir_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 60px">No Serie:</span>
                <asp:TextBox ID="txtSerie" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 60px">No. Recibo:</span>
                <asp:TextBox ID="txtNoRecibo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="input-group" style="width: 80%;">
                <span class="input-group-addon input-sm" style="width: 100px">Monto:</span>
                <asp:TextBox ID="txtMontoRecibido" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="input-group" style="width: 80%;">
                <span class="input-group-addon input-sm" style="width: 150px">Fecha Aplicacion:</span>
                <asp:TextBox ID="txtFechaEfectiva" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtFechaEfectiva_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaEfectiva" Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </div>
        </div>
    </div>

    <div class="box-name">
        <i class="fa fa-table input-sm"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="container">
        <%--<asp:HiddenField ID="hfSelectedTab" runat="server" />
        <asp:HiddenField ID="hfTabContacto" runat="server" />--%>
        <asp:HiddenField ID="hfIdMovimiento" runat="server" />
        <asp:HiddenField ID="hfIdCliente" runat="server" />
        <asp:HiddenField ID="hfIdCuenta" runat="server" />
        <asp:HiddenField ID="hfIdCuota" runat="server" />

        <asp:GridView ID="gvDetalle" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdMovimiento" 
            EmptyDataText="No se encontró ningun dato registrado" DataMember="vw_DatosCredito" ShowFooter="True" Caption="Detalle de las cuentas"
            OnRowCommand="gvDatos_RowCommand">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="IdMovimiento" Visible="false" />
                <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                <asp:BoundField DataField="Marca" HeaderText="Descripcion" />
                <asp:BoundField DataField="Placa" HeaderText="No.Placa" />
                <asp:BoundField DataField="MontoTransaccion" HeaderText="Monto" DataFormatString="{0:N2}"/>
            </Columns>

        </asp:GridView>

        <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Nombre Completo:</span>
                    <asp:TextBox ID="txtNombre" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Identificacion:</span>
                    <asp:TextBox ID="txtIdentificacion" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>

            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">No. Tarjeta:</span>
                    <asp:TextBox ID="txtNoTarjeta" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">No. Cuenta:</span>
                    <asp:TextBox ID="txtNocuenta" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Monto:</span>
                    <asp:TextBox ID="txtMonto" CssClass="form-control input-sm dinero" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">No. Cuotas:</span>
                    <asp:TextBox ID="txtNoCuotas" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Plazo Meses:</span>
                    <asp:TextBox ID="txtPlazo" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Colector:</span>
                    <asp:TextBox ID="txtColector" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Fecha Movimiento:</span>
                    <asp:TextBox ID="txtFechaDesembolso" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <%--<div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Fecha Vencimiento:</span>
                    <asp:TextBox ID="txtFechaVencimiento" CssClass="form-control input-sm" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>--%>
        </div>
        <%--<div class="box-name">
            <i class="fa fa-table"></i>
            <h4>Detalle de Cuotas Pendientes</h4>
        </div>
        <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="50%" AutoGenerateColumns="False" DataKeyNames="FechaCouta"
            EmptyDataText="No se encontró ningun dato registrado" DataMember="SP_CuotasPendientes">
            <Columns>
                <asp:BoundField DataField="FechaCouta" HeaderText="Fecha Cuota" DataFormatString="{0:d}">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="Cuota" HeaderText="Cuota" DataFormatString="{0:N2}">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="Abonos" HeaderText="Abonos" DataFormatString="{0:N2}">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N2}">
                    <ItemStyle Width="30%" />
                </asp:BoundField>

            </Columns>
        </asp:GridView>--%>
    </div>

</asp:Content>
