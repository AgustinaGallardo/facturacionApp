using CRUDfacturacion.Dominio;
using FacturacionFINAL.Datos;
using FacturacionFINAL.Servicios.Implementacion;
using FacturacionFINAL.Servicios.Intefaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionFINAL
{
    public partial class Form1 : Form
    {
        private IServicio gestor;
        private Factura nueva;
        public Form1()
        {
            InitializeComponent();
            gestor = new Servicio();
            nueva = new Factura();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ObtenerProximo();
            ObtenerArticulos();
            ObtenerFormasPagos();

        }

        private void ObtenerFormasPagos()
        {
            cboFormaPago.ValueMember="IdFormaPago";
            cboFormaPago.DisplayMember="TipoFP";
            cboFormaPago.DataSource= gestor.ObtenerFormasPagos();
            cboFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ObtenerArticulos()
        {
            cboArticulo.ValueMember ="IdArticulo";
            cboArticulo.DisplayMember ="Nombre";
            cboArticulo.DataSource=gestor.ObtenerArticulos();
            cboArticulo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ObtenerProximo()
        {
            int Next = gestor.ObtenerProximo();
            if (Next >0)
            {
                lblNext.Text = "Factura Nro: " +Next.ToString();
            }
            else
            {
                MessageBox.Show("Nose puede obtener el proximo", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cboArticulo.SelectedIndex==-1)
            {
                MessageBox.Show("Tiene que elegir un artiulo", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
            foreach(DataGridViewRow row in dgvFactura.Rows)
            {
                if(row.Cells["colArticulo"].Value.ToString().Equals(cboArticulo.Text))
                {
                    MessageBox.Show("Ya agrego el articulo " + cboArticulo.Text + " a la factura", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                    return;
                }
            }


            Articulo art = (Articulo)cboArticulo.SelectedItem;
            int cant = Convert.ToInt32(txtCantidad.Text);
            
            DetalleFactura df = new DetalleFactura(art,cant);

            nueva.AgregarDetalle(df);

            dgvFactura.Rows.Add(df.Articulo.IdArticulo, df.Articulo.Nombre, df.Cantidad, df.Articulo.PrecioUnitario);

            txtSubTotal.Text= df.CalcularSubTotal().ToString();
           txtTotal.Text= nueva.CalcularTotal().ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtCliente.Text =="")
            {
                MessageBox.Show("Tiene que agregar un cliente", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }

            nueva.Cliente = txtCliente.Text;
            FormaPago fp = (FormaPago)cboFormaPago.SelectedItem;
            nueva.FormaPago = fp;
            nueva.Fecha =Convert.ToDateTime (dtpFecha.Value);

            if(Helper.ObtenerInstancia().ConformarFactura(nueva))
            {
                MessageBox.Show("Se agrego con exito la Factura", "siiii", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("NO SE PUDO AGREGAR LA FACTURA", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
        }

        private void dgvFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvFactura.CurrentCell.ColumnIndex == 4)
            {
                nueva.QuitarDetalle(dgvFactura.CurrentRow.Index);
                dgvFactura.Rows.Remove(dgvFactura.CurrentRow);
            }
        }
    }
}
