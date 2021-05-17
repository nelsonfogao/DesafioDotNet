using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Turma;

namespace Web.ApiServices
{
    public interface ITurmaApi
    {
        Task<List<TurmaViewModel>> GetTurmas();
        Task<TurmaViewModel> GetTurma(int id);
    }
}
