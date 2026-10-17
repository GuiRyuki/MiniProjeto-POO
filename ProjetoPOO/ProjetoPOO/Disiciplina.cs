using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoPOO
{
    internal class Disciplina
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public int CargaHoraria { get; private set; }

        public Disciplina(string codigo, string nome, string categoria, int cargaHoraria) 
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Categoria = categoria;
            this.CargaHoraria = cargaHoraria;
        }

        public int CalcularCargaHorariaEmMinutos()
        {
            return this.CargaHoraria * 60;
        }

        public void ExibirResumo()
        {
            Console.WriteLine($"[ADS201] Programação Orientada a Objetos (Computação) - {this.CargaHoraria} horas");
        }
    }
}
