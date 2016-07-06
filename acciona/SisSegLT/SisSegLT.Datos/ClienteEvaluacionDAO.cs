using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ClienteEvaluacionDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(ClienteEvaluacion entidad)
        {
            try
            {
                bool exito = false;
                db.ClienteEvaluacion.Add(entidad);
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(ClienteEvaluacion entidad)
        {
            bool exito = false;
            try
            {
                db.ClienteEvaluacion.Attach(entidad);
                db.Entry(entidad).State = System.Data.EntityState.Modified;
                ActualizarEvaluacionCategoria(entidad.EvaluacionCategoria);
                exito = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                exito = false;
                throw ex;
            }
            return exito;
        }

        private void ActualizarEvaluacionCategoria(ICollection<EvaluacionCategoria> collection)
        {
            foreach (var item in collection)
            {
                db.Entry(item).State = System.Data.EntityState.Modified;
                foreach (var item2 in item.EvaluacionCategoriaClasificacion)
                {
                    db.Entry(item2).State = System.Data.EntityState.Modified;
                }
            }
        }

        public List<ClienteEvaluacion> ObtenerEvaluacionesPorIdCliente(Guid idCliente)
        {
            var resultado = db.ClienteEvaluacion.Where(x => x.IdCliente == idCliente);
            return resultado.ToList();
        }

        public ClienteEvaluacion ObtenerEvaluacionPorIdEvaluacion(Guid idClienteEvaluacion)
        {
            var resultado = db.ClienteEvaluacion.FirstOrDefault(x => x.IdClienteEvaluacion == idClienteEvaluacion);
            return resultado;
        }

        public IQueryable<vwEvaluaciones> ObtenerEvaluacionesPorFiltro(DateTime fechaInicio, DateTime fechaFin, string sucursal, string metodologia, string nivelRiesgo, string promotor)
        {
            DateTime fecFin = fechaFin.AddHours(23.9997);
            DateTime fecInicio = fechaInicio.AddHours(00.0000);                

            IQueryable<vwEvaluaciones> resultado = null;
            if (fecInicio != new DateTime() && fecFin != new DateTime())
            {
                resultado = db.vwEvaluaciones.Where(x => x.FechaRegistro >= fecInicio && x.FechaRegistro <= fecFin);
            }
            if (!string.IsNullOrEmpty(sucursal))
            {
                resultado = resultado.Where(x => x.Nombre == sucursal);
            }
            if (!string.IsNullOrEmpty(metodologia))
            {
                resultado = resultado.Where(x => x.Metodologia == metodologia);
            }
            if (!string.IsNullOrEmpty(nivelRiesgo))
            {
                resultado = resultado.Where(x => x.NivelRiesgo == nivelRiesgo);
            }
            if (!string.IsNullOrEmpty(promotor))
            {
                resultado = resultado.Where(x => x.Promotor == promotor);
            }

            return resultado;
        }

        public List<vwEvaluacionCliente> ObtenervwClienteEvaluacionPorIdEvaluacion(Guid idClienteEvaluacion)
        {
            var resultado = db.vwEvaluacionCliente.Where(x => x.IdClienteEvaluacion == idClienteEvaluacion);
            return resultado.ToList();
        }

        public List<vwEvaluacionClienteCategoria> ObtenervwClienteEvaluacionCategoriaPorIdEvaluacion(Guid idClienteEvaluacion)
        {
            var resultado = db.vwEvaluacionClienteCategoria.Where(x => x.IdClienteEvaluacion == idClienteEvaluacion);
            return resultado.ToList();
        }
    }
}