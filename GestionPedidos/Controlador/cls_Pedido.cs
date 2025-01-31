using GestionPedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidos.Controlador
{
    internal class cls_Pedido
    {
        private readonly dto_Conexion conexion = new dto_Conexion();

        public bool RegistrarOrden(dto_Pedido pedido, List<dto_DetallePedido> detalles)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // Insertar Pedido
                        string queryPedido = "INSERT INTO Pedidos (ClienteID, MesaID, FechaHora, Estado) OUTPUT INSERTED.PedidoID VALUES (@ClienteID, @MesaID, @FechaHora, @Estado)";
                        int pedidoID;
                        using (SqlCommand cmd = new SqlCommand(queryPedido, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);
                            cmd.Parameters.AddWithValue("@MesaID", pedido.MesaID);
                            cmd.Parameters.AddWithValue("@FechaHora", pedido.FechaHora);
                            cmd.Parameters.AddWithValue("@Estado", pedido.Estado);
                            pedidoID = (int)cmd.ExecuteScalar();
                        }

                        // Insertar Detalles del Pedido
                        foreach (var detalle in detalles)
                        {
                            string queryDetalle = "INSERT INTO DetallePedidos (PedidoID, PlatoID, Cantidad, Subtotal) VALUES (@PedidoID, @PlatoID, @Cantidad, @Subtotal)";
                            using (SqlCommand cmd = new SqlCommand(queryDetalle, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@PedidoID", pedidoID);
                                cmd.Parameters.AddWithValue("@PlatoID", detalle.PlatoID);
                                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                                cmd.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public bool ActualizarPedido(dto_Pedido pedido)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "UPDATE Pedidos SET ClienteID = @ClienteID, MesaID = @MesaID, FechaHora = @FechaHora, Estado = @Estado WHERE PedidoID = @PedidoID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);
                        cmd.Parameters.AddWithValue("@MesaID", pedido.MesaID);
                        cmd.Parameters.AddWithValue("@FechaHora", pedido.FechaHora);
                        cmd.Parameters.AddWithValue("@Estado", pedido.Estado);
                        cmd.Parameters.AddWithValue("@PedidoID", pedido.PedidoID);
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

        public bool EliminarPedido(int pedidoID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // Eliminar Detalles del Pedido
                        string queryDetalle = "DELETE FROM DetallePedidos WHERE PedidoID = @PedidoID";
                        using (SqlCommand cmd = new SqlCommand(queryDetalle, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PedidoID", pedidoID);
                            cmd.ExecuteNonQuery();
                        }

                        // Eliminar el Pedido
                        string queryPedido = "DELETE FROM Pedidos WHERE PedidoID = @PedidoID";
                        using (SqlCommand cmd = new SqlCommand(queryPedido, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PedidoID", pedidoID);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public List<dto_Pedido> Listar()
        {
            List<dto_Pedido> listaPedidos = new List<dto_Pedido>();
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "SELECT * FROM Pedidos";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaPedidos.Add(new dto_Pedido
                                {
                                    PedidoID = Convert.ToInt32(reader["PedidoID"]),
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    MesaID = Convert.ToInt32(reader["MesaID"]),
                                    FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                                    Estado = reader["Estado"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                
            }
            return listaPedidos;
        }

        public dto_Pedido ObtenerPorID(int pedidoID)
        {
            dto_Pedido pedido = null;
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "SELECT * FROM Pedidos WHERE PedidoID = @PedidoID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PedidoID", pedidoID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pedido = new dto_Pedido
                                {
                                    PedidoID = Convert.ToInt32(reader["PedidoID"]),
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    MesaID = Convert.ToInt32(reader["MesaID"]),
                                    FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                                    Estado = reader["Estado"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch
            {
              
            }
            return pedido;
        }

        public List<dto_DetallePedido> ObtenerDetallesPorPedidoID(int pedidoID)
        {
            List<dto_DetallePedido> detalles = new List<dto_DetallePedido>();
            try
            {
                using (SqlConnection con = conexion.Conectarse())
                {
                    con.Open();
                    string query = "SELECT * FROM DetallePedidos WHERE PedidoID = @PedidoID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PedidoID", pedidoID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                detalles.Add(new dto_DetallePedido
                                {
                                    DetalleID = Convert.ToInt32(reader["DetalleID"]),
                                    PedidoID = Convert.ToInt32(reader["PedidoID"]),
                                    PlatoID = Convert.ToInt32(reader["PlatoID"]),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Subtotal = Convert.ToDecimal(reader["Subtotal"])
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                
            }
            return detalles;
        }
    }
}
