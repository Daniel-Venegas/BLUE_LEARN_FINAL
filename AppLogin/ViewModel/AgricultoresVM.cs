namespace AppLogin.ViewModel
{
    public class AgricultoresVM
    {
        
        public required int IdJugador { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string Direccion { get; set; }
        public required string Contacto { get; set; }
        public bool Eliminado { get; set; }
        public required string password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
