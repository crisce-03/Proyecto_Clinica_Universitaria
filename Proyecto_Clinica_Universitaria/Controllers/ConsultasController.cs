using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Clinica_Universitaria.Controllers
{
    public class ConsultasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
