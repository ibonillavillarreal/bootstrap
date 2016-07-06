using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ItemMenuDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(ItemMenu entidad)
        {
            try
            {
                bool exito = false;
                db.ItemMenu.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(ItemMenu entidad)
        {
            try
            {
                bool exito = false;

                ItemMenu modificado = CopiarEntidad(entidad);
                db.ItemMenu.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(ItemMenu entidad)
        {
            try
            {
                bool exito = false;
                entidad.EsActivo = false;
                exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public ItemMenu CopiarEntidad(ItemMenu entidad)
        {
            ItemMenu nuevo = new ItemMenu()
            {
                IdItemMenu = entidad.IdItemMenu,
                IdItemMenuPadre = entidad.IdItemMenuPadre,
                Ruta = entidad.Ruta,
                Texto = entidad.Texto,
                Descripcion = entidad.Descripcion,
                Visible = entidad.Visible,
                FechaRegistro = entidad.FechaRegistro,
                EsActivo =  entidad.EsActivo
            };
            return nuevo;
        }

        public List<ItemMenu> Listar(Guid? idRol = null, bool soloVisible = false)
        {
            if (idRol != null)
            {
                return (from r in db.ItemMenu
                    where
                        (soloVisible ? r.Visible : true) &&
                        (r.ItemRol.Any(iR => iR.IdRol == (Guid) idRol)) &&
                        (r.EsActivo)
                    orderby r.Texto
                    select r).ToList();
            }
            else
            {
                return (from r in db.ItemMenu
                        where
                            (soloVisible ? r.Visible : true) &&
                            (r.IdItemMenuPadre == null) &&
                            (r.EsActivo)
                        orderby r.Texto
                        select r).ToList();
            }
        }

        public ItemMenu ObtenerPorIdItemMenu(Guid idItemMenu)
        {
            return (from r in db.ItemMenu
                    where (r.IdItemMenu == idItemMenu) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public ItemMenu ObtenerPorRuta(string ruta)
        {
            return (from r in db.ItemMenu
                    where (r.Ruta.ToLower().Contains(ruta.ToLower())) &&
                            (r.EsActivo)
                select r).FirstOrDefault();
        }

        public List<ItemMenu> ObtenerPorIdItemMenuPadre(Guid idItemMenuPadre, Guid? idRol = null, bool? mostrarVisibles = null)
        {
            if (idRol != null)
            {
                return (from r in db.ItemMenu
                    where ((r.IdItemMenuPadre == idItemMenuPadre) && (r.ItemRol.Any(iR => iR.IdRol == (Guid) idRol))) &&
                          (mostrarVisibles != null ? r.Visible == mostrarVisibles : true) &&
                          (r.EsActivo)
                    select r).ToList();
            }
            else
            {
                return (from r in db.ItemMenu
                        where (r.IdItemMenuPadre == idItemMenuPadre) &&
                              (mostrarVisibles != null ? r.Visible == mostrarVisibles : true) &&
                              (r.EsActivo)
                        select r).ToList();
            }
        }

        public string ObtenerRutaMenu(Guid idItemMenu, bool actual = false)
        {
            ItemMenu itemMenu = ObtenerPorIdItemMenu(idItemMenu);
            string textoHtml = "<li " + (actual ? "class='active'" : "") + ">" + itemMenu.Texto + "</li>";
            if (itemMenu.IdItemMenuPadre != null)
            {
                return ObtenerRutaMenu((Guid) itemMenu.IdItemMenuPadre) + textoHtml;
            }
            else
            {
                return textoHtml;
            }
        }

        public List<ItemMenu> ObtenerMenuPorRol(Guid? idRol = null)
        {
            List<ItemMenu> q = new List<ItemMenu>();

            if (idRol != null)
            {
                q = (from r in db.ItemMenu
                    where (r.ItemRol.Any(ir => ir.IdRol == idRol)) &&
                          (r.EsActivo)
                    orderby r.Texto
                    select r).ToList();
            }
            else
            {
                q = (from r in db.ItemMenu
                     where (r.EsActivo)
                     orderby r.Texto
                     select r).ToList();
            }
            return q;
        }

        public string ObtenerMenuCompleto(Guid? idRol = null)
        {
            string menu = "<nav class='navbar navbar-default' role='navigation'>" +
                          "   <div class='container-fluid'>" +
                          "      <div class='collapse navbar-collapse' id='bs-example-navbar-collapse-1'>" +
                          "         <ul class='nav navbar-nav'>" +
                          "            [MENU]" +
                          "         </ul>" +
                          "      </div>" +
                          "   </div>" +
                          "</nav>";
            List<ItemMenu> listaItemMenu = Listar(idRol);
            foreach (ItemMenu itemMenu in listaItemMenu)
            {
                menu = menu.Replace("[MENU]", ObtenerTextoHtml(itemMenu) + "[MENU]");
            }
            return menu;
        }

        public string ObtenerTextoHtml(ItemMenu itemMenu)
        {
            if (itemMenu.ItemRol.Count > 0)
                return "<li class='dropdown'>" +
                       "   <a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + itemMenu.Texto + "<span class='caret'></span></a>" + 
                       "   [LISTA]" +
                       "</li>";
            else
                return "<li >" + itemMenu.Texto + "</li>";
        }
    }
}
