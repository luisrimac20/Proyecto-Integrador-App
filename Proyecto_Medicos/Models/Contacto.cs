using System.ComponentModel.DataAnnotations;

namespace Proyecto_Medicos.Models
{
    public class Contacto
    {
        [Display(Name = "Nombre")]
        public string name { get; set; }

        [Display(Name = "Correo")]
        public string email { get; set; }

        [Display(Name = "Celular")]
        public string phone { get; set; }

        [Display(Name = "Asunto")]
        public string issue { get; set; }

        [Display(Name = "Mensaje")]
        public string message { get; set; }
    }
}
