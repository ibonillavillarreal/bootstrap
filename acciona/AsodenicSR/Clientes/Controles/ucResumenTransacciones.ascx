<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucResumenTransacciones.ascx.cs" Inherits="Acciona.Clientes.Controles.ucResumenTransacciones" %>
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
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdTransaccionesInstitucion" EmptyDataText="No se encontró ningun dato registrado" DataMember="ResumenTransaccion" OnDataBound="gvDatos_DataBound">
        <Columns>
            <asp:BoundField DataField="IdTransaccionesInstitucion" HeaderText="IdTransaccionesInstitucion" Visible="False" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
            <asp:BoundField DataField="NoPrestamo" HeaderText="No. Credito">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <%--<asp:TemplateField HeaderText="No Credito">
                <ItemTemplate>
                    <asp:LinkButton ID="NoPrestamo" runat="server" Text='<%# Eval("NoPrestamo") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="10%" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="Metodologia.Nombre" HeaderText="Metodologia">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaInicioCredito" HeaderText="Fecha Inicio" DataFormatString="{0:d}">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaFinCredito" HeaderText="Fecha Fin" DataFormatString="{0:d}">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="MontoPromedio" HeaderText="Monto" DataFormatString="{0:N2}" >
                <ItemStyle Width="10%" />
            </asp:BoundField>  
            <asp:BoundField DataField="MaximoDiasMora" HeaderText="Dias de Mora">
                <ItemStyle Width="5%" />
            </asp:BoundField>  
            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones">
                <ItemStyle Width="3%" />
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
            <span class="input-group-addon" style="width: 150px">No. Credito:</span>
            <asp:TextBox ID="txtNoCredito" CssClass="form-control required" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Metodologia:</span>
            <asp:DropDownList ID="ddlMetodologia" runat="server" CssClass="form-control required"></asp:DropDownList>
        </div>
        <div class="input-group"> 
            <span class="input-group-addon" style="width: 150px">Fecha Inicio Credito:</span>
            <asp:TextBox ID="txtFechaInicio" CssClass="form-control required" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender runat="server" Enabled="True" TargetControlID="txtFechaInicio" ID="txtFechaInicio_CalendarExtender" Format="dd/MM/yyyy">

            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Fecha Cancelacion:</span>
            <asp:TextBox ID="txtFechaFin" CssClass="form-control required" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender runat="server" Enabled="True" TargetControlID="txtFechaFin" ID="txtFechaFin_CalendarExtender" Format="dd/MM/yyyy">

            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Monto:</span>
            <asp:TextBox ID="txtMonto" CssClass="form-control required dinero" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Mora (Dias):</span>
            <asp:TextBox ID="txtMora" CssClass="form-control required number" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Observaciones:</span>
            <asp:TextBox ID="txtObservaciones" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon" style="width: 150px">Activo:</span>
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control"/>
        </div>
    </asp:Panel>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });
        });
    </script>--%>
</div>