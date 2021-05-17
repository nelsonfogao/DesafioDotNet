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
using Web.ApiServices;
using Web.Models.Aluno;
using Web.Models.Turma;

namespace Web.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoApi alunoApi;
        private readonly ITurmaApi turmaApi;
        private readonly AlunoServices AlunoServices;
        private readonly TurmaServices TurmaServices;
        private readonly IMapper mapper;
        private readonly WebApiContext _context;


        public AlunosController(IAlunoApi alunoApi, ITurmaApi turmaApi, AlunoServices alunoServices, TurmaServices turmaServices, IMapper mapper, WebApiContext context)
        {
            this.alunoApi = alunoApi;
            this.turmaApi = turmaApi;
            this.AlunoServices = alunoServices;
            this.TurmaServices = turmaServices;
            this.mapper = mapper;
            this._context = context;
        }

        // GET: Alunos
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

            var request = new RestRequest("https://localhost:5001/api/Alunos", DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<List<AlunoViewModel>>(request);

            return View(response.Data);
        }

        // GET: Alunos/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/Alunos/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<AlunoViewModel>(request);

            var aluno = response.Data;
            var idTurma = aluno.TurmaId;
            var turma = TurmaServices.GetTurmaById(idTurma);
            aluno.Turma = mapper.Map<TurmaViewModel>(turma);
            return View(aluno);

        }

        // GET: Alunos/Create
        public ActionResult Create()
        {
            var viewModel = new CriaAlunoViewModel();


            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/Turmas", DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<List<TurmaViewModel>>(request);

            var list = response.Data;
            viewModel.Turmas = mapper.Map<List<TurmaViewModel>>(list);

            return View(viewModel);
        }

        // POST: PessoaController/Create

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,TurmaId")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/Alunos/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response1 = client.Get<AlunoViewModel>(request);

            return View(response1.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,TurmaId")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
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
            return View(aluno);
        }
        public ActionResult Delete(int? id)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/Alunos/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response1 = client.Get<AlunoViewModel>(request);

            return View(response1.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }


    }
}
