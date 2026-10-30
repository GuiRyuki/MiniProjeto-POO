namespace ProjetoPOO
{
    public abstract class Usuario
    {
        private int id;
        private string nome;
        private string email;
        private string senha;

        protected Usuario(int id, string nome, string email, string senha)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
        }

        public int GetId() { return id; }
        public string GetNome() { return nome; }
        public string GetEmail() { return email; }
        // Sem GetSenha(): a senha nunca sai da classe, só é comparada aqui dentro

        public bool Autenticar(string senhaDigitada)
        {
            return senha == senhaDigitada;
        }

        public abstract string ObterTipoPerfil();
        public abstract string GerarPainelResumo();
    }
}
