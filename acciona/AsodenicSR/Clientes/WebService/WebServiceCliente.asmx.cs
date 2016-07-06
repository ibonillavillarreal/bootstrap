using Newtonsoft.Json;
using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

namespace Acciona.Clientes.WebService
{
    /// <summary>
    /// Descripción breve de WebServiceCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
    [System.Web.Script.Services.ScriptService]
    public class WebServiceCliente : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetData()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query);
            string sEcho = nvc["sEcho"].ToString();
            int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
            var displayLength = int.Parse(nvc["iDisplayLength"]);

            var roleId = nvc["roleId"].ToString(CultureInfo.CurrentCulture);

            var filtro = nvc["sSearch"].ToString(CultureInfo.CurrentCulture);

            Usuario user = new UsuarioDAO().ObtenerPorNombre(roleId).FirstOrDefault();

            var CustomerPaged = new DataTablePager();
            if (user == null)
            {
                return string.Empty;
            }
            var vwClienteDAO = new vwPICCliente();
            var Count = vwClienteDAO.ObtenerContador(user.IdSucursal);

            var Customers = vwClienteDAO.ObtenerClientes(user.IdSucursal, iDisplayStart, displayLength, filtro);

            //strFinal.Add(stringList);
            //CustomerPaged.sEcho = sEcho;
            //CustomerPaged.iTotalRecords = Count;
            //CustomerPaged.iTotalDisplayRecords = displayLength;
            //CustomerPaged.aaData = strFinal;

            var hasMoreRecords = false;

            var sb = new StringBuilder();
            sb.Append(@"{" + "\"sEcho\": " + sEcho + ",");
            sb.Append("\"recordsTotal\": " + Customers.Count + ",");
            sb.Append("\"recordsFiltered\": " + Customers.Count + ",");
            sb.Append("\"iTotalRecords\": " + Customers.Count + ",");
            sb.Append("\"iTotalDisplayRecords\": " + Customers.Count + ",");
            sb.Append("\"aaData\": [");
            foreach (var result in Customers)
            {
                if (hasMoreRecords)
                {
                    sb.Append(",");
                }

                sb.Append("[");
                //sb.Append("\"" + result.IdSucursal + "\",");
                sb.Append("\"" + result.Nombre + "\",");
                sb.Append("\"" + result.Cedula + "\"");
                //sb.Append("\"" + result.Sucursal + "\",");
                //sb.Append("\"<img class='image-details' src='content/details_open.png' runat='server' height='16' width='16' alt='View Details'/>\"");
                sb.Append("]");
                hasMoreRecords = true;
            }
            sb.Append("]}");
            return sb.ToString();

            //return JsonConvert.SerializeObject(CustomerPaged);
        }
    }
}