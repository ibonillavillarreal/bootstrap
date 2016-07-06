using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;

namespace Acciona
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPass.Text;
            Usuario usuario = Consulta.ValidarCredenciales(user, pass);
            if (usuario != null)
            {
                Session["UserAsodenicAutentication"] = usuario;
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}