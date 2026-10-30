namespace ProjetoPOO
{
    public abstract class SessaoEstudo
    {
        private Disciplina disciplina;
        private int duracaoMinutos;

        protected SessaoEstudo(Disciplina disciplina, int duracaoMinutos)
        {
            this.disciplina = disciplina;
            this.duracaoMinutos = duracaoMinutos;
        }

        public Disciplina GetDisciplina() { return disciplina; }
        public int GetDuracaoMinutos() { return duracaoMinutos; }

        public abstract string ObterModalidade();
        public abstract int CalcularXp();

        // virtual: tem um comportamento padrão, mas as filhas podem sobrescrever
        public virtual string GerarFeedback()
        {
            return $"{ObterModalidade()} em {disciplina.GetNome()} ({duracaoMinutos} min) -> +{CalcularXp()} XP";
        }
    }
}
