using Microsoft.AspNetCore.Mvc;
using Proyecto_Clinica_Universitaria.Datos;
using Proyecto_Clinica_Universitaria.Models;

namespace Proyecto_Clinica_Universitaria.Controllers
{
    public class MedicosController : Controller
    {
        private readonly MedicoDatos _medicoDatos = new MedicoDatos();
        private readonly MedicosEspecialidadDatos _especialidadDatos = new MedicosEspecialidadDatos();

        public IActionResult Index()
        {
            ViewBag.ListaMedicos = _medicoDatos.Listar();   // <- nada de .Take(2)
            ViewBag.ListaEspecialidades = _especialidadDatos.Listar();
            return View(new MedicoModel());
        }

        [HttpGet]
        public IActionResult Editar(int codigo)
        {
            var medico = _medicoDatos.Obtener(codigo);
            if (medico == null)
            {
                TempData["Mensaje"] = "No se encontró el médico solicitado.";
                return RedirectToAction("Index");
            }

            ViewBag.ListaMedicos = _medicoDatos.Listar();
            ViewBag.ListaEspecialidades = _especialidadDatos.Listar();
            return View("Index", medico);
        }

        [HttpPost]
        public IActionResult GuardarOEditar(MedicoModel modelo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ListaMedicos = _medicoDatos.Listar();
                    ViewBag.ListaEspecialidades = _especialidadDatos.Listar();
                    return View("Index", modelo);
                }

                bool ok = (modelo.Codigo == 0)
                    ? _medicoDatos.Guardar(modelo)
                    : _medicoDatos.Editar(modelo);

                TempData["Mensaje"] = ok ? "Guardado correctamente." : "Ocurrió un error al guardar.";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message; // mostrará "La Cédula o el Usuario ya existen..." si aplica
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int codigo)
        {
            bool ok = _medicoDatos.Eliminar(codigo);
            TempData["Mensaje"] = ok ? "Registro eliminado." : "Ocurrió un error al eliminar.";
            return RedirectToAction("Index");
        }
    }
}
