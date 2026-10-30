namespace ProjetoPOO
{
    public class SessaoExercicios : SessaoEstudo
    {
        private int totalQuestoes;
        private int acertos;

        public SessaoExercicios(Disciplina disciplina, int duracaoMinutos, int totalQuestoes, int acertos)
            : base(disciplina, duracaoMinutos)
        {
            this.totalQuestoes = totalQuestoes;

            // Acertos ficam entre 0 e o total de questões (evita XP inflado)
            if (acertos < 0)
            {
                acertos = 0;
            }
            else if (acertos > totalQuestoes)
            {
                acertos = totalQuestoes;
            }
            this.acertos = acertos;
        }

        public int GetTotalQuestoes() { return totalQuestoes; }
        public int GetAcertos() { return acertos; }

        public double CalcularPercentualAcerto()
        {
            if (totalQuestoes <= 0)
            {
                return 0;
            }
            return acertos * 100.0 / totalQuestoes;
        }

        public override string ObterModalidade()
        {
            return "Exercicios";
        }

        public override int CalcularXp()
        {
            return GetDuracaoMinutos() + (acertos * 10);
        }

        public override string GerarFeedback()
        {
            return base.GerarFeedback() + $" | {acertos}/{totalQuestoes} acertos ({CalcularPercentualAcerto():0}%)";
        }
    }
}
