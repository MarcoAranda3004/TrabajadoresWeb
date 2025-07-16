using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresWeb.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        public int Id { get; set; }
        public string? NombreDepartamento { get; set; } 
        public ICollection<Provincia>? Provincias { get; set; }

        [InverseProperty("Departamento")]
        public ICollection<Trabajador>? Trabajadores { get; set; }
    }
}
