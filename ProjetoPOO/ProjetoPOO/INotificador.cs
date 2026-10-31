namespace ProjetoPOO
{
    // Contrato: quem assina promete saber enviar um alerta, não importa por qual canal
    public interface INotificador
    {
        string ObterCanal();
        bool EnviarAlerta(string destinatario, string mensagem);
    }
}
