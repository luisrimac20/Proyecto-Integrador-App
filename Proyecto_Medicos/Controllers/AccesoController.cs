using Microsoft.AspNetCore.Mvc;
using Proyecto_Medicos.Models;
using Proyecto_Medicos.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security;
using Microsoft.AspNetCore.Authorization;




namespace Proyecto_Medicos.Controllers
{
    public class AccesoController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private String cadena_conexion;
        public AccesoController(IConfiguration _config)
        {
            _configuration = _config;
            //recuperar la cadena de conexion almacenada en la cadane a de conexion cn1

            cadena_conexion = _configuration.GetConnectionString("cn1");
        }

        // index del login
        public IActionResult Index()
        {
           return View();
        }

        [HttpPost]

        public IActionResult Index(string correo, string clave)
        {

            Usuarios obj = new Logica().EncontrarUsuario(correo, clave);
            if (obj.nombre!=null)
            {

                


                return RedirectToAction("Index","Home");
            }
            else
            {

                ViewBag.Error = "Usuario o Contraseña Invalidos";
            }
            return View(obj);
        }

        public IActionResult Salir()
        {
            return RedirectToAction("Index", "Acceso");
        }


        //INDEX DEL PROYECTO (Medicos)
        public async Task<ActionResult> Medicos(String Especialidad)
        {
            if (!String.IsNullOrEmpty(Especialidad))
            {
                var listado = new List<Medicos>();
                using (HttpClient cliente = new HttpClient())
                {
                    var respuesta = await cliente.GetAsync("https://localhost:7179/api/Api/GetMedicoEspecialidad/" + Especialidad);
                    String respuestaApi = await respuesta.Content.ReadAsStringAsync();
                    listado = JsonConvert.DeserializeObject<List<Medicos>>(respuestaApi);
                }

                ViewBag.total = "Total de medicos registrados: " + listado.Count();
                return View(listado);
            }
            else
            {
                // Obtener todos los Medicos
                var listado = new List<Medicos>();
                using (HttpClient cliente = new HttpClient())
                {
                    var respuesta = await cliente.GetAsync("https://localhost:7179/api/Api/GetMedicos");
                    string respuestaApi = await respuesta.Content.ReadAsStringAsync();
                    listado = JsonConvert.DeserializeObject<List<Medicos>>(respuestaApi);
                }
                ViewBag.total = "Total de Medicos: " + listado.Count();
                return View(listado);
            }
        }


        //HISTORIAL DE LAS CITAS
        public async Task<ActionResult> HistorialCitas()
        {
            var listado = new List<CitasProgramadas>();

            using (HttpClient client = new HttpClient())
            {
                var respuesta = await client.GetAsync("https://localhost:7179/api/Api/GetCitas");

                string rptaApi = await respuesta.Content.ReadAsStringAsync();

                listado = JsonConvert.DeserializeObject<List<CitasProgramadas>>(rptaApi);
            }
            return View(listado);

        }

        public async Task<ActionResult> CreateCita()
        {
            // para el dropdownlist de la Especialidad
            var listaemed = new List<Medicos>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/GetMedicos");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaemed = JsonConvert.DeserializeObject<List<Medicos>>(respuestaAPI);
            }
            //
            ViewBag.Medicos = new SelectList(
                                     listaemed, "codMed", "nomMed");



