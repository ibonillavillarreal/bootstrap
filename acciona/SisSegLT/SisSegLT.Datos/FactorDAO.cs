using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class FactorDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Factor entidad)
        {
            try
            {
                bool exito = false;
                db.Factor.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Factor entidad)
        {
            try
            {
                bool exito = false;

                Factor modificado = CopiarEntidad(entidad);
                db.Factor.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Factor entidad)
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

        public Factor CopiarEntidad(Factor entidad)
        {
            Factor nuevo = new Factor()
            {
                IdFactor = entidad.IdFactor,
                Nombre = entidad.Nombre,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Factor> Listar()
        {
            return (from r in db.Factor
                    orderby r.Nombre
                    select r).ToList();
        }

        public Factor ObtenerPorIdFactor(Guid idFactor)
        {
            return (from r in db.Factor
                    where (r.IdFactor == idFactor) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Factor> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Factor
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }
    }
}