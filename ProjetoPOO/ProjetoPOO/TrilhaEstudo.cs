namespace ProjetoPOO
{
    public class TrilhaEstudo
    {
        private string titulo;
        private Administrador responsavel; // Associação: o administrador existe fora da trilha
        private List<Disciplina> disciplinas = new List<Disciplina>(); // Agregação ◇──

        public TrilhaEstudo(string titulo, Administrador responsavel)
        {
            this.titulo = titulo;
            this.responsavel = responsavel;
        }

        public string GetTitulo() { return titulo; }
        public Administrador GetResponsavel() { return responsavel; }
        public IReadOnlyList<Disciplina> GetDisciplinas() { return disciplinas.AsReadOnly(); }

        // Recebe a disciplina pronta, criada fora: a trilha não dá "new Disciplina"
        public bool AdicionarDisciplina(Disciplina disciplina)
        {
            if (disciplinas.Contains(disciplina))
            {
                return false;
            }
            disciplinas.Add(disciplina);
            return true;
        }

        public int CalcularCargaHorariaTotal()
        {
            int total = 0;
            foreach (Disciplina disciplina in disciplinas)
            {
                total += disciplina.GetCargaHorariaSemanalHoras();
            }
            return total;
        }

        public string ExibirResumo()
        {
            return $"Trilha \"{titulo}\" (por {responsavel.GetNome()}) — {disciplinas.Count} disciplina(s), {CalcularCargaHorariaTotal()}h/semana";
        }
    }
}
