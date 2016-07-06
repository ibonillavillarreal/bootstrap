<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Acciona.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema de Administracion de Credito</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/ccompras.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="App_Themes/Default/Estilos.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <nav class="navbar navbar-custom navbar-fixed-top" style="height: 55px;" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <span class="navbar-brand" runat="server" href="~/">
                        <img src="Content/img/credicompras.png" alt="Credicompras" height="40" />
                    </span>
                </div>
            </div>
        </nav>
    </div>

    <!--<span class="col-sm-2 control-label"><img src="App_Themes/Default/img/user.png" alt="" /></span>
        <span class="col-sm-2 control-label"><img src="App_Themes/Default/img/password.png" alt="" /></span>-->
    <div class="row" style="margin-top: 100px; align-content: center;">
        <div class="col-xs-8 col-sm-6 pull-right" style="margin-top: 60px;">
            <form id="LoginForm" class="form-horizontal" role="form" runat="server">
                <div>
                    <div class="form-group">
                        <label for='<%# txtUser.ClientID %>' class="col-sm-2 control-label">Usuario</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtUser" CssClass="form-control" runat="server" Placeholder="Usuario"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for='<%# txtPass.ClientID %>' class="col-sm-2 control-label">Contraseña</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtPass" CssClass="form-control" TextMode="Password" runat="server" Placeholder="Contraseña"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnEntrar" CssClass="btn btn-default" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
                        </div>
                    </div>

                    <asp:Literal ID="litmensaje" Text="" runat="server" />
                </div>
            </form>
        </div>
        <div class="col-xs-4 col-sm-6">
            <%--<div class="panel pull-right">
                <img src="App_Themes/Default/img/credicompras.png" alt="Alternate Text" />
            </div>--%>
        </div>
    </div>

    <footer class="well well-sm navbar-fixed-bottom" style="margin-bottom: 0px;">
        <p>&copy; <%: DateTime.Now.Year %> - CrediCompras</p>
    </footer>
</body>
</html>