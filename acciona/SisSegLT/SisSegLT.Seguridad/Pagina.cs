using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ClientServices;
using System.Web.ClientServices.Providers;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;
using SisSegLT.Negocio;

namespace SisSegLT.Seguridad
{
    public partial class Pagina : Page
    {
        #region Propiedades

        public Usuario MiUsuario
        {
            get
            {
                if (Session["UserAsodenicAutentication"] == null)
                    Session["UserAsodenicAutentication"] = null;
                return (Usuario)Session["UserAsodenicAutentication"];
            }
            set
            {
                Session["UserAsodenicAutentication"] = value;
            }
        }

        #endregion
        protected override void OnPreLoad(EventArgs e)
        {
            if (MiUsuario != null)
            {
                ItemMenu item = new ItemMenuBLL().ObtenerPorRuta(Page.AppRelativeVirtualPath);
                if (item != null)
                {
                    List<ItemRol> listaRol = new ItemRolBLL().ObtenerPorIdItemMenu(item.IdItemMenu);
                    if (!listaRol.Any(r => r.IdRol == MiUsuario.UsuarioRol.FirstOrDefault().IdRol))
                    {
                        Response.Redirect("~/Default.aspx");
                        throw new Exception("Su usuario no tiene permitido acceder a este contenido");
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                    throw new Exception("No se ha encontrado la ruta a la cual se refiere");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
                throw new Exception("Su usuario no tiene permitido acceder a este contenido");
            }
        }

        public string ObtenerMenu()
        {
            if (MiUsuario != null)
            {
                string menuHtml = "<ul class='nav navbar-nav' role='menu'>";
                List<ItemMenu> listaMenu = new ItemMenuBLL().ObtenerMenuPorRol(MiUsuario.UsuarioRol.FirstOrDefault().IdRol).Where(l => l.Visible).ToList();

                foreach (ItemMenu itemMenu in listaMenu.Where(l => l.IdItemMenuPadre == null))
                {
                    menuHtml += "<li class='dropdown'>" +
                                "<a role='button' data-toggle='dropdown' data-target='#' href='#'>" +
                                itemMenu.Texto + "<span class='caret'></span>" +
                                "</a>" +
                                "[SUBMENU]" +
                                "</li>";
                    string subMenuHtml = ExtraerItem(itemMenu.IdItemMenu, listaMenu, true);
                    if (!string.IsNullOrEmpty(subMenuHtml))
                    {
                        subMenuHtml = "<ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'>" +
                                    subMenuHtml +
                                    "</ul>";
                    }
                    menuHtml = menuHtml.Replace("[SUBMENU]", subMenuHtml);
                }
                return menuHtml + "</ul>";
            }
            else
            {
                return string.Empty;
            }
        }

        private string ExtraerItem(Guid idItemMenu, List<ItemMenu> listaMenu, bool primerNivel = false)
        {
            string menuhtml = "";
            List<ItemMenu> listaSubMenus = listaMenu.Where(l => l.IdItemMenuPadre == idItemMenu).ToList();
            ItemMenu item = listaMenu.FirstOrDefault(l => l.IdItemMenu == idItemMenu);
            string hostUri = ResolveClientUrl(VirtualPathUtility.ToAbsolute("~"));
            if (item != null)
            {
                if (listaSubMenus.Count > 0)
                {
                    menuhtml = "<li class='dropdown-submenu'>" +
                               "<a href='#'>" + item.Texto + "</a>" +
                               "<ul class='dropdown-menu'>[SUBMENU]</ul></li>";
                    menuhtml = menuhtml.Replace("[TEXTO]", "<a href='#'>" + item.Texto + "</a>");
                    string subMenuHtml = "";
                    foreach (ItemMenu subMenu in listaSubMenus)
                    {
                        subMenuHtml += ExtraerItem(subMenu.IdItemMenu, listaMenu);
                    }
                    menuhtml = primerNivel ? subMenuHtml : menuhtml.Replace("[SUBMENU]", subMenuHtml);
                }
                else
                {
                    menuhtml = "<li><a href='" + item.Ruta.Replace("~", hostUri).Replace("//", "/") + "'>" + item.Texto + "</a></li>";
                }
            }
            return menuhtml;
        }
    }
}
