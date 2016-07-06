using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ListaCobroDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool InsertarRutaPrincipal(RutaCobroPrincipal Ruta)
        {
            try
            {
                bool exito = false;
                db.RutaCobroPrincipal.Add(Ruta); 
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Insertar(List<RutaCobro> Rutas)
        {
            try
            {
                bool exito = false;

                foreach (var ruta in Rutas)
                {
                    db.RutaCobro.Add(ruta);
                }
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(RutaCobro entidad)
        {
            try
            {
                bool exito = false;

                RutaCobro modificado = CopiarEntidad(entidad);
                db.RutaCobro.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }


        public bool ActualizarRutaPadre(RutaCobroPrincipal entidad)
        {
            try
            {
                bool exito = false;

                RutaCobroPrincipal modificado = CopiarEntidadPrincipal(entidad);
                db.RutaCobroPrincipal.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public RutaCobroPrincipal CopiarEntidadPrincipal(RutaCobroPrincipal entidad)
        {
            RutaCobroPrincipal nuevo = new RutaCobroPrincipal()
            {                  
                IdRutaCobro = entidad.IdRutaCobro,
                IdColector = entidad.IdColector,
                Serie = entidad.Serie,
                Nombre = entidad.Nombre,
                Impreso = entidad.Impreso,
                Procesado = entidad.Procesado,                  
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                DireccionIP = entidad.DireccionIP,
                DireccionPC = entidad.DireccionPC
               
            };
            return nuevo;
        }

        public RutaCobro CopiarEntidad(RutaCobro entidad)
        {
            RutaCobro nuevo = new RutaCobro()
            {
                IdCobro = entidad.IdCobro,
                IdRutaCobro = entidad.IdRutaCobro,
                IdCuenta  = entidad.IdCuenta,
                NombreCompleto = entidad.NombreCompleto,
                NoIdentificacion = entidad.NoIdentificacion,
                NoCuenta = entidad.NoCuenta,
                Direccion = entidad.Direccion,
                Frecuencia = entidad.Frecuencia,
                SaldoTotal = entidad.SaldoTotal,
                CuotadelDia = entidad.CuotadelDia,
                Mora = entidad.Mora,
                CuotasPendientes = entidad.CuotasPendientes,
                CuotaIdeal  = entidad.CuotaIdeal,
                Colector = entidad.Colector,
                MontoRecibido = entidad.MontoRecibido,
                NoRecibo = entidad.NoRecibo,
                Diferencia = entidad.Diferencia,
                Procesado = entidad.Procesado,
                FechaCobro = entidad.FechaCobro,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC
            };
            return nuevo;
        }

        public List<SP_ListarRutaCobro_Result> ListarRutadeCobro(string fecha, string nombre)
        {
            var fechainicio = Convert.ToDateTime(fecha);
     
            return db.SP_ListarRutaCobro(fechainicio, nombre).ToList();
        }

        public List<SP_ListarConcilicacionRecibo_Result> ListarConciliacionRecibos(string fecha, string serie)
        {
            //var fechainicio = DateTime.ParseExact(fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return db.SP_ListarConcilicacionRecibo(Convert.ToDateTime(fecha), serie).ToList();
        }

        public List<SP_RecuperacionCarteraPorColector_Result> ListarRecuperacionCarteraXColector(string fechaInicio, string FechaFin, string serie, string idusuario)
        {               
            //var fechainicio = DateTime.ParseExact(fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return db.SP_RecuperacionCarteraPorColector(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaInicio), serie, Guid.Parse(idusuario)).ToList();
        }

        public RutaCobro ObtenerPorIdCobro(Guid idCobro)
        {
            return (from r in db.RutaCobro
                    where (r.IdCobro == idCobro)
                    select r).FirstOrDefault();
        }

        public RutaCobroPrincipal ObtenerPorIdCobroPrincipal(Guid idCobroPrincipal)
        {
            return (from r in db.RutaCobroPrincipal
                    where (r.IdRutaCobro == idCobroPrincipal)
                    select r).FirstOrDefault();
        }

        public IQueryable<vw_DatosCredito2> ObtenerCreditosNuevos(string fecha, string fechafi, string IdPromotor, bool todo)
        {
            IQueryable<vw_DatosCredito2> resultado = null;
            DateTime fechai = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaf = DateTime.ParseExact(fechafi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            
            if (todo)
            {
                resultado = db.vw_DatosCredito2.Where(x => x.fechadesembolso >= fechai && x.fechadesembolso <= fechaf);
                
            }
            else
            {
                Guid idpromotor = Guid.Parse(IdPromotor);
                resultado = db.vw_DatosCredito2.Where(x => x.fechadesembolso >= fechai && x.fechadesembolso <= fechaf && x.IdPromotor == idpromotor);
            }
            return resultado;
           
        }

        public List<SP_CreditoPorMes_Result> ListarCreditosPorMes(int anio)
        {
            //var fechainicio = DateTime.ParseExact(fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return db.SP_CreditoPorMes(anio).ToList();
        }

        public List<SP_RecuperacionPorMes_Result> ListarRecuperacionPorMes(int anio)
        {
            //var fechainicio = DateTime.ParseExact(fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return db.SP_RecuperacionPorMes(anio).ToList();
        }

    }
}
