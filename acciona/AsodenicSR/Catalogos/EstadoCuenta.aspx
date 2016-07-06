<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="EstadoCuenta.aspx.cs" Inherits="Acciona.Catalogos.EstadoCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    Estado de Cuentas
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
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdEstadoCuenta" 
        EmptyDataText="No se encontró ningun dato registrado" DataMember="tEstadoCuenta">
        <Columns>
            <asp:BoundField DataField="IdEstadoCuenta" HeaderText="IdEstadoCuenta" Visible="False" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                <ItemStyle Width="40%" />
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
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" CommandName="Editar" onclick="btnEditar_Click" runat="server" />
                    <ItemStyle Width="10%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        CommandName="Delete" onclick="btn_Click" runat="server" />
                    <ItemStyle Width="10%" />
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
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required" />
        </div>
    </asp:Panel>
</div>
</asp:Content>
