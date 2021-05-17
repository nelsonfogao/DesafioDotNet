using Domain;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class AlunoRepository
    {
        private WebApiContext Context { get; set; }

        public AlunoRepository(WebApiContext context)
        {
            this.Context = context;
        }

        public List<Aluno> GetAll()
        {
            return Context.Alunos.ToList();

        }

        public Aluno GetAlunoById(int id)
        {
            return Context.Alunos.FirstOrDefault(x => x.TurmaId == id);
        }
        public List<Aluno> GetAlunosById(int id)
        {
            var aluno = Context.Alunos.Where(x => x.TurmaId == id);
            return aluno.ToList();
        }

        public void Save(Aluno aluno)
        {
            this.Context.Alunos.Add(aluno);
            this.Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var aluno = Context.Alunos.FirstOrDefault(x => x.Id == id);
            this.Context.Alunos.Remove(aluno);
            this.Context.SaveChanges();
        }

        public void Update(int id, Aluno aluno)
        {
            var alunoOld = Context.Alunos.FirstOrDefault(x => x.Id == id);
            alunoOld.Nome = aluno.Nome;
            alunoOld.Sobrenome = aluno.Sobrenome;

            Context.Alunos.Update(alunoOld);
            this.Context.SaveChanges();
        }
    }
}
