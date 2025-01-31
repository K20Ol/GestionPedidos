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
    public partial class Pedido : Form
    {
        public Pedido()
        {
            InitializeComponent();
        }
        private List<dto_DetallePedido> listaPlatos = new List<dto_DetallePedido>();
        private int pedidoIDSeleccionado = 0;
        private void Pedido_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarPedidos();
        }
        private void CargarCombos()
        {
            // Clientes
            cls_Clientes clienteService = new cls_Clientes();
            cmbCliente.DataSource = clienteService.Listar();
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.ValueMember = "ClienteID";

            // Mesas
            cls_Mesas mesaService = new cls_Mesas();
            cmbMesa.DataSource = mesaService.Listar();
            cmbMesa.DisplayMember = "NumeroMesa";
            cmbMesa.ValueMember = "MesaID";

            // Platos
            cls_Plato platoService = new cls_Plato();
            cmbPlato.DataSource = platoService.Listar();
            cmbPlato.DisplayMember = "NombrePlato";
            cmbPlato.ValueMember = "PlatoID";
        }
        private void CargarPedidos()
        {
            cls_Pedido pedidoService = new cls_Pedido();
            dgvDatos.DataSource = pedidoService.Listar();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbPlato.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Seleccione un plato y escriba la cantidad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int platoID = (int)cmbPlato.SelectedValue;
            string nombrePlato = cmbPlato.Text;
            int cantidad = int.Parse(txtCantidad.Text);

            if (listaPlatos.Exists(p => p.PlatoID == platoID))
            {
                MessageBox.Show("El plato ya está agregado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dto_DetallePedido detalle = new dto_DetallePedido
            {
                PlatoID = platoID,
                Cantidad = cantidad,
                Subtotal = cantidad * ((dto_Plato)cmbPlato.SelectedItem).Precio // Precio se obtiene del DTO del plato
            };

            listaPlatos.Add(detalle);
            lstPlatos.Items.Add($"{nombrePlato} - Cantidad: {cantidad}");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstPlatos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un plato de la lista para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = lstPlatos.SelectedIndex;
            listaPlatos.RemoveAt(index);
            lstPlatos.Items.RemoveAt(index);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedIndex == -1 || cmbMesa.SelectedIndex == -1 || listaPlatos.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente, una mesa y al menos un plato.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cls_Pedido pedidoService = new cls_Pedido();
            dto_Pedido pedido = new dto_Pedido
            {
                ClienteID = (int)cmbCliente.SelectedValue,
                MesaID = (int)cmbMesa.SelectedValue,
                FechaHora = dtpFecha.Value,
                Estado = cmbEstado.Text
            };

            bool resultado = pedidoService.RegistrarOrden(pedido, listaPlatos);
            if (resultado)
            {
                MessageBox.Show("Pedido registrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarPedidos();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                pedidoIDSeleccionado = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["PedidoID"].Value);

                cls_Pedido pedidoService = new cls_Pedido();
                dto_Pedido pedido = pedidoService.ObtenerPorID(pedidoIDSeleccionado);

                if (pedido != null)
                {
                    cmbCliente.SelectedValue = pedido.ClienteID;
                    cmbMesa.SelectedValue = pedido.MesaID;
                    dtpFecha.Value = pedido.FechaHora;
                    cmbEstado.Text = pedido.Estado;

                    listaPlatos = pedidoService.ObtenerDetallesPorPedidoID(pedidoIDSeleccionado);
                    lstPlatos.Items.Clear();

                    foreach (var detalle in listaPlatos)
                    {
                        string nombrePlato = string.Empty;

                        // Buscar el nombre del plato en el ComboBox
                        foreach (dto_Plato plato in cmbPlato.Items)
                        {
                            if (plato.PlatoID == detalle.PlatoID)
                            {
                                nombrePlato = plato.NombrePlato;
                                break;
                            }
                        }

                        lstPlatos.Items.Add($"{nombrePlato} - Cantidad: {detalle.Cantidad}");
                    }
                }
            }
        }
        private void LimpiarControles()
        {
            cmbCliente.SelectedIndex = -1;
            cmbMesa.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;
            cmbEstado.SelectedIndex = -1;
            lstPlatos.Items.Clear();
            listaPlatos.Clear();
            pedidoIDSeleccionado = 0;
        }
    }
}
