using System.ComponentModel.DataAnnotations;

namespace Proyecto_Medicos.Models
{
    public class Medicos
    {
        [Required]
        [Display(Name = "Codigo Medico")]
        public string? codMed { get; set; }

        [Required]
        [Display(Name = "Nombre Medico")]
        public string? nomMed { get; set; }

        [Required]
        [Display(Name = "Año Colegiado")] 
        public int anioColegio { get; set; }

        [Required]
        [Display(Name = "Distrito")]
        public string? codDis { get; set; }

        [Required]
        [Display(Name = "Especialidad")]
        public string? codEsp { get; set; }

        [Required]
        [Display(Name = "Horario")]
        public string? codHora { get; set; }

        [Display(Name = "Ocupado")]
        public string? ocupado { get; set; }
    }
}
