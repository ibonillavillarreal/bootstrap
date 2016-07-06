<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Paises.aspx.cs" Inherits="SisSegLT.Administracion.Paises" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><small><a href="../" class="glyphicon glyphicon-home"></a></small>Países</h2>
    <h3>
        <asp:Literal ID="litAyuda" runat="server"></asp:Literal></h3>
        <asp:Literal ID="litmensaje" Text="" runat="server" />
    <!-- PAISES BARRA -->
    <div class="btn-toolbar" role="toolbar">
        <div class="btn-group">
            <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
            <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
            <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar_Click" />
            <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbCiudad" SkinID="imbCiudad" runat="server" CssClass="btn btn-default" OnClick="imbCiudad_Click" />
        </div>
        <div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click" />
        </div>
    </div>
    <!-- PAISES GRID -->
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdPais" EmptyDataText="No se encontraron datos que mostrar" DataMember="Pais">
        <Columns>
            <asp:BoundField DataField="IdPais" HeaderText="IdPais" Visible="False" />
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad">
                <ItemStyle Width="25%" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>
    <!-- PAISES EDICIÓN -->
    <asp:Panel ID="pnlAgregar" runat="server" Visible="False" Width="100%">
        <div class="input-group">
            <span class="input-group-addon">Nombre:</span>
            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon">Nacionalidad:</span>
            <asp:TextBox ID="txtNacionalidad" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlCiudad" runat="server">
        <h3>
            <asp:Literal ID="litAyuda2" runat="server"></asp:Literal></h3>
        <!-- CIUDADES BARRA -->
        <div class="btn-toolbar" role="toolbar">
            <div class="btn-group">
                <asp:ImageButton ID="imbAgregar2" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar2_Click" />
                <asp:ImageButton ID="imbEditar2" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar2_Click" />
                <asp:ImageButton ID="imbCancelar2" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar2_Click" />
                <asp:ImageButton ID="imbGuardar2" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar2_Click" />
                <asp:ImageButton ID="imbActualizar2" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar2_Click" />
            </div>
            <div class="btn-group">
                <asp:ImageButton ID="imbEliminar2" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar2_Click" />
            </div>
            <div class="btn-group pull-right">
                <asp:ImageButton ID="imbCerrarCiudad" SkinID="imbCerrar" runat="server" CssClass="btn btn-danger" OnClick="imbCerrar2_Click" />
            </div>
        </div>
        <!-- CIUDADES GRID -->
        <asp:GridView ID="gvHijo" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdCiudad" EmptyDataText="No se encontraron datos que mostrar" DataMember="Ciudad">
            <Columns>
                <asp:BoundField DataField="IdCiudad" HeaderText="IdCiudad" Visible="False" />
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar2_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridViewExtender ID="GridViewExtender1" TargetControlID="gvHijo" runat="server"></asp:GridViewExtender>
        <!-- CIUDADES EDICIÓN -->
        <asp:Panel ID="pnlAgregar2" runat="server" Visible="False" Width="100%">
            <div class="input-group">
                <span class="input-group-addon">Nombre:</span>
                <asp:TextBox ID="txtNombre2" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </asp:Panel>
    </asp:Panel>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#MainContent_gvDatos').dataTable({
                "sDom": "lftip",
                "bStateSave": false,
                "splaceHolder": "",
                "iDisplayLength": 0,
                "bDestroy": true,
                "aLengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Todos"]]
            });
        });
    </script>--%>
</asp:Content>