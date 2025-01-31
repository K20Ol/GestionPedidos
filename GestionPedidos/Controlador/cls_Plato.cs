using GestionPedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Controlador
{
    internal class cls_Plato
    {
        private readonly dto_Conexion conexion = new dto_Conexion();

        public bool Registrar(dto_Plato plato)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "INSERT INTO Platos (NombrePlato, Precio, Descripcion, Categoria) VALUES (@NombrePlato, @Precio, @Descripcion, @Categoria)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NombrePlato", plato.NombrePlato);
                        cmd.Parameters.AddWithValue("@Precio", plato.Precio);
                        cmd.Parameters.AddWithValue("@Descripcion", plato.Descripcion);
                        cmd.Parameters.AddWithValue("@Categoria", plato.Categoria);
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

        public bool Actualizar(dto_Plato plato)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "UPDATE Platos SET NombrePlato = @NombrePlato, Precio = @Precio, Descripcion = @Descripcion, Categoria = @Categoria WHERE PlatoID = @PlatoID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NombrePlato", plato.NombrePlato);
                        cmd.Parameters.AddWithValue("@Precio", plato.Precio);
                        cmd.Parameters.AddWithValue("@Descripcion", plato.Descripcion);
                        cmd.Parameters.AddWithValue("@Categoria", plato.Categoria);
                        cmd.Parameters.AddWithValue("@PlatoID", plato.PlatoID);
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

        public bool Eliminar(int platoID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "DELETE FROM Platos WHERE PlatoID = @PlatoID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PlatoID", platoID);
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

        public List<dto_Plato> Listar()
        {
            List<dto_Plato> listaPlatos = new List<dto_Plato>();
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "SELECT * FROM Platos";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaPlatos.Add(new dto_Plato
                                {
                                    PlatoID = Convert.ToInt32(reader["PlatoID"]),
                                    NombrePlato = reader["NombrePlato"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Categoria = reader["Categoria"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
        
            }
            return listaPlatos;
        }
    }
}
