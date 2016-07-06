<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="TipoTransacciones.aspx.cs" Inherits="Acciona.Catalogos.TipoTransacciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    Tipo de Transacciones
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
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

        </div>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
        <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdTipoTransaccion"
            EmptyDataText="No se encontró ningun dato registrado" DataMember="TipoTransaccion">
            <Columns>
                <asp:BoundField DataField="IdTipoTransaccion" HeaderText="IdTipoTransaccion" Visible="False" />
                <asp:BoundField DataField="CuentaContable" HeaderText="Cuenta Contable">
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="ContraCuenta" HeaderText="Contra Cuenta">
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="Concepto" HeaderText="Concepto">
                    <ItemStyle Width="20%" />
                </asp:BoundField>
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
                <span class="input-group-addon" style="width: 150px">Cuenta Contable:</span>
                <asp:TextBox ID="txtCuentaContable" CssClass="form-control required" runat="server" Width="200%"></asp:TextBox>
            </div>
             <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Contra Cuenta:</span>
                <asp:TextBox ID="txtContraCuenta" CssClass="form-control required" runat="server" Width="200%"></asp:TextBox>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Descripcion:</span>
                <asp:TextBox ID="txtDescripcion" CssClass="form-control required" runat="server" Width="200%"></asp:TextBox>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Concepto:</span>
                <asp:TextBox ID="txtConcepto" CssClass="form-control required" runat="server" Width="200%"></asp:TextBox>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Activo:</span>
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
