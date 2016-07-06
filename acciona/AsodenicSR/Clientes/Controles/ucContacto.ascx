<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucContacto.ascx.cs" Inherits="Acciona.Clientes.Controles.ucContacto" %>
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
    <asp:GridView ID="gvDatos" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdContacto"
        EmptyDataText="No se encontró ningun dato registrado" DataMember="Contacto">
        <Columns>
            <asp:BoundField DataField="IdContacto" HeaderText="IdDomicilio" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:BoundField DataField="TipoContacto" HeaderText="Tipo de Contacto">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                <ItemStyle Width="50%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="Descripcion">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Descripcion") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="50%" />
            </asp:TemplateField>--%>
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" CommandName="Editar" OnClick="btnEditar_Click" runat="server" />
                    <itemstyle width="10%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        CommandName="Delete" OnClick="btn_Click" runat="server" />
                    <itemstyle width="10%" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%--<asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>--%>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">

        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Tipo Contacto:</span>
            <asp:DropDownList ID="ddlTipoContacto" runat="server" CssClass="form-control required">
                <asp:ListItem Value="Telefono Convencional">Telefono Convencional</asp:ListItem>
                <asp:ListItem Value="Telefono Celular">Telefono Celular</asp:ListItem>
                <asp:ListItem Value="Telefono Trabajo">Telefono Trabajo</asp:ListItem>
                <asp:ListItem Value="Email">Email</asp:ListItem>
                <asp:ListItem Value="Redes Sociales">Redes Sociales</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Descripcion:</span>
            <asp:TextBox ID="txtDescripcion" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required" />
        </div>
    </asp:Panel>
</div>
