using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class CategoriaDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Categoria entidad)
        {
            try
            {
                bool exito = false;
                db.Categoria.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Categoria entidad)
        {
            try
            {
                bool exito = false;

                Categoria modificado = CopiarEntidad(entidad);
                db.Categoria.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Categoria entidad)
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

        public Categoria CopiarEntidad(Categoria entidad)
        {
            Categoria nuevo = new Categoria()
            {
                IdCategoria = entidad.IdCategoria,
                IdFactor = entidad.IdFactor,
                Nombre = entidad.Nombre,
                Ponderacion = entidad.Ponderacion,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Categoria> Listar()
        {
            return (from r in db.Categoria
                    orderby r.Nombre
                    select r).ToList();
        }

        public Categoria ObtenerPorIdCategoria(Guid idCategoria)
        {
            return (from r in db.Categoria
                    where (r.IdCategoria == idCategoria) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Categoria> ObtenerPorIdFactor(Guid idFactor)
        {
            return (from r in db.Categoria
                    where (r.IdFactor == idFactor) &&
                          (r.EsActivo)
                    select r).ToList();
        }

        public List<Categoria> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Categoria
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }
    }
}