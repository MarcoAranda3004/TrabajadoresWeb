using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresWeb.Models
{
    [Table("Provincia")]
    public class Provincia
    {
        public int Id { get; set; }

        public string? NombreProvincia { get; set; }

        public int? IdDepartamento { get; set; }

        [ForeignKey("IdDepartamento")]
        public Departamento? Departamento { get; set; }

        public ICollection<Distrito>? Distritos { get; set; }
    }
}
