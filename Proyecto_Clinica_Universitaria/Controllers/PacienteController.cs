using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Clinica_Universitaria.Controllers
{
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
