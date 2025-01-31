using GestionPedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Controlador
{
    internal class cls_Mesas
    {
        private readonly dto_Conexion conexion = new dto_Conexion();

        public bool Registrar(dto_Mesas mesa)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "INSERT INTO Mesas (NumeroMesa, Capacidad, Ubicacion) VALUES (@NumeroMesa, @Capacidad, @Ubicacion)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NumeroMesa", mesa.NumeroMesa);
                        cmd.Parameters.AddWithValue("@Capacidad", mesa.Capacidad);
                        cmd.Parameters.AddWithValue("@Ubicacion", mesa.Ubicacion);
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

        public bool Actualizar(dto_Mesas mesa)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "UPDATE Mesas SET NumeroMesa = @NumeroMesa, Capacidad = @Capacidad, Ubicacion = @Ubicacion WHERE MesaID = @MesaID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NumeroMesa", mesa.NumeroMesa);
                        cmd.Parameters.AddWithValue("@Capacidad", mesa.Capacidad);
                        cmd.Parameters.AddWithValue("@Ubicacion", mesa.Ubicacion);
                        cmd.Parameters.AddWithValue("@MesaID", mesa.MesaID);
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

        public bool Eliminar(int mesaID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "DELETE FROM Mesas WHERE MesaID = @MesaID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MesaID", mesaID);
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

        public List<dto_Mesas> Listar()
        {
            List<dto_Mesas> listaMesas = new List<dto_Mesas>();
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "SELECT * FROM Mesas";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaMesas.Add(new dto_Mesas
                                {
                                    MesaID = Convert.ToInt32(reader["MesaID"]),
                                    NumeroMesa = Convert.ToInt32(reader["NumeroMesa"]),
                                    Capacidad = Convert.ToInt32(reader["Capacidad"]),
                                    Ubicacion = reader["Ubicacion"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
             
            }
            return listaMesas;
        }
    }
}
