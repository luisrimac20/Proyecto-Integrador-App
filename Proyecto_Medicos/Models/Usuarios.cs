using System.ComponentModel.DataAnnotations;

namespace Proyecto_Medicos.Models
{
    public class Usuarios
    {
        [Display(Name = "Codigo de Usuario")]
        public string? id { get; set; }
        [Display(Name = "Nombres")]
        public string? nombre { get; set; }
        [Display(Name = "Apellidos")]
        public string? apellido{ get; set; }
        [Display(Name = "Correo Electronico")]
        public string? correo { get; set; }
        [Display(Name = "ContraseÑa")]
        public string? clave { get; set; }
        [Display(Name = "Repita ContraseÑa")]
        public string? confimar_clave { get; set; }
        [Display(Name = "Codigo del Rol")]
        public Rol idRol { get; set; }

    }
}
