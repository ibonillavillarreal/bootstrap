using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class FeriadoBLL
    {
        public bool Insertar(Feriados entidad)
        {
            return new FeriadosDAO().Insertar(entidad);
        }

        public bool Actualizar(Feriados entidad)
        {
            return new FeriadosDAO().Actualizar(entidad);
        }
                   

        public Feriados CopiarEntidad(Feriados entidad)
        {
            return new FeriadosDAO().CopiarEntidad(entidad);
        }

        public bool Eliminar(Feriados entidad)
        {
            return new FeriadosDAO().Eliminar(entidad);
        }

        public List<Feriados> Listar()
        {
            return new FeriadosDAO().Listar();
        }

        public Feriados ObtenerPorIdFeriado(Guid IdFeriado)
        {
            return new FeriadosDAO().ObtenerPorIdFeriado(IdFeriado);
        }

        public List<Feriados> ObtenerPorNombre(string nombre)
        {
            return new FeriadosDAO().ObtenerPorNombre(nombre);
        }
        public List<Feriados> ObtenerPorFecha(int dia, int mes, int anio)
        {
            return new FeriadosDAO().ObtenerPorFecha(dia, mes, anio);
        }
    }
}
