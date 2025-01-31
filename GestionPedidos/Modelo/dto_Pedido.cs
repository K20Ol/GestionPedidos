using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Modelo
{
    internal class dto_Pedido
    {
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public int MesaID { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; }
    }
}
