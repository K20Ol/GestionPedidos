using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Modelo
{
    internal class dto_Plato
    {
        public int PlatoID { get; set; }
        public string NombrePlato { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
    }
}
