namespace ProjetoPOO
{
    public class Responsavel : Usuario
    {
        private string parentesco;

        // Associação (Missão 7): os discentes são criados fora e apenas vinculados aqui
        private List<Discente> discentesAcompanhados = new List<Discente>();
        private Dictionary<Discente, MetaEstudo> metas = new Dictionary<Discente, MetaEstudo>();

        public Responsavel(int id, string nome, string email, string senha, string parentesco)
            : base(id, nome, email, senha)
        {
            this.parentesco = parentesco;
        }

        public string GetParentesco() { return parentesco; }
        public IReadOnlyList<Discente> GetDiscentesAcompanhados() { return discentesAcompanhados.AsReadOnly(); }

        public bool VincularDiscente(Discente discente)
        {
            if (discentesAcompanhados.Contains(discente))
            {
                return false; // já vinculado: não duplica
            }
            discentesAcompanhados.Add(discente);
            return true;
        }

        // Cria a meta do discente ou ajusta a existente (usando a validação de MetaEstudo)
        public bool DefinirMeta(Discente discente, int metaMinutosSemana)
        {
            if (!discentesAcompanhados.Contains(discente))
            {
                return false; // só define meta de quem acompanha
            }
            if (metas.ContainsKey(discente))
            {
                metas[discente].RedefinirMeta(metaMinutosSemana);
            }
            else
            {
                metas[discente] = new MetaEstudo(metaMinutosSemana);
            }
            return true;
        }

        public MetaEstudo? ObterMeta(Discente discente)
        {
            return metas.GetValueOrDefault(discente);
        }

        public override string ObterTipoPerfil()
        {
            return "Responsavel";
        }

        public override string GerarPainelResumo()
        {
            return $"[{ObterTipoPerfil()}] {GetNome()} ({parentesco}) | {GetEmail()} | Acompanha: {discentesAcompanhados.Count} discente(s)";
        }
    }
}
