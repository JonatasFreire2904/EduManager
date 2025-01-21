using EduManager.Domain.Entitys;

public class Nota
{
    public int Id { get; set; }

    // Relação com Aluno
    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; }

    // Relação com Materia
    public int MateriaId { get; set; }
    public Materia Materia { get; set; }

    // Nota da matéria
    public decimal Notas { get; set; }
}
