<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="RecalcularCartera.aspx.cs" Inherits="Acciona.Credito.RecalcularCartera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
     <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Recalcular Cartera</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <%--<asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control" placeholder="No. Cuenta" required="true" />--%>

                    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default " OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnSubir" Text="Cargar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnSubir_Click"  />
                    <asp:FileUpload ID="FileUpload1" runat="server" />

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
                        <%--<asp:ImageButton ID="imbCancelar" SkinID="imbCancelar" runat="server" CssClass="btn btn-default" OnClick="imbCancelar_Click" />--%>
                        <%--<asp:ImageButton ID="imbGuardar" SkinID="imbGuardar" runat="server" CssClass="btn btn-default submit" OnClick="imbGuardar_Click"
                            OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Recalculando...';"
                            UseSubmitBehavior="false" />--%>

                        <%--<asp:ImageButton ID="imbImprimir" SkinID="imbImprimir" runat="server" CssClass="btn btn-default" OnClick="imbImprimir_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <%--<div class="col-md-6">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 50px">Cliente:</span>
                <asp:Literal Text="" ID="litCliente" runat="server" />
            </div>
        </div>--%>
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <asp:Button ID="btnRecalcular" Text="Recalcular Cuenta" runat="server" CssClass="btn" OnClick="btnRecalcular_Click" />
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
     <div class="box-name">
        <i class="fa fa-table input-sm"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>

    <asp:GridView ID="gvPagos" runat="server" >
    </asp:GridView>
</asp:Content>
