namespace ProjetoPOO
{
    public class Discente : Usuario
    {
        private string ra;
        private int xpTotal = 0;
        private List<SessaoEstudo> historico = new List<SessaoEstudo>();
        private PlanoEstudo plano = new PlanoEstudo(); // cada discente nasce com seu próprio plano

        public Discente(int id, string nome, string email, string senha, string ra)
            : base(id, nome, email, senha)
        {
            this.ra = ra;
        }

        public string GetRa() { return ra; }
        public int GetXpTotal() { return xpTotal; }
        public PlanoEstudo GetPlanoEstudo() { return plano; }

        // Somente leitura: quem está de fora consegue listar, mas não adicionar nem remover
        public IReadOnlyList<SessaoEstudo> GetHistorico() { return historico.AsReadOnly(); }

        public void AdicionarXp(int pontos)
        {
            // XP só aumenta: pontos negativos ou zero são ignorados
            if (pontos > 0)
            {
                xpTotal += pontos;
            }
        }

        // ===== Sobrecarga (Missão 5): mesmo nome, parâmetros diferentes =====

        // Forma 1: recebe a sessão pronta
        public void RegistrarEstudo(SessaoEstudo sessao)
        {
            historico.Add(sessao);
            AdicionarXp(sessao.CalcularXp());
        }

        // Forma 2 (atalho rápido): cria um Pomodoro padrão
        public void RegistrarEstudo(Disciplina disciplina, int minutos)
        {
            int ciclos = minutos / 25;
            if (ciclos < 1)
            {
                ciclos = 1;
            }
            RegistrarEstudo(new SessaoPomodoro(disciplina, minutos, ciclos));
        }

        // Forma 3 (por horário): cria uma Leitura com resumo
        public void RegistrarEstudo(Disciplina disciplina, int horaInicio, int horaFim, int paginasLidas)
        {
            if (horaFim <= horaInicio)
            {
                return; // intervalo inválido não gera sessão
            }
            int minutos = (horaFim - horaInicio) * 60;
            RegistrarEstudo(new SessaoLeitura(disciplina, minutos, paginasLidas, true));
        }

        public override string ObterTipoPerfil()
        {
            return "Discente";
        }

        public override string GerarPainelResumo()
        {
            return $"[{ObterTipoPerfil()}] {GetNome()} — RA: {ra} | XP: {xpTotal} | Sessões: {historico.Count}";
        }
    }
}
