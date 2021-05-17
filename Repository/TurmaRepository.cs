using Domain;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class TurmaRepository
    {
        private WebApiContext Context { get; set; }

        public TurmaRepository(WebApiContext context)
        {
            this.Context = context;
        }

        public List<Turma> GetAll()
        {
            return Context.Turmas.ToList();

        }

        public Turma GetTurmaById(int id)
        {
            return Context.Turmas.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Turma turma)
        {
            this.Context.Turmas.Add(turma);
            this.Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var turma = Context.Turmas.FirstOrDefault(x => x.Id == id);
            this.Context.Turmas.Remove(turma);
            this.Context.SaveChanges();
        }

        public void Update(int id, Turma turma)
        {
            var turmaOld = Context.Turmas.FirstOrDefault(x => x.Id == id);
            turmaOld.Nome = turma.Nome;

            Context.Turmas.Update(turmaOld);
            this.Context.SaveChanges();
        }
    }
}
