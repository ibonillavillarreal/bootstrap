<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemMenus.aspx.cs" Inherits="SisSegLT.Administracion.ItemMenus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><small><a href="../" class="glyphicon glyphicon-home"></a></small>  Menú</h2>
    <h3><asp:Literal ID="litAyuda" runat="server"></asp:Literal></h3>
    <!-- Ruta del menú -->
    <ol id="rutaMenu" class="breadcrumb" runat="server">
        <asp:Literal ID="litRutaMenu" runat="server"></asp:Literal>
    </ol>
    <!-- ITEMMENUES BARRA -->
    <div class="btn-toolbar" role="toolbar">
        <div class="btn-group">
            <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
            <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
            <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar_Click" />
            <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbItems" SkinID="imbItems" runat="server" CssClass="btn btn-default" OnClick="imbItems_Click"/>
            <asp:ImageButton ID="imbRoles" SkinID="imbRoles" runat="server" CssClass="btn btn-default" OnClick="imbRoles_Click"/>
        </div>
        <div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click"/>
        </div>
    </div>
    <!-- ITEMMENUES GRID -->
    <asp:Literal ID="litmensaje" Text="" runat="server" />
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdItemMenu" EmptyDataText="No se encontraron datos que mostrar" DataMember="ItemMenu">
        <Columns>
            <asp:BoundField DataField="IdItemMenu" HeaderText="IdItemMenu" Visible="False" />
            <asp:TemplateField HeaderText="Texto">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbTexto" runat="server" Text='<%# Eval("Texto") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Ruta" HeaderText="Ruta">
                <ItemStyle Width="25%" />
            </asp:BoundField>
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción">
                <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="Visible" HeaderText="Visible">
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de registro">
                <ItemStyle Width="15%" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <!-- ITEMMENUES EDICIÓN -->
    <asp:Panel ID="pnlAgregar" runat="server" Visible="False" Width="100%">
        <div class="input-group">
            <span class="input-group-addon">Texto:</span>
            <asp:TextBox ID="txtTexto" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Ruta:</span>
            <asp:TextBox ID="txtRuta" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Descripción:</span>
            <asp:TextBox ID="txtDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">
                <asp:CheckBox ID="chkVisible" runat="server" Text="" />
            </span>
            <span class="form-control">Mostrar</span>
        </div>
    </asp:Panel>
    
    <asp:panel ID="pnlRol" runat="server">
        <h3><asp:Literal ID="litAyuda2" runat="server"></asp:Literal></h3>
        <!-- CIUDADES BARRA -->
        <div class= "btn-toolbar" role= "toolbar">
            <div class="btn-group">
                <asp:ImageButton ID="imbAgregar2" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar2_Click" />
                <asp:ImageButton ID="imbEditar2" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar2_Click" />
                <asp:ImageButton ID="imbCancelar2" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar2_Click" />
                <asp:ImageButton ID="imbGuardar2" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar2_Click" />
                <asp:ImageButton ID="imbActualizar2" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar2_Click" />
            </div>
            <div class="btn-group">
                <asp:ImageButton ID="imbEliminar2" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar2_Click"/>
            </div>
            <div class="btn-group pull-right">
                <asp:ImageButton ID="imbCerrarRol" SkinID="imbCerrar" runat="server" CssClass="btn btn-danger" OnClick="imbCerrar2_Click" />
            </div>
        </div>
        <!-- CIUDADES GRID -->
        <asp:GridView ID="gvHijo" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdItemRol" EmptyDataText="Aún no hay roles asignados para este menú" DataMember="ItemRol">
            <Columns>
                <asp:BoundField DataField="IdItemRol" HeaderText="IdItemRol" Visible="False" />
                <asp:TemplateField HeaderText="Rol asignado">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Rol.Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar2_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <!-- CIUDADES EDICIÓN -->
        <asp:Panel ID="pnlAgregar2" runat="server" Visible="False" Width="100%">
            <div class="input-group">
                <span class="input-group-addon">Rol:</span>
                <asp:DropDownList ID="ddlIdRol" runat="server" DataValueField="IdRol" DataTextField="Nombre" CssClass="form-control"></asp:DropDownList>
            </div>
        </asp:Panel>
    </asp:panel>
</asp:Content>
