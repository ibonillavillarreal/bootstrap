<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="SeguroCredito.aspx.cs" Inherits="Acciona.Credito.SeguroCredito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Seguro de Vehiculo</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form form" role="search">
                <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <span class="input-group-addon" style="width: 150px">Parametros:</span>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control required" Width="100%">
                                <asp:ListItem Value="0">Placa</asp:ListItem>
                                <asp:ListItem Value="1">Identificacion</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">Codigo</asp:ListItem>
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
        <asp:GridView ID="gvDetalle" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdMovimiento"
            EmptyDataText="No se encontró ningun dato registrado" DataMember="vw_DatosCredito" ShowFooter="True" Caption="Detalle de Vehiculo"
            OnRowCommand="gvDatos_RowCommand">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="IdMovimiento" Visible="false" />
                <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                <asp:BoundField DataField="Marca" HeaderText="Descripcion" />
                <asp:BoundField DataField="Placa" HeaderText="No.Placa" />
                <asp:BoundField DataField="MontoTransaccion" HeaderText="Monto" DataFormatString="{0:N2}" />
            </Columns>

        </asp:GridView>

        <asp:GridView ID="gvDatosSeguro" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdPrestamo"
            EmptyDataText="No se encontró ningun dato registrado" DataMember="vw_CargosMensuales" ShowFooter="True" Caption="Datos de Seguros"
             OnRowCommand="gvDatosSeguro_RowCommand">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="IdPrestamo" Visible="false" />
                <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:d}" />
                <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="FechaCorte" HeaderText="Fecha Corte" DataFormatString="{0:d}"/>
            </Columns>

        </asp:GridView>

        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon" style="width: 150px">Fecha Inicio:</span>
                    <asp:TextBox ID="txtFechaInicio" CssClass="form-control" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFechaInicio" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>

                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon" style="width: 150px">Fecha Fin:</span>
                    <asp:TextBox ID="txtFechaFin" CssClass="form-control" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtFechaFin" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon" style="width: 150px">Fecha Corte:</span>
                    <asp:TextBox ID="txtFechaCorte" CssClass="form-control" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtFechaCorte" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon" style="width: 150px">Monto:</span>
                    <asp:TextBox ID="txtMonto" CssClass="form-control" runat="server"></asp:TextBox>

                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon" style="width: 150px">Frecuencia:</span>
                    <asp:DropDownList ID="ddlFrecuencia" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon" style="width: 150px">Tipo Transaccion:</span>
                    <asp:DropDownList ID="ddlTipoTransaccion" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>

            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon" style="width: 150px">Observaciones:</span>
                    <asp:TextBox ID="txtObservaciones" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
