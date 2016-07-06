using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ReferenciasDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Referencias entidad)
        {
            try
            {
                bool exito = false;
                db.Referencias.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Referencias entidad)
        {
            try
            {
                bool exito = false;

                Referencias modificado = CopiarEntidad(entidad);
                db.Referencias.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Referencias entidad)
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

        public Referencias CopiarEntidad(Referencias entidad)
        {
            Referencias nuevo = new Referencias()
            {
                IdReferencia = entidad.IdReferencia,
                IdCliente = entidad.IdCliente,
                NombreCompleto = entidad.NombreCompleto,
                NoIdentificacion = entidad.NoIdentificacion,
                Telefono = entidad.Telefono,
                Tiempo = entidad.Tiempo,
                Sexo = entidad.Sexo,
                CentroLaboral = entidad.CentroLaboral,
                Direccion = entidad.Direccion,
                Profesion = entidad.Profesion,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC
            };
            return nuevo;
        }

        public List<Referencias> Listar()
        {
            return (from r in db.Referencias
                    orderby r.NombreCompleto
                    select r).ToList();
        }

        public Referencias ObtenerPorIdReferencias(Guid idReferencias)
        {
            return (from r in db.Referencias
                    where r.IdReferencia == idReferencias && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<Referencias> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Referencias
                    where r.NombreCompleto.Equals(nombre)
                    select r).ToList();
        }

        public List<Referencias> ObtenerReferenciasPorIdCliente(Guid idCliente)
        {
            return (from r in db.Referencias
                    where r.IdCliente.Equals(idCliente) && r.EsActivo == true
                    select r).ToList();
        }

        public List<vwReferencias> ObtenervwReferenciasPorIdCliente(Guid idCliente)
        {
            return (from r in db.vwReferencias
                    where r.IdCliente.Equals(idCliente)
                    select r).ToList();
        }
    }
}