using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace SisSegLT.Negocio
{
    public class ItemRolBLL
    {
        public bool Insertar(ItemRol entidad)
        {
            return new ItemRolDAO().Insertar(entidad);
        }

        public bool Actualizar(ItemRol entidad)
        {
            return new ItemRolDAO().Actualizar(entidad);
        }

        public bool Eliminar(ItemRol entidad)
        {
            return new ItemRolDAO().Eliminar(entidad);
        }

        public ItemRol CopiarEntidad(ItemRol entidad)
        {
            return new ItemRolDAO().CopiarEntidad(entidad);
        }

        public List<ItemRol> Listar()
        {
            return new ItemRolDAO().Listar();
        }

        public ItemRol ObtenerPorIdItemRol(Guid idItemRol)
        {
            return new ItemRolDAO().ObtenerPorIdItemRol(idItemRol);
        }

        public List<ItemRol> ObtenerPorIdItemMenu(Guid idItemMenu)
        {
            return new ItemRolDAO().ObtenerPorIdItemMenu(idItemMenu);
        }

        public List<ItemRol> ObtenerPorIdRol(Guid idRol)
        {
            return new ItemRolDAO().ObtenerPorIdRol(idRol);
        }
    }
}
