using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DatosClienteImpDAO
    {
        private SisSegDB db = new SisSegDB();

        public DatosClienteImp CopiarEntidad(DatosClienteImp entidad)
        {
            DatosClienteImp nuevo = new DatosClienteImp()
            {
                IdClienteImp = entidad.IdClienteImp,
                NoCedula = entidad.NoCedula,
                Nombres = entidad.Nombres,
                Apellidos = entidad.Apellidos,
                FechaInicio = entidad.FechaInicio,
                Sexo = entidad.Sexo,
                FechaNacimiento = entidad.FechaNacimiento,
                EstadoCivil = entidad.EstadoCivil,
                TelefonoCliente = entidad.TelefonoCliente,
                Domicilio = entidad.Domicilio,
                Departamento = entidad.Departamento,
                Casa = entidad.Casa,
                TelefonoResidencia = entidad.TelefonoResidencia,
                CodigoProfesion = entidad.CodigoProfesion,
                Profesion = entidad.Profesion,
                NoMiembros = entidad.NoMiembros,
                NoDependientes = entidad.NoDependientes,
                CodigoTipoNegocio = entidad.CodigoTipoNegocio,
                NombreTipoNegocio = entidad.NombreTipoNegocio,
                DireccionNegocio = entidad.DireccionNegocio,
                CasaNegocio = entidad.CasaNegocio,
                Metodologia = entidad.Metodologia,
                NoExpediente = entidad.NoExpediente,
                CodigoAgencia = entidad.CodigoAgencia,
                CodigoPromotor = entidad.CodigoPromotor,
                NombrePromotor = entidad.NombrePromotor,
                FechaActualizacion = entidad.FechaActualizacion
            };
            return nuevo;
        }

        public List<DatosClienteImp> Listar()
        {
            return (from r in db.DatosClienteImp
                    orderby r.NoCedula
                    select r).ToList();
        }

        public DatosClienteImp ObtenerPorIdDatosClienteImp(Guid idDatosClienteImp)
        {
            return (from r in db.DatosClienteImp
                    where (r.IdClienteImp == idDatosClienteImp)
                    select r).FirstOrDefault();
        }

        public List<DatosClienteImp> ObtenerPorCedula(string noCedula)
        {
            return (from r in db.DatosClienteImp
                    where r.NoCedula.Equals(noCedula)
                    orderby r.FechaActualizacion descending
                    select r).ToList();
        }

    }
}
