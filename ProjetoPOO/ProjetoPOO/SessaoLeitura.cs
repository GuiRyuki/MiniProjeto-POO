namespace ProjetoPOO
{
    public class SessaoLeitura : SessaoEstudo
    {
        private int paginasLidas;
        private bool fezResumo;

        public SessaoLeitura(Disciplina disciplina, int duracaoMinutos, int paginasLidas, bool fezResumo)
            : base(disciplina, duracaoMinutos)
        {
            this.paginasLidas = paginasLidas;
            this.fezResumo = fezResumo;
        }

        public int GetPaginasLidas() { return paginasLidas; }
        public bool GetFezResumo() { return fezResumo; }

        public override string ObterModalidade()
        {
            return "Leitura";
        }

        public override int CalcularXp()
        {
            return GetDuracaoMinutos() + (paginasLidas * 3) + (fezResumo ? 25 : 0);
        }

        // Não sobrescreve GerarFeedback(): usa o padrão da classe mãe
    }
}
