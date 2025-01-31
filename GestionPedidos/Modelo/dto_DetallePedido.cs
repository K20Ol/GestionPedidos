using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Modelo
{
    internal class dto_DetallePedido
    {
        public int DetalleID { get; set; }
        public int PedidoID { get; set; }
        public int PlatoID { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}
