<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="ClienteCuenta.aspx.cs" Inherits="Acciona.Credito.ClienteCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Administracion de Cuentas</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control required" placeholder="No. Identificación" />
                </div>
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default submit" OnClick="btnBuscar_Click" />
                <%--<asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default submit" runat="server" OnClick="btImprimir_Click" />--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <%--<div class="jumbotron">
        <h4>Adminstración de Cuenta</h4>
    </div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">



    <asp:HiddenField ID="hfIdCliente" runat="server" />
    <asp:HiddenField ID="hfIdCuenta" runat="server" />
    <div class="box-name">
        <i class="fa fa-table"></i>
        <asp:Literal ID="LitCliente" runat="server"></asp:Literal>
    </div>
    <div role="navigation" class="navbar navbar-default">
        <!-- Brand and toggle get grouped for better mobile display -->

        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapses" class="collapse navbar-collapse">
            <div class="navbar-form navbar-left" role="search">
                <div class="btn-toolbar" role="toolbar">
                    <div class="btn-group">
                        <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
                        <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
                        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
                        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click" />
                        <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
                    </div>
                    <div class="btn-group">
                        <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="box-name">
        <i class="fa fa-table"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">

        <%--<div class="input-group" style="width: 40%;">
                <span class="input-group-addon" style="width: 150px">Cliente:</span>
                <asp:TextBox ID="txtCliente" CssClass="form-control required" runat="server"></asp:TextBox>
            </div>--%>
        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">Tipo de Cuenta:</span>
            <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="form-control required">
            </asp:DropDownList>
        </div>
        <div class="input-group" style="width: 40%;">
            <span class="input-group-addon" style="width: 150px">NoCuenta:</span>
            <asp:TextBox ID="txtNoCuenta" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">Aprobado por:</span>
            <asp:DropDownList ID="ddlAprobado" runat="server" CssClass="form-control required"></asp:DropDownList>
        </div>
        <div class="input-group" style="width: 40%;">
            <span class="input-group-addon" style="width: 150px">Fecha Aprobacion:</span>
            <asp:TextBox ID="txtFechaAprobacion" CssClass="form-control required" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtFechaAprobacion_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaAprobacion" Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="input-group" style="width: 40%;">
            <span class="input-group-addon" style="width: 150px">Limite:</span>
            <asp:TextBox ID="txtLimite" CssClass="form-control required dinero" runat="server"></asp:TextBox>
        </div>
        <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">Estado de la Cuenta:</span>
            <asp:DropDownList ID="ddlEstadoCuenta" runat="server" CssClass="form-control required"></asp:DropDownList>
        </div>

    </asp:Panel>

    <div class="box-name">
        <i class="fa fa-table"></i>
        <h4>Detalles de Cuentas</h4>
    </div>

    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdCuenta"
        EmptyDataText="No se encontró ningun dato registrado" DataMember="tClienteCuenta">
        <Columns>
            <asp:BoundField DataField="IdCuenta" HeaderText="IdCuenta" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:BoundField DataField="IdAprobado" HeaderText="IdAprobado" Visible="False" />
            <asp:BoundField DataField="TipoCuenta" HeaderText="IdTipoCuenta" Visible="False" />
            <asp:BoundField DataField="tTipoCuenta.Descripcion" HeaderText="Tipo de Cuenta" />
            <asp:BoundField DataField="NoCuenta" HeaderText="NoCuenta" />
            <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha de Aprobacion" DataFormatString="{0:d}" />
            <asp:BoundField DataField="Limite" HeaderText="Limite" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="tEstadoCuentas.Descripcion" HeaderText="Estado" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" CommandName="Editar" OnClick="btnEditar_Click" runat="server" />
                    <itemstyle width="8%" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <script>
        $("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });
    </script>

</asp:Content>