            var listaEspecialidades = new List<Especialidades>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/GetEspecialidades");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaEspecialidades = JsonConvert.DeserializeObject<List<Especialidades>>(respuestaAPI);
            }
            //
            ViewBag.Especialidad = new SelectList(
                                     listaEspecialidades, "codEsp", "nomEsp");


            var listaTurno = new List<Turno>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/GetTurno");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaTurno = JsonConvert.DeserializeObject<List<Turno>>(respuestaAPI);
            }
            //
            ViewBag.Turno = new SelectList(
                                     listaTurno, "codTurno", "nomTurno");



            // para el dropdownlist de los distritos

            return View(new Citas());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCita(Citas obj)
        {
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {

                StringContent contenido = new StringContent(
                   JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var respuesta =
                      await cliente.PostAsync("https://localhost:7179/api/Api/AddCita", contenido);

                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                ViewBag.MENSAJE = respuestaAPI;
            }

            // para el dropdownlist de la Especialidad
            var listamed = new List<Medicos>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/GetMedicos");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listamed = JsonConvert.DeserializeObject<List<Medicos>>(respuestaAPI);
            }
            //
            ViewBag.Medicos = new SelectList(
                                     listamed, "codMed", "nomMed");


            var listaEspecialidades = new List<Especialidades>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/GetEspecialidades");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaEspecialidades = JsonConvert.DeserializeObject<List<Especialidades>>(respuestaAPI);
            }
            //
            ViewBag.Especialidad = new SelectList(
                                     listaEspecialidades, "codEsp", "nomEsp");


            var listaTurno = new List<Turno>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/GetTurno");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaTurno = JsonConvert.DeserializeObject<List<Turno>>(respuestaAPI);
            }
            //
            ViewBag.Turno = new SelectList(
                                     listaTurno, "codTurno", "nomTurno");

            //
            return View(obj);
        }




        public async Task<ActionResult> ProgramarCitas(string id)
        {
            Medicos? obj = new Medicos();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta = await cliente.GetAsync("https://localhost:7179/api/Api/GetMedicos/" + id);

                // convertimos el contenido de la variable respuesta a una cadena
                string respuestaPI = await respuesta.Content.ReadAsStringAsync();

                // para despues deserializarlo al formato Json de un objeto Medicos
                obj = JsonConvert.DeserializeObject<Medicos>(respuestaPI);
            }
            //
            // para el dropdownlist de la Especialidad

            var listaesp = new List<Especialidades>();
            var listadis = new List<Turno>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/GetEspecialidades");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp = JsonConvert.DeserializeObject<List<Especialidades>>(respuestaAPI);
            }
            //
            ViewBag.Especialidad = new SelectList(
                                     listaesp, "codEsp", "nomEsp");



            using (HttpClient cliente2 = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente2.GetAsync("https://localhost:7179/api/Api/GetTurno");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listadis = JsonConvert.DeserializeObject<List<Turno>>(respuestaAPI);
            }
            //
            ViewBag.Turno = new SelectList(
                                     listadis, "codTurno", "nomTurno");


            //
            return View(obj);
        }

        // POST: MedicosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProgramarCita(string id, Citas obj)
        {
            //Medicos? obj = new Medicos();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {    
                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var respuesta =
                      await cliente.PostAsync("https://localhost:7179/api/Api/AddCita", contenido);

                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                ViewBag.MENSAJE = respuestaAPI;
            }
            //
            // para el dropdownlist de la Especialidad
            var listaesp = new List<Especialidades>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("https://localhost:7179/api/Api/ListadoEspecialidad");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp = JsonConvert.DeserializeObject<List<Especialidades>>(respuestaAPI);
            }
            //
            ViewBag.Especialidad = new SelectList(
                                     listaesp, "codEsp", "nomEsp");

            var listadis = new List<Distritos>();

            using (HttpClient cliente2 = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente2.GetAsync("https://localhost:7179/api/Api/ListadoDistritos");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listadis = JsonConvert.DeserializeObject<List<Distritos>>(respuestaAPI);
            }
            //
            ViewBag.Turno = new SelectList(
                                     listadis, "codTurno", "nomTurno");


            //
            return View(obj);
        }


        public async Task<ActionResult> AddContacto(string msj)
        {
            Contacto obj = new Contacto();
            obj.name = "";
            obj.email = "";
            obj.phone = "";
            obj.issue = "";
            obj.message = "";
            ViewBag.msj = msj;
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContacto(Contacto obj)
        {
            string msj;
            using (var cliente = new HttpClient())
            {
                StringContent cont = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync("https://localhost:7179/api/Api/AddContacto", cont);
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                msj = respuestaAPI;
            }

            return RedirectToAction("AddContacto", new { msj = msj });
        }

        public async Task<ActionResult> RegistroUsuario(string msj)
        {
            RegistroUsuarios obj = new RegistroUsuarios();
            obj.nombre = "";
            obj.apellido = "";
            obj.correo = "";
            obj.clave = "";
            obj.confimar_clave = "";
            ViewBag.msj = msj;
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistroUsuario(RegistroUsuarios obj)
        {
            string msj;
            using (var cliente = new HttpClient())
            {
                StringContent registro = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync("https://localhost:7179/api/Api/AddRegistroUsuario", registro);
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                msj = respuestaAPI;
            }

            return RedirectToAction("RegistroUsuario", new { msj = msj });
        }

    }
}
