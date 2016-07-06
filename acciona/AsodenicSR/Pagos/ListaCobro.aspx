<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="ListaCobro.aspx.cs" Inherits="Acciona.Pagos.ListaCobro" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Pagos Cuotas PYME</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
                        <div class="col-md-4">
                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon input-sm" style="width: 150px">Colector:</span>
                                <asp:DropDownList ID="ddlColector" runat="server" CssClass="form-control required input-sm" Width="100%" required="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group" style="width: 80%;">
                                <span class="input-group-addon input-sm" style="width: 70px">Fecha de Cobro:</span>
                                <asp:TextBox ID="txtFechaCobro" CssClass="form-control required input-sm" placeholder="Fecha de Cobro" runat="server" required="true"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFechaCobro_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaCobro" Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="input-group" style="width: 100%;">
                                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnBuscar_Click" />
                                <asp:Button ID="btImprimir" Text="Imprimir" CssClass="btn btn-default input-sm" runat="server" OnClick="btImprimir_Click" />
                                <asp:Button ID="txtLimpiar" Text="Limpiar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnLimpiar_Click" />
                                <asp:Button ID="btnReimpresion" Text="Reimprimir" runat="server" CssClass="btn input-sm" OnClick="btnReimpresion_Click" />

                            </div>
                             
                        </div>
                        <%--<div class="col-md-2">
                            <div class="input-group" style="width: 100%;">
                                <asp:Button ID="txtLimpiar" Text="Limpiar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnLimpiar_Click" />
                               
                            </div>

                        </div>--%>
                    </div>
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
        <asp:Panel ID="pnlGrid" runat="server" Visible="false">
            <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="fila"
                EmptyDataText="No se encontró ningun dato registrado" DataMember="Sp_ListadeCobroXColector" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Fila" HeaderText="Fila" />
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                    <asp:BoundField DataField="NoIdentificacion" HeaderText="Identificacion" />
                    <asp:BoundField DataField="NoCuenta" HeaderText="NoCuenta" />
                    <asp:BoundField DataField="fechav" HeaderText="Fecha Vencimiento" />
                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                    <asp:BoundField DataField="Frecuencia" HeaderText="Frecuencia" FooterText="Total:"/>
                    <asp:BoundField DataField="SaldoTotal" HeaderText="SaldoTotal" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="CuotadelDia" HeaderText="Cuota Dia" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="Mora" HeaderText="Mora" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="CuotaIdeal" HeaderText="Cuota Ideal" DataFormatString="{0:N2}" />

                </Columns>
                <FooterStyle BackColor="#99CCFF" Font-Bold="True" />
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlReporte" runat="server" Visible="false">
            <rsweb:ReportViewer ID="rvListaCobro" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reportes\rldc\ListaCobro.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
</asp:Content>
