<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNegocio.ascx.cs" Inherits="Acciona.Clientes.Controles.ucNegocio" %>
<div class="container form">
    <asp:Literal ID="litmensaje" Text="" runat="server" />

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
        <asp:Literal ID="Literal1" Text="" runat="server" />
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdDatosNegocio" EmptyDataText="No se encontró ningun dato registrado" DataMember="DatosNegocio">
        <Columns>
            <asp:BoundField DataField="IdDatosNegocio" HeaderText="IdDatosNegocio" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:TemplateField HeaderText="UbicacionNegocio">
                <ItemTemplate>
                    <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("UbicacionNegocio") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="65%" />
            </asp:TemplateField>
            <asp:CheckBoxField DataField="Alquila" HeaderText="Alquila" ReadOnly="True" />
            <asp:CheckBoxField DataField="EsPropio" HeaderText="Propio" ReadOnly="True" />
            <asp:CheckBoxField DataField="Familiar" HeaderText="Familiar" ReadOnly="True" />
            <asp:BoundField DataField="Tiempo" HeaderText="Antiguedad Negocio" />
            <asp:BoundField DataField="IngresoVolumen" HeaderText="Ingresos" />
            <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlAgregar" runat="server" CssClass="ordenPanel" Visible="False">
        <div class="table-responsive">
            <table class="table table-condensed">
                <tr>
                    <th>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 150px">Tipo de Negocio:</span>
                            <asp:TextBox ID="txtTipoNegocio" CssClass="form-control required" runat="server"></asp:TextBox>
                        </div>
                    </th>
                    <th>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 150px">Ubicacion:</span>
                            <asp:TextBox ID="txtUbicacion" CssClass="form-control required" runat="server"></asp:TextBox>
                        </div>
                    </th>
                </tr>
                <tr>
                    <th>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 150px">Tiene Registros:</span>
                            <asp:CheckBox ID="chkRegistros" runat="server" class="input-group-addon" />
                        </div>
                    </th>
                    <th>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 150px">Lugar:</span>
                            <asp:RadioButton ID="rbFamiliar" runat="server" Text="Familiar" />
                            <asp:RadioButton ID="rbPropia" runat="server" Text="Propia" />
                            <asp:RadioButton ID="rbAlquilada" runat="server" Text="Alquilada" />
                        </div>
                    </th>
                </tr>
                <tr>
                    <th>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 150px">Ingreso Anual:</span>
                            <asp:TextBox ID="txtIngreso" CssClass="form-control required dinero" css="dinero" runat="server"></asp:TextBox>
                        </div>
                    </th>
                    <th>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 150px">Antiguedad Negocio:</span>
                            <asp:TextBox ID="txtTiempo" CssClass="form-control number required" runat="server"></asp:TextBox>
                        </div>
                    </th>
                </tr>
                <tr>
                    <th>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 150px">Activo:</span>
                            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control"/>
                        </div>
                    </th>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });
        });
    </script>--%>
</div>
