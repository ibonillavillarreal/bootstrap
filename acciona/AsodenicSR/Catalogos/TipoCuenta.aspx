<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="TipoCuenta.aspx.cs" Inherits="Acciona.Catalogos.TipoCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    Tipo de Cuentas
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
        <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdTipoCuenta"
            EmptyDataText="No se encontró ningun dato registrado" DataMember="tTipoCuenta">
            <Columns>
                <asp:BoundField DataField="IdTipoCuenta" HeaderText="IdTipoCuenta" Visible="False" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="Prefijo" HeaderText="Prefijo Cuenta">
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="Numero" HeaderText="Consecutivo Cuenta">
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="PrefijoPlastico" HeaderText="Prefijo Plastico">
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="ConsecutivoPlastico" HeaderText="Consecutivo Plastico">
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
                <span class="input-group-addon" style="width: 150px">Descripcion:</span>
                <asp:TextBox ID="txtDescripcion" CssClass="form-control required" runat="server" Width="200%"></asp:TextBox>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Prefijo Cuenta:</span>
                <asp:TextBox ID="txtPrefijo" CssClass="form-control required" runat="server" Width="200%"></asp:TextBox>
            </div>           
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Prefijo Plastico:</span>
                <asp:TextBox ID="txtPrefijoPlastico" CssClass="form-control required" runat="server" Width="200%"></asp:TextBox>
            </div>            
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Activo:</span>
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
