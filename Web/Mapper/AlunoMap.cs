using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Aluno;

namespace Web.Mapper
{
    public class AlunoMap : Profile
    {
        public AlunoMap()
        {
            CreateMap<Aluno, AlunoViewModel>();
        }
    }
}
