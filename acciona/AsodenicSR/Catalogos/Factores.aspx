<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="Factores.aspx.cs" Inherits="Acciona.Catalogos.Factores" %>

<%@ Register Src="~/Controles/wucCategoria.ascx" TagPrefix="factor" TagName="ucCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <asp:Literal runat="server" Text="Factores"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="btn-toolbar" role="toolbar">
        <div class="btn-group">
            <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
            <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
            <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar_Click" />
            <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbItems" SkinID="imbItems" runat="server" CssClass="btn btn-default" OnClick="imbItems_Click" />
        </div>
        <div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click" />
        </div>
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdFactor" EmptyDataText="No se encontró ningún factor registrado" DataMember="Factor">
        <Columns>
            <asp:BoundField DataField="IdFactor" HeaderText="IdFactor" Visible="False" />
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
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
            <span class="input-group-addon">Es Activo:</span>
            <asp:CheckBox ID="chkEsActivo" Text="" runat="server" />
        </div>
    </asp:Panel>
    <factor:ucCategoria runat="server" ID="catCategoria" />
</asp:Content>