<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="SisSegLT.Administracion.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><small><a href="../" class="glyphicon glyphicon-home"></a></small>  Roles</h2>
    <h3><asp:Literal ID="litAyuda" runat="server"></asp:Literal></h3>
    <div class="btn-toolbar" role="toolbar">
        <div class="btn-group">
            <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
            <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
            <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
            <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar_Click" />
            <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
        </div>
        <div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click"/>
        </div>
        <asp:Literal ID="litmensaje" Text="" runat="server" />
    </div>
    <div class="container">
            <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdRol" EmptyDataText="No se encontraron datos que mostrar" DataMember="Rol"
                CssClass="table table-hover table-condensed" >
                <Columns>
                    <asp:BoundField DataField="IdRol" HeaderText="IdRol" Visible="False" />
                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Nombre") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FechaRegistro" DataFormatString="{0:d}" HeaderText="Fecha de Registro">
                        <ItemStyle Width="25%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
	</div>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="input-group">
            <span class="input-group-addon">Nombre del rol:</span>
            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </asp:Panel>
    
    <script>
		$(document).ready(function(){
			$('<%# gvDatos.ClientID %>').DataTable();
		});
	</script>
</asp:Content>
