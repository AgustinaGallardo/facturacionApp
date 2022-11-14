using CRUDfacturacion.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFINAL.Servicios.Intefaz
{
    internal interface IServicio
    {
        int ObtenerProximo();

        List<Articulo> ObtenerArticulos();

        List<FormaPago> ObtenerFormasPagos();
    }
}
