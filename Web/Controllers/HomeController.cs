using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TurmaServices services;
        private readonly AlunoServices services2;

        public HomeController(ILogger<HomeController> logger, TurmaServices services, AlunoServices services2)
        {
            _logger = logger;
            this.services = services;
            this.services2 = services2;
        }

        public ActionResult Index()
        {
            var paginaInicial = new PaginaInicialViewModel();

            var turmas = services.GetAll();
            paginaInicial.QuantidadeDeTurmas = turmas.Count;

            var alunos = services2.GetAll();
            paginaInicial.QuantidadeDeAlunos = alunos.Count;

            return View(paginaInicial);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
