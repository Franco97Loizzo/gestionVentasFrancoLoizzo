using GestionVentasFRANCOLOIZZO.Models;
using System.Data;
using System.Data.SqlClient;

namespace GestionVentasFRANCOLOIZZO.Repositories
{
    public class VentaRepository
    {
        private SqlConnection? conexion;
        private String conncectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=francoloizzo_coderh_curso;" +
            "User Id=francoloizzo_coderh_curso;Password=francoloizzo1997;";

        public VentaRepository()
        {
            try
            {
                conexion = new SqlConnection(conncectionString);
            }
            catch (Exception ex)
            {


            }
        }

        //listar ventas
        public List<Venta> listarVentas()
        {
            List<Venta> lista = new List<Venta>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = obtenerVentaReader(reader);
                                lista.Add(venta);

                                lista.Add(venta);
                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public Venta? obtenerVenta(long id)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Venta venta = obtenerVentaReader(reader);
                            return venta;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

        }

        private Venta obtenerVentaReader(SqlDataReader reader)
        {
            Venta venta = new Venta();
            venta.Id = int.Parse(reader["Id"].ToString());
            venta.Comentarios = reader["Comentarios"].ToString();
            venta.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
            return venta;
        }

        //delete
        public bool eliminarVenta(long id)
        {
            List<Venta> lista = new List<Venta>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Venta WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                conexion.Close();
                return filasAfectadas > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //crear
        public void crearVenta(Venta venta)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Comentarios, IdUsuario) VALUES(@comentarios, @idUsuario)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Decimal) { Value = venta.IdUsuario });
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        //actualizar
        public Venta? actualizarVenta(long id, Venta ventaAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("conxion no establecida");
            }
            try
            {
                Venta? venta = obtenerVenta(id);
                if (venta == null)
                {
                    return null;
                }
                List<string> campoAActualizar = new List<string>();
                if (venta.Comentarios != ventaAActualizar.Comentarios && !string.IsNullOrEmpty(ventaAActualizar.Comentarios))
                {
                    campoAActualizar.Add("comentarios = @comentarios");
                }
                if (venta.IdUsuario != ventaAActualizar.IdUsuario && ventaAActualizar.IdUsuario > 0)
                {
                    campoAActualizar.Add("idUsuario = @idUsuario");
                }
                if (campoAActualizar.Count == 0)
                {
                    throw new Exception("No new files to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Venta SET {String.Join(", ", campoAActualizar)} WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = ventaAActualizar.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = ventaAActualizar.IdUsuario });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return venta;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
