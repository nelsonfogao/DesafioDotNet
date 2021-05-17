using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Aluno;

namespace Web.Models.Turma
{
    public class TurmaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]

        public string Nome { get; set; }

        public virtual IList<AlunoViewModel> Alunos { get; set; }
    }
}
