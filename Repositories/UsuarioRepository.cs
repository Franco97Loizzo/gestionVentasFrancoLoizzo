using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        private Usuario obtenerUsuarioReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = int.Parse(reader["Id"].ToString());
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;
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
                using(SqlCommand cmd = new SqlCommand("SELECT Id, Nombre, Apellido, NombreUsuario, Mail FROM Usuario", conexion))
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
                                usuario.Mail = reader["Mail"].ToString();
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

        //listar por ID
        public Usuario? obtenerUsuario(long id)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = obtenerUsuarioReader(reader);
                            return usuario;
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

        //delete
        public bool eliminarUsuario(long id)
        {
            List<Usuario> lista = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
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
        public void crearUsuario(Usuario usuario)
        {
            if (conexion == null)
            {
                throw new Exception("No establecido");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES(@nombre, @apellido, @nombreUsuario, @contraseña, @mail)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail });
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
        public Usuario? actualizarUsuario(long id, Usuario usuarioAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("conxion no establecida");
            }
            try
            {
                Usuario? usuario = obtenerUsuario(id);
                if (usuario == null)
                {
                    return null;
                }
                List<string> campoAActualizar = new List<string>();
                if (usuario.Nombre != usuarioAActualizar.Nombre && !string.IsNullOrEmpty(usuarioAActualizar.Nombre))
                {
                    campoAActualizar.Add("nombre = @nombre");
                }
                if (usuario.Apellido != usuarioAActualizar.Apellido && !string.IsNullOrEmpty(usuarioAActualizar.Apellido))
                {
                    campoAActualizar.Add("apellido = @apellido");
                }
                if (usuario.NombreUsuario != usuarioAActualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioAActualizar.NombreUsuario))
                {
                    campoAActualizar.Add("nombreusuario = @nombreusuario");
                }
                if (usuario.Mail != usuarioAActualizar.Mail && !string.IsNullOrEmpty(usuarioAActualizar.Mail))
                {
                    campoAActualizar.Add("mail = @mail");
                }

                if (campoAActualizar.Count == 0)
                {
                    throw new Exception("No new files to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", campoAActualizar)} WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuarioAActualizar.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuarioAActualizar.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreusuario", SqlDbType.VarChar) { Value = usuarioAActualizar.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuarioAActualizar.Mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return usuario;
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
