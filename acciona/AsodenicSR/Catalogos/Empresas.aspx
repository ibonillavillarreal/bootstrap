<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="Acciona.Credito.CatEmpresas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
     <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Empresas</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control required" placeholder="Nombre" />
                </div>
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default submit" OnClick="btnBuscar_Click" />
                <%--<asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default submit" runat="server" OnClick="btImprimir_Click" />--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <asp:HiddenField ID="hfIdEmpresa" runat="server" />
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
            <span class="input-group-addon" style="width: 150px">Codigo:</span>
            <asp:TextBox ID="txtCodigo" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group" style="width: 40%;">
            <span class="input-group-addon" style="width: 150px">Nombre:</span>
            <asp:TextBox ID="txtNombreEmpresa" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
       <%-- <div class="input-group" style="width: 37%;">
            <span class="input-group-addon" style="width: 150px">Descripcion:</span>
            <asp:TextBox ID="txtDescripcion" CssClass="form-control required" runat="server" Height="46px" TextMode="MultiLine" Width="354px"></asp:TextBox>
        </div>--%>
        <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Activo:</span>
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required" />
            </div>

    </asp:Panel>

    <div class="box-name">
        <i class="fa fa-table"></i>
        <h4>Detalle de Empresa</h4>
    </div>

    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdEmpresa"
        EmptyDataText="No se encontró ningun dato registrado" DataMember="Empresas" Visible="true">
        <Columns>
            <asp:BoundField DataField="IdEmpresa" HeaderText="IdCuenta" Visible="False" />
            <asp:BoundField DataField="Codigo" HeaderText="Codigo"  />
            <asp:BoundField DataField="Nombre" HeaderText="IdAprobado"  />
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" CommandName="Editar" OnClick="btnEditar_Click" runat="server" />
                    <itemstyle width="8%" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
