namespace ProjetoPOO
{
    public class NotificadorWhatsApp : INotificador
    {
        public string ObterCanal()
        {
            return "WhatsApp";
        }

        public bool EnviarAlerta(string destinatario, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(destinatario))
            {
                return false;
            }
            // Simulação: um sistema real chamaria a API do WhatsApp aqui
            Console.WriteLine($"[{ObterCanal()}] Para: {destinatario} | {mensagem}");
            return true;
        }
    }
}
