using GestionPedidos.Controlador;
using GestionPedidos.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionPedidos.Vista
{
    public partial class Mesas : Form
    {
        public Mesas()
        {
            InitializeComponent();
        }
        private int mesaIDSeleccionada = 0;
        private void Mesas_Load(object sender, EventArgs e)
        {
            CargarDatosMesas();

        }
        private void CargarDatosMesas()
        {
            cls_Mesas mesaService = new cls_Mesas();
            var listaMesas = mesaService.Listar();
            dgvDatos.DataSource = listaMesas;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                mesaIDSeleccionada = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["MesaID"].Value);
                txtNumeroMesa.Text = dgvDatos.Rows[e.RowIndex].Cells["NumeroMesa"].Value.ToString();
                txtUbicacion.Text = dgvDatos.Rows[e.RowIndex].Cells["Ubicacion"].Value.ToString();
                txtCapacidad.Text = dgvDatos.Rows[e.RowIndex].Cells["Capacidad"].Value.ToString();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            cls_Mesas mesaService = new cls_Mesas();
            dto_Mesas mesa = new dto_Mesas
            {
                NumeroMesa = int.Parse(txtNumeroMesa.Text),
                Ubicacion = txtUbicacion.Text,
                Capacidad = int.Parse(txtCapacidad.Text)
            };

            bool resultado = mesaService.Registrar(mesa);
            if (resultado)
            {
                MessageBox.Show("Mesa registrada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosMesas();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar la mesa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (mesaIDSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una mesa de la lista para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cls_Mesas mesaService = new cls_Mesas();
            dto_Mesas mesa = new dto_Mesas
            {
                MesaID = mesaIDSeleccionada,
                NumeroMesa = int.Parse(txtNumeroMesa.Text),
                Ubicacion = txtUbicacion.Text,
                Capacidad = int.Parse(txtCapacidad.Text)
            };

            bool resultado = mesaService.Actualizar(mesa);
            if (resultado)
            {
                MessageBox.Show("Mesa actualizada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosMesas();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al actualizar la mesa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (mesaIDSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una mesa de la lista para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar esta mesa?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                cls_Mesas mesaService = new cls_Mesas();
                bool resultado = mesaService.Eliminar(mesaIDSeleccionada);

                if (resultado)
                {
                    MessageBox.Show("Mesa eliminada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosMesas();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la mesa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LimpiarControles()
        {
            txtNumeroMesa.Text = string.Empty;
            txtUbicacion.Text = string.Empty;
            txtCapacidad.Text = string.Empty;
            mesaIDSeleccionada = 0; // Restablecer el ID seleccionado
        }
    }
}
