<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="PagosPymeColector.aspx.cs" Inherits="Acciona.Pagos.PagosPymeColector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Pagos Cuotas PYME Colector</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                        <div class="col-md-5">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 70px">Colector:</span>
                                <asp:DropDownList ID="ddlColector" runat="server" CssClass="form-control required input-sm" Width="100%" required="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 70px">Fecha de Cobro:</span>
                                <asp:TextBox ID="txtFechaCobro" CssClass="form-control required input-sm" placeholder="Fecha de Cobro" runat="server" required="true"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFechaCobro_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaCobro" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group" style="width: 100%;">
                                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn input-sm" OnClick="btnBuscar_Click" />

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group" style="width: 100%;">
                                <%--<asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default " runat="server" />--%>
                            </div>
                        </div>
                    </div>
                    <%--OnClick="btnBuscar_Click"--%>
                    <%--<asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control input-sm" placeholder="No. Identificación" required="true" />--%>


                    <%--<asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default " runat="server" OnClick="btImprimir_Click" />--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="container">
        <div class="box-name">
            <i class="fa fa-table"></i>
            <asp:Literal Text="" ID="litmensaje" runat="server" />
        </div>
        <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">No Serie:</span>
                    <asp:TextBox ID="txtSerie" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn input-sm" OnClick="btnGuardar_Click" OnClientClick="javascript:return confirm('Esta seguro que desea guardar los registros?');"/>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlGrid" runat="server">
            <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdCuenta,IdCobro"
                EmptyDataText="No se encontró ningun dato registrado" DataMember="RutaCobro" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="IdCobro" Visible="false" />
                    <asp:BoundField DataField="IdCuenta" Visible="false" />
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" ReadOnly="true" />
                    <asp:BoundField DataField="NoIdentificacion" HeaderText="Identificacion" ReadOnly="true" />
                    <asp:BoundField DataField="NoCuenta" HeaderText="NoCuenta" ReadOnly="true" FooterText="Total:" />
                    <%--<asp:BoundField DataField="Direccion" HeaderText="Direccion" ReadOnly="true"/>--%>
                    <%--<asp:BoundField DataField="Frecuencia" HeaderText="Direccion" FooterText="Total:" ReadOnly="true"/>--%>
                    <asp:BoundField DataField="CuotadelDia" HeaderText="Cuota Dia" DataFormatString="{0:N2}" ReadOnly="true" />
                    <%--<asp:BoundField DataField="MontoRecibido" HeaderText="Monto Recibido" DataFormatString="{0:N2}" ApplyFormatInEditMode="True" />--%>
                    <asp:TemplateField HeaderText="Monto Recibido">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMontoRecibido" runat="server" Text='<%# Eval("MontoRecibido", "{0:N}") %>' class="cantidad"/>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LBLTotal" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Diferencia" HeaderText="Diferencia" DataFormatString="{0:N2}" ReadOnly="true" />
                    <%--<asp:BoundField DataField="NoRecibo" HeaderText="ROC" ApplyFormatInEditMode="True" />--%>
                    <asp:TemplateField HeaderText="NoRecibo">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRecibo" runat="server" Text='<%# Eval("NoRecibo") %>' class="cantidad" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#99CCFF" Font-Bold="True" />
            </asp:GridView>
        </asp:Panel>

        <script>
            $("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });
            $(document).ready(function () {

                $("#<%=gvDatos.ClientID%> [id*='txtMontoRecibido']").change(function () {

                var tr = $(this).parent().parent();
                var precio = $("td:eq(3)", tr).html();
                var monto = $("td:eq(4)", tr).html();

                $("td:eq(5)", tr).html(parseFloat($(this).val() - precio).toFixed(2));

                CalcularTotal();

                });

                $(window).keydown(function (event) {
                    if (event.keyCode == 13) {
                        event.preventDefault();
                        return false;
                    }
                });

        });



        //        calculo total sin paginado             
            function CalcularTotal() {

                            var total = 0;
                            $("#<%=gvDatos.ClientID%> [id*='txtMontoRecibido']").each(function() {
                                var tr = $(this).parent().parent();
                                var monto = $(this).val();
                                monto = monto.replace(',', '');
                                var coltotal = parseFloat(monto);
                                total += coltotal;
                                //if (!isNaN(coltotal)) {
                                //    total += coltotal;
                                //}

                            });

                $("#<%=gvDatos.ClientID%> [id*='LBLTotal']").html(total);
            }

            

        </script>
    </div>
</asp:Content>
