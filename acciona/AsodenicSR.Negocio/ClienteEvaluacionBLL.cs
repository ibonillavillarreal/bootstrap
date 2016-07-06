using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class ClienteEvaluacionBLL
    {
        public bool GuardarEvaluacion(ClienteEvaluacion evaluacionCliente)
        {
            return new ClienteEvaluacionDAO().Insertar(evaluacionCliente);
        }

        public bool ActualizarEvaluacion(ClienteEvaluacion evaluacionCliente)
        {
            return new ClienteEvaluacionDAO().Actualizar(evaluacionCliente);
        }

        public List<ClienteEvaluacion> ObtenerEvaluacionesPorIdCliente(Guid idCliente)
        {
            return new ClienteEvaluacionDAO().ObtenerEvaluacionesPorIdCliente(idCliente);
        }

        public ClienteEvaluacion ObtenerEvaluacionPorIdEvaluacion(Guid idCliente)
        {
            return new ClienteEvaluacionDAO().ObtenerEvaluacionPorIdEvaluacion(idCliente);
        }

        public IQueryable<vwEvaluaciones> ObtenerEvaluacionesPorFiltro(DateTime fechaInicio, DateTime fechaFin, string sucursal, string metodologia, string nivelRiesgo, string promotor)
        {
            return new ClienteEvaluacionDAO().ObtenerEvaluacionesPorFiltro(fechaInicio, fechaFin, sucursal, metodologia, nivelRiesgo, promotor);
        }

        public List<vwEvaluacionCliente> ObtenervwClienteEvaluacionPorIdEvaluacion(Guid idClienteEvaluacion)
        {
            return new ClienteEvaluacionDAO().ObtenervwClienteEvaluacionPorIdEvaluacion(idClienteEvaluacion);
        }

        public List<vwEvaluacionClienteCategoria> ObtenervwClienteEvaluacionCategoriaPorIdEvaluacion(Guid idClienteEvaluacion)
        {
            return new ClienteEvaluacionDAO().ObtenervwClienteEvaluacionCategoriaPorIdEvaluacion(idClienteEvaluacion);
        }
    }
}