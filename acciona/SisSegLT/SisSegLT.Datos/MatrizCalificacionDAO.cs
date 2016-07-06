using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class MatrizCalificacionDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(MatrizCalificacion entidad)
        {
            try
            {
                bool exito = false;
                db.MatrizCalificacion.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(MatrizCalificacion entidad)
        {
            try
            {
                bool exito = false;

                MatrizCalificacion modificado = CopiarEntidad(entidad);
                db.MatrizCalificacion.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(MatrizCalificacion entidad)
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

        public MatrizCalificacion CopiarEntidad(MatrizCalificacion entidad)
        {
            MatrizCalificacion nuevo = new MatrizCalificacion()
            {
                IdMatrizCalificacion = entidad.IdMatrizCalificacion,
                Nombre = entidad.Nombre,
                ValorMin = entidad.ValorMin,
                ValorMax = entidad.ValorMax,
                Impacto = entidad.Impacto,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<MatrizCalificacion> Listar()
        {
            return (from r in db.MatrizCalificacion
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public MatrizCalificacion ObtenerPorIdMatrizCalificacion(Guid idMatrizCalificacion)
        {
            return (from r in db.MatrizCalificacion
                    where (r.IdMatrizCalificacion == idMatrizCalificacion) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<MatrizCalificacion> ObtenerPorNombre(string nombre)
        {
            return (from r in db.MatrizCalificacion
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }

        public List<MatrizCalificacion> ObtenerPorValor(int valor)
        {
            return (from r in db.MatrizCalificacion
                    where ((r.ValorMin <= valor) && (valor <= r.ValorMax)) &&
                          (r.EsActivo)
                    select r).ToList();
        }
    }
}
