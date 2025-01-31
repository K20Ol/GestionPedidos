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
    public partial class Plato : Form
    {
        public Plato()
        {
            InitializeComponent();
        }
        private int platoIDSeleccionado = 0;
        private void Plato_Load(object sender, EventArgs e)
        {
            CargarDatosPlatos();
        }
        private void CargarDatosPlatos()
        {
            cls_Plato platoService = new cls_Plato();
            var listaPlatos = platoService.Listar();
            dgvDatos.DataSource = listaPlatos;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                platoIDSeleccionado = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["PlatoID"].Value);
                txtNombre.Text = dgvDatos.Rows[e.RowIndex].Cells["NombrePlato"].Value.ToString();
                txtPrecio.Text = dgvDatos.Rows[e.RowIndex].Cells["Precio"].Value.ToString();
                txtDescripcion.Text = dgvDatos.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                cmbCategoria.Text = dgvDatos.Rows[e.RowIndex].Cells["Categoria"].Value.ToString();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            cls_Plato platoService = new cls_Plato();
            dto_Plato plato = new dto_Plato
            {
                NombrePlato = txtNombre.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                Descripcion = txtDescripcion.Text,
                Categoria = cmbCategoria.Text
            };

            bool resultado = platoService.Registrar(plato);
            if (resultado)
            {
                MessageBox.Show("Plato registrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosPlatos();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar el plato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (platoIDSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un plato de la lista para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cls_Plato platoService = new cls_Plato();
            dto_Plato plato = new dto_Plato
            {
                PlatoID = platoIDSeleccionado,
                NombrePlato = txtNombre.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                Descripcion = txtDescripcion.Text,
                Categoria = cmbCategoria.Text
            };

            bool resultado = platoService.Actualizar(plato);
            if (resultado)
            {
                MessageBox.Show("Plato actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosPlatos();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al actualizar el plato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (platoIDSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un plato de la lista para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar este plato?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                cls_Plato platoService = new cls_Plato();
                bool resultado = platoService.Eliminar(platoIDSeleccionado);

                if (resultado)
                {
                    MessageBox.Show("Plato eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosPlatos();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el plato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cmbCategoria.SelectedIndex = -1; // Restablecer el ComboBox
            platoIDSeleccionado = 0; // Restablecer el ID seleccionado
        }
    }
}
