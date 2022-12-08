using System.Data;
using System.Data.SqlClient;
using GestionVentasFRANCOLOIZZO.Models;

namespace GestionVentasFRANCOLOIZZO.Repositories
{
    public class ProductoVendidoRepository
    {
        private SqlConnection? conexion;
        private String conncectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=francoloizzo_coderh_curso;" +
            "User Id=francoloizzo_coderh_curso;Password=francoloizzo1997;";

        public ProductoVendidoRepository()
        {
            try
            {
                conexion = new SqlConnection(conncectionString);
            }
            catch (Exception ex)
            {


            }
        }

        //listar productos vendidos
        public List<ProductoVendido> listarProductoVendido()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();
                                productoVendido.Id = int.Parse(reader["Id"].ToString());
                                productoVendido.Stock = int.Parse(reader["Stock"].ToString());
                                productoVendido.IdProducto = int.Parse(reader["IdProducto"].ToString());
                                productoVendido.IdVenta = int.Parse(reader["IdVenta"].ToString());
                                lista.Add(productoVendido);
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

        public ProductoVendido? obtenerProductoVendido(long id)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ProductoVendido productoVendido = obtenerProductoVendidoReader(reader);
                            return productoVendido;
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

        private ProductoVendido obtenerProductoVendidoReader(SqlDataReader reader)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.Id = int.Parse(reader["Id"].ToString());
            productoVendido.Stock = int.Parse(reader["Descripciones"].ToString());
            productoVendido.IdProducto = int.Parse(reader["Costo"].ToString());
            productoVendido.IdVenta = int.Parse(reader["PrecioVenta"].ToString());
            return productoVendido;
        }

        //delete
        public bool eliminarProductoVendido(long id)
        {
            List<Producto> lista = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE id = @id", conexion))
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
        public void crearProductoVendido(ProductoVendido productoVendido)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(Id, Stock, IdProducto, IdVenta) VALUES(@id, @stock, @idProducto, @idVenta)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendido.Id });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta });
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
        public ProductoVendido? actualizarProductoVendido(long id, ProductoVendido productoVendidoAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("conxion no establecida");
            }
            try
            {
                ProductoVendido? productoVendido = obtenerProductoVendido(id);
                if (productoVendido == null)
                {
                    return null;
                }
                List<string> campoAActualizar = new List<string>();
                if (productoVendido.Id != productoVendidoAActualizar.Id && productoVendidoAActualizar.Id > 0)
                {
                    campoAActualizar.Add("id = @id");
                }
                if (productoVendido.Stock != productoVendidoAActualizar.Stock && productoVendidoAActualizar.Stock > 0)
                {
                    campoAActualizar.Add("stock = @stock");
                }
                if (productoVendido.IdProducto != productoVendidoAActualizar.IdProducto && productoVendidoAActualizar.IdProducto > 0)
                {
                    campoAActualizar.Add("idProducto = @idProducto");
                }
                if (productoVendido.IdVenta != productoVendidoAActualizar.IdVenta && productoVendidoAActualizar.IdVenta > 0)
                {
                    campoAActualizar.Add("idVenta = @idVenta");
                }

                if (campoAActualizar.Count == 0)
                {
                    throw new Exception("No new files to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", campoAActualizar)} WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendidoAActualizar.Id });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendidoAActualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdVenta });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return productoVendido;
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
