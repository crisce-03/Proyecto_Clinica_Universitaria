using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

using Proyecto_Clinica_Universitaria.Models;
using Proyecto_Clinica_Universitaria.Datos;

namespace Proyecto_Clinica_Universitaria.Controllers
{
    public class MedicosEspecialidadController : Controller
    {
        MedicosEspecialidadDatos _MedicoEspecialidad = new MedicosEspecialidadDatos();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(MedicosEspecialidadModel especialidadmedico)
        {
            var respuesta = _MedicoEspecialidad.Guardar(especialidadmedico);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
