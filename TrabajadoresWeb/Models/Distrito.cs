using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresWeb.Models
{
    [Table("Distrito")]
    public class Distrito
    {
        public int Id { get; set; }

        public string? NombreDistrito { get; set; }

        public int? IdProvincia { get; set; }

        [ForeignKey("IdProvincia")]
        public Provincia? Provincia { get; set; }
    }
}
