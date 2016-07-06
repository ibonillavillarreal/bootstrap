using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class VehiculosDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(tVehiculos entidad)
        {
            try
            {
                bool exito = false;
                db.tVehiculos.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Actualizar(tVehiculos entidad)
        {
            try
            {
                bool exito = false;

                tVehiculos modificado = CopiarEntidad(entidad);
                db.tVehiculos.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(tVehiculos entidad)
        {
            try
            {
                bool exito = false;
                exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public tVehiculos CopiarEntidad(tVehiculos entidad)
        {
            tVehiculos nuevo = new tVehiculos()
            {
                IdVehiculo = entidad.IdVehiculo,
                Codigo = entidad.Codigo,
                Marca = entidad.Marca,
                Modelo = entidad.Modelo,
                Placa = entidad.Placa,
                Color = entidad.Color,
                Anio = entidad.Anio,
                Tipo = entidad.Tipo,
                Chasis = entidad.Chasis,
                Motor = entidad.Motor,
                Circulacion = entidad.Circulacion,
                Usuario = entidad.Usuario,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC
            };
            return nuevo;
        }

        public List<tVehiculos> Listar()
        {
            return (from r in db.tVehiculos
                    select r).ToList();
        }

        public tVehiculos ObtenerPorIdVehiculos(Guid idVehiculos)
        {
            return (from r in db.tVehiculos
                    where (r.IdVehiculo == idVehiculos) 
                    select r).FirstOrDefault();
        }

        
    }
}
