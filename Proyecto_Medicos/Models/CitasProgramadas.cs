namespace Proyecto_Medicos.Models
{
    public class CitasProgramadas
    {
        public int codCita { get; set; }
        public string? codMed { get; set; }
        public string? nomPac { get; set; }
        public string? codEsp { get; set; }
        public string? codTurno { get; set; }
        public DateTime fecha { get; set; }
    }
}
