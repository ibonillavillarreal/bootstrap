<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="Feriado.aspx.cs" Inherits="Acciona.Catalogos.Feriado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    Administracion de Dias Feriados
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
        <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdFeriado"
            EmptyDataText="No se encontró ningun dato registrado" DataMember="Feriados">
            <Columns>
                <asp:BoundField DataField="IdFeriado" HeaderText="IdFrecuencia" Visible="False" />
                <asp:BoundField DataField="Descripcion" HeaderText="Nombre">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="Dia" HeaderText="Dia">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Mes" HeaderText="Mes">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Anio" HeaderText="Año">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Departamento" HeaderText="Departamento">
                    <ItemStyle Width="18%" />
                </asp:BoundField>
                <asp:BoundField DataField="Municipio" HeaderText="Municipio">
                    <ItemStyle Width="18%" />
                </asp:BoundField>               
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" CommandName="Editar" OnClick="btnEditar_Click" runat="server" />
                        <itemstyle width="2%" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                            CommandName="Delete" OnClick="btn_Click" runat="server" />
                        <itemstyle width="2%" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <%--<asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>--%>
        <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">

            <div class="input-group" style="width: 78%;">
                <span class="input-group-addon" style="width: 150px">Departamento:</span>
                <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-control" Width="100%">
                    <asp:ListItem Value="Managua">Managua</asp:ListItem>
                    <asp:ListItem Value="Masaya">Masaya</asp:ListItem>
                    <asp:ListItem Value="Granada">Granada</asp:ListItem>
                    <asp:ListItem Value="Carazo">Carazo</asp:ListItem>  
                    <asp:ListItem Value="NACIONAL">NACIONAL</asp:ListItem>                 
                </asp:DropDownList>
            </div>
            <div class="input-group" style="width: 78%;">
                <span class="input-group-addon" style="width: 150px">Municipio:</span>
                <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-control" Width="100%">
                    <asp:ListItem Value="Managua">Managua</asp:ListItem>
                    <asp:ListItem Value="Ciudad Sandino">Ciudad Sandino</asp:ListItem>
                    <asp:ListItem Value="Tipitapa">Tipitapa</asp:ListItem>
                    <asp:ListItem Value="Ticuantepe">Ticuantepe</asp:ListItem>
                    <asp:ListItem Value="NACIONAL">NACIONAL</asp:ListItem>                    
                </asp:DropDownList>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Dia:</span>
                <asp:TextBox ID="txtDia" CssClass="form-control required" runat="server" Width="100%"></asp:TextBox>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Mes:</span>
                <asp:TextBox ID="txtMes" CssClass="form-control required" runat="server" Width="100%"></asp:TextBox>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Año:</span>
                <asp:TextBox ID="txtAnio" CssClass="form-control required" runat="server" Width="100%"></asp:TextBox>
            </div>
            <div class="input-group">
                <span class="input-group-addon" style="width: 150px">Nombre:</span>
                <asp:TextBox ID="txtDescripcion" CssClass="form-control required" runat="server" Width="100%"></asp:TextBox>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
