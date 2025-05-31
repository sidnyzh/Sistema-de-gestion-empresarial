using System.ComponentModel.DataAnnotations;

namespace MVCFacturacion.Models
{
    public class Persona
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}