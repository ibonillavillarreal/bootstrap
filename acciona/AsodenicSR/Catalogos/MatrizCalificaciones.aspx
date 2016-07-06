<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="MatrizCalificaciones.aspx.cs" Inherits="Acciona.Catalogos.MatrizCalificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    Matriz de Calificaciones
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
        </div>
        <div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click" />
        </div>
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdMatrizCalificacion" EmptyDataText="No se encontró ninguna matriz de calificación registrada" DataMember="MatrizCalificacion">
        <Columns>
            <asp:BoundField DataField="IdMatrizCalificacion" HeaderText="IdMatrizCalificacion" Visible="False" />
            <asp:TemplateField HeaderText="Valor mínimo">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ValorMin" HeaderText="Valor mínimo">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorMax" HeaderText="Valor máximo">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="Impacto" HeaderText="Impacto">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="EsActivo" HeaderText="Es activo">
                <ItemStyle Width="15%" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="input-group">
            <span class="input-group-addon">Nombre:</span>
            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Valor mínimo:</span>
            <asp:TextBox ID="txtValorMin" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Valor máximo:</span>
            <asp:TextBox ID="txtValorMax" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Impacto:</span>
            <asp:TextBox ID="txtImpacto" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </asp:Panel>
</asp:Content>