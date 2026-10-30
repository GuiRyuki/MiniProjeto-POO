namespace ProjetoPOO
{
    public class Discente : Usuario
    {
        private string ra;
        private int xpTotal = 0;

        public Discente(int id, string nome, string email, string senha, string ra)
            : base(id, nome, email, senha)
        {
            this.ra = ra;
        }

        public string GetRa() { return ra; }
        public int GetXpTotal() { return xpTotal; }

        public void AdicionarXp(int pontos)
        {
            // XP só aumenta: pontos negativos ou zero são ignorados
            if (pontos > 0)
            {
                xpTotal += pontos;
            }
        }

        public override string ObterTipoPerfil()
        {
            return "Discente";
        }

        public override string GerarPainelResumo()
        {
            return $"[{ObterTipoPerfil()}] {GetNome()} — RA: {ra} | XP: {xpTotal}";
        }
    }
}
