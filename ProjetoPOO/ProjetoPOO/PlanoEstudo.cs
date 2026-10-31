namespace ProjetoPOO
{
    public class PlanoEstudo
    {
        // Composição ◆──: os itens nascem aqui dentro e nenhuma referência sai daqui
        private List<ItemCronograma> itens = new List<ItemCronograma>();

        // Recebe dados primitivos e cria o item internamente
        public bool AdicionarItem(string diaSemana, int minutos)
        {
            if (string.IsNullOrWhiteSpace(diaSemana) || minutos <= 0 || minutos > 480)
            {
                return false;
            }
            itens.Add(new ItemCronograma(diaSemana, minutos));
            return true;
        }

        public int CalcularTotalMinutos()
        {
            int total = 0;
            foreach (ItemCronograma item in itens)
            {
                total += item.GetMinutosPlanejados();
            }
            return total;
        }

        // Devolve textos, não os objetos: ninguém de fora consegue alterar um item
        public List<string> ListarItens()
        {
            List<string> resumos = new List<string>();
            foreach (ItemCronograma item in itens)
            {
                resumos.Add(item.ExibirResumo());
            }
            return resumos;
        }
    }
}
