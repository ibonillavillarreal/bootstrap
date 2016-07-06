using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class ProfesionBLL
    {
        public bool Insertar(Profesion entidad)
        {
            return new ProfesionDAO().Insertar(entidad);
        }

        public bool Actualizar(Profesion entidad)
        {
            return new ProfesionDAO().Actualizar(entidad);
        }

        public bool Eliminar(Profesion entidad)
        {
            return new ProfesionDAO().Eliminar(entidad);
        }

        public Profesion CopiarEntidad(Profesion entidad)
        {
            return new ProfesionDAO().CopiarEntidad(entidad);
        }

        public List<Profesion> Listar()
        {
            return new ProfesionDAO().Listar();
        }

        public Profesion ObtenerPorIdProfesion(Guid idProfesion)
        {
            return new ProfesionDAO().ObtenerPorIdProfesion(idProfesion);
        }

        public List<Profesion> ObtenerPorNombre(string nombre)
        {
            return new ProfesionDAO().ObtenerPorNombre(nombre);
        }
    }
}
