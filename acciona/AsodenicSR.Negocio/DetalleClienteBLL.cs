using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class DetalleClienteBLL
    {
        public bool Insertar(DetalleCliente entidad)
        {
            return new DetalleClienteDAO().Insertar(entidad);
        }

        public DetalleCliente ObtenerDetalleClientePorIdCliente(Guid idCliente)
        {
            return new DetalleClienteDAO().ObtenerDetalleClientePorIdCliente(idCliente);
        }

        public bool Actualizar(DetalleCliente detalleCliente)
        {
            return new DetalleClienteDAO().Actualizar(detalleCliente);
        }
    }
}