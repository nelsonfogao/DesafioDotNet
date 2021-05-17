using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Turma;

namespace Web.Mapper
{
    public class TurmaMap : Profile
    {
        public TurmaMap()
        {
            CreateMap<Turma, TurmaViewModel>();
        }
    }
}
