using ProjetoPOO;

namespace FecapStudyUI
{
    // Mais um canal para o mesmo contrato da Missão 6: o alerta aparece numa janela
    public class NotificadorPainel : INotificador
    {
        public string ObterCanal()
        {
            return "Painel";
        }

        public bool EnviarAlerta(string destinatario, string mensagem)
        {
            MessageBox.Show(mensagem, $"Alerta para {destinatario}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
    }
}
