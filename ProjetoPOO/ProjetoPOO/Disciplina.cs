namespace ProjetoPOO
{
    public class Disciplina
    {
        private string codigo;
        private string nome;
        private string categoria;
        private int cargaHorariaSemanalHoras;

        public Disciplina(string codigo, string nome, string categoria, int cargaHorariaSemanalHoras)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.categoria = categoria;

            // Regra da Missão 1: carga inválida assume o padrão de 2 horas
            if (cargaHorariaSemanalHoras <= 0)
            {
                cargaHorariaSemanalHoras = 2;
            }
            this.cargaHorariaSemanalHoras = cargaHorariaSemanalHoras;
        }

        public string GetCodigo() { return codigo; }
        public string GetNome() { return nome; }
        public string GetCategoria() { return categoria; }
        public int GetCargaHorariaSemanalHoras() { return cargaHorariaSemanalHoras; }

        public int CalcularCargaEmMinutos()
        {
            return cargaHorariaSemanalHoras * 60;
        }

        public string ExibirResumo()
        {
            return $"[{codigo}] {nome} ({categoria}) — {cargaHorariaSemanalHoras}h/semana ({CalcularCargaEmMinutos()} min)";
        }

        // Sobrescreve o ToString() de object: é o texto que o ComboBox da tela exibe
        public override string ToString()
        {
            return $"[{codigo}] {nome}";
        }
    }
}
