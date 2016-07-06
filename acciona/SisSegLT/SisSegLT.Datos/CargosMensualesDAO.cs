using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class CargosMensualesDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(CargosMensuales entidad)
        {
            try
            {
                bool exito = false;
                db.CargosMensuales.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Actualizar(CargosMensuales entidad)
        {
            try
            {
                bool exito = false;

                CargosMensuales modificado = CopiarEntidad(entidad);
                db.CargosMensuales.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(CargosMensuales entidad)
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

        public CargosMensuales CopiarEntidad(CargosMensuales entidad)
        {
            CargosMensuales nuevo = new CargosMensuales()
            {
                IdPrestamo = entidad.IdPrestamo,
                IdTipoTransaccion = entidad.IdTipoTransaccion,
                IdFrecuencia = entidad.IdFrecuencia,
                IdMovimiento = entidad.IdMovimiento,
                Monto = entidad.Monto,
                FechaCorte = entidad.FechaCorte,
                FechaInicio = entidad.FechaInicio,
                FechaFin = entidad.FechaFin,                
                Observaciones = entidad.Observaciones,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC
            };
            return nuevo;
        }

        public List<CargosMensuales> Listar()
        {
            return (from r in db.CargosMensuales
                    where r.EsActivo == true
                    orderby r.FechaCorte
                    select r).ToList();
        }

        public CargosMensuales ObtenerPorIdCargosMensuales(Guid IdPrestamo)
        {
            return (from r in db.CargosMensuales
                    where (r.IdPrestamo == IdPrestamo) && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<CargosMensuales> ObtenerPorTipoTransaccion(string tipoTransaccion)
        {
            return (from r in db.CargosMensuales
                    join t in db.TipoTransaccion on r.IdTipoTransaccion equals t.IdTipoTransaccion
                    where t.Descripcion.Contains(tipoTransaccion)
                    select r).ToList();
        }

        public List<vw_CargosMensuales> ObtenerCargosMensualessPorIdMovimiento(Guid idMovimiento)
        {
            return (from r in db.vw_CargosMensuales
                    where r.IdMovimiento == idMovimiento
                    select r).ToList();
        }
    }
}
