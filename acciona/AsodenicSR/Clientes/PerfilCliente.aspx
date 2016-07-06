<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="PerfilCliente.aspx.cs" Inherits="Acciona.Clientes.PerfilCliente" %>

<%@ Register Src="~/Clientes/Controles/ucDomicilio.ascx" TagPrefix="perfil" TagName="Domicilio" %>
<%@ Register Src="~/Clientes/Controles/ucContacto.ascx" TagPrefix="perfil" TagName="Contacto" %>
<%@ Register Src="~/Clientes/Controles/ucReferenciasPersonales.ascx" TagPrefix="perfil" TagName="ReferenciasPersonales" %>
<%@ Register Src="~/Clientes/Controles/ucReferenciasCrediticias.ascx" TagPrefix="perfil" TagName="ReferenciasCrediticias" %>
<%@ Register Src="~/Clientes/Controles/ucDatosNegocio.ascx" TagPrefix="perfil" TagName="DatosNegocio" %>
<%@ Register Src="~/Clientes/Controles/ucAprobacionInstitucion.ascx" TagPrefix="perfil" TagName="AprobacionInstitucion" %>
<%@ Register Src="~/Clientes/Controles/ucResumenTransacciones.ascx" TagPrefix="perfil" TagName="Resumen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
                <%--<span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>--%>
            </button>
            <a href="#" class="navbar-brand">Datos Clientes</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control required" placeholder="No. Identificación" />
                </div>
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default submit" OnClick="btnBuscar_Click" OnClientClick="validartxtBuscar()" />
                <asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default submit" runat="server" OnClick="btImprimir_Click" />
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
    <div class="container">
        <asp:HiddenField ID="hfSelectedTab" runat="server" />
        <asp:HiddenField ID="hfIdCliente" runat="server" />
        <asp:HiddenField ID="hfTabContacto" runat="server" />
        <asp:HiddenField ID="hfNombreCliente" runat="server" />
        <asp:HiddenField ID="hfIdentificacion" runat="server" />
        <div id="content">
            <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                <li class="active"><a href="#datos" data-toggle="tab">Datos Generales</a></li>
                <li><a href="#contactos" data-toggle="tab">Contactos</a></li>
                <li><a href="#domicilio" data-toggle="tab">Domicilio</a></li>
                <li><a href="#negocio" data-toggle="tab">Datos Negocio</a></li>
                <li><a href="#transacciones" data-toggle="tab">Transacciones</a></li>
                <li><a href="#referenciasc" data-toggle="tab">Ref Crediticias</a></li>
                <li><a href="#referenciasp" data-toggle="tab">Ref Personales</a></li>
                <li><a href="#aprobacion" data-toggle="tab">Exclusivo Institucion</a></li>
            </ul>
            <div id="my-tab-content" class="tab-content">
                <div class="tab-pane active form-group" id="datos">

                    <div role="navigation" class="navbar navbar-default">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-header">
                            <button type="button" data-target="#navbarCollapses" data-toggle="collapse" class="navbar-toggle">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <a href="#" class="navbar-brand">Datos Generales</a>
                        </div>
                        <!-- Collection of nav links, forms, and other content for toggling -->
                        <div id="navbarCollapses" class="collapse navbar-collapse">
                            <div class="navbar-form navbar-right" role="search">
                                <div class="btn-toolbar" role="toolbar">
                                    <div class="btn-group">
                                        <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
                                        <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
                                        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
                                        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click" />
                                        <asp:ImageButton ID="imbImportar" SkinID="imbImportar" runat="server" CssClass="btn btn-default" OnClick="imbImportar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:Literal ID="litmensaje" Text="" runat="server" />
                    <div id="datoGenerales">
                        <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Nombres:</span>
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" required="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Apellidos:</span>
                                    <asp:TextBox ID="txtApellidos" CssClass="form-control" runat="server" required="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Identificacion:</span>
                                    <asp:TextBox ID="txtIdentificacion" CssClass="form-control customname" runat="server" onlosefocus="alert(1);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Fecha Emision:</span>
                                    <asp:TextBox ID="txtFechaEmision" CssClass="form-control" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFechaEmision_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaEmision" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Fecha Vencimiento:</span>
                                    <asp:TextBox ID="txtFechaVencimiento" CssClass="form-control" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFechaVencimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaVencimiento" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group" style="width: 100%;">
                                    <span class="input-group-addon" style="width: 150px">Pais Emisor:</span>
                                    <asp:DropDownList ID="ddlPaisEmisor" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Fecha Perfil:</span>
                                    <asp:TextBox ID="txtFechaPerfil" CssClass="form-control" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFechaPerfil_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaPerfil" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group" style="width: 100%;">
                                    <span class="input-group-addon" style="width: 150px">Estado Perfil:</span>
                                    <asp:DropDownList ID="ddlEstado" runat="server" disabled="disabled" CssClass="form-control required">
                                        <asp:ListItem Value="1">En Proceso</asp:ListItem>
                                        <asp:ListItem Value="2">Finalizado</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="box-name">
                            <i class="fa fa-table"></i>
                            <h4>Detalles del Cliente</h4>
                        </div>
                        <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                            <div class="col-md-6">
                                <div class="input-group" style="width: 100%;">
                                    <span class="input-group-addon" style="width: 150px">Estado Civil:</span>
                                    <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-control" Width="100%">
                                        <asp:ListItem Value="0">Soltero</asp:ListItem>
                                        <asp:ListItem Value="1">Casado</asp:ListItem>
                                        <asp:ListItem Value="2">Divorciado</asp:ListItem>
                                        <asp:ListItem Value="3">Otro</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group" style="width: 100%;">
                                    <span class="input-group-addon" style="width: 150px">Pais Nacimiento:</span>
                                    <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Nacionalidad:</span>
                                    <asp:TextBox ID="txtNacionalidad" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Fecha Nacimiento:</span>
                                    <asp:TextBox ID="txtFechaNacimiento" CssClass="form-control" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFechaNacimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaNacimiento" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-md-6">
                                <div class="input-group" style="width: 100%;">
                                    <span class="input-group-addon" style="width: 150px">Sexo:</span>
                                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control" Width="100%">
                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group" style="width: 100%;">
                                    <span class="input-group-addon" style="width: 150px">Profesion:</span>
                                    <asp:TextBox ID="txtProfesion" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <%--<div class="col-md-6">
                                <div class="input-group" style="width: 100%;">
                                    <span class="input-group-addon" style="width: 150px">Profesion:</span>
                                    <asp:DropDownList ID="ddlProfesion" runat="server" CssClass="form-control required" Width="100%"></asp:DropDownList>
                                </div>
                            </div>--%>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Ocupacion:</span>
                                    <asp:TextBox ID="txtOcupacion" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Miembros Familia:</span>
                                    <asp:TextBox ID="txtMiembrosFamilia" CssClass="form-control number" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Cuantos no tienen ingresos:</span>
                                    <asp:TextBox ID="txtIngresos" CssClass="form-control number" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group">
                                    <span class="input-group-addon" style="width: 150px">Alias:</span>
                                    <asp:TextBox ID="txtAlias" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="contactos">
                    <h4>Contactos</h4>
                    <perfil:Contacto ID="Contactos" runat="server" />
                </div>
                <div class="tab-pane" id="domicilio">
                    <h4>Domicilio</h4>
                    <perfil:Domicilio ID="Domicilio" runat="server" />
                </div>
                <div class="tab-pane" id="negocio">
                    <h4>Datos del Negocio</h4>
                    <perfil:DatosNegocio ID="DatosNegocios" runat="server" />
                </div>
                <div class="tab-pane" id="transacciones">
                    <h4>Transacciones</h4>
                    <perfil:Resumen ID="ResumenTransaccion" runat="server" />
                </div>
                <div class="tab-pane" id="referenciasc">
                    <h4>Referencias crediticias</h4>
                    <perfil:ReferenciasCrediticias ID="ReferenciasCrediticia" runat="server" />
                </div>
                <div class="tab-pane" id="referenciasp">
                    <h4>Referencias Personales</h4>
                    <perfil:ReferenciasPersonales ID="ReferenciaPersonal" runat="server" />
                </div>
                <div class="tab-pane" id="aprobacion">
                    <h4>Exclusivo Institucion (Aprobacion)</h4>
                    <perfil:AprobacionInstitucion ID="Aprobacion" runat="server" />
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" data-backdrop="static" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Buscar PIC</h4>
                    </div>
                    <div class="modal-body">
                        <table id="tblData" class="hover">
                            <thead>
                                <tr class="gridStyle">
                                    <th>Nombre</th>
                                    <th>Cedula</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnSeleccionar" Text="Seleccionar" CssClass="btn btn-primary" runat="server" OnClick="btnSeleccionar_Click" />
                    </div>
                </div>
            </div>
        </div>

        <script>

            $('#myForm').validator()
            $("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });

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

            /*1. Crear un hidden field
            2.  Guardar en el hidden field el valor del tab siguiente*/

            if ($("#<%=hfTabContacto.ClientID%>").val() != "") {
                var hash = $("#<%= hfTabContacto.ClientID%>").val();
                $('#tabs a[href="#' + hash + '"]').tab('show');

                $("#<%= hfTabContacto.ClientID%>").val("");
            }

            $(document).ready(function () {
              
                /*disable non active tabs*/
                $('#tabs li').not('.active').addClass('disabled');
                /*to actually disable clicking the bootstrap tab, as noticed in comments by user3067524*/
                $('#tabs li').not('.active').find('a').removeAttr("data-toggle");

                if ($("#<%=hfIdCliente.ClientID%>").val() != "") {
                    /*enable next tab*/
                    //$('.nav li.active').next('li').removeClass('disabled');
                    //$('.nav li.active').next('li').find('a').attr("data-toggle", "tab")
                    $('#tabs li').not('.active').removeClass('disabled');
                    $('#tabs li').not('.active').find('a').attr("data-toggle", "tab");
                }

                $("#txtIdentificacion").prop("name", $("#txtIdentificacion").prop("id"));

                $.validator.addMethod("customname", function (value, element) {
                    //var i = /\d{13}[A-Z]/;
                    //return this.optional(element) || (i.test(value) > 0);
                    var cadena = document.getElementById('<%=txtIdentificacion.ClientID%>').value;

                    return EsCedula(cadena);
                }, "La Cedula no contiene el formato correcto.");

                $("form").validateWebForm({
                    rules: {
                        txtIdentificacion: {
                            customname: true
                        }
                    }
                });

            });

                function validartxtBuscar() {
                    if ($('#<%= txtBuscar.ClientID%>').valid() == false) {
                        getData();
                        return false;
                    }
                }

                function EsCedula(elTexto) {
                    var es = true;
                    //validar longitud
                    if (elTexto.length != 14) {
                        es = false;
                    } else {
                        //verificar si tiene el formato correcto
                        var RegExPattern = /^\d{13}[A-Z]{1}$/;
                        if (!elTexto.match(RegExPattern)) {
                            es = false;
                        } else {
                            //verificar si contiene una fecha válida
                            var sFecha = elTexto.substring(3, 9);
                            var sDia = elTexto.substring(3, 5);
                            var sMes = elTexto.substring(5, 7);
                            var sAnio = elTexto.substring(7, 9);
                            var aa = parseInt(sAnio);
                            var mm = parseInt(sMes);
                            var dd = parseInt(sDia);
                            if (aa >= 0 && aa <= 29) {
                                aa += 2000;
                            } else {
                                aa += 1900;
                            }
                            var bisiesto = false;
                            if (aa % 2 == 0) {
                                if (aa % 4 == 0) {
                                    bisiesto = true;
                                }
                            }
                            if (mm < 1 || mm > 12) {
                                es = false;
                            } else {
                                switch (mm) {
                                    case 1:
                                    case 3:
                                    case 5:
                                    case 7:
                                    case 8:
                                    case 10:
                                    case 12:
                                        if (dd < 1 || dd > 31) {
                                            return false;
                                        }
                                        break;
                                    case 2:
                                        if (bisiesto) {
                                            if (dd < 1 || dd > 29) {
                                                es = false;
                                            }
                                        } else {
                                            if (dd < 1 || dd > 28) {
                                                es = false;
                                            }
                                        }
                                        break;
                                    default:
                                        if (dd < 1 || dd > 30) {
                                            es = false;
                                        }
                                }
                            }
                        }
                    }
                    return es;
                }

                function ValidarCedula() {
                    var cadena = document.getElementById('<%=txtIdentificacion.ClientID%>').value;

                    if (EsCedula(cadena)) {
                        alert('Es n° cedula es correcto!');
                        cadena = "";
                    } else {
                        alert('¡El n° de cédula no es válido. Deben ser trece dígitos mas una letra mayuscula al final, los caracteres del 4to al 9no deben representar una fecha válida!');
                        cadena = "";
                    }
                    return false;
                }

                function getData() {
                    var table = $('#tblData').DataTable({
                        "destroy": true,
                        "filter": true,
                        "pagingType": "simple_numbers",
                        "orderClasses": false,
                        "order": [[0, "asc"]],
                        "info": false,
                        "scrollY": "450px",
                        "scrollCollapse": true,
                        "bProcessing": true,
                        "bServerSide": true,
                        "sAjaxSource": "WebService/WebServiceCliente.asmx/GetData",
                        "fnServerData": function (sSource, aoData, fnCallback) {
                            aoData.push({ "name": "roleId", "value": $("#<%= hfNombreCliente.ClientID %>").val() });
                            $.ajax({
                                "dataType": 'json',
                                "contentType": "application/json; charset=utf-8",
                                "type": "GET",
                                "url": sSource,
                                "data": aoData,
                                "success": function (msg) {
                                    var json = jQuery.parseJSON(msg.d);
                                    fnCallback(json);
                                    $("#tblData").show();
                                    $('#myModal').modal('show');
                                },
                                error: function (xhr, textStatus, error) {
                                    if (typeof console == "object") {
                                        console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                                    }
                                }
                            });
                        },
                        fnDrawCallback: function () {
                            $('.image-details').bind("click", showDetails);
                        }
                    });

                        $('.dataTables_filter input')
                            .unbind('keypress keyup')
                            .bind('keypress keyup', function (e) {
                                if ($(this).val().length < 5 && e.keyCode != 13) return;
                                table.fnFilter($(this).val());
                            });

                        $('#tblData tbody').unbind('click').on('click', 'tr', function (e) {
                            //console.log(table.row(this).data());
                            //console.log(table.cell('.selected', 1).data());
                            e.stopPropagation();
                            
                            if ($(this).hasClass('selected')) {
                                $(this).removeClass('selected');

                            }
                            else {
                                table.$('tr.selected').removeClass('selected');
                                $(this).addClass('selected');
                            }

                            $("#<%= hfIdentificacion.ClientID%>").val(table.cell('.selected', 1).data());
                        });

                            function showDetails() {
                                //so something funky with the data
                            }
                        };
        </script>
    </div>
</asp:Content>