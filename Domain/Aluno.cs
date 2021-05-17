using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Aluno
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }


        public int TurmaId { get; set; }

        [JsonIgnore]
        public virtual Turma Turma { get; set; }

    }
}
