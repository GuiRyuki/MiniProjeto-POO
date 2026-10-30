namespace ProjetoPOO
{
    public class MetaEstudo
    {
        private int metaMinutosSemana;
        private int minutosCumpridos = 0;

        public MetaEstudo(int metaInicial)
        {
            RedefinirMeta(metaInicial);
        }

        // Somente leitura: não existe SetMinutosCumpridos
        public int GetMinutosCumpridos() { return minutosCumpridos; }
        public int GetMetaMinutosSemana() { return metaMinutosSemana; }

        public bool RegistrarAvanco(int minutos)
        {
            if (minutos <= 0 || minutos > 480)
            {
                return false;
            }
            minutosCumpridos += minutos;
            return true;
        }

        public void RedefinirMeta(int novaMeta)
        {
            if (novaMeta < 30)
            {
                novaMeta = 30;
            }
            else if (novaMeta > 3000)
            {
                novaMeta = 3000;
            }
            metaMinutosSemana = novaMeta;
        }

        public double CalcularPercentual()
        {
            double percentual = (minutosCumpridos * 100.0) / metaMinutosSemana;
            if (percentual > 100.0)
            {
                percentual = 100.0;
            }
            return percentual;
        }

        public bool EstaCumprida()
        {
            return minutosCumpridos >= metaMinutosSemana;
        }
    }
}
