using Microsoft.AspNetCore.Mvc;
using Proyecto_Medicos.Models;
using System.Diagnostics;

namespace Proyecto_Medicos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //formulario de citas programadas 
        public IActionResult CitasProgramadas()
        {
            return View();
        }
        //formulario de pacientes
        public IActionResult Pacientes()
        {
            return View();
        }

        // formulario de harario disponible 
        public IActionResult HorarioDisponible()
        {
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}