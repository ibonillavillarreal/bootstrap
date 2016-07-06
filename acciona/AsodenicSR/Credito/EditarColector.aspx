<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCatalog.master" AutoEventWireup="true" CodeBehind="EditarColector.aspx.cs" Inherits="Acciona.Credito.EditarColector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntTitulo" runat="server">

    <div role="navigation" class="">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbarCollapse">
            <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle">
            </button>
            <a href="#" class="navbar-brand">Editar Ruta PYME</a>
        </div>
        <!-- Collection of nav links, forms, and other content for toggling -->
        <div id="navbarCollapse" class="collapse navbar-collapse">
            <div class="navbar-form navbar-right form" role="search">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control input-sm" placeholder="Nombre Cliente" required="true" />

                    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-default input-sm" OnClick="btnBuscar_Click" />

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
        <i class="fa fa-table"></i>
        <asp:Literal Text="" ID="litmensaje" runat="server" />
    </div>


    <div class="input-group" style="width: 37%;">
        <span class="input-group-addon" style="width: 150px">Cliente:</span>
        <asp:TextBox ID="txtCliente" CssClass="form-control required" runat="server" ReadOnly="true"></asp:TextBox>
    </div>
    <div class="input-group" style="width: 37%;">
        <span class="input-group-addon" style="width: 150px">NoPrestamo:</span>
        <asp:TextBox ID="txtNoPrestamo" CssClass="form-control required" ReadOnly="true" runat="server"></asp:TextBox>
    </div>
    <div class="input-group" style="width: 37%;">
        <span class="input-group-addon" style="width: 150px">Fecha Aprobacion:</span>
        <asp:TextBox ID="txtFechaAprobacion" CssClass="form-control required" ReadOnly="true" runat="server"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txtFechaAprobacion_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaAprobacion" Format="dd/MM/yyyy">
        </ajaxToolkit:CalendarExtender>
    </div>
    <div class="input-group" style="width: 37%;">
        <span class="input-group-addon" style="width: 150px">Colector:</span>
        <asp:DropDownList ID="ddlColector" runat="server" CssClass="form-control required"></asp:DropDownList>
    </div>




</asp:Content>
