<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReferenciasCrediticias.ascx.cs" Inherits="Acciona.Clientes.Controles.ucReferenciasCrediticias" %>
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
        <asp:Literal ID="litmensaje" Text="" runat="server" />
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdReferenciaCrediticia" EmptyDataText="No se encontró ningun dato registrado" DataMember="Referencia">
        <Columns>
            <asp:BoundField DataField="IdReferenciaCrediticia" HeaderText="IdReferenciaCrediticia" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:BoundField DataField="Banco" HeaderText="Institución Crediticia">
                <ItemStyle Width="50%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="Institución Crediticia">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("Banco") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="20%" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:N2}">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                <ItemStyle Width="6%" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" Text="Editar" CommandName="Editar" onclick="btnEditar_Click" runat="server" />
                    <ItemStyle Width="10%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        Text="Delete" CommandName="Delete" onclick="btn_Click" runat="server" />
                    <ItemStyle Width="10%" />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    <asp:GridViewExtender ID="geDatos" TargetControlID="gvDatos" runat="server"></asp:GridViewExtender>
    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Institución Crediticia:</span>
            <asp:TextBox ID="txtBanco" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Monto:</span>
            <asp:TextBox ID="txtMonto" CssClass="form-control required dinero" css="dinero" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Plazo:</span>
            <asp:TextBox ID="txtPlazo" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control required"/>
        </div>
    </asp:Panel>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });
        });
    </script>--%>
</div>