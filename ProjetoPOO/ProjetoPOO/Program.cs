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
        }
    }
}
