using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Clinica_Universitaria.Controllers
{
    public class MedicamentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
