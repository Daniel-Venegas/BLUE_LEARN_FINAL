using System.ComponentModel.DataAnnotations;

namespace AppLogin.Model
{
    public class Etapa
    {

        [Key]
        public int IdEtapa { get; set; }
        public required string Descripcion { get; set; }
        public bool Eliminado { get; set; }
    }
}
