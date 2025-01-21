using EduManager.Domain.Entitys;

public class Professor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Especialidade Especialidade { get; set; }

    // Relação com as matérias que ele é responsável
    public List<Materia> MateriasResponsaveis { get; set; } = new();
}
