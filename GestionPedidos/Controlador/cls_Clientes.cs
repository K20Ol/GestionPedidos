using GestionPedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Controlador
{
    internal class cls_Clientes
    {
        private readonly dto_Conexion conexion = new dto_Conexion();

        public bool Registrar(dto_Clientes cliente)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "INSERT INTO Clientes (Nombre, Telefono, Email) VALUES (@Nombre, @Telefono, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(dto_Clientes cliente)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "UPDATE Clientes SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email WHERE ClienteID = @ClienteID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int clienteID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "DELETE FROM Clientes WHERE ClienteID = @ClienteID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ClienteID", clienteID);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<dto_Clientes> Listar()
        {
            List<dto_Clientes> listaClientes = new List<dto_Clientes>();
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "SELECT * FROM Clientes";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaClientes.Add(new dto_Clientes
                                {
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Email = reader["Email"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
               
            }
            return listaClientes;
        }
    }
}
