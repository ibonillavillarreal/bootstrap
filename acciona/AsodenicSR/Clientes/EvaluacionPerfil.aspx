<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="EvaluacionPerfil.aspx.cs" Inherits="Acciona.Clientes.EvaluacionPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <a href="#" class="navbar-brand">Evaluación de Riesgo</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">                
                    <div class="form-group">
                        <asp:TextBox ID="txtBuscar" runat="server" placeholder="No. Identificación" CssClass="form-control required" />
                    </div>
                    <asp:Button ID="btnBuscar" Text="Buscar" CssClass="btn btn-default submit" runat="server" OnClick="btnBuscar_Click" />                   
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <%-- <style type="text/css">
        .navbar {
            position: static;
        }

            .navbar .nav > li {
                z-index: 1001;
            }
    </style>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <asp:Literal Text="" ID="litmensaje" runat="server" />
    <div class="container form">
        <div class="btn-toolbar" role="toolbar">
            <div class="btn-group">
                <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" Visible="false" OnClick="imbAgregar_Click" />
                <%-- <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" />--%>
                <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" Visible="false" OnClick="imbCancelar_Click" />
                <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" Visible="false" OnClick="imbGuardar_Click" />
                <asp:ImageButton ID="imbActualizar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" Visible="false" OnClick="imbActualizar_Click" />
            </div>
            <%--<div class="btn-group">
            <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click" />
        </div>--%>
        </div>
        <asp:Panel ID="pnlGridview" runat="server" class="well container">
            <asp:GridView ID="grvEvaluaciones" runat="server" AutoGenerateColumns="False" DataKeyNames="IdClienteEvaluacion" SkinID="sGridPrincipal" EmptyDataText="No se encontró ningun dato registrado">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEditar" runat="server" Text="Editar" CommandArgument='<%# Eval("IdClienteEvaluacion") %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbImprimir" runat="server" Text="Imprimir" CommandArgument='<%# Eval("IdClienteEvaluacion") %>' OnClick="lbImprimir_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IdClienteEvaluacion" HeaderText="IdClienteEvaluacion" SortExpression="IdClienteEvaluacion" Visible="false" />
                    <asp:BoundField DataField="Cliente.NombreCompleto" HeaderText="Nombre Cliente" SortExpression="IdCliente" />
                    <asp:BoundField DataField="Usuario1.Nombre" HeaderText="Nombre Usuario" SortExpression="IdUsuario" />
                    <asp:BoundField DataField="Metodologia.Nombre" HeaderText="Metodologia" SortExpression="IdMetodologia" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" Visible="false" />
                    <asp:BoundField DataField="Puntaje" HeaderText="Puntaje" SortExpression="Puntaje" />
                    <asp:BoundField DataField="NoExpediente" HeaderText="No. Expediente" SortExpression="NoExpediente" />
                    <asp:BoundField DataField="NoCredito" HeaderText="No. Crédito" SortExpression="NoCredito" />
                    <asp:BoundField DataField="FechaHoraEvaluacion" HeaderText="Fecha Hora Evaluacion" SortExpression="FechaHoraEvaluacion" />
                    <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <asp:Panel ID="pnlDatos" runat="server" class="container well" Visible="false">
            <div class="row">
                <div class="col-md-6">

                    <label for="txtNombre">Nombre Completo</label>

                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control required" />
                </div>
                <div class="col-md-6">

                    <label for="ddlMetodologia">Metodología</label>

                    <asp:DropDownList runat="server" ID="ddlMetodologia" Width="100%" CssClass="form-control required">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label for="txtNoCredito">No. Crédito</label>
                    <asp:TextBox runat="server" ID="txtNoCredito" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label for="txtFecha">Fecha</label>
                    <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control required" />
                    <ajaxToolkit:CalendarExtender ID="txtFechaPerfil_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFecha" Format="dd/MM/yyyy hh:mm:ss tt">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">

                    <label for="txtCodAgencia">Código Agencia</label>
                    <asp:TextBox runat="server" ID="txtCodAgencia" ReadOnly="true" CssClass="form-control required" />
                </div>
                <div class="col-md-6">
                    <label for="txtNoExpediente">No. Expediente</label>
                    <asp:TextBox runat="server" ID="txtNoExpediente" CssClass="form-control" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">

                    <label for="txtPromotor">Nombre Promotor</label>
                    <%--<asp:TextBox runat="server" ID="txtPromotor" ReadOnly="true" CssClass="form-control" />--%>
                    <asp:DropDownList ID="ddlPromotor" runat="server" Width="100%" CssClass="form-control required">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">

                    <label for="txtCodigo">Código</label>
                    <asp:TextBox runat="server" ID="txtCodigo" ReadOnly="true" CssClass="form-control required" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView runat="server" ID="gvDatos" AutoGenerateColumns="false" SkinID="sGridPrincipal" ShowFooter="true" OnRowDataBound="gvDatos_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="IdCategoria" Visible="false" />
                            <asp:BoundField HeaderText="Factor de Riesgo" DataField="Nombre" />
                            <asp:TemplateField HeaderText="Categoria">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlClasificacion" runat="server" Width="100%">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hfClasificacion" runat="server" />
                                    <asp:HiddenField ID="hfEditarEvaluacionCategoria" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Puntuación">
                                <ItemTemplate>
                                    <asp:Label ID="lblPuntuacion" Text="0" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalPuntuacion" Text="0" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ponderación">
                                <ItemTemplate>
                                    <asp:Label ID="lblPonderacion" runat="server" Text='<%#Eval("Ponderacion") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalPonderacion" Text="0" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calculo de Riesgo">
                                <ItemTemplate>
                                    <asp:Label ID="lblCalculo" Text="0" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalCalculo" Text="0" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <asp:HiddenField ID="hfIdCliente" runat="server" />
            <asp:HiddenField ID="hfIdUsuario" runat="server" />
            <asp:HiddenField ID="hfUsuario" runat="server" />
            <asp:HiddenField ID="hfEvaluacionCliente" runat="server" />
            <asp:HiddenField ID="hfEvaluacionCategoria" runat="server" />
        </asp:Panel>
    </div>
    <script>
        var jsonClasificacion = [];
        var evaluacionCliente = {};
        var jsonEvaluacionCategoria = [];

        $(document).delegate("select[id*='ddlClasificacion']", "change", function (e) {
            e.preventDefault();
            onChange_ddlClasificacion(e.currentTarget);
        });

        $(document).ready(function () {

            var element = $("select[id*='ddlClasificacion']");
            if (element != undefined) {
                $.each(element, function () {
                    onChange_ddlClasificacion($(this).get(0));
                });
            }
        });

        var onChange_ddlClasificacion = function (e) {
            // code
            var tr = $(e).closest('tr');
            var strClasificacion = $(tr).find('input[id*="hfClasificacion"]')
            jsonClasificacion = JSON.parse(strClasificacion.val());
            var clasificacion = ObtenerClasificacion(e.value);

            if (clasificacion !== null) {
                var puntuacion = $(tr).find('span[id*="lblPuntuacion"]').text(clasificacion.Puntuacion);
                var ponderacion = $(tr).find('span[id*="Ponderacion"]').text();
                var ponderacion = parseFloat(ponderacion) / 100;
                var calculo = $(tr).find('span[id*="lblCalculo"]').text((parseFloat(puntuacion.text()) * parseFloat(ponderacion)).toFixed(2));
            }

            var grandTotalPuntuacion = 0;
            var grandTotalCalculo = 0;
            $("span[id*=lblPuntuacion]").each(function () {
                grandTotalPuntuacion = grandTotalPuntuacion + parseFloat($(this).html());
            });
            $("span[id*=lblTotalPuntuacion]").html(grandTotalPuntuacion.toString());

            $("span[id*=lblCalculo]").each(function () {
                grandTotalCalculo = grandTotalCalculo + parseFloat($(this).html());
            });
            $("span[id*=lblTotalCalculo]").html(grandTotalCalculo.toString());

            var hfEditar = $(tr).find('input[id*="hfEditarEvaluacionCategoria"]')
            if ($(hfEditar).val() === "") {
                llenarObjetoEvaluacionCliente("", grandTotalCalculo.toString());

                if (calculo !== undefined) {
                    agregarEvaluacionCategoria("", "", "", parseFloat(calculo.text()), e.value);
                }
            }
            else {
                var jsonEvaluacionCategoria = JSON.parse(hfEditar.val());
                llenarObjetoEvaluacionCliente(jsonEvaluacionCategoria.IdClienteEvaluacion, grandTotalCalculo.toString());

                if (calculo !== undefined) {
                    agregarEvaluacionCategoria(jsonEvaluacionCategoria.IdEvaluacionCategoria, jsonEvaluacionCategoria.IdClienteEvaluacion, jsonEvaluacionCategoria.IdEvaluacionCategoriaClasificacion, parseFloat(calculo.text()), e.value);
                }
            }

        };

        var llenarObjetoEvaluacionCliente = function (idClienteEvaluacion, puntuacionTotal) {
            //Se llena el objeto ClienteEvaluacion
            if (idClienteEvaluacion === "") {
                idClienteEvaluacion = "00000000-0000-0000-0000-000000000000";
            }
            evaluacionCliente.IdClienteEvaluacion = idClienteEvaluacion;
            evaluacionCliente.IdCliente = $("#<%=hfIdCliente.ClientID%>").val();
            evaluacionCliente.IdUsuario = $("#<%=hfIdUsuario.ClientID%>").val();
            evaluacionCliente.IdMetodologia = $("#<%=ddlMetodologia.ClientID%>").val();
            evaluacionCliente.Descripcion = "";
            evaluacionCliente.Puntaje = parseFloat(puntuacionTotal);
            evaluacionCliente.NoExpediente = $("#<%=txtNoExpediente.ClientID%>").val();
            evaluacionCliente.NoCredito = $("#<%=txtNoCredito.ClientID%>").val();
            evaluacionCliente.FechaHoraEvaluacion = new Date();
            evaluacionCliente.EsActivo = true;
            evaluacionCliente.FechaRegistro = new Date();
            evaluacionCliente.Usuario = "";
            evaluacionCliente.UserPC = "";
            evaluacionCliente.UserIP = "";

            $("#<%=hfEvaluacionCliente.ClientID%>").val(JSON.stringify(evaluacionCliente));
        }

        var agregarEvaluacionCategoria = function (idEvaluacionCategoria, idClienteEvaluacion, idEvaluacionCategoriaClasificacion, calculo, idClasificacion) {
            //[EvaluacionCategoria]
            var evaluacionCategoria = {};

            var clasificacio = ObtenerClasificacion(idClasificacion);
            if (idEvaluacionCategoria === "") {
                idEvaluacionCategoria = "00000000-0000-0000-0000-000000000000";
            }
            if (idClienteEvaluacion === "") {
                idClienteEvaluacion = "00000000-0000-0000-0000-000000000000";
            }
            if (idEvaluacionCategoriaClasificacion === "") {
                idEvaluacionCategoriaClasificacion = "00000000-0000-0000-0000-000000000000";
            }

            var evaluacionCategoriaEditar = ObtenerEvaluacionCategoria(clasificacio.IdCategoria);

            evaluacionCategoria.IdEvaluacionCategoria = idEvaluacionCategoria;
            evaluacionCategoria.IdCategoria = clasificacio.IdCategoria;
            evaluacionCategoria.IdClienteEvaluacion = idClienteEvaluacion;
            evaluacionCategoria.CalculoRiesgo = calculo;
            evaluacionCategoria.EsActivo = true;
            evaluacionCategoria.FechaRegistro = new Date();
            evaluacionCategoria.Usuario = "";
            evaluacionCategoria.UserPC = "";
            evaluacionCategoria.UserIP = "";
            evaluacionCategoria.EvaluacionCategoriaClasificacion = {};
            evaluacionCategoria.EvaluacionCategoriaClasificacion.IdEvaluacionCategoriaCategoria = idEvaluacionCategoriaClasificacion;
            evaluacionCategoria.EvaluacionCategoriaClasificacion.IdEvaluacionCategoria = idEvaluacionCategoria;
            evaluacionCategoria.EvaluacionCategoriaClasificacion.IdClasificacion = idClasificacion;
            evaluacionCategoria.EvaluacionCategoriaClasificacion.EsActivo = true;
            evaluacionCategoria.EvaluacionCategoriaClasificacion.FechaRegistro = new Date();
            evaluacionCategoria.EvaluacionCategoriaClasificacion.Usuario = "";
            evaluacionCategoria.EvaluacionCategoriaClasificacion.UserPC = "";
            evaluacionCategoria.EvaluacionCategoriaClasificacion.UserIP = "";

            if (evaluacionCategoriaEditar === null) {
                jsonEvaluacionCategoria.push(evaluacionCategoria);
            }
            else {
                EditarEvaluacionCategoria(evaluacionCategoria);
            }

            $("#<%=hfEvaluacionCategoria.ClientID%>").val(JSON.stringify(jsonEvaluacionCategoria));
        }

        var EditarEvaluacionCategoria = function (evaluacionCategoria) {
            for (var i in jsonEvaluacionCategoria) {
                if (jsonEvaluacionCategoria[i].IdCategoria == evaluacionCategoria.IdCategoria) {
                    jsonEvaluacionCategoria[i] = evaluacionCategoria;
                    break; //Stop this loop, we found it!
                }
            }
        }

        var ObtenerClasificacion = function (idClasificacion) {
            var i = null;
            for (i = 0; jsonClasificacion.length > i; i += 1) {
                if (jsonClasificacion[i].IdClasificacion === idClasificacion) {
                    return jsonClasificacion[i];
                }
            }

            return null;
        };

        var ObtenerEvaluacionCategoria = function (idCategoria) {
            var i = null;
            for (i = 0; jsonEvaluacionCategoria.length > i; i += 1) {
                if (jsonEvaluacionCategoria[i].IdCategoria === idCategoria) {
                    return jsonEvaluacionCategoria[i];
                }
            }

            return null;
        };

        $("select[id*='ddlPromotor']").on('change', function (e) {
            var usuarioJson = JSON.parse($("#<%=hfUsuario.ClientID%>").val());
            var usuario = ObtenerCodigo(e.target.value, usuarioJson);
            $("#<%=txtCodigo.ClientID%>").val("");
            $("#<%=hfIdUsuario.ClientID%>").val("");
            if (usuario !== null) {
                $("#<%=txtCodigo.ClientID%>").val(usuario.Codigo);
                $("#<%=hfIdUsuario.ClientID%>").val(usuario.IdUsuario);
            }

        });

        var ObtenerCodigo = function (idUsuario, jsonString) {
            var i = null;
            for (i = 0; jsonString.length > i; i += 1) {
                if (jsonString[i].IdUsuario === idUsuario) {
                    return jsonString[i];
                }
            }

            return null;
        };
    </script>
</asp:Content>
