namespace ProjetoPOO
{
    public class SessaoPomodoro : SessaoEstudo
    {
        private int ciclosCompletos;

        public SessaoPomodoro(Disciplina disciplina, int duracaoMinutos, int ciclosCompletos)
            : base(disciplina, duracaoMinutos)
        {
            this.ciclosCompletos = ciclosCompletos;
        }

        public int GetCiclosCompletos() { return ciclosCompletos; }

        public override string ObterModalidade()
        {
            return "Pomodoro";
        }

        public override int CalcularXp()
        {
            return GetDuracaoMinutos() * 2 + (ciclosCompletos * 15);
        }

        public override string GerarFeedback()
        {
            return base.GerarFeedback() + $" | {ciclosCompletos} ciclo(s) concluído(s)";
        }
    }
}
