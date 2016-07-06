using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ReferenciaCrediticiaDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(ReferenciaCrediticia entidad)
        {
            try
            {
                bool exito = false;
                db.ReferenciaCrediticia.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(ReferenciaCrediticia entidad)
        {
            try
            {
                bool exito = false;

                ReferenciaCrediticia modificado = CopiarEntidad(entidad);
                db.ReferenciaCrediticia.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(ReferenciaCrediticia entidad)
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

        public ReferenciaCrediticia CopiarEntidad(ReferenciaCrediticia entidad)
        {
            ReferenciaCrediticia nuevo = new ReferenciaCrediticia()
            {
                IdReferenciaCrediticia = entidad.IdReferenciaCrediticia,
                IdCliente = entidad.IdCliente,
                Banco = entidad.Banco,
                Monto = entidad.Monto,
                Plazo = entidad.Plazo,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UsuarioIP = entidad.UsuarioIP,
                UsuarioPC = entidad.UsuarioPC
            };
            return nuevo;
        }

        public List<ReferenciaCrediticia> Listar()
        {
            return (from r in db.ReferenciaCrediticia
                    orderby r.Banco
                    select r).ToList();
        }

        public ReferenciaCrediticia ObtenerPorIdReferenciaCrediticia(Guid idReferenciaCrediticia)
        {
            return (from r in db.ReferenciaCrediticia
                    where r.IdReferenciaCrediticia == idReferenciaCrediticia
                    select r).FirstOrDefault();
        }

        public List<ReferenciaCrediticia> ObtenerPorNombreBanco(string nombreBanco)
        {
            return (from r in db.ReferenciaCrediticia
                    where r.Banco.Equals(nombreBanco)
                    select r).ToList();
        }

        public List<ReferenciaCrediticia> ObtenerReferenciaCrediticiasPorIdCliente(Guid idCliente)
        {
            return (from r in db.ReferenciaCrediticia
                    where r.IdCliente.Equals(idCliente) && r.EsActivo == true
                    select r).ToList();
        }
    }
}