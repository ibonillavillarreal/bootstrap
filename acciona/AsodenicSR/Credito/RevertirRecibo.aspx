<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="RevertirRecibo.aspx.cs" Inherits="Acciona.Credito.RevertirRecibo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div class="navbarCollapse">
        <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
        </button>
        <a href="#" class="navbar-brand">Revertir Recibos PYME</a>
    </div>
    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 60px">No Serie:</span>
                <asp:TextBox ID="txtSerie" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 60px">No. Recibo:</span>
                <asp:TextBox ID="txtNoRecibo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnBuscar_Click" />
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
                        <asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default"  />
                        <asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click" />
                        <%--<asp:ImageButton ID="imbImprimir" SkinID="imbImprimir" runat="server" CssClass="btn btn-default" OnClick="imbImprimir_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <div class="box-name">
        <i class="fa fa-table input-sm"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>
    <div class="box-name">
        <i class="fa fa-table"></i>
        <h4>Recibo</h4>
    </div>
    <asp:GridView ID="gvDatos" SkinID="sGridPrincipal" runat="server" Width="50%" AutoGenerateColumns="False" DataKeyNames="IdPago"
        EmptyDataText="No se encontró ningun dato registrado" DataMember="tCredito">
        <Columns>
            <asp:BoundField DataField="IdPago" Visible="false"></asp:BoundField>
            <asp:BoundField DataField="FechaEfectiva" HeaderText="Fecha" DataFormatString="{0:d}">
                <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="Recibidode" HeaderText="Cliente">
                <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="Serie" HeaderText="Serie">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="NoReferencia" HeaderText="NoReferencia">
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="Concepto" HeaderText="Concepto">
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="MontoRecibido" HeaderText="Monto Recibido" DataFormatString="{0:N2}">
                <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imageButtonDelete" ImageUrl="~/App_Themes/Default/img/remover.png" OnClientClick="javascript:return confirm('Esta seguro que desea eliminar el registro?');"
                        OnClick="btn_Click" runat="server" />
                    <itemstyle width="10%" />
                </ItemTemplate>
            </asp:TemplateField>           
            
        </Columns>
    </asp:GridView>
    <div class="box-name">
        <i class="fa fa-table"></i>
        <h4>Registrar Reversion</h4>
    </div>
    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <div class="col-md-6">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 150px">Motivo Reversion:</span>
                <asp:TextBox ID="txtMotivo" CssClass="form-control " runat="server"  Height="69px" Width="499px"></asp:TextBox>
            </div>
        </div>
    </div>



</asp:Content>
