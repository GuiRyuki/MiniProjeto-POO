namespace ProjetoPOO
{
    // Dependência - - ->: usa Discente, MetaEstudo, Responsavel e INotificador
    // apenas durante o método, sem guardar nenhum deles em atributos
    public class ServicoAlertaResponsavel
    {
        public bool AvaliarDiscente(Discente discente, MetaEstudo meta, Responsavel responsavel, INotificador notificador)
        {
            string mensagem;
            if (meta.EstaCumprida())
            {
                mensagem = $"Parabéns! {discente.GetNome()} atingiu 100% da meta semanal ({meta.GetMinutosCumpridos()}/{meta.GetMetaMinutosSemana()} min).";
            }
            else
            {
                mensagem = $"{discente.GetNome()} está com {meta.CalcularPercentual():0}% da meta semanal ({meta.GetMinutosCumpridos()}/{meta.GetMetaMinutosSemana()} min).";
            }
            return notificador.EnviarAlerta(responsavel.GetEmail(), mensagem);
        }
    }
}
