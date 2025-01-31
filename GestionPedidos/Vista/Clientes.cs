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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }
        private int clienteIDSeleccionado = 0;
        private void CargarDatosClientes()
        {
            cls_Clientes clienteService = new cls_Clientes();
            var listaClientes = clienteService.Listar();
            dgvDatos.DataSource = listaClientes;
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            CargarDatosClientes();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                clienteIDSeleccionado = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["ClienteID"].Value);
                txtNombre.Text = dgvDatos.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtTelefono.Text = dgvDatos.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                txtEmail.Text = dgvDatos.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            cls_Clientes clienteService = new cls_Clientes();
            dto_Clientes cliente = new dto_Clientes
            {
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };

            bool resultado = clienteService.Registrar(cliente);
            if (resultado)
            {
                MessageBox.Show("Cliente registrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosClientes();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (clienteIDSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cls_Clientes clienteService = new cls_Clientes();
            dto_Clientes cliente = new dto_Clientes
            {
                ClienteID = clienteIDSeleccionado,
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };

            bool resultado = clienteService.Actualizar(cliente);
            if (resultado)
            {
                MessageBox.Show("Cliente actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosClientes();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al actualizar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (clienteIDSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                cls_Clientes clienteService = new cls_Clientes();
                bool resultado = clienteService.Eliminar(clienteIDSeleccionado);

                if (resultado)
                {
                    MessageBox.Show("Cliente eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosClientes();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            clienteIDSeleccionado = 0; // Restablecer el ID seleccionado
        }
    }
}
