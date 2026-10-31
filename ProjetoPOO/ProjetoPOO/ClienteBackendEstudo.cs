using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace ProjetoPOO
{
    // Dependência (Missão 10): a tela usa este cliente para conversar com o BackendEstudoAPI.
    // Se o servidor estiver desligado, os métodos devolvem false/null em vez de travar a tela.
    public class ClienteBackendEstudo
    {
        private static readonly HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5080"),
            Timeout = TimeSpan.FromSeconds(5)
        };

        public async Task<StatusServidor?> ObterStatusAsync()
        {
            try
            {
                return await http.GetFromJsonAsync<StatusServidor>("/api/status");
            }
            catch (Exception e) when (ErroDeConexao(e))
            {
                return null;
            }
        }

        // POST /api/sessoes: reaproveita o ParaJson() da Missão 6 e acrescenta o discente
        public async Task<bool> SalvarSessaoAsync(Discente discente, SessaoEstudo sessao)
        {
            JsonNode dados = JsonNode.Parse(sessao.ParaJson())!;
            dados["discenteNome"] = discente.GetNome();

            StringContent conteudo = new StringContent(dados.ToJsonString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage resposta = await http.PostAsync("/api/sessoes", conteudo);
                return resposta.IsSuccessStatusCode;
            }
            catch (Exception e) when (ErroDeConexao(e))
            {
                return false;
            }
        }

        // GET /api/sessoes?discente=...
        public async Task<List<SessaoServidor>?> ListarSessoesAsync(string discenteNome)
        {
            try
            {
                return await http.GetFromJsonAsync<List<SessaoServidor>>($"/api/sessoes?discente={Uri.EscapeDataString(discenteNome)}");
            }
            catch (Exception e) when (ErroDeConexao(e))
            {
                return null;
            }
        }

        // GET /api/relatorio/{discente}
        public async Task<RelatorioServidor?> ObterRelatorioAsync(string discenteNome)
        {
            try
            {
                return await http.GetFromJsonAsync<RelatorioServidor>($"/api/relatorio/{Uri.EscapeDataString(discenteNome)}");
            }
            catch (Exception e) when (ErroDeConexao(e))
            {
                return null;
            }
        }

        // Servidor desligado (HttpRequestException) ou sem resposta a tempo (TaskCanceledException)
        private static bool ErroDeConexao(Exception e)
        {
            return e is HttpRequestException || e is TaskCanceledException;
        }
    }
}
