namespace ProjetoPOO
{
    public class Responsavel : Usuario
    {
        private string parentesco;

        public Responsavel(int id, string nome, string email, string senha, string parentesco)
            : base(id, nome, email, senha)
        {
            this.parentesco = parentesco;
        }

        public string GetParentesco() { return parentesco; }

        public override string ObterTipoPerfil()
        {
            return "Responsavel";
        }

        public override string GerarPainelResumo()
        {
            return $"[{ObterTipoPerfil()}] {GetNome()} ({parentesco}) | {GetEmail()}";
        }
    }
}
