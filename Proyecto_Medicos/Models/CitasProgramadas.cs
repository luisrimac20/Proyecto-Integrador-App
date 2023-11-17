using System.ComponentModel.DataAnnotations;

namespace Proyecto_Medicos.Models
{
    public class CitasProgramadas
    {
        [Display(Name = "Codigo Cita")]
        public int codCita { get; set; }
        [Display(Name = "Nombres Paciente")]
        public string? nomPac { get; set; }
        [Display(Name = "Nombre Medico")]
        public string? codMed { get; set; }
        [Display(Name = "Especialidad")]
        public string? codEsp { get; set; }
        [Display(Name = "Turno")]
        public string? codTurno { get; set; }
        [Display(Name = "Fecha Cita")]
        public DateTime fecha { get; set; }
    }
}
