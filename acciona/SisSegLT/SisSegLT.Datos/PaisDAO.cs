using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class PaisDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(Pais entidad)
        {
            try
            {
                bool exito = false;
                db.Pais.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Pais entidad)
        {
            try
            {
                bool exito = false;

                Pais modificado = CopiarEntidad(entidad);
                db.Pais.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Pais entidad)
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

        public Pais CopiarEntidad(Pais entidad)
        {
            Pais nuevo = new Pais()
            {
                IdPais = entidad.IdPais,
                Nombre = entidad.Nombre,
                Nacionalidad = entidad.Nacionalidad,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Pais> Listar()
        {
            return (from r in db.Pais
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public List<Pais> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Pais
                    where (r.Nombre.ToLowerInvariant().Equals(nombre.ToLowerInvariant())) &&
                          (r.EsActivo)
                    orderby r.Nombre
                    select r).ToList();
        }

        public Pais ObtenerPorIdPais(Guid idPais)
        {
            return (from r in db.Pais
                    where (r.IdPais == idPais) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }
    }
}
