<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDocumentosNegocio.ascx.cs" Inherits="Acciona.Clientes.Controles.ucDocumentosNegocio" %>
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
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdDocumentoNegocio" EmptyDataText="No se encontró ningun dato registrado" DataMember="DocumentosNegocio">
        <Columns>
            <asp:BoundField DataField="IdDocumentoNegocio" HeaderText="IdDocumentoNegocio" Visible="False" />
            <asp:BoundField DataField="IdDatosNegocio" HeaderText="IdDatosNegocio" Visible="False" />
            <asp:BoundField DataField="TipoRegistros" HeaderText="Documento">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="Documento">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("TipoRegistros") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="20%" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="Institucion" HeaderText="Institucion">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaEmision" HeaderText="Fecha Emision" DataFormatString="{0:d}">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" Text="Editar" CommandName="Editar" OnClick="btnEditar_Click" runat="server" />
                    <itemstyle width="8%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        Text="Delete" CommandName="Delete" OnClick="btn_Click" runat="server" />
                    <itemstyle width="8%" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Documento:</span>
            <asp:TextBox ID="txtDocumento" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Institución:</span>
            <asp:TextBox ID="txtInstitucion" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Fecha Emision:</span>
            <asp:TextBox ID="txtFechaEmision" CssClass="form-control required" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtFechaEmision_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaEmision" Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Fecha Vencimiento:</span>
            <asp:TextBox ID="txtFechaVencimiento" CssClass="form-control required" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtFechaVencimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaVencimiento" Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required" />
        </div>
    </asp:Panel>
</div>
