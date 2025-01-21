using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduManager.Domain.Interfaces
{
    public interface IDiretorService
    {
        Task<Aluno?> GetAlunoByName(string name);
        Task<Aluno?> GetAlunoByCpf(string cpf);
        Task<Professor?> GetProfessorByName(string name);
        Task<bool> AddProfessor(Professor professor);
        Task<bool> RemoveProfessor(string professor);
        Task<bool> AddAluno(Aluno aluno);
        Task<bool> RemoveAluno(string aluno);
        Task<bool> AtualizaProfessor(Professor professor);
        Task<bool?> AtualizaAluno(Aluno aluno);
    }
}
