using GestionPedidos.Controlador;
using GestionPedidos.Modelo.Datos;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GestionPedidos.Vista.Reportes
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
        }
        private void CargarClientes()
        {
            cls_Clientes clienteService = new cls_Clientes();
            cmbClientes.DataSource = clienteService.Listar();
            cmbClientes.DisplayMember = "Nombre";
            cmbClientes.ValueMember = "ClienteID";
        }
        private void Reporte_Load(object sender, EventArgs e)
        {

            CargarClientes();
            this.reportViewer1.RefreshReport();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {

            if (cmbClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int clienteID = (int)cmbClientes.SelectedValue; string nombreCliente = cmbClientes.Text;
            GenerarReporte(clienteID, nombreCliente);
        }
        private void GenerarReporte(int clienteID, string nombreCliente)
        {
            try
            {
                Modelo.Datos.DataSetReporteTableAdapters.DataTable1TableAdapter adapter =
                    new Modelo.Datos.DataSetReporteTableAdapters.DataTable1TableAdapter();
                DataTable dt = adapter.GetPedidosPorCliente(clienteID);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay pedidos disponibles para el cliente seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reportViewer1.Clear();
                    return;
                }

                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSetPedidos", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                ReportParameter parametroCliente = new ReportParameter("NombreCliente", nombreCliente);
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { parametroCliente });



                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
