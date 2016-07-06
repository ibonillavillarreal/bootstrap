<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SisSegLT._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>SisSegLT</h1>
        <p class="lead">SisSegLT es una solución rápida a la seguridad, sientase libre de administrar su seguridad, desde los roles, usuarios, surcursales, entre otros datos necesarios por su aplicación principal.</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Iniciar &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Roles</h2>
            <p>
            Los roles son agrupaciones en que cada usuario puede ubicarse, estos roles facilitan el trabajo al permitir o
            restringir el acceso a determinadas páginas en la aplicación final.
            </p>
            <p>
                <a class="btn btn-default" href="Administracion/Roles.aspx">Administrar...</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Usuarios</h2>
            <p>
            Controle todos los usuarios, modifique sus datos según las políticas de su empresa o reinicie la contraseña a
            petición de sus usuarios.
            </p>
            <p>
                <a class="btn btn-default" href="Administracion/Usuarios.aspx">Administrar...</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Menú</h2>
            <p>
            Agregue, ordene, oculte items en el menú que el usuario verá en aplicación principal.</p>
            <p>
                <a class="btn btn-default" href="Administracion/ItemMenus.aspx">Administrar...</a>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Sucursales</h2>
            <p>
            Administre los datos de las sucursales de su empresa.</p>
            <p>
                <a class="btn btn-default" href="Administracion/Sucursales.aspx">Administrar...</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Ciudades / Países</h2>
            <p>
            Agregue ciudades o países a la lista para que estén disponibles al usuario final.</p>
            <p>
                <a class="btn btn-default" href="Administracion/Paises.aspx">Administrar...</a>
            </p>
        </div>
    </div>

</asp:Content>
