using ProjetoPOO;

namespace FecapStudyUI
{
    public class FormPrincipal : Form
    {
        // ===== Objetos de domínio (Missões 1 a 8) =====
        private readonly List<Disciplina> disciplinas;
        private readonly Responsavel responsavel;
        private readonly Discente discente;
        private readonly MetaEstudo meta;
        private readonly ServicoAlertaResponsavel servicoAlerta = new ServicoAlertaResponsavel();
        private readonly ClienteBackendEstudo clienteBackend = new ClienteBackendEstudo(); // Missão 10

        // ===== Controles da tela =====
        private readonly Label lblDiscente = new Label();
        private readonly Label lblResponsavel = new Label();
        private readonly ComboBox cmbDisciplina = new ComboBox();
        private readonly ComboBox cmbModalidade = new ComboBox();
        private readonly NumericUpDown numDuracao = new NumericUpDown();
        private readonly Label lblExtra = new Label();
        private readonly NumericUpDown numExtra = new NumericUpDown();
        private readonly Label lblTotalQuestoes = new Label();
        private readonly NumericUpDown numTotalQuestoes = new NumericUpDown();
        private readonly CheckBox chkResumo = new CheckBox();
        private readonly Button btnRegistrar = new Button();
        private readonly ProgressBar barraMeta = new ProgressBar();
        private readonly Label lblMeta = new Label();
        private readonly ListBox lstHistorico = new ListBox();
        private readonly Button btnNotificar = new Button();
        private readonly Label lblSincronizacao = new Label();
        private readonly Label lblStatusServidor = new Label();
        private readonly Button btnCarregarServidor = new Button();
        private readonly Label lblRelatorio = new Label();
        private readonly ListBox lstServidor = new ListBox();

        public FormPrincipal()
        {
            // Os objetos são criados uma vez e vivem enquanto a janela estiver aberta
            disciplinas = new List<Disciplina>
            {
                new Disciplina("ADS201", "Programação Orientada a Objetos", "Computação", 4),
                new Disciplina("ADS202", "Matemática Discreta", "Computação", 3),
                new Disciplina("ADS203", "Desenvolvimento Full Stack", "Computação", 5)
            };
            responsavel = new Responsavel(2, "Carlos Silva", "carlos@email.com", "carlos123", "Pai");
            discente = new Discente(3, "Lucas Silva", "lucas.silva@edu.fecap.br", "lucas123", "RA-2026101");
            responsavel.VincularDiscente(discente);
            responsavel.DefinirMeta(discente, 200);
            meta = responsavel.ObterMeta(discente)!;

            MontarTela();
            AtualizarCamposExtras();
            AtualizarTela();

            // Assim que a janela aparece, verifica se o backend está rodando
            Shown += async (s, e) => await AtualizarStatusServidorAsync();
        }

        // ===== Montagem dos controles =====

