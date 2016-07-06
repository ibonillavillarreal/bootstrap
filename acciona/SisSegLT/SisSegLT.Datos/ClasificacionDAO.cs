using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ClasificacionDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Clasificacion entidad)
        {
            try
            {
                bool exito = false;
                db.Clasificacion.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Clasificacion entidad)
        {
            try
            {
                bool exito = false;

                Clasificacion modificado = CopiarEntidad(entidad);
                db.Clasificacion.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Clasificacion entidad)
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

        public Clasificacion CopiarEntidad(Clasificacion entidad)
        {
            Clasificacion nuevo = new Clasificacion()
            {
                IdClasificacion = entidad.IdClasificacion,
                IdCategoria = entidad.IdCategoria,
                Nombre = entidad.Nombre,
                Puntuacion = entidad.Puntuacion,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Clasificacion> Listar()
        {
            return (from r in db.Clasificacion
                    orderby r.Nombre
                    select r).ToList();
        }

        public Clasificacion ObtenerPorIdClasificacion(Guid idClasificacion)
        {
            return (from r in db.Clasificacion
                    where (r.IdClasificacion == idClasificacion) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Clasificacion> ObtenerPorIdCategoria(Guid idCategoria)
        {
            return (from r in db.Clasificacion
                    where (r.IdCategoria == idCategoria) &&
                          (r.EsActivo)
                    select r).ToList();
        }

        public List<Clasificacion> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Clasificacion
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }
    }
}