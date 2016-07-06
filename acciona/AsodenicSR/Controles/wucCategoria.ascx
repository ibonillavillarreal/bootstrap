<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCategoria.ascx.cs" Inherits="Acciona.Controles.wucCategoria" %>
<%@ Register Src="~/Controles/wucClasificacion.ascx" TagPrefix="Categoria" TagName="ucClasificacion" %>
<div class="navbar navbar-default">
    <div class="container">
        <div class="navbar-header">
            <span class="navbar-brand">Categorías
            </span>
        </div>
    </div>
</div>
<div class="btn-toolbar" role="toolbar">
    <div class="btn-group">
        <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar2_Click" />
        <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar2_Click" />
        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar2_Click" />
        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar2_Click" />
        <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar2_Click" />
        <asp:ImageButton ID="imbItems" SkinID="imbItems" runat="server" CssClass="btn btn-default" OnClick="imbItems2_Click" />
    </div>
    <div class="btn-group">
        <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar2_Click" />
    </div>
</div>
<asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdCategoria" EmptyDataText="No se encontró ninguna categoría registrada" DataMember="Categoria">
    <Columns>
        <asp:BoundField DataField="IdCategoria" HeaderText="IdCategoria" Visible="False" />
        <asp:TemplateField HeaderText="Nombre">
            <ItemTemplate>
                <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar2_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Ponderacion" HeaderText="Ponderación">
            <ItemStyle Width="15%" />
        </asp:BoundField>
        <asp:CheckBoxField DataField="EsActivo" HeaderText="Es Activo" SortExpression="EsActivo" />
    </Columns>
</asp:GridView>
<asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>

<asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
    <div class="input-group">
        <span class="input-group-addon">Nombre:</span>
        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="input-group">
        <span class="input-group-addon">Poderación:</span>
        <asp:TextBox ID="txtPoderacion" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="input-group">
        <span class="input-group-addon">Es Activo:</span>
        <asp:CheckBox ID="chkEsActivo" Text="" runat="server" />
    </div>
</asp:Panel>
<Categoria:ucClasificacion runat="server" ID="catClasificacion" />