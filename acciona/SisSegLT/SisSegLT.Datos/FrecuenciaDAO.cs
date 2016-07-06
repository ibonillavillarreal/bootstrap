using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class FrecuenciaDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tFrecuencia entidad)
        {
            try
            {
                bool exito = false;
                db.tFrecuencia.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tFrecuencia entidad)
        {
            try
            {
                bool exito = false;

                tFrecuencia modificado = CopiarEntidad(entidad);
                db.tFrecuencia.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(tFrecuencia entidad)
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

        public tFrecuencia CopiarEntidad(tFrecuencia entidad)
        {
            tFrecuencia nuevo = new tFrecuencia()
            {
                IdFrecuencia = entidad.IdFrecuencia,                
                Descripcion = entidad.Descripcion,
                Formula = entidad.Formula,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tFrecuencia> Listar()
        {
            return (from r in db.tFrecuencia
                    orderby r.Descripcion
                    select r).ToList();
        }
        public List<tFrecuencia> ListarActivos()
        {
            return (from r in db.tFrecuencia
                    where r.EsActivo == true
                    orderby r.Descripcion
                    select r).ToList();
        }

        public tFrecuencia ObtenerPorIdFrecuencia(Guid idFrecuencia)
        {
            return (from r in db.tFrecuencia
                    where (r.IdFrecuencia == idFrecuencia)
                    select r).FirstOrDefault();
        }

        public List<tFrecuencia> ObtenerPorNombre(string nombre)
        {
            return (from r in db.tFrecuencia
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }
    }
}
