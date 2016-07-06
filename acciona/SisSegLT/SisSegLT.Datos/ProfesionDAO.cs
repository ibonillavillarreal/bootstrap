using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ProfesionDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(Profesion entidad)
        {
            try
            {
                bool exito = false;
                db.Profesion.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Profesion entidad)
        {
            try
            {
                bool exito = false;

                Profesion modificado = CopiarEntidad(entidad);
                db.Profesion.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Profesion entidad)
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

        public Profesion CopiarEntidad(Profesion entidad)
        {
            Profesion nuevo = new Profesion()
            {
                IdProfesion = entidad.IdProfesion,
                Nombre = entidad.Nombre,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro
            };
            return nuevo;
        }

        public List<Profesion> Listar()
        {
            return (from r in db.Profesion
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public Profesion ObtenerPorIdProfesion(Guid idProfesion)
        {
            return (from r in db.Profesion
                    where (r.IdProfesion == idProfesion) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Profesion> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Profesion
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }
    }
}
