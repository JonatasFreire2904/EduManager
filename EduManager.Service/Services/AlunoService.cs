using EduManager.Domain.Interfaces;
using EduManager.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduManager.Service.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository ?? throw new ArgumentNullException(nameof(alunoRepository));
        }

        public async Task<bool> AdicionarMateriaAoAluno(int alunoId, int materiaId)
        {
            if (alunoId <= 0)
                throw new ArgumentException("O ID do aluno deve ser maior que zero.", nameof(alunoId));

            if (materiaId <= 0)
                throw new ArgumentException("O ID da matéria deve ser maior que zero.", nameof(materiaId));

            return await _alunoRepository.AdicionarMateriaAoAluno(alunoId, materiaId);
        }

        public async Task<IEnumerable<Nota>> ObterNotas(int alunoId)
        {
            if (alunoId <= 0)
                throw new ArgumentException("O ID do aluno deve ser maior que zero.", nameof(alunoId));

            return await _alunoRepository.ObterNotas(alunoId);
        }
    }
}
