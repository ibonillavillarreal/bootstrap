<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucClasificacion.ascx.cs" Inherits="Acciona.Controles.wucClasificacion" %>
<div class="navbar navbar-default">
    <div class="container">
        <div class="navbar-header">
            <span class="navbar-brand">Clasificación
            </span>
        </div>
    </div>
</div>
<div class="btn-toolbar" role="toolbar">
    <div class="btn-group">
        <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar3_Click" />
        <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar3_Click" />
        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar3_Click" />
        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar3_Click" />
        <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar3_Click" />
    </div>
    <div class="btn-group">
        <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar3_Click" />
    </div>
</div>
<asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdClasificacion" AllowPaging="True"
    EmptyDataText="No se encontró ninguna clasificación registrada" DataMember="Clasificacion" OnPageIndexChanging="gvDatos_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="IdClasificacion" HeaderText="IdClasificacion" Visible="False" />
        <asp:TemplateField HeaderText="Nombre">
            <ItemTemplate>
                <%--<asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar3_Click" ></asp:LinkButton>--%>
                <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnClick="lnbSeleccionar3_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Puntuacion" HeaderText="Puntuación">
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
        <span class="input-group-addon">Puntuación:</span>
        <asp:TextBox ID="txtPuntuacion" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="input-group">
        <span class="input-group-addon">Es Activo:</span>
        <asp:CheckBox ID="chkEsActivo" Text="" runat="server" />
    </div>
</asp:Panel>