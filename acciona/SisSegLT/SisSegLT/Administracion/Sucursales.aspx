<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sucursales.aspx.cs" Inherits="SisSegLT.Administracion.Sucursales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><small><a href="../" class="glyphicon glyphicon-home"></a></small>  Sucursales</h2>
    <h3><asp:Literal ID="litAyuda" runat="server"></asp:Literal></h3>
    <div class="btn-toolbar" role="toolbar">
        <div class="btn-group">
            <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
            <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
            <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar_Click" />
            <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
        </div>
        <div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click"/>
        </div>
        <asp:Literal ID="litmensaje" Text="" runat="server" />
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdSucursal" EmptyDataText="No se encontró ninguna sucursal registrada" DataMember="Sucursal">
        <Columns>
            <asp:BoundField DataField="IdSucursal" HeaderText="IdSucursal" Visible="False" />
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Direccion" HeaderText="Dirección">
                <ItemStyle Width="60%" />
            </asp:BoundField>
            <asp:BoundField DataField="Ciudad.Nombre" HeaderText="Ciudad">
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
            <span class="input-group-addon">Dirección:</span>
            <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Ciudad:</span>
            <asp:DropDownList ID="ddlCiudad" CssClass="form-control" runat="server" DataTextField="Nombre" DataValueField="IdCiudad"></asp:DropDownList>
        </div>
    </asp:Panel>
</asp:Content>
