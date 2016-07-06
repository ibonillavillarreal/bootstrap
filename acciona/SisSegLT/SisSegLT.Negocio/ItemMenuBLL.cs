using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace SisSegLT.Negocio
{
    public class ItemMenuBLL
    {
        public bool Insertar(ItemMenu entidad)
        {
            return new ItemMenuDAO().Insertar(entidad);
        }

        public bool Actualizar(ItemMenu entidad)
        {
            return new ItemMenuDAO().Actualizar(entidad);
        }

        public bool Eliminar(ItemMenu entidad)
        {
            return new ItemMenuDAO().Eliminar(entidad);
        }

        public ItemMenu CopiarEntidad(ItemMenu entidad)
        {
            return new ItemMenuDAO().CopiarEntidad(entidad);
        }

        public List<ItemMenu> Listar(Guid? idRol = null)
        {
            return new ItemMenuDAO().Listar(idRol);
        }

        public ItemMenu ObtenerPorIdItemMenu(Guid idItemMenu)
        {
            return new ItemMenuDAO().ObtenerPorIdItemMenu(idItemMenu);
        }

        public ItemMenu ObtenerPorRuta(string ruta)
        {
            return new ItemMenuDAO().ObtenerPorRuta(ruta);
        }

        public List<ItemMenu> ObtenerPorIdItemMenuPadre(Guid idItemMenuPadre, Guid? idRol = null, bool? mostrarVisibles = null)
        {
            return new ItemMenuDAO().ObtenerPorIdItemMenuPadre(idItemMenuPadre, idRol, mostrarVisibles);
        }

        public string ObtenerRutaMenu(Guid idItemMenu, bool actual = false)
        {
            return new ItemMenuDAO().ObtenerRutaMenu(idItemMenu, actual);
        }

        public List<ItemMenu> ObtenerMenuPorRol(Guid? idRol = null)
        {
            return new ItemMenuDAO().ObtenerMenuPorRol(idRol);
        }

        public string ObtenerMenuCompleto(Guid? idRol = null)
        {
            return new ItemMenuDAO().ObtenerMenuCompleto(idRol);
        }

        public string ObtenerTextoHtml(ItemMenu itemMenu)
        {
            return new ItemMenuDAO().ObtenerTextoHtml(itemMenu);
        }
    }
}
