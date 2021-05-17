using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService
{
    public class TurmaServices
    {
        private TurmaRepository Repository { get; set; }

        public TurmaServices(TurmaRepository repository)
        {
            this.Repository = repository;
        }

        public List<Turma> GetAll()
        {
            return Repository.GetAll();
        }

        public Turma GetTurmaById(int id)
        {
            return Repository.GetTurmaById(id);
        }

        public void Save(Turma turma)
        {

            this.Repository.Save(turma);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }

        public void Update(int id, Turma turma)
        {
            Repository.Update(id, turma);
        }

    }
}
