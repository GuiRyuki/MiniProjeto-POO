namespace ProjetoPOO
{
    public class NotificadorEmail : INotificador
    {
        public string ObterCanal()
        {
            return "E-mail";
        }

        public bool EnviarAlerta(string destinatario, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(destinatario) || !destinatario.Contains('@'))
            {
                return false;
            }
            // Simulação: um sistema real usaria SMTP aqui
            Console.WriteLine($"[{ObterCanal()}] Para: {destinatario} | {mensagem}");
            return true;
        }
    }
}
