using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;

namespace SisSegLT
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Usuario user = ((Pagina)Page).MiUsuario;
                //litUsuario.Text = user.Nombre;                 
                
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