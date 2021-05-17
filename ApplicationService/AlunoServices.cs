using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService
{
    public class AlunoServices
    {
        private AlunoRepository Repository { get; set; }

        public AlunoServices(AlunoRepository repository)
        {
            this.Repository = repository;
        }

        public List<Aluno> GetAll()
        {
            return Repository.GetAll();
        }

        public Aluno GetAlunoById(int id)
        {
            return Repository.GetAlunoById(id);
        }
        public List<Aluno> GetAlunosById(int id)
        {
            return Repository.GetAlunosById(id);
        }

        public void Save(Aluno aluno)
        {

            this.Repository.Save(aluno);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }

        public void Update(int id, Aluno aluno)
        {
            Repository.Update(id, aluno);
        }

    }
}
