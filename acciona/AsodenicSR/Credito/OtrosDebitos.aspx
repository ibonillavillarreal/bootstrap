<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="OtrosDebitos.aspx.cs" Inherits="Acciona.Credito.OtrosDebitos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Otros Debitos</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <span class="input-group-addon" style="width: 150px">Parametros:</span>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control required" Width="100%">
                                <asp:ListItem Value="0" Selected="True">Cuenta</asp:ListItem>
                                <asp:ListItem Value="1">Identificacion</asp:ListItem>                                
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="input-group" style="width: 100%;">
                            <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn" OnClick="btnBuscar_Click" />
                        <%--<asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default " runat="server" OnClick="btImprimir_Click" />--%>
                    </div>
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
        <%-- <div role="navigation" class="navbar navbar-default">
            <!-- Brand and toggle get grouped for better mobile display -->

            <!-- Collection of nav links, forms, and other content for toggling -->
            <div id="navbarCollapses" class="collapse navbar-collapse ">
                <div class="navbar-form navbar-left" role="search">
                    <div class="btn-toolbar" role="toolbar">
                        <div class="btn-group">
                            
                            <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" />
                            <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" />
                           
                        </div>                        
                    </div>
                </div>
            </div>
        </div>--%>


        <div id="content">

            <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                <li class="active"><a href="#red" data-toggle="tab">Datos Generales</a></li>
                <li><%--<a href="#orange" data-toggle="tab">Datos del Debito</a>--%></li>

            </ul>
            <div class="tab-content" id="tab-content">
                <div class="tab-pane active" id="red">

                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Nombre Completo:</span>
                                <asp:TextBox ID="txtNombre" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Tipo Transaccion:</span>
                                <asp:DropDownList ID="ddlTipoTransaccion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoTransaccion_SelectedIndexChanged" OnTextChanged="ddlTipoTransaccion_TextChanged" ></asp:DropDownList>
                            </div>
                        </div>
                        
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Identificacion:</span>
                                <asp:TextBox ID="txtIdentificacion" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                       <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Monto:</span>
                                <asp:TextBox ID="txtMonto" CssClass="form-control input-sm dinero" runat="server" ></asp:TextBox>
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
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Frecuencia:</span>
                                <asp:DropDownList ID="ddlFrecuencia" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Monto Autorizado:</span>
                                <asp:TextBox ID="txtMontoAutorizado" CssClass="form-control dinero input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                       <%-- <div id='foo' class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">1er Dia de Pago:</span>
                                <asp:DropDownList ID="ddlDias" runat="server" CssClass="form-control required input-sm" Width="100%">
                                    <asp:ListItem Value="0">Lunes</asp:ListItem>
                                    <asp:ListItem Value="1">Martes</asp:ListItem>
                                    <asp:ListItem Value="2">Miercoles</asp:ListItem>
                                    <asp:ListItem Value="3">Jueves</asp:ListItem>
                                    <asp:ListItem Value="4">Viernes</asp:ListItem>
                                    <asp:ListItem Value="5">Sabado</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                    </div>
                    <%--<div class="row" style="margin-bottom: 5px;">
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
                                    <asp:ListItem Value="0">Fijo</asp:ListItem>
                                    <asp:ListItem Value="1">Sobre Saldo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Autorizante:</span>
                                <asp:DropDownList ID="ddlAutorizante" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <%--<div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">% Interes:</span>
                                <asp:TextBox ID="txtInteres" CssClass="form-control dinero input-sm" runat="server"> </asp:TextBox>
                            </div>
                        </div>--%>
                    </div>
                     <%--<div class="row" style="margin-bottom: 5px;">
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
                               <asp:Button ID="btnComision" runat="server" Text="..." OnClientClick="operaciones('multiplicar');"/>
                                <asp:Button ID="btnComision" Text="..." runat="server" CssClass="btn btn-default" OnClientClick="return operaciones('multiplicar');" />
                            </div>
                        </div>
                    </div>--%>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Fecha Movimiento:</span>
                                <asp:TextBox ID="txtFechaMovimiento" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFechaMovimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaMovimiento" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                       <%-- <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Comision:</span>
                                <asp:TextBox ID="txtComision" CssClass="form-control dinero input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>--%>
                    </div>
                    <%-- <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Colector:</span>
                                <asp:DropDownList ID="ddlColector" runat="server" CssClass="form-control input-sm" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                           <div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Saldo:</span>
                                <asp:TextBox ID="txtSaldo" CssClass="form-control required dinero input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-md-6">
                            <div class="input-group" style="width: 100%;">
                            </div>
                        </div>
                        <div class="col-md-6">
                            <%--<div class="input-group" style="width: 78%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Estado Transaccion:</span>
                                <asp:DropDownList ID="ddlEstadoTransaccion" runat="server" CssClass="form-control required input-sm" Width="100%">
                                    <asp:ListItem Value="0">Activa</asp:ListItem>
                                    <asp:ListItem Value="1">Aprobada</asp:ListItem>
                                    <asp:ListItem Value="2">Rechazada</asp:ListItem>
                                    <asp:ListItem Value="3">Cancelada</asp:ListItem>
                                    <asp:ListItem Value="4">Mora</asp:ListItem>
                                    <asp:ListItem Value="5">Saneada</asp:ListItem>
                                    <asp:ListItem Value="6">Inactiva</asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                        </div>
                    </div>


                </div>

                <div class="tab-pane" id="orange">
                    <%--<h4>Datos del Credito</h4>--%>


                </div>
            </div>

        </div>
       

        <script>



            <%--$('#tabs a').click(function (e) {
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
            $('#tabs a[href="#' + hash + '"]').tab('show');--%>

           


            /*1. Crear un hidden field
            2.  Guardar en el hidden field el valor del tab siguiente*/

           <%-- if ($("#<%=hfTabContacto.ClientID%>").val() != "") {
                var hash = $("#<%= hfTabContacto.ClientID%>").val();
                $('#tabs a[href="#' + hash + '"]').tab('show');

                $("#<%= hfTabContacto.ClientID%>").val("");--%>
            //}
            //$("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });


            $(document).ready(function () {

                /*disable non active tabs*/
                //$('#tabs li').not('.active').addClass('disabled');
                /*to actually disable clicking the bootstrap tab, as noticed in comments by user3067524*/
                //$('#tabs li').not('.active').find('a').removeAttr("data-toggle");
                
            });
        </script>
    </div>
</asp:Content>
