<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDatosNegocio.ascx.cs" Inherits="Acciona.Clientes.Controles.ucDatosNegocio" %>
<%@ Register Src="~/Clientes/Controles/ucDocumentosNegocio.ascx" TagPrefix="perfil" TagName="DocumentosNegocio" %>
<%@ Register Src="~/Clientes/Controles/ucSuplidores.ascx" TagPrefix="perfil" TagName="Suplidores" %>

<div class="container form">
    <asp:HiddenField ID="hfSelectedTab1" runat="server" />
    <asp:HiddenField ID="hfIdDatosNegocio" runat="server" />
    <div id="content">
        <ul id="tabs-negocio" class="nav nav-tabs" data-tabs="tabs">
            <li class="active"><a href="#datosnegocios" data-toggle="tab">Datos Negocio</a></li>
            <li><a href="#documentos" data-toggle="tab">Documentos del Negocio</a></li>
            <li><a href="#proveedores" data-toggle="tab">Suplidores o Clientes</a></li>
        </ul>
        <div id="my-tab-content-negocio" class="tab-content">
            <div class="tab-pane active" id="datosnegocios">
                <h4>Datos Negocio</h4>
                <asp:Literal ID="litmensaje" Text="" runat="server" />

                <div class="btn-toolbar" role="toolbar">
                    <div class="btn-group">
                        <asp:ImageButton ID="imbAgregar" SkinID="imbAgregar" runat="server" CssClass="btn btn-default" OnClick="imbAgregar_Click" />
                        <asp:ImageButton ID="imbEditar" SkinID="imbEditar" runat="server" CssClass="btn btn-default" OnClick="imbEditar_Click" />
                        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
                        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default" OnClick="imbGuardar_Click" />
                        <asp:ImageButton ID="imbActualizar" SkinID="imbActualizar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />
                    </div>
                    <div class="btn-group">
                        <asp:ImageButton ID="imbEliminar" SkinID="imbEliminar" runat="server" CssClass="btn btn-default" OnClick="imbEliminar_Click" />
                    </div>

                </div>
                <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IdDatosNegocio" 
                    EmptyDataText="No se encontró ningun dato registrado" DataMember="DatosNegocio">
                    <Columns>
                        <asp:BoundField DataField="IdDatosNegocio" HeaderText="IdDatosNegocio" Visible="False" />
                        <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" Visible="False" />
                        <asp:BoundField DataField="UbicacionNegocio" HeaderText="UbicacionNegocio">
                            <ItemStyle Width="65%" />
                        </asp:BoundField>
                        <%--<asp:TemplateField HeaderText="UbicacionNegocio">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnbNombre" runat="server" Text='<%# Eval("UbicacionNegocio") %>' CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnbSeleccionar_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="65%" />
                        </asp:TemplateField>--%>
                        <asp:CheckBoxField DataField="Alquila" HeaderText="Alquila" ReadOnly="True" />
                        <asp:CheckBoxField DataField="EsPropio" HeaderText="Propio" ReadOnly="True" />
                        <asp:CheckBoxField DataField="Familiar" HeaderText="Familiar" ReadOnly="True" />
                        <asp:BoundField DataField="Tiempo" HeaderText="Antiguedad Negocio" />
                        <asp:BoundField DataField="IngresoVolumen" HeaderText="Ingresos" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="DestinoCredito.Nombre" HeaderText="Destino Credito">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="EsActivo" HeaderText="EsActivo" SortExpression="EsActivo" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imageButtonEditar" ImageUrl="~/App_Themes/Default/img/edit.png" CommandName="Editar" OnClick="btnEditar_Click" runat="server" />
                                <itemstyle width="8%" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                                    CommandName="Delete" OnClick="btn_Click" runat="server" />
                                <itemstyle width="8%" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                                        <asp:CheckBox ID="chkRegistros" runat="server" CssClass="form-control" />
                                    </div>
                                </th>
                                <th>
                                    <div class="input-group">
                                        <span class="input-group-addon" style="width: 150px">Lugar:</span>
                                        <asp:RadioButtonList runat="server" ID="rblVivienda" CssClass="form-control required" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Familiar" />
                                            <asp:ListItem Text="Propia" />
                                            <asp:ListItem Text="Alquilada" />
                                        </asp:RadioButtonList>
                                    </div>
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    <div class="input-group">
                                        <span class="input-group-addon" style="width: 150px">Ingreso Anual:</span>
                                        <asp:TextBox ID="txtIngreso" CssClass="form-control required dinero" runat="server"></asp:TextBox>
                                    </div>
                                </th>
                                <th>
                                    <div class="input-group">
                                        <span class="input-group-addon" style="width: 150px">Antiguedad Negocio:</span>
                                        <asp:TextBox ID="txtTiempo" CssClass="form-control required" runat="server"></asp:TextBox>
                                    </div>
                                </th>

                            </tr>
                            <tr>
                                <th>
                                    <div class="input-group">
                                        <span class="input-group-addon" style="width: 150px">Destino Credito:</span>
                                        <asp:DropDownList ID="ddlDestinoCredito" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </th>
                                <th>
                                    <div class="input-group">
                                        <span class="input-group-addon" style="width: 150px">Activo:</span>
                                        <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-control" />
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <div class="tab-pane" id="documentos">
                <h4>Documentos del Negocio</h4>
                <perfil:DocumentosNegocio ID="DoumentoNegocio" runat="server" />
            </div>
            <div class="tab-pane" id="proveedores">
                <h4>Suplidores o Clientes</h4>
                <perfil:Suplidores ID="Suplidores" runat="server" />
            </div>
        </div>
    </div>
    <script>
        //$("input.dinero").maskMoney({ showSymbol: false, symbol: "C$", decimal: ".", thousands: "," });

        $('#tabs-negocio a').click(function (e) {
            e.preventDefault();
            //$(this).tab('show');
        });

        // store the currently selected tab in the hash value
        $("#tabs-negocio > li > a").on("shown.bs.tab", function (e) {
            var id = $(e.target).attr("href").substr(1);
            //window.location.hash = id;
            $("#<%= hfSelectedTab1.ClientID%>").val(id);
        });

        // on load of the page: switch to the currently selected tab
        var hash = $("#<%= hfSelectedTab1.ClientID%>").val();
        $('#tabs-negocio a[href="#' + hash + '"]').tab('show');

        $(document).ready(function () {
            /*disable non active tabs*/
            $('#tabs-negocio li').not('.active').addClass('disabled');
            /*to actually disable clicking the bootstrap tab, as noticed in comments by user3067524*/
            $('#tabs-negocio li').not('.active').find('a').removeAttr("data-toggle");

            if ($("#<%=hfIdDatosNegocio.ClientID%>").val() != "") {
                /*enable next tab*/
                //$('.nav li.active').next('li').removeClass('disabled');
                //$('.nav li.active').next('li').find('a').attr("data-toggle", "tab")
                $('#tabs-negocio li').not('.active').removeClass('disabled');
                $('#tabs-negocio li').not('.active').find('a').attr("data-toggle", "tab");
            }

            $("form").validateWebForm();
        });
    </script>
</div>
