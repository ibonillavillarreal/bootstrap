using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class CategoriaBLL
    {
        public bool Insertar(Categoria entidad)
        {
            return new CategoriaDAO().Insertar(entidad);
        }

        public bool Actualizar(Categoria entidad)
        {
            return new CategoriaDAO().Actualizar(entidad);
        }

        public bool Eliminar(Categoria entidad)
        {
            return new CategoriaDAO().Eliminar(entidad);
        }

        public Categoria CopiarEntidad(Categoria entidad)
        {
            return new CategoriaDAO().CopiarEntidad(entidad);
        }

        public List<Categoria> Listar()
        {
            return new CategoriaDAO().Listar();
        }

        public Categoria ObtenerPorIdCategoria(Guid idCategoria)
        {
            return new CategoriaDAO().ObtenerPorIdCategoria(idCategoria);
        }

        public List<Categoria> ObtenerPorIdFactor(Guid idFactor)
        {
            return new CategoriaDAO().ObtenerPorIdFactor(idFactor);
        }

        public List<Categoria> ObtenerPorNombre(string nombre)
        {
            return new CategoriaDAO().ObtenerPorNombre(nombre);
        }
    }
}
