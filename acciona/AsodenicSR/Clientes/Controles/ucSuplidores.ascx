<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSuplidores.ascx.cs" Inherits="Acciona.Clientes.Controles.ucSuplidores" %>
<div class="container form">
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
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdNegocioProveedor" EmptyDataText="No se encontró ningun dato registrado" DataMember="NegocioProveedor">
        <Columns>
            <asp:BoundField DataField="IdNegocioProveedor" HeaderText="IdNegocioProveedor" Visible="False" />
            <asp:BoundField DataField="IdDatosNegocio" HeaderText="IdDatosNegocio" Visible="False" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                <ItemStyle Width="50%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="30%" />
            </asp:TemplateField>--%>
            <asp:CheckBoxField DataField="EsCliente" HeaderText="Cliente" ReadOnly="True" />            
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" Text="Editar" CommandName="Editar" onclick="btnEditar_Click" runat="server" />
                    <ItemStyle Width="10%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        Text="Delete" CommandName="Delete" onclick="btn_Click" runat="server" />
                    <ItemStyle Width="10%" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Cliente/Suplidor:</span>
            <asp:TextBox ID="txtNombre" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Es Cliente:</span>
            <asp:CheckBox ID="chkCliente" runat="server" CssClass="form-control"/>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control"/>
        </div>
    </asp:Panel>
</div>