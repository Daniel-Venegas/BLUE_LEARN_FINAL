using System.ComponentModel.DataAnnotations;

namespace AppLogin.Model
{
    public class DescripcionMonitoreo
    {

        [Key]
        public int IdDescripcionMonitoreo { get; set; }
        public required string Variable { get; set; }
        public required string UnidadMedida { get; set; }
        public bool Eliminado { get; set; }
    }
}
