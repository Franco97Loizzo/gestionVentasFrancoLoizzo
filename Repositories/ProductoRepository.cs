using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GestionVentasFRANCOLOIZZO.Models;


namespace GestionVentasFRANCOLOIZZO.Repositories
{
    public class ProductoRepository
    {
        private SqlConnection? conexion;
        private String conncectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=francoloizzo_coderh_curso;" +
            "User Id=francoloizzo_coderh_curso;Password=francoloizzo1997;";

        public ProductoRepository()
        {
            try
            {
                conexion = new SqlConnection(conncectionString);
            }
            catch (Exception ex)
            {


            }
        }

        public List<Producto> listarProductos()
        {
            List<Producto> lista = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = obtenerProductoReader(reader);
                                lista.Add(producto);
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

        public Producto? obtenerProducto(long id)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Producto producto = obtenerProductoReader(reader);
                            return producto;
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

        private Producto obtenerProductoReader(SqlDataReader reader)
        {
            Producto producto = new Producto();
            producto.Id = long.Parse(reader["Id"].ToString());
            producto.Descripciones = reader["Descripciones"].ToString();
            producto.Costo = decimal.Parse(reader["Costo"].ToString());
            producto.PrecioVenta = decimal.Parse(reader["PrecioVenta"].ToString());
            producto.Stock = int.Parse(reader["Stock"].ToString());
            producto.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
            return producto;
        }

        //delete
        public bool eliminarProducto(long id)
        {
            List<Producto> lista = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Producto WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id});
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
        public void crearProducto(Producto producto)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@descripcion, @costo, @precioventa, @stock, @idUsuario)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = producto.Descripciones });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Decimal) { Value = producto.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioventa", SqlDbType.Decimal) { Value = producto.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
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
        public Producto? actualizarProducto(long id, Producto productoAActualizar)
        {
            if(conexion== null) 
            {
                throw new Exception("conxion no establecida");
            }
            try
            {
                Producto? producto = obtenerProducto(id);
                if (producto == null)
                {
                    return null;
                }
                List<string> campoAActualizar = new List<string>();
                if (producto.Descripciones != productoAActualizar.Descripciones && !string.IsNullOrEmpty(productoAActualizar.Descripciones))
                {
                    campoAActualizar.Add("descripciones = @descripcion");
                }
                if (producto.Costo != productoAActualizar.Costo && productoAActualizar.Costo > 0)
                {
                    campoAActualizar.Add("costo = @costo");
                }
                if (producto.PrecioVenta != productoAActualizar.PrecioVenta && productoAActualizar.PrecioVenta > 0)
                {
                    campoAActualizar.Add("precioventa = @precioventa");
                }
                if (producto.Stock != productoAActualizar.Stock && productoAActualizar.Stock > 0)
                {
                    campoAActualizar.Add("stock = @stock");
                }

                if(campoAActualizar.Count == 0)
                {
                    throw new Exception("No new files to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", campoAActualizar)} WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = productoAActualizar.Descripciones });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Decimal) { Value = productoAActualizar.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioventa", SqlDbType.Decimal) { Value = productoAActualizar.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoAActualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return producto;
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
