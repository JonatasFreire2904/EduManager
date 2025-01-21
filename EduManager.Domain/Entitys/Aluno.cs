using EduManager.Domain.Entitys;

public class Aluno
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }

    // Relação com notas (que ligam aluno e matérias)
    public List<Nota> Notas { get; set; } = new();
}
