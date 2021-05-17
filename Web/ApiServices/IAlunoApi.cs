using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Aluno;

namespace Web.ApiServices
{
    public interface IAlunoApi
    {
        Task PostAluno(CriaAlunoViewModel form);
        Task<List<AlunoViewModel>> GetAlunos();
        Task<AlunoViewModel> GetAluno(int id);
    }
}
