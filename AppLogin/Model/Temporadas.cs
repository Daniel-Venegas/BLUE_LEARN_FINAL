using System.ComponentModel.DataAnnotations;

namespace AppLogin.Model
{
    public class Temporadas
    {
        [Key]
        public int IdTemporada { get; set; }
        public required string Temporada { get; set; }
        public bool Eliminado { get; set; }
    }
}
