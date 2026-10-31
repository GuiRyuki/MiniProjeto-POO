using System.Text.Encodings.Web;
using System.Text.Json;

namespace ProjetoPOO
{
    public abstract class SessaoEstudo : IExportavelJson
    {
        // Mantém acentos legíveis no JSON (sem ç)
        private static readonly JsonSerializerOptions opcoesJson = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

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

        // Nomes dos campos iguais aos que o backend espera em POST /api/sessoes
        public string ParaJson()
        {
            var dados = new
            {
                disciplina = disciplina.GetNome(),
                tipoModalidade = ObterModalidade(),
                duracaoMinutos = duracaoMinutos,
                xpGanho = CalcularXp(),
                resumoFeedback = GerarFeedback()
            };
            return JsonSerializer.Serialize(dados, opcoesJson);
        }
    }
}