        private void MontarTela()
        {
            Text = "FecapStudy — Plataforma de Apoio ao Estudo";
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(1180, 640);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            // Cabeçalho de perfis
            GroupBox grpPerfis = CriarGrupo("Perfis", 12, 10, 696, 90);
            Posicionar(grpPerfis, lblDiscente, 14, 26, 668, 24);
            Posicionar(grpPerfis, lblResponsavel, 14, 54, 668, 24);
            lblDiscente.Font = new Font(Font, FontStyle.Bold);

            // Formulário de nova sessão
            GroupBox grpSessao = CriarGrupo("Nova Sessão de Estudo", 12, 110, 696, 210);
            Posicionar(grpSessao, CriarRotulo("Disciplina:"), 14, 32, 120, 24);
            Posicionar(grpSessao, cmbDisciplina, 140, 28, 400, 28);
            Posicionar(grpSessao, CriarRotulo("Modalidade:"), 14, 68, 120, 24);
            Posicionar(grpSessao, cmbModalidade, 140, 64, 200, 28);
            Posicionar(grpSessao, CriarRotulo("Duração (min):"), 14, 104, 120, 24);
            Posicionar(grpSessao, numDuracao, 140, 100, 100, 28);
            Posicionar(grpSessao, lblExtra, 14, 140, 120, 24);
            Posicionar(grpSessao, numExtra, 140, 136, 100, 28);
            Posicionar(grpSessao, lblTotalQuestoes, 260, 140, 130, 24);
            Posicionar(grpSessao, numTotalQuestoes, 395, 136, 100, 28);
            Posicionar(grpSessao, chkResumo, 260, 138, 200, 26);
            Posicionar(grpSessao, btnRegistrar, 140, 172, 200, 32);
            Posicionar(grpSessao, lblSincronizacao, 352, 178, 330, 24);

            cmbDisciplina.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisciplina.Items.AddRange(disciplinas.ToArray());
            cmbDisciplina.SelectedIndex = 0;

            cmbModalidade.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModalidade.Items.AddRange(new object[] { "Pomodoro", "Exercicios", "Leitura" });
            cmbModalidade.SelectedIndex = 0;
            cmbModalidade.SelectedIndexChanged += (s, e) => AtualizarCamposExtras();

            numDuracao.Minimum = 1;
            numDuracao.Maximum = 480; // mesmo limite do RegistrarAvanco da Missão 2
            numDuracao.Value = 25;

            lblTotalQuestoes.Text = "Total de questões:";
            numTotalQuestoes.Minimum = 1;
            numTotalQuestoes.Maximum = 200;
            numTotalQuestoes.Value = 10;
            numTotalQuestoes.ValueChanged += (s, e) => numExtra.Maximum = numTotalQuestoes.Value;

            chkResumo.Text = "Fez resumo";

            btnRegistrar.Text = "Registrar Estudo";
            btnRegistrar.Click += BtnRegistrar_Click;

            // Meta semanal
            GroupBox grpMeta = CriarGrupo("Meta Semanal", 12, 330, 696, 80);
            Posicionar(grpMeta, barraMeta, 14, 30, 440, 28);
            Posicionar(grpMeta, lblMeta, 466, 34, 220, 24);
            barraMeta.Minimum = 0;
            barraMeta.Maximum = 100;

            // Histórico
            GroupBox grpHistorico = CriarGrupo("Histórico de Sessões", 12, 420, 696, 210);
            Posicionar(grpHistorico, lstHistorico, 14, 26, 668, 130);
            Posicionar(grpHistorico, btnNotificar, 14, 166, 220, 32);
            lstHistorico.HorizontalScrollbar = true;

            btnNotificar.Text = "Notificar Responsável";
            btnNotificar.Click += BtnNotificar_Click;

            // Servidor (Missão 10)
            GroupBox grpServidor = CriarGrupo("Servidor (Backend C# + MySQL)", 720, 10, 448, 620);
            Posicionar(grpServidor, lblStatusServidor, 14, 28, 420, 24);
            Posicionar(grpServidor, btnCarregarServidor, 14, 58, 280, 32);
            Posicionar(grpServidor, lblRelatorio, 14, 100, 420, 90);
            Posicionar(grpServidor, lstServidor, 14, 196, 420, 410);
            lstServidor.HorizontalScrollbar = true;

            lblStatusServidor.Text = "Verificando servidor...";
            btnCarregarServidor.Text = "Carregar Histórico do Servidor";
            btnCarregarServidor.Click += async (s, e) => await CarregarDadosServidorAsync();
        }

        private GroupBox CriarGrupo(string titulo, int x, int y, int largura, int altura)
        {
            GroupBox grupo = new GroupBox
            {
                Text = titulo,
                Location = new Point(x, y),
                Size = new Size(largura, altura)
            };
            Controls.Add(grupo);
            return grupo;
        }

        private static Label CriarRotulo(string texto)
        {
            return new Label { Text = texto };
        }

        private static void Posicionar(Control pai, Control controle, int x, int y, int largura, int altura)
        {
            controle.Location = new Point(x, y);
            controle.Size = new Size(largura, altura);
            pai.Controls.Add(controle);
        }

        // ===== Comportamento da tela =====

        // Troca o "dado extra" conforme a modalidade escolhida
        private void AtualizarCamposExtras()
        {
            string modalidade = (string)cmbModalidade.SelectedItem!;
            lblTotalQuestoes.Visible = modalidade == "Exercicios";
            numTotalQuestoes.Visible = modalidade == "Exercicios";
            chkResumo.Visible = modalidade == "Leitura";

            if (modalidade == "Pomodoro")
            {
                lblExtra.Text = "Ciclos:";
                numExtra.Maximum = 20;
                numExtra.Value = 1;
            }
            else if (modalidade == "Exercicios")
            {
                lblExtra.Text = "Acertos:";
                numExtra.Maximum = numTotalQuestoes.Value;
                numExtra.Value = 0;
            }
            else
            {
                lblExtra.Text = "Páginas lidas:";
                numExtra.Maximum = 1000;
                numExtra.Value = 10;
            }
        }

        // O único lugar que decide QUAL subclasse criar; depois disso, tudo é polimórfico
        private SessaoEstudo CriarSessao(Disciplina disciplina, int minutos)
        {
            string modalidade = (string)cmbModalidade.SelectedItem!;
            int extra = (int)numExtra.Value;

            if (modalidade == "Pomodoro")
            {
                return new SessaoPomodoro(disciplina, minutos, extra);
            }
            if (modalidade == "Exercicios")
            {
                return new SessaoExercicios(disciplina, minutos, (int)numTotalQuestoes.Value, extra);
            }
            return new SessaoLeitura(disciplina, minutos, extra, chkResumo.Checked);
        }

