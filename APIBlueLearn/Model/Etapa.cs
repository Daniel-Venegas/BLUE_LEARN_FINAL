using System.ComponentModel.DataAnnotations;

namespace APIBlueLearn.Model
{
    public class Etapa
    {

        [Key]
        public int IdEtapa { get; set; }
        public required string Descripcion { get; set; }
        public bool Eliminado { get; set; }
    }
}
