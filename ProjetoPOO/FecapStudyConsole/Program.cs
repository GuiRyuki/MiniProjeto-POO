using System.Text;

namespace ProjetoPOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // ===== Missão 1: Abstração, Classes e Objetos =====
            Console.WriteLine("===== Missão 1: Disciplinas =====");

            Disciplina poo = new Disciplina("ADS201", "Programação Orientada a Objetos", "Computação", 4);
            Disciplina matematica = new Disciplina("ADS202", "Matemática Discreta", "Computação", 3);
            Disciplina fullStack = new Disciplina("ADS203", "Desenvolvimento Full Stack", "Computação", 5);
            Disciplina invalida = new Disciplina("TST999", "Teste de Carga Inválida", "Teste", -4);

            Console.WriteLine(poo.ExibirResumo());
            Console.WriteLine(matematica.ExibirResumo());
            Console.WriteLine(fullStack.ExibirResumo());
            Console.WriteLine(invalida.ExibirResumo());

            // ===== Missão 2: Encapsulamento e Validação =====
            Console.WriteLine();
            Console.WriteLine("===== Missão 2: Meta de Estudo =====");

            MetaEstudo meta = new MetaEstudo(100);
            Console.WriteLine($"RegistrarAvanco(-20) -> {meta.RegistrarAvanco(-20)}");
            Console.WriteLine($"RegistrarAvanco(900) -> {meta.RegistrarAvanco(900)}");
            Console.WriteLine($"Minutos cumpridos após recusas: {meta.GetMinutosCumpridos()}");
            Console.WriteLine($"RegistrarAvanco(60)  -> {meta.RegistrarAvanco(60)}");
            Console.WriteLine($"Percentual: {meta.CalcularPercentual()}% | Cumprida? {meta.EstaCumprida()}");

            MetaEstudo metaSabotada = new MetaEstudo(-100);
            Console.WriteLine($"Meta criada com -100 foi ajustada para: {metaSabotada.GetMetaMinutosSemana()} min");

            // ===== Missão 3: Classe Abstrata e Herança =====
            Console.WriteLine();
            Console.WriteLine("===== Missão 3: Perfis de Usuário =====");

            Administrador admin = new Administrador(1, "Profa. Helena", "helena@fecap.br", "admin123", "Coordenação PI");
            Responsavel responsavel = new Responsavel(2, "Carlos Silva", "carlos@email.com", "carlos123", "Pai");
            Discente discente = new Discente(3, "Lucas Silva", "lucas.silva@edu.fecap.br", "lucas123", "RA-2026101");

            discente.AdicionarXp(50);
            discente.AdicionarXp(-30); // ignorado

            // Lista do tipo da classe mãe guardando objetos das 3 filhas
            List<Usuario> usuarios = new List<Usuario> { admin, responsavel, discente };
            foreach (Usuario usuario in usuarios)
            {
                Console.WriteLine(usuario.GerarPainelResumo());
            }

            Console.WriteLine($"Login do Lucas com 'lucas123': {discente.Autenticar("lucas123")}");
            Console.WriteLine($"Login do Lucas com 'errada':   {discente.Autenticar("errada")}");
            // new Usuario(...) não compila: Usuario é abstrata

            // ===== Missão 4: Polimorfismo Dinâmico =====
            Console.WriteLine();
            Console.WriteLine("===== Missão 4: Sessões de Estudo =====");

            List<SessaoEstudo> sessoes = new List<SessaoEstudo>
            {
                new SessaoPomodoro(poo, 50, 2),
                new SessaoExercicios(matematica, 30, 10, 8),
                new SessaoLeitura(fullStack, 40, 20, true)
            };

            int xpTotalSessoes = 0;
            foreach (SessaoEstudo sessao in sessoes)
            {
                // Nenhum if testando o tipo: cada objeto sabe calcular o próprio XP
                Console.WriteLine(sessao.GerarFeedback());
                xpTotalSessoes += sessao.CalcularXp();
            }
            Console.WriteLine($"XP total das sessões: {xpTotalSessoes}");

            // ===== Missão 5: Polimorfismo Estático (Sobrecarga) =====
            Console.WriteLine();
            Console.WriteLine("===== Missão 5: Registro de Estudo =====");
            Console.WriteLine($"Antes:  {discente.GerarPainelResumo()}");

            discente.RegistrarEstudo(sessoes[0]);                // Forma 1: sessão pronta
            discente.RegistrarEstudo(poo, 50);                   // Forma 2: atalho Pomodoro
            discente.RegistrarEstudo(fullStack, 14, 16, 30);     // Forma 3: das 14h às 16h
            discente.RegistrarEstudo(fullStack, 16, 14, 30);     // Forma 3 inválida: ignorada

            foreach (SessaoEstudo sessao in discente.GetHistorico())
            {
                Console.WriteLine($"  {sessao.GerarFeedback()}");
            }
            Console.WriteLine($"Depois: {discente.GerarPainelResumo()}");

            // ===== Missão 6: Interfaces =====
            Console.WriteLine();
            Console.WriteLine("===== Missão 6: Interfaces =====");

            // Mesma chamada, canais diferentes: o código só conhece o contrato INotificador
            List<INotificador> notificadores = new List<INotificador>
            {
                new NotificadorEmail(),
                new NotificadorWhatsApp()
            };
            foreach (INotificador notificador in notificadores)
            {
                bool enviado = notificador.EnviarAlerta(responsavel.GetEmail(), $"{discente.GetNome()} está com {discente.GetXpTotal()} XP!");
                Console.WriteLine($"  Enviado por {notificador.ObterCanal()}? {enviado}");
            }
            Console.WriteLine($"  E-mail para destinatário inválido? {new NotificadorEmail().EnviarAlerta("sem-arroba", "teste")}");

            // Classes de famílias diferentes, tratadas pelo mesmo contrato IExportavelJson
            List<IExportavelJson> exportaveis = new List<IExportavelJson> { sessoes[0], sessoes[1], meta };
            foreach (IExportavelJson item in exportaveis)
            {
                Console.WriteLine(item.ParaJson());
            }

            // ===== Missão 7: Associação e Agregação =====
            Console.WriteLine();
            Console.WriteLine("===== Missão 7: Associação e Agregação =====");

            // Associação: Responsavel ——> Discente (os dois já existiam antes do vínculo)
            Discente mariana = new Discente(4, "Mariana Costa", "mariana.costa@edu.fecap.br", "mari123", "RA-2026102");
            Console.WriteLine($"Vincular Lucas:              {responsavel.VincularDiscente(discente)}");
            Console.WriteLine($"Vincular Mariana:            {responsavel.VincularDiscente(mariana)}");
            Console.WriteLine($"Vincular Lucas de novo:      {responsavel.VincularDiscente(discente)}");
            responsavel.DefinirMeta(discente, 200);
            Console.WriteLine($"Meta do Lucas definida pelo responsável: {responsavel.ObterMeta(discente)?.GetMetaMinutosSemana()} min");
            Console.WriteLine(responsavel.GerarPainelResumo());

            // Agregação: TrilhaEstudo ◇── Disciplina (a mesma disciplina em duas trilhas)
            TrilhaEstudo trilhaAds = new TrilhaEstudo("Trilha 2º Semestre ADS", admin);
            trilhaAds.AdicionarDisciplina(poo);
            trilhaAds.AdicionarDisciplina(matematica);
            trilhaAds.AdicionarDisciplina(fullStack);

            TrilhaEstudo trilhaPi = new TrilhaEstudo("Trilha Projeto Interdisciplinar", admin);
            trilhaPi.AdicionarDisciplina(poo); // mesma instância de poo, sem copiar
            trilhaPi.AdicionarDisciplina(fullStack);

            Console.WriteLine(trilhaAds.ExibirResumo());
            Console.WriteLine(trilhaPi.ExibirResumo());

            trilhaPi = null!; // "apaga" a trilha...
            Console.WriteLine($"Trilha PI apagada, mas a disciplina continua existindo: {poo.ExibirResumo()}");

            // ===== Missão 8: Composição e Dependência =====
            Console.WriteLine();
            Console.WriteLine("===== Missão 8: Composição e Dependência =====");

            // Composição: só passamos dados; o PlanoEstudo cria os ItemCronograma lá dentro
            PlanoEstudo plano = discente.GetPlanoEstudo();
            plano.AdicionarItem("Segunda", 60);
            plano.AdicionarItem("Quarta", 90);
            plano.AdicionarItem("Sexta", 50);
            Console.WriteLine($"Item inválido (-30 min) aceito? {plano.AdicionarItem("Sábado", -30)}");
            Console.WriteLine($"Plano do Lucas: {string.Join(" | ", plano.ListarItens())} — total {plano.CalcularTotalMinutos()} min");

            // Dependência: o serviço recebe tudo por parâmetro e não guarda nada
            MetaEstudo metaLucas = responsavel.ObterMeta(discente)!;
            ServicoAlertaResponsavel servicoAlerta = new ServicoAlertaResponsavel();

            metaLucas.RegistrarAvanco(120);
            servicoAlerta.AvaliarDiscente(discente, metaLucas, responsavel, new NotificadorEmail());

            metaLucas.RegistrarAvanco(100);
            servicoAlerta.AvaliarDiscente(discente, metaLucas, responsavel, new NotificadorWhatsApp());
        }
    }
}
