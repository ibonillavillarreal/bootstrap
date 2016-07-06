<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="AtencionCliente.aspx.cs" Inherits="Acciona.Clientes.AtencionCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Atencion al Cliente</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form form" role="search">
                <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <span class="input-group-addon" style="width: 150px">Parametros:</span>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control required" Width="100%">
                                <asp:ListItem Value="0" Selected="True">Cuenta</asp:ListItem>
                                <asp:ListItem Value="1">Identificacion</asp:ListItem>
                                <asp:ListItem Value="2">Nombre</asp:ListItem>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">Informacion Personal</div>
                <div class="input-group" style="width: 37%;">
                    <span class="input-group-addon" style="width: 150px">Identificacion:</span>
                    <asp:TextBox ID="txtIdentificacion" CssClass="form-control required" runat="server"></asp:TextBox>
                </div>
                <div class="input-group" style="width: 40%;">
                    <span class="input-group-addon" style="width: 150px">Nombre:</span>
                    <asp:TextBox ID="txtNombre" CssClass="form-control required" runat="server"></asp:TextBox>
                </div>
                <div class="input-group" style="width: 37%;">
                    <span class="input-group-addon" style="width: 150px">Direccion:</span>
                    <asp:TextBox ID="txtDireccion" CssClass="form-control required" runat="server"></asp:TextBox>
                </div>
                <div class="input-group" style="width: 40%;">
                    <span class="input-group-addon" style="width: 150px">Telefono:</span>
                    <asp:TextBox ID="txtTelefono" CssClass="form-control required" runat="server"></asp:TextBox>
                </div>
                <div class="panel-body">
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">Informacion Cuenta</div>
                <div class="input-group" style="width: 37%;">
                    <span class="input-group-addon" style="width: 150px">Tipo de Cuenta:</span>
                   <asp:TextBox ID="txtTipoCuenta" CssClass="form-control required" runat="server"></asp:TextBox>
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
                </div>
                <div class="input-group" style="width: 40%;">
                    <span class="input-group-addon" style="width: 150px">Limite:</span>
                    <asp:TextBox ID="txtLimite" CssClass="form-control required dinero" runat="server"></asp:TextBox>
                </div>
                <div class="input-group" style="width: 37%;">
                    <span class="input-group-addon" style="width: 150px">Estado de la Cuenta:</span>
                    <asp:TextBox ID="txtEstado" CssClass="form-control required dinero" runat="server"></asp:TextBox>
                </div>
                <div class="panel-body">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
