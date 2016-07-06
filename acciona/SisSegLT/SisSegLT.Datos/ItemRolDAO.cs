using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ItemRolDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(ItemRol entidad)
        {
            try
            {
                bool exito = false;
                db.ItemRol.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(ItemRol entidad)
        {
            try
            {
                bool exito = false;

                ItemRol modificado = CopiarEntidad(entidad);
                db.ItemRol.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(ItemRol entidad)
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

        public ItemRol CopiarEntidad(ItemRol entidad)
        {
            ItemRol nuevo = new ItemRol()
            {
                IdItemRol = entidad.IdItemRol,
                IdItemMenu = entidad.IdItemMenu,
                IdRol = entidad.IdRol,
                FechaRegistro = entidad.FechaRegistro,
                EsActivo =  entidad.EsActivo
            };
            return nuevo;
        }

        public List<ItemRol> Listar()
        {
            return (from r in db.ItemRol
                    where r.EsActivo
                    orderby r.IdItemMenu
                    select r).ToList();
        }

        public ItemRol ObtenerPorIdItemRol(Guid idItemRol)
        {
            return (from r in db.ItemRol
                    where (r.IdItemRol == idItemRol) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<ItemRol> ObtenerPorIdItemMenu(Guid idItemMenu)
        {
            return (from r in db.ItemRol
                    where (r.IdItemMenu == idItemMenu) &&
                          (r.EsActivo)
                orderby r.FechaRegistro
                select r).ToList();
        }

        public List<ItemRol> ObtenerPorIdRol(Guid idRol)
        {
            return (from r in db.ItemRol
                    where (r.IdRol == idRol) &&
                          (r.EsActivo)
                    orderby r.IdItemMenu
                    select r).ToList();
        }
    }
}
