<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="ImportarPagos.aspx.cs" Inherits="Acciona.Pagos.ImportarPagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">
    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Importar Pagos</a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntEncabezado" runat="server">
    <div class="box-name">
        <i class="fa fa-table"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>
    <div class="row" style="margin-bottom: 5px; margin-top: 5px;">
        <div class="col-md-4">
            <div class="input-group" style="width: 100%;">
                <span class="input-group-addon input-sm" style="width: 150px">Ruta:</span>
                <asp:TextBox ID="txtSerie" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="input-group" style="width: 100%;">
                <asp:Button ID="btnSubir" Text="Cargar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnSubir_Click"  />
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntCuerpo" runat="server">
    <asp:Button ID="btnGuardar" Text="Importar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnGuardar_Click"  />
    <div class="box-name">
        <i class="fa fa-table"></i>
        <asp:Literal Text="" ID="Literal1" runat="server" />
    </div>

    <asp:GridView ID="gvPagos" runat="server" >
    </asp:GridView>
    
    
</asp:Content>
