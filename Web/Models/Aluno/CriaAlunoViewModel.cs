using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Turma;

namespace Web.Models.Aluno
{
    public class CriaAlunoViewModel
    {

        [Required(ErrorMessage = "Campo Nome é obrigatório")]

        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Sobrenome é obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Campo Turma é obrigatório")]
        public List<TurmaViewModel> Turmas { get; set; }

        public int TurmaId { get; set; }
    }
}
