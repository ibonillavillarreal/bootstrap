using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisSegLT.Seguridad;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using AccionaSR.Negocio;

namespace Acciona
{
    public partial class prueba : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            selected_tab.Value = Request.Form[selected_tab.UniqueID];    
        }
    }
}