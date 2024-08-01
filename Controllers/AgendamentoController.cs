using System.Diagnostics;
using BH_Estética_VisualStudio.Models;
using Microsoft.AspNetCore.Mvc;

namespace BH_Estética_VisualStudio.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly ILogger<AgendamentoController> _logger;

        public AgendamentoController(ILogger<AgendamentoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AgendamentoUser()
        {
            return View();
        }
        public IActionResult CadastroAgendamento()
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
