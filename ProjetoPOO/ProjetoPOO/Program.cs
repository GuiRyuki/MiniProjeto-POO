using System.Runtime.CompilerServices;

namespace ProjetoPOO
{
    internal class Program
    {
        static void Main(string[] args)
        {

         Disciplina pOO = new Disciplina(
             "ADS201", 
             "Programação Orientada a Objetos", 
             "Computação", 
             60);

            pOO.ExibirResumo();

            Disciplina matematica = new Disciplina(
                "ADS201",
                "Matemática Discreta",
                "Computação",
                45);
            matematica.ExibirResumo();

            Disciplina fullStack = new Disciplina(
                "ADS201",
                "Desenvolvimento Full Stack",
                "Computação",
                80);
            fullStack.ExibirResumo();
        }
    }
}

