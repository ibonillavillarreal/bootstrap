using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class MetodologiaDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(Metodologia entidad)
        {
            try
            {
                bool exito = false;
                db.Metodologia.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Metodologia entidad)
        {
            try
            {
                bool exito = false;

                Metodologia modificado = CopiarEntidad(entidad);
                db.Metodologia.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Metodologia entidad)
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

        public Metodologia CopiarEntidad(Metodologia entidad)
        {
            Metodologia nuevo = new Metodologia()
            {
                IdMetodologia = entidad.IdMetodologia,
                Nombre = entidad.Nombre,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Metodologia> Listar()
        {
            return (from r in db.Metodologia
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public Metodologia ObtenerPorIdMetodologia(Guid idMetodologia)
        {
            return (from r in db.Metodologia
                    where (r.IdMetodologia == idMetodologia) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Metodologia> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Metodologia
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }
    }
}
