<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAprobacionInstitucion.ascx.cs" Inherits="Acciona.Clientes.Controles.ucAprobacionInstitucion" %>
<div class="container">
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
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdAprobacionInstitucion" EmptyDataText="No se encontró ningun dato registrado" DataMember="AprobacionInstitucion">
        <Columns>
            <asp:BoundField DataField="IdAprobacionInstitucion" HeaderText="IdAprobacionInstitucion" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                <ItemStyle Width="55%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="Descripcion">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Descripcion") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="35%" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="Usuario1.Nombre" HeaderText="Nombre Verificador">
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaHoraVerificacion" HeaderText="Fecha Verificacion" DataFormatString="{0:d}">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="NivelRiesgo" HeaderText="Nivel Riesgo">
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
            <span class="input-group-addon" style="width: 150px">Descripcion de Resultados:</span>
            <asp:TextBox ID="txtDescripcion" CssClass="form-control col-lg-12" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Fecha Verificador:</span>
            <asp:TextBox ID="txtFechaAprobacion" CssClass="form-control" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender runat="server" Enabled="True" TargetControlID="txtFechaAprobacion" ID="txtFechaAprobacion_CalendarExtender" Format="dd/MM/yyyy hh:mm:ss tt"></ajaxToolkit:CalendarExtender>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Nivel Riesgo:</span>
            <asp:TextBox ID="txtNivelRiesgo" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon"  style="width: 150px">Nombre Promotor</span>
            <%--<asp:TextBox runat="server" ID="txtPromotor" ReadOnly="true" CssClass="form-control" />--%>
            <asp:DropDownList ID="ddlPromotor" runat="server" Width="100%" CssClass="form-control required">
            </asp:DropDownList>
        </div>

    </asp:Panel>
</div>
