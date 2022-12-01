using System.Data.SqlClient;
using GestionVentasFRANCOLOIZZO.Models;

namespace GestionVentasFRANCOLOIZZO.Repositories
{
    public class UsuarioRepository
    {
        private SqlConnection? conexion;
        private String conncectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=francoloizzo_coderh_curso;" +
            "User Id=francoloizzo_coderh_curso;Password=francoloizzo1997;";

        public UsuarioRepository()
        {
            try
            {
                conexion = new SqlConnection(conncectionString);
            }
            catch (Exception ex)
            {

                
            }
        }

        public List<Usuario> listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using(SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", conexion))
                {
                    conexion.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = int.Parse(reader["Id"].ToString());
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                                usuario.Password = reader["Contraseña"].ToString();
                                usuario.Mail = reader["mail"].ToString();
                                lista.Add(usuario); 
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
