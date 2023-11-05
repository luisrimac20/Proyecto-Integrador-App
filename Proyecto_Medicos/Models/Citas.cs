using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proyecto_Medicos.Models
{
    public class Citas
    {
        

        [Display(Name = "Nombres Paciente")]
        public string? nomPac { get; set; }

        [Display(Name = "Nombre Medico")]
        public string? codMed { get; set; }

        [Display(Name = "Especialidad")]
        public string? codEsp { get; set; }

        [Display(Name = "Turno")]
        public string? codTurno { get; set; }

        [Display(Name = "Fecha de Cita")]
        public string? fecha { get; set; }

    }
}
