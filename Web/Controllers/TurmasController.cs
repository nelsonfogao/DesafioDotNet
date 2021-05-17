using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationService;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Data;
using RestSharp;
using Web.Models.Aluno;
using Web.Models.Turma;

namespace Web.Controllers
{
    public class TurmasController : Controller
    {
        private TurmaServices TurmaServices { get; set; }
        private AlunoServices AlunoServices { get; set; }
        private readonly IMapper mapper;
        private readonly WebApiContext _context;

        public TurmasController(WebApiContext context, IMapper mapper, TurmaServices turmaServices, AlunoServices alunoServices)
        {
            this.TurmaServices = turmaServices;
            this.AlunoServices = alunoServices;
            this.mapper = mapper;
            _context = context;
        }

        // GET: Turmas
        public ActionResult Index()
        {

            var client = new RestClient();

            var requestToken = new RestRequest("https://localhost:5001/api/Usuarios/token");

            requestToken.AddJsonBody(JsonConvert.SerializeObject(new
            {
                Login = "Nelson",
                Password = "abc123"
            }));


            var result = client.Post<TokenResult>(requestToken).Data;

            this.HttpContext.Session.SetString("Token", result.Token);

            var request = new RestRequest("https://localhost:5001/api/Turmas", DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<List<TurmaViewModel>>(request);

            return View(response.Data);
        }
        public ActionResult GetTurmas()
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/Turmas", DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<List<TurmaViewModel>>(request);

            return View(response.Data);
        }
        public ActionResult GetTurma(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/Turmas" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<TurmaViewModel>(request);

            return View(response.Data);
        }
        // GET: Turmas/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/Turmas/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<TurmaViewModel>(request);

            return View(response.Data);
        }

        // GET: Turmas/Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Turma turma)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(turma);


                var client = new RestClient();
                var request = new RestRequest("https://localhost:5001/api/Turmas", DataFormat.Json);
                request.AddJsonBody(turma);

                var response = client.Post<TurmaViewModel>(request);
                _context.Add(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("APP_ERROR", ex.Message);
                return View(turma);
            }
        }

        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/Turmas/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response1 = client.Get<TurmaViewModel>(request);

            return View(response1.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Turma turma)
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurmaExists(turma.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }
        // GET: Turmas/Delete/5
        public ActionResult Delete(int? id)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/Turmas/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response1 = client.Get<TurmaViewModel>(request);

            return View(response1.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         
            try
            {
                var turma = await _context.Turmas.FindAsync(id);

                _context.Turmas.Remove(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));




            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult Alunos(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/Turmas/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<Turma>(request);


            var turma = response.Data;
            var turmaList = turma;
            var turmaViewModel = mapper.Map<TurmaViewModel>(turmaList);
            var busca = AlunoServices.GetAlunosById(turmaViewModel.Id);
            var alunos = mapper.Map<List<AlunoViewModel>>(busca);
            turmaViewModel.Alunos = alunos;
            return View(turmaViewModel);
        }
        private bool TurmaExists(int id)
        {
            return _context.Turmas.Any(e => e.Id == id);
        }
    }
    public class TokenResult
    {
        public String Token { get; set; }
    }
}
