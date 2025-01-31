using GestionPedidos.Vista;
using GestionPedidos.Vista.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionPedidos
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            Mesas mesas = new Mesas();
            mesas.ShowDialog();


        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido();
            pedido.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.ShowDialog();
        }

        private void btnPlatos_Click(object sender, EventArgs e)
        {
            Plato plato = new Plato();
            plato.ShowDialog();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reporte reporte
                = new Reporte();
            reporte.ShowDialog();
        }
    }
}
