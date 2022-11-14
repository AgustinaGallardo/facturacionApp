using CRUDfacturacion.Dominio;
using FacturacionFINAL.Datos.Intefaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFINAL.Datos.Implementacion
{
    internal class DaoFactura : IDaoFactura
    {
        public List<Articulo> ObtenerArticulos()
        {
            List<Articulo> lst = new List<Articulo>();
            string sp_nombre = "SP_ARTICULOS";
            DataTable tabla= Helper.ObtenerInstancia().CargarCombo(sp_nombre);
            foreach (DataRow dr in tabla.Rows)
            {
                int id = Convert.ToInt32(dr["id_articulo"].ToString());
                string nombre = dr["descripcion"].ToString();
                double precio = Convert.ToDouble(dr["pre_unitario"].ToString());

                Articulo aux = new Articulo(id, nombre, precio);
                lst.Add(aux);
            }
            return lst;
        }

        public List<FormaPago> ObtenerFormasPagos()
        {
            List<FormaPago> lst = new List<FormaPago>();
            string sp_nombre = "SP_FORMAS_PAGO";
            DataTable table = Helper.ObtenerInstancia().CargarCombo(sp_nombre);
            foreach(DataRow dr in table.Rows)
            {
                int id = Convert.ToInt32(dr["id_formapago"].ToString()) ;
                string nombre = dr["formapago"].ToString();
              
                FormaPago aux = new FormaPago(id, nombre);
                lst.Add(aux);
            }
            return lst;
        }

        public int ObtenerProximo()
        {
            string sp_nombre = "sp_proximo";
            string outPut = "@Next";
            return Helper.ObtenerInstancia().ObtenerProximo(sp_nombre, outPut);
        }
    }
}
