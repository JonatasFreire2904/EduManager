using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduManager.Domain.Entitys
{
    public class Materia
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relação com o professor
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } // Propriedade de navegação

        // Relação com alunos e notas
        public List<Nota> Notas { get; set; } = new();

    }
}
