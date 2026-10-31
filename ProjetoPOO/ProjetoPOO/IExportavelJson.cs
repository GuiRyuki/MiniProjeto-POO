namespace ProjetoPOO
{
    // Contrato: quem assina sabe se converter para JSON antes de ir para o backend
    public interface IExportavelJson
    {
        string ParaJson();
    }
}
