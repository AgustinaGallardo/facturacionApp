using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDfacturacion.Dominio
{
    internal class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }

        public List<DetalleFactura> ListDetalles;

        public Factura()
        {
            NroFactura = 0;
            Fecha = DateTime.Now;
            FormaPago= null;
            Cliente="";
            ListDetalles = new List<DetalleFactura>();
        }
        public Factura(int nroFactura, DateTime fecha, FormaPago formaPago, string cliente, List<DetalleFactura> ListDetalles)
        {
            NroFactura = nroFactura;
            Fecha = fecha;
            FormaPago = formaPago;
            Cliente = cliente;
            this.ListDetalles = ListDetalles;
        }

        public override string ToString()
        {
            return NroFactura.ToString();
        }

        internal void QuitarDetalle(int indice_detalle)
        {
            ListDetalles.RemoveAt(indice_detalle);
        }

        internal void AgregarDetalle(DetalleFactura detalle)
        {
            ListDetalles.Add(detalle);
        }
        public double CalcularTotal()
        {
            double total = 0;
            foreach (DetalleFactura item in ListDetalles)
            {
                total += item.CalcularSubTotal();
            }
            return total;
        }
    }
}
