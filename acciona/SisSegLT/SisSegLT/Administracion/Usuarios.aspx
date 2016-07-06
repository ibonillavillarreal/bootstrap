<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SisSegLT.Administracion.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><small><a href="../" class="glyphicon glyphicon-home"></a></small>  Usuarios</h2>
    <h3><asp:Literal ID="litAyuda" runat="server"></asp:Literal></h3>
    <div class="btn-toolbar" role="toolbar">
        <div class="btn-group">
            <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
            <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
            <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar_Click" />
            <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />         
            <asp:ImageButton ID="imbRoles" SkinID="imbRoles" runat="server" CssClass="btn btn-default" OnClick="imbRoles_Click"/>
        </div>
        <div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click"/>
        </div>
        <asp:Literal ID="litmensaje" Text="" runat="server" />
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdUsuario" EmptyDataText="No se encontró ninguna sucursal registrada" DataMember="Usuario">
        <Columns>
            <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" Visible="False" />
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Login" HeaderText="Usuario">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Cargo" HeaderText="Cargo">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="Rol.Nombre" HeaderText="Rol">
                <ItemStyle Width="20%" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="Sucursal.Nombre" HeaderText="Sucursal">
                <ItemStyle Width="20%" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="input-group">
            <span class="input-group-addon">Nombre:</span>
            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Código:</span>
            <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Usuario:</span>
            <asp:TextBox ID="txtLogin" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Contraseña:</span>
            <asp:TextBox ID="txtPass" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Cargo:</span>
            <asp:TextBox ID="txtCargo" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Sexo:</span>
            <asp:DropDownList ID="ddlSexo" CssClass="form-control" runat="server">
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Femenino</asp:ListItem>
            </asp:DropDownList>
        </div>
      <%--  <div class="input-group">
            <span class="input-group-addon">Rol:</span>
            <asp:DropDownList ID="ddlRol" CssClass="form-control" runat="server" DataTextField="Nombre" DataValueField="IdRol"></asp:DropDownList>
        </div>--%>
        <div class="input-group">
            <span class="input-group-addon">Sucursal:</span>
            <asp:DropDownList ID="ddlSucursal" CssClass="form-control" runat="server" DataTextField="Nombre" DataValueField="IdSucursal"></asp:DropDownList>
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
        <asp:GridView ID="gvHijo" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdUsuarioRol" EmptyDataText="Aún no hay roles asignados para este menú" DataMember="UsuarioRol">
            <Columns>
                <asp:BoundField DataField="IdUsuarioRol" HeaderText="IdUsuarioRol" Visible="False" />
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
