namespace ProjetoPOO
{
    // Parte de uma composição: só é criado dentro de PlanoEstudo
    public class ItemCronograma
    {
        private string diaSemana;
        private int minutosPlanejados;

        public ItemCronograma(string diaSemana, int minutosPlanejados)
        {
            this.diaSemana = diaSemana;
            this.minutosPlanejados = minutosPlanejados;
        }

        public string GetDiaSemana() { return diaSemana; }
        public int GetMinutosPlanejados() { return minutosPlanejados; }

        public string ExibirResumo()
        {
            return $"{diaSemana}: {minutosPlanejados} min";
        }
    }
}
