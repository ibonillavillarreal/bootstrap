<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="RecalcularCredito.aspx.cs" Inherits="Acciona.Credito.RecalcularCredito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Modificar Movimiento</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control" placeholder="Codigo" required="true" />

                    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default " OnClick="btnBuscar_Click" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <div role="navigation" class="navbar navbar-default">
        <!-- Brand and toggle get grouped for better mobile display -->

        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapses" class="collapse navbar-collapse">
            <div class="navbar-form navbar-left" role="search">
                <div class="btn-toolbar" role="toolbar">
                    <div class="form-group">
                        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
                        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click"/>
                           <%-- OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Recalculando...';"
                            UseSubmitBehavior="false" --%>

                        <%--<asp:ImageButton ID="imbImprimir" SkinID="imbImprimir" runat="server" CssClass="btn btn-default" OnClick="imbImprimir_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <div class="col-md-6">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 50px">Cliente:</span>
                <asp:Literal Text="" ID="litCliente" runat="server" />
            </div>
        </div>
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <asp:Button ID="btnRecalcular" Text="Recalcular Cuenta" runat="server" CssClass="btn" OnClick="btnRecalcular_Click" />
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <asp:HiddenField ID="hfSelectedTab" runat="server" />
    <asp:HiddenField ID="hfTabContacto" runat="server" />
    <asp:HiddenField ID="hfIdMovimiento" runat="server" />
    <asp:HiddenField ID="hfIdCliente" runat="server" />
    <asp:HiddenField ID="hfIdCuenta" runat="server" />
    <div class="box-name">
        <i class="fa fa-table input-sm"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>

    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdMovimiento"
        EmptyDataText="No se encontró ningun dato registrado" DataMember="tMovimientos" ShowFooter="True" Caption="Movimientos de la Cuenta"
        OnRowCommand="gvDatos_RowCommand">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="IdMovimiento" Visible="false" />
            <asp:BoundField DataField="NoMovimiento" HeaderText="No.Movimiento" />
            <asp:BoundField DataField="MontoTransaccion" HeaderText="Principal" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Salvamento" HeaderText="Salvamento" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Canon" HeaderText="Canon" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="PlazoMeses" HeaderText="Plazo" />
            <asp:BoundField DataField="Interes" HeaderText="Interes" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="tFrecuencia.Descripcion" HeaderText="Frecuencia" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="FechaEfectiva" HeaderText="Fecha Efectiva" DataFormatString="{0:d}" />
        </Columns>

    </asp:GridView>

    <div id="content">
        <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Monto:</span>
                    <asp:TextBox ID="txtMonto" CssClass="form-control input-sm dinero" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">No. Cuotas:</span>
                    <asp:TextBox ID="txtNoCuotas" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Frecuencia:</span>
                    <asp:DropDownList ID="ddlFrecuencia" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
            </div>

        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Promotor:</span>
                    <asp:DropDownList ID="ddlPromotor" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Tipo de Interes:</span>
                    <asp:DropDownList ID="ddlTipoInteres" runat="server" CssClass="form-control input-sm" Width="100%">
                        <asp:ListItem Value="0" Selected="True">Fijo</asp:ListItem>
                        <asp:ListItem Value="1">Sobre Saldo</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Colector:</span>
                    <asp:DropDownList ID="ddlColector" runat="server" CssClass="form-control input-sm" Width="100%">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">% Interes:</span>
                    <asp:TextBox ID="txtInteres" CssClass="form-control dinero input-sm" runat="server"> </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 78%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Desembolsa:</span>
                    <asp:DropDownList ID="ddlDesembolsa" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Plazo en Meses:</span>
                    <asp:TextBox ID="txtPlazoMeses" CssClass="form-control dinero input-sm" runat="server"></asp:TextBox>
                    <%--<asp:Button ID="btnComision" runat="server" Text="..." OnClientClick="operaciones('multiplicar');"/>--%>
                    <asp:Button ID="btnComision" Text="..." runat="server" CssClass="btn btn-default" OnClientClick="return operaciones('multiplicar');" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Fecha Movimiento:</span>
                    <asp:TextBox ID="txtFechaMovimiento" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtFechaMovimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaMovimiento" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-md-6">
                <div class="input-group" style="width: 100%;">
                    <span class="input-group-addon input-sm" style="width: 150px">Comision:</span>
                    <asp:TextBox ID="txtComision" CssClass="form-control dinero input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>

    </div>

    <script type="text/javascript">
        //<![CDATA[
        function operaciones(op) {

            var ops = {
                sumar: function sumarNumeros(n1, n2) {
                    return (parseInt(n1) + parseInt(n2));
                },

                restar: function restarNumeros(n1, n2) {
                    return (parseInt(n1) - parseInt(n2));
                },

                multiplicar: function multiplicarNumeros(n1, n2, n3) {
                    var n4 = parseFloat(n2) / 100;
                    return (parseFloat(n1) * parseFloat(n4) * parseFloat(n3));
                },

                dividir: function dividirNumeros(n1, n2) {
                    return (parseInt(n1) / parseInt(n2));
                }


            };

            var num1 = document.getElementById('<%=txtMonto.ClientID %>').value;
            var num2 = document.getElementById('<%=txtInteres.ClientID %>').value;
            var num3 = document.getElementById('<%=txtPlazoMeses.ClientID %>').value;


            //Comprobamos si se ha introducido números en las cajas
            if (isNaN(parseFloat(document.getElementById('<%=txtMonto.ClientID %>').value))) {
                alert("Indique el monto");
                document.getElementById('<%=txtMonto.ClientID %>').innerText = "0";
                document.getElementById('<%=txtMonto.ClientID %>').focus();
            } else if (isNaN(parseFloat(document.getElementById('<%=txtInteres.ClientID %>').value))) {
                alert("Indique el interes");
                document.getElementById('<%=txtInteres.ClientID %>').innerText = "0";
                    document.getElementById('<%=txtInteres.ClientID %>').focus();
                }
                else if (isNaN(parseFloat(document.getElementById('<%=txtPlazoMeses.ClientID %>').value))) {
                    alert("Indique el plazo");
                    document.getElementById('<%=txtPlazoMeses.ClientID %>').innerText = "0";
                    document.getElementById('<%=txtPlazoMeses.ClientID %>').focus();
                }
                else {
                    //Si se han introducido los números en ámbas cajas, operamos:
                    switch (op) {
                        case 'sumar':
                            var resultado = ops.sumar(num1, num2);
                            alert(resultado);
                            break;
                        case 'restar':
                            var resultado = ops.restar(num1, num2);
                            alert(resultado);
                            break;
                        case 'multiplicar':
                            var resultado = ops.multiplicar(num1, num2, num3);
                            //alert(resultado);
                            document.getElementById('<%=txtComision.ClientID %>').value = parseFloat(parseFloat(parseFloat(resultado).toFixed(2) * 100) / 100).toFixed(2);

                            break;
                        case 'dividir':
                            var resultado = ops.dividir(num1, num2);
                            alert(resultado);
                            break;

                    }
                }

    }



    //]]>
    </script>
</asp:Content>
