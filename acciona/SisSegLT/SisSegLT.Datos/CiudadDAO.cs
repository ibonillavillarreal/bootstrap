using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class CiudadDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(Ciudad entidad)
        {
            try
            {
                bool exito = false;
                db.Ciudad.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Ciudad entidad)
        {
            try
            {
                bool exito = false;

                Ciudad modificado = CopiarEntidad(entidad);
                db.Ciudad.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Ciudad entidad)
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

        public Ciudad CopiarEntidad(Ciudad entidad)
        {
            Ciudad nuevo = new Ciudad()
            {
                IdCiudad = entidad.IdCiudad,
                IdPais = entidad.IdPais,
                Nombre = entidad.Nombre,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Ciudad> Listar()
        {
            return (from r in db.Ciudad
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public List<Ciudad> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Ciudad
                where (r.Nombre.ToLowerInvariant().Equals(nombre.ToLowerInvariant())) && (r.EsActivo)
                orderby r.Nombre
                select r).ToList();
        }

        public Ciudad ObtenerPorIdCiudad(Guid idCiudad)
        {
            return (from r in db.Ciudad
                    where (r.IdCiudad == idCiudad) && (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Ciudad> ObtenerPorIdPais(Guid idPais)
        {
            return (from r in db.Ciudad
                where (r.IdPais == idPais) && (r.EsActivo)
                select r).ToList();
        }
    }
}
