<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="MovimientosPyme.aspx.cs" Inherits="Acciona.Credito.MovimientosPyme" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">

    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Transacciones </a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control input-sm" placeholder="No. Identificación" required="true" />

                    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default " runat="server" OnClick="btImprimir_Click" />
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
                        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click" />
                        <%--<asp:ImageButton ID="imbImprimir" SkinID="imbImprimir" runat="server" CssClass="btn btn-default" OnClick="imbImprimir_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-name">
        <i class="fa fa-table input-sm"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">

    <div class="container">
        <asp:HiddenField ID="hfSelectedTab" runat="server" />
        <asp:HiddenField ID="hfTabContacto" runat="server" />
        <asp:HiddenField ID="hfIdMovimiento" runat="server" />
        <asp:HiddenField ID="hfIdCliente" runat="server" />
        <asp:HiddenField ID="hfIdCuenta" runat="server" />

        <div id="content">

            <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                <li><a href="#carro" data-toggle="tab">Datos Vehiculo</a></li>
                <li class="active"><a href="#red" data-toggle="tab">Datos Generales</a></li>
                <li><a href="#orange" data-toggle="tab">Datos del Credito</a></li>
                <li><a href="#desembolso" data-toggle="tab">Datos de Desembolso</a></li>

            </ul>
            <div class="tab-content" id="tab-content">
                 <%-- -----------------------DATOS VEHICULO------------------------------------%>
                <div class="tab-pane" id="carro">

                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Codigo:</span>
                                <asp:TextBox ID="txtCodCarro" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Marca:</span>
                                <asp:TextBox ID="txtMarca" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Modelo:</span>
                                <asp:TextBox ID="txtModelo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Año:</span>
                                <asp:TextBox ID="txtAnio" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <%----%>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Color:</span>
                                <asp:TextBox ID="txtColor" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Placa:</span>
                                <asp:TextBox ID="txtPlaca" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            <%----%>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Chasis:</span>
                                <asp:TextBox ID="txtChasis" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Motor:</span>
                                <asp:TextBox ID="txtMotor" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            <%----%>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">No.Circulacion:</span>
                                <asp:TextBox ID="txtCirculacion" CssClass="form-control input-sm" runat="server"></asp:TextBox>                               
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Tipo Vehiculo:</span>
                                <asp:TextBox ID="txtTipoVehiculo" CssClass="form-control" runat="server"> </asp:TextBox>
                            </div>
                            <%----%>
                        </div>

                    </div>
                    <%--<div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Meses:</span>
                                <asp:TextBox ID="TextBox8" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                           
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%; display: normal;">
                                <span class="input-group-addon input-sm" style="width: 150px">Cuota Programada:</span>
                                <asp:TextBox ID="TextBox9" CssClass="form-control required dinero input-sm" runat="server"></asp:TextBox>

                                <asp:Button ID="Button1" Text="..." runat="server" CssClass="btn btn-default" OnClick="btnCuota_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Observaciones:</span>
                                <asp:TextBox ID="TextBox10" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Height="150px"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Fecha Movimiento:</span>
                                <asp:TextBox ID="TextBox11" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtFechaMovimiento" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>

                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                           
                        </div>
                        <div id="divCuota" class="col-md-6">
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>--%>

                </div>


                 <%-- -----------------------DATOS GENERALES------------------------------------%>
                <div class="tab-pane active" id="red">

                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Nombre Completo:</span>
                                <asp:TextBox ID="txtNombre" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Identificacion:</span>
                                <asp:TextBox ID="txtIdentificacion" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Disponible:</span>
                                <asp:TextBox ID="txtDisponible" CssClass="form-control dinero input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Monto Autorizado:</span>
                                <asp:TextBox ID="txtMontoAutorizado" CssClass="form-control dinero input-sm" runat="server"></asp:TextBox>
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
                                <span class="input-group-addon input-sm" style="width: 150px">Autorizante:</span>
                                <asp:DropDownList ID="ddlAutorizante" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
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
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Colector:</span>
                                <asp:DropDownList ID="ddlColector" runat="server" CssClass="form-control input-sm" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Origen Fondos:</span>
                                <asp:DropDownList ID="ddlOrigen" runat="server" CssClass="form-control input-sm" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Tipo Cuenta:</span>
                                <asp:DropDownList ID="ddlPlastico" runat="server" CssClass="form-control input-sm" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <%-- -----------------------DATOS CREDITO------------------------------------%>
                <div class="tab-pane" id="orange">

                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Monto:</span>
                                <asp:TextBox ID="txtMonto" CssClass="form-control input-sm dinero" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Monto Transaccion:</span>
                                <asp:TextBox ID="txtMonto2" CssClass="form-control input-sm" runat="server"></asp:TextBox>
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
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Prima:</span>
                                <asp:TextBox ID="txtPrima" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            <%----%>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Tipo de Interes:</span>
                                <asp:DropDownList ID="ddlTipoInteres" runat="server" CssClass="form-control input-sm" Width="100%" onchange="ocultar(this);">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="1">Fijo</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">Sobre Saldo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Valor Salvamento:</span>
                                <asp:TextBox ID="txtSalvamento" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            <%----%>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Tipo Transaccion:</span>
                                <asp:DropDownList ID="ddlTipoTransaccion" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                <%--<asp:Button ID="btnComision" runat="server" Text="..." OnClientClick="operaciones('multiplicar');"/>--%>
                                <%--<asp:Button ID="btnComision" Text="..." runat="server" CssClass="btn btn-default" OnClientClick="return operaciones('multiplicar');" />--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">No. Cuotas:</span>
                                <asp:TextBox ID="txtNoCuotas" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            <%----%>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Fecha Corte:</span>
                                <asp:TextBox ID="txtFechaCorte" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtFechaCorte" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">% Interes:</span>
                                <asp:TextBox ID="txtInteres" CssClass="form-control dinero input-sm" runat="server"> </asp:TextBox>
                            </div>
                            <%----%>
                        </div>

                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Meses:</span>
                                <asp:TextBox ID="txtPlazoAnios" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                           
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%; display: normal;">
                                <span class="input-group-addon input-sm" style="width: 150px">Cuota Programada:</span>
                                <asp:TextBox ID="txtCuota" CssClass="form-control required dinero input-sm" runat="server"></asp:TextBox>

                                <asp:Button ID="btnCuota" Text="..." runat="server" CssClass="btn btn-default" OnClick="btnCuota_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Observaciones:</span>
                                <asp:TextBox ID="txtComentarios" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Height="150px"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Fecha Movimiento:</span>
                                <asp:TextBox ID="txtFechaMovimiento" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFechaMovimiento" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>

                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                           
                        </div>
                        <div id="divCuota" class="col-md-6">
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>

                </div>
                <%-- -----------------------DESEMBOLSO------------------------------------%>
                <div class="tab-pane" id="desembolso">
                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon" style="width: 150px">No.Documento:</span>
                                <asp:TextBox ID="txtNoCheque" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon" style="width: 150px">Banco:</span>
                                <asp:DropDownList ID="ddlBanco" runat="server" CssClass="form-control">
                                    <asp:ListItem>BANPRO</asp:ListItem>
                                    <asp:ListItem>FICOHSA</asp:ListItem>
                                    <asp:ListItem Selected="True">BAC</asp:ListItem>
                                    <asp:ListItem>BANCENTRO</asp:ListItem>
                                    <asp:ListItem>BDF</asp:ListItem>
                                    <asp:ListItem>ST. GEORGE</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon" style="width: 150px">Tipo Documento:</span>
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control">
                                    <asp:ListItem>Cheque</asp:ListItem>
                                    <asp:ListItem>Comprobante Diario</asp:ListItem>
                                    <asp:ListItem>Cuenta Ahorro</asp:ListItem>
                                    <asp:ListItem>Cuenta Corriente</asp:ListItem>
                                    <asp:ListItem>Transferencia</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon" style="width: 150px">Moneda:</span>
                                <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="form-control">
                                    <asp:ListItem>Cordobas</asp:ListItem>
                                    <asp:ListItem>Dolares</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>

    </div>
    <script type="text/javascript">
        //<![CDATA[

        function ocultar(ddlTipoInteres) {

            var selectedText = ddlTipoInteres.options[ddlTipoInteres.selectedIndex].innerHTML;
            var selectedValue = ddlTipoInteres.value;
            //alert("Selected Text: " + selectedText + " Value: " + selectedValue);

            //var e = document.getElementById("ddlTipoInteres");
            //var strUser = e.options[e.selectedIndex].value;

            //var ControlName = document.getElementById(ddlId.id);

            if (selectedValue == "1") {
                document.getElementById('divCuota').style.display = 'none';
            }
            else {
                document.getElementById('divCuota').style.display = '';
            }


        };

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
                    var n5 = n4 * parseFloat(n3);
                    var n6 = parseFloat(n1) * parseFloat(n5);
                    return (n6);
                },

                dividir: function dividirNumeros(n1, n2) {
                    return (parseInt(n1) / parseInt(n2));
                }


            };

            var num1 = document.getElementById('<%=txtMonto.ClientID %>').value;
            num1 = num1.replace(',', '');
            var num2 = document.getElementById('<%=txtInteres.ClientID %>').value;
                <%-- var num3 = document.getElementById('<%=txtPlazoMeses.ClientID %>').value;--%>


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
               <%-- else if (isNaN(parseFloat(document.getElementById('<%=txtPlazoMeses.ClientID %>').value))) {
                    alert("Indique el plazo");
                    document.getElementById('<%=txtPlazoMeses.ClientID %>').innerText = "0";
                    document.getElementById('<%=txtPlazoMeses.ClientID %>').focus();
                }--%>
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
                        document.getElementById('<%=txtPlazoAnios.ClientID %>').value = parseFloat(resultado).toFixed(2);

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



    <script>



        $('#tabs a').click(function (e) {
            e.preventDefault();
            //$(this).tab('show');
        });

        // store the currently selected tab in the hash value
        $("#tabs > li > a").on("shown.bs.tab", function (e) {
            var id = $(e.target).attr("href").substr(1);
            //window.location.hash = id;
            $("#<%= hfSelectedTab.ClientID%>").val(id);
        });

        // on load of the page: switch to the currently selected tab
        var hash = $("#<%= hfSelectedTab.ClientID%>").val();
        $('#tabs a[href="#' + hash + '"]').tab('show');

        //$('#foo').show();


        /*1. Crear un hidden field
        2.  Guardar en el hidden field el valor del tab siguiente*/

           <%-- if ($("#<%=hfTabContacto.ClientID%>").val() != "") {
                var hash = $("#<%= hfTabContacto.ClientID%>").val();
                $('#tabs a[href="#' + hash + '"]').tab('show');

                $("#<%= hfTabContacto.ClientID%>").val("");--%>
        //}
        $("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });


        $(document).ready(function () {

            /*disable non active tabs*/
            //$('#tabs li').not('.active').addClass('disabled');
            /*to actually disable clicking the bootstrap tab, as noticed in comments by user3067524*/
            //$('#tabs li').not('.active').find('a').removeAttr("data-toggle");

        });
    </script>
    </div>
</asp:Content>
