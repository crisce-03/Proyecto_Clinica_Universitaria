using Microsoft.AspNetCore.Mvc;
using Proyecto_Clinica_Universitaria.Datos;
using Proyecto_Clinica_Universitaria.Models;

namespace Proyecto_Clinica_Universitaria.Controllers
{
    public class MedicamentosController : Controller
    {
        private readonly MedicamentosDatos _datos = new MedicamentosDatos();

        public IActionResult Index()
        {
            ViewBag.Lista = _datos.Listar();
            return View(new MedicamentoModel());
        }

        [HttpGet]
        public IActionResult Editar(int codigo)
        {
            var modelo = _datos.Obtener(codigo);
            if (modelo == null)
            {
                TempData["Mensaje"] = "No se encontró el medicamento.";
                return RedirectToAction("Index");
            }

            ViewBag.Lista = _datos.Listar();
            return View("Index", modelo);
        }

        [HttpPost]
        public IActionResult GuardarOEditar(MedicamentoModel modelo)
        {
            try
            {
                bool ok = (modelo.Codigo == 0)
                    ? _datos.Guardar(modelo)
                    : _datos.Editar(modelo);

                TempData["Mensaje"] = ok ? "Guardado correctamente." : "Ocurrió un error al guardar.";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int codigo)
        {
            try
            {
                _datos.Eliminar(codigo);
                TempData["Mensaje"] = "Registro eliminado.";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "No se pudo eliminar: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
