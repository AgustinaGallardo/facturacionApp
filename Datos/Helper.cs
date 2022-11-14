using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CRUDfacturacion.Dominio;

namespace FacturacionFINAL.Datos
{
    internal class Helper
    {
        SqlConnection cnn = new SqlConnection(Properties.Resources.cnnFactura);
        private static Helper instancia;

        public static Helper ObtenerInstancia()
        {
            if(instancia == null)
                instancia = new Helper();
            return instancia;
        }

        public int ObtenerProximo(string sp_nombre,string OutPut)
        {
            cnn.Open();
            SqlCommand cmdNext = new SqlCommand();
            cmdNext.CommandText = sp_nombre;
            cmdNext.CommandType = CommandType.StoredProcedure;
            cmdNext.Connection=cnn;

            SqlParameter pOutPut = new SqlParameter();
            pOutPut.ParameterName = OutPut;
            pOutPut.Direction = ParameterDirection.Output;
            pOutPut.DbType = DbType.Int32;
            cmdNext.Parameters.Add(pOutPut);
            cmdNext.ExecuteNonQuery();

            cnn.Close();
            return (int)pOutPut.Value;
        }

        public DataTable CargarCombo(string sp_nombre)
        {
            DataTable dt = new DataTable();
            cnn.Open();
            SqlCommand cmdCombo= new SqlCommand();
            cmdCombo.Connection=cnn;
            cmdCombo.CommandText=sp_nombre;
            cmdCombo.CommandType = CommandType.StoredProcedure;

            dt.Load(cmdCombo.ExecuteReader());
            cnn.Close();
            return dt;
        }
        public bool ConformarFactura(Factura oFactura)
        {
            bool ok = true;
            SqlTransaction t = null;
            try
            { 
                SqlCommand cmdMaestro = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();
               
                
                cmdMaestro.Connection=cnn;
                cmdMaestro.CommandText="SP_INSERT_MAESTRO";
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.Transaction=t;

                cmdMaestro.Parameters.AddWithValue("@fecha", oFactura.Fecha);
                cmdMaestro.Parameters.AddWithValue("@id_formapago", oFactura.FormaPago.IdFormaPago);
                cmdMaestro.Parameters.AddWithValue("@cliente", oFactura.Cliente);

                SqlParameter pOutPut = new SqlParameter();
                pOutPut.ParameterName="@nroFactura";
                pOutPut.Direction= ParameterDirection.Output;
                pOutPut.DbType=DbType.Int32;

                cmdMaestro.Parameters.Add(pOutPut);

                cmdMaestro.ExecuteNonQuery();
                int nroFactura = (int)pOutPut.Value;

                foreach(DetalleFactura item in oFactura.ListDetalles)
                {
                    SqlCommand cmdDetalle = new SqlCommand();
                    cmdDetalle.Connection=cnn;
                    cmdDetalle.Transaction=t;
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.CommandText = "sp_insert_detalle";

                    cmdDetalle.Parameters.AddWithValue("@nroFactura", nroFactura);
                    cmdDetalle.Parameters.AddWithValue("@id_articulo", item.Articulo.IdArticulo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);

                    cmdDetalle.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch (Exception)
            {
                if(t !=null)
                    t.Rollback();
                return ok  = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return ok;
        }

    }
}
