using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models.Turma;

namespace Web.ApiServices
{
    public class TurmaApi : ITurmaApi
    {
        private readonly HttpClient httpClient;

        public TurmaApi()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<TurmaViewModel> GetTurma(int id)
        {
            var response = await httpClient.GetAsync($"api/Turmas/" + id);

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<TurmaViewModel>(content);

            return viewModel;
        }

        public async Task<List<TurmaViewModel>> GetTurmas()
        {
            var response = await httpClient.GetAsync($"api/Turmas");

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<List<TurmaViewModel>>(content);

            return viewModel;
        }
    }
}
