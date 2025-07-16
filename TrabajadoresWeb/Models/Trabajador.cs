using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresWeb.Models
{
    public class Trabajador
    {
        public int Id { get; set; }

        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombres { get; set; }
        public string? Sexo { get; set; }

        public int? IdDepartamento { get; set; }
        [ForeignKey("IdDepartamento")]
        public Departamento? Departamento { get; set; }

        public int? IdProvincia { get; set; }
        [ForeignKey("IdProvincia")]
        public Provincia? Provincia { get; set; }

        public int? IdDistrito { get; set; }
        [ForeignKey("IdDistrito")]
        public Distrito? Distrito { get; set; }



    }
}