        private async void BtnRegistrar_Click(object? sender, EventArgs e)
        {
            // O clique só lê a tela e pede aos objetos que façam o trabalho (nada de fórmula de XP aqui)
            Disciplina disciplina = (Disciplina)cmbDisciplina.SelectedItem!;
            int minutos = (int)numDuracao.Value;

            if (!meta.RegistrarAvanco(minutos))
            {
                MessageBox.Show("Duração inválida: informe de 1 a 480 minutos.", "Registro recusado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SessaoEstudo sessao = CriarSessao(disciplina, minutos);
            discente.RegistrarEstudo(sessao);
            lstHistorico.Items.Add(sessao.GerarFeedback());
            AtualizarTela();

            // Missão 10: além da memória, grava no backend (POST /api/sessoes)
            btnRegistrar.Enabled = false;
            lblSincronizacao.Text = "Enviando ao servidor...";
            bool salvo = await clienteBackend.SalvarSessaoAsync(discente, sessao);
            btnRegistrar.Enabled = true;

            if (salvo)
            {
                lblSincronizacao.ForeColor = Color.ForestGreen;
                lblSincronizacao.Text = "Sessão salva no servidor ✔";
                await CarregarDadosServidorAsync();
            }
            else
            {
                lblSincronizacao.ForeColor = Color.Firebrick;
                lblSincronizacao.Text = "Servidor offline: sessão ficou só na memória";
                await AtualizarStatusServidorAsync();
            }
        }

        private async Task AtualizarStatusServidorAsync()
        {
            StatusServidor? status = await clienteBackend.ObterStatusAsync();
            if (status == null)
            {
                lblStatusServidor.ForeColor = Color.Firebrick;
                lblStatusServidor.Text = "Offline — execute o Backend\\iniciar_backend.bat";
            }
            else
            {
                lblStatusServidor.ForeColor = Color.ForestGreen;
                lblStatusServidor.Text = $"Online — {status.ModoBanco}";
            }
        }

        // GET /api/sessoes e GET /api/relatorio/{discente}
        private async Task CarregarDadosServidorAsync()
        {
            btnCarregarServidor.Enabled = false;
            List<SessaoServidor>? sessoes = await clienteBackend.ListarSessoesAsync(discente.GetNome());
            RelatorioServidor? relatorio = await clienteBackend.ObterRelatorioAsync(discente.GetNome());
            btnCarregarServidor.Enabled = true;

            if (sessoes == null || relatorio == null)
            {
                await AtualizarStatusServidorAsync();
                MessageBox.Show("Não foi possível falar com o servidor em http://localhost:5080.\nInicie o Backend\\iniciar_backend.bat e tente de novo.",
                    "Servidor offline", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await AtualizarStatusServidorAsync();
            lblRelatorio.Text =
                $"Relatório de {relatorio.Discente}\n" +
                $"Sessões: {relatorio.TotalSessoes} | Minutos: {relatorio.TotalMinutosEstudados} | XP: {relatorio.TotalXpAcumulado}\n" +
                $"Meta no servidor: {relatorio.MetaMinutosSemana} min ({relatorio.PercentualMetaConcluida:0}%)\n" +
                relatorio.StatusAcompanhamento;

            lstServidor.Items.Clear();
            foreach (SessaoServidor sessao in sessoes)
            {
                lstServidor.Items.Add($"{sessao.DataRegistro} | {sessao.TipoModalidade} — {sessao.Disciplina} ({sessao.DuracaoMinutos} min) +{sessao.XpGanho} XP");
            }
        }

        private void BtnNotificar_Click(object? sender, EventArgs e)
        {
            // Dependência + interface (Missão 8): o serviço aceita qualquer INotificador
            servicoAlerta.AvaliarDiscente(discente, meta, responsavel, new NotificadorPainel());
        }

        private void AtualizarTela()
        {
            lblDiscente.Text = discente.GerarPainelResumo();
            lblResponsavel.Text = responsavel.GerarPainelResumo();

            barraMeta.Value = (int)meta.CalcularPercentual();
            lblMeta.Text = $"{meta.GetMinutosCumpridos()}/{meta.GetMetaMinutosSemana()} min ({meta.CalcularPercentual():0}%)";
            if (meta.EstaCumprida())
            {
                lblMeta.Text += " ✔";
            }
        }
    }
}
