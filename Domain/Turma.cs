using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Turma
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]

        public string Nome { get; set; }

        public virtual IList<Aluno> Alunos { get; set; }
    }
}
