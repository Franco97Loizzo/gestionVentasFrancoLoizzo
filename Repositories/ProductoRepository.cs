using System.Data.SqlClient;
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
                                Producto producto = new Producto();
                                producto.Id = int.Parse(reader["Id"].ToString());
                                producto.Descripciones = reader["Descripciones"].ToString();
                                producto.Costo = double.Parse(reader["Costo"].ToString());
                                producto.PrecioVenta = double.Parse(reader["PrecioVenta"].ToString());
                                producto.Stock = int.Parse(reader["Stock"].ToString());
                                producto.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
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
    }
}
