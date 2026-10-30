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
        }
    }
}
