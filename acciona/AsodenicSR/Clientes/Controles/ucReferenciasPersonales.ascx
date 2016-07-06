<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReferenciasPersonales.ascx.cs" Inherits="Acciona.Clientes.Controles.ucReferenciasPersonales" %>
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
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdReferencia" EmptyDataText="No se encontró ningun dato registrado" DataMember="Referencia" OnDataBound="gvDatos_DataBound">
        <Columns>
            <asp:BoundField DataField="IdReferencia" HeaderText="IdReferencia" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="Nombre Completo">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("NombreCompleto") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="20%" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="NoIdentificacion" HeaderText="Identificacion">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Sexo" HeaderText="Sexo">
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="Profesion" HeaderText="Profesion">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Telefono" HeaderText="Telefono">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Tiempo" HeaderText="Antiguedad">
                <ItemStyle Width="6%" />
            </asp:BoundField>
            <asp:BoundField DataField="CentroLaboral" HeaderText="Centro Laboral">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" Text="Editar" CommandName="Editar" OnClick="btnEditar_Click" runat="server" />
                    <itemstyle width="5%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        Text="Delete" CommandName="Delete" OnClick="btn_Click" runat="server" />
                    <itemstyle width="5%" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Identificacion:</span>
            <asp:TextBox ID="txtIdentificacion" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Nombre:</span>
            <asp:TextBox ID="txtNombre" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Telefono:</span>
            <asp:TextBox ID="txtTelefono" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Direccion:</span>
            <asp:TextBox ID="txtDireccion" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Sexo:</span>
            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control required">
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Femenino</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Profesión:</span>
            <asp:TextBox ID="txtProfesion" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <%--<div class="input-group">
            <span class="input-group-addon" style="width: 150px">Profesion:</span>
            <asp:DropDownList ID="ddlProfesion" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>--%>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Centro Laboral:</span>
            <asp:TextBox ID="txtLugarTrabajo" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Antiguedad:</span>
            <asp:TextBox ID="txtAntiguedad" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required" />
        </div>
    </asp:Panel>
</div>
