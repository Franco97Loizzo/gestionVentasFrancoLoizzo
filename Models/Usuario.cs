namespace GestionVentasFRANCOLOIZZO.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        public Usuario() { }
        public Usuario(int id, string nombre, string apellido, string nombreUsuario, string password, string mail)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreUsuario;
            Password = password;
            Mail = mail;
        }
    }
}
