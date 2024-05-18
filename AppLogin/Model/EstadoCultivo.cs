using System.ComponentModel.DataAnnotations;

namespace AppLogin.Model
{
    public class EstadoCultivo
    {

        [Key]
        public int IdEstadoCultivo { get; set; }
        public required string Descripcion { get; set; }
        public bool Eliminado { get; set; }
    }
}
