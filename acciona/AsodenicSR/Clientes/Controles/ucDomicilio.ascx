<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDomicilio.ascx.cs" Inherits="Acciona.Clientes.Controles.ucDomicilio" %>
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
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdDomicilio" 
        EmptyDataText="No se encontró ningun dato registrado" DataMember="Domicilio">
        <Columns>
            <asp:BoundField DataField="IdDomicilio" HeaderText="IdDomicilio" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                <ItemStyle Width="65%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="Descripcion">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Descripcion") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="65%" />
            </asp:TemplateField>--%>
            <asp:CheckBoxField DataField="EsAlquilada" HeaderText="Alquilada" ReadOnly="True" />
            <asp:CheckBoxField DataField="EsPropia" HeaderText="Propia" ReadOnly="True" />
            <asp:CheckBoxField DataField="Familiar" HeaderText="Familiar" ReadOnly="True" />
            <asp:BoundField DataField="TiempoResidir" HeaderText="TiempoResidir" />
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" Text="Editar" CommandName="Editar" onclick="btnEditar_Click" runat="server" />
                    <ItemStyle Width="8%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        Text="Delete" CommandName="Delete" onclick="btn_Click" runat="server" />
                    <ItemStyle Width="8%" />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    <asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">

        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Vivienda:</span>
            <asp:RadioButtonList runat="server" ID="rblVivienda" RepeatDirection="Horizontal" CssClass="required">
                <asp:ListItem Text="Familiar" />
                <asp:ListItem Text="Propia" />
                <asp:ListItem Text="Alquilada" />
            </asp:RadioButtonList>
        </div>

        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Direccion:</span>
            <asp:TextBox ID="txtDireccion" CssClass="form-control required" TextMode="MultiLine" runat="server" Width="698px"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Tiempo Residir:</span>
            <asp:TextBox ID="txtResidir" CssClass="form-control required number" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required"/>
        </div>
    </asp:Panel>
</div>