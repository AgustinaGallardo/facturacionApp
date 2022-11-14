using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDfacturacion.Dominio
{
    internal class FormaPago
    {
        public int IdFormaPago { get; set; }
        public string TipoFP { get; set; }

        public FormaPago()
        {
            IdFormaPago = 0;
            TipoFP = string.Empty;
        }
        public FormaPago(int id, string nombre)
        {
            this.IdFormaPago = id;
            this.TipoFP = nombre;
        }
    }
}
