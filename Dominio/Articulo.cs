using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDfacturacion.Dominio
{
     class Articulo
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public bool ProductoActivo { get; set; }

        public Articulo()
        {
            IdArticulo=0;
            Nombre = Nombre;
            PrecioUnitario = PrecioUnitario;
            ProductoActivo = true;
        }

        public Articulo(int idArticulo, string nombre, double precioUnitario)
        {

            IdArticulo = idArticulo;
            Nombre = nombre;
            PrecioUnitario = precioUnitario;

        }
        public Articulo(int idArticulo, string nombre)
        {
            IdArticulo = idArticulo;
            Nombre = nombre;
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
