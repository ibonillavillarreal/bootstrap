using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                litUsuario.Text = user.Nombre;
                litUsuarioSucursal.Text = user.Sucursal.Nombre; //.Length > 10 ? user.Sucursal.Nombre.Substring(0, 10) + "..." : user.Sucursal.Nombre;
                litMenu.Text = ((SisSegLT.Seguridad.Pagina)Page).ObtenerMenu();
            }
            catch (Exception)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void lbSalir_Click(object sender, EventArgs e)
        {
            Session.Remove("UserAsodenicAutentication");
            Response.Redirect("~/Login.aspx");
        }
    }
}