using CRUDfacturacion.Dominio;
using FacturacionFINAL.Datos.Implementacion;
using FacturacionFINAL.Datos.Intefaz;
using FacturacionFINAL.Servicios.Intefaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFINAL.Servicios.Implementacion
{
    internal class Servicio : IServicio
    {
        private IDaoFactura dao;

        public Servicio()
        {
            dao = new DaoFactura();
        }

        public List<Articulo> ObtenerArticulos()
        {
          return  dao.ObtenerArticulos();
        }

        public List<FormaPago> ObtenerFormasPagos()
        {
            return dao.ObtenerFormasPagos();
        }

        public int ObtenerProximo()
        {
            return dao.ObtenerProximo();
        }
    }
}
