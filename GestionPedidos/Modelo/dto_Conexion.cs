using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Modelo
{
    internal class dto_Conexion
    {
        private readonly string connectionString;

        public dto_Conexion()
        {
            connectionString = "Server=.;Database=SistemaPedidos;User Id=sa;Password=123456;";
        }

        public SqlConnection Conectarse()
        {
            return new SqlConnection(connectionString);
        }
    }
}
