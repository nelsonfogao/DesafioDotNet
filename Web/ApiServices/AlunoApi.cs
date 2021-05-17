using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models.Aluno;

namespace Web.ApiServices
{
    public class AlunoApi : IAlunoApi
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly ITurmaApi turmaApi;

        public AlunoApi(ITurmaApi turmaApi)
        {
            this.turmaApi = turmaApi;
        }


        public async Task<List<AlunoViewModel>> GetAlunos()
        {

            var response = await httpClient.GetAsync($"https://localhost:5001/api/Alunos");

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<List<AlunoViewModel>>(content);

            return viewModel;
        }

        public async Task<AlunoViewModel> GetAluno(int id)
        {

            var response = await httpClient.GetAsync($"https://localhost:5001/api/Alunos/" + id);

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<AlunoViewModel>(content);

            viewModel.Turma = await turmaApi.GetTurma(viewModel.TurmaId);

            return viewModel;
        }

        public Task PostAluno(CriaAlunoViewModel form)
        {
            var json = JsonConvert.SerializeObject(form);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.PostAsync($"https://localhost:5001/api/Alunos", content);

            return Task.CompletedTask;
        }
    }
}
