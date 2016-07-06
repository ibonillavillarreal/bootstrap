using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class RolDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(Rol entidad)
        {
            try
            {
                bool exito = false;
                db.Rol.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Rol entidad)
        {
            try
            {
                bool exito = false;

                Rol modificado = CopiarEntidad(entidad);
                db.Rol.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Rol entidad)
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

        public Rol CopiarEntidad(Rol entidad)
        {
            Rol nuevo = new Rol()
            {
                IdRol = entidad.IdRol,
                Nombre = entidad.Nombre,
                FechaRegistro = entidad.FechaRegistro,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Rol> Listar()
        {
            return (from r in db.Rol
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public List<Rol> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Rol
                    where (r.Nombre.ToLowerInvariant().Equals(nombre.ToLowerInvariant())) &&
                          (r.EsActivo)
                orderby r.Nombre
                select r).ToList();
        }

        public Rol ObtenerPorIdRol(Guid idRol)
        {
            return (from r in db.Rol
                    where (r.IdRol == idRol) &&
                          (r.EsActivo)
                select r).FirstOrDefault();
        }
    }
}
