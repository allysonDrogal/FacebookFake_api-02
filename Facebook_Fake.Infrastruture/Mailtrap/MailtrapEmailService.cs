
using Facebook_Fake.Domain.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace Facebook_Fake.Infrastruture.Mailtrap
{
    public class MailtrapEmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly string _resetPasswordUrl;

        public MailtrapEmailService(
            string smtpHost,
            int smtpPort,
            string smtpUsername,
            string smtpPassword,
            string fromEmail,
            string fromName,
            string resetPasswordUrl)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _fromEmail = fromEmail;
            _fromName = fromName;
            _resetPasswordUrl = resetPasswordUrl;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string userName, string resetToken)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_fromName, _fromEmail));
            message.To.Add(new MailboxAddress(userName, toEmail));
            message.Subject = "Redefinição de Senha - Facebook Fake";

            var resetLink = $"{_resetPasswordUrl}?token={resetToken}";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <h2>Olá, {userName}!</h2>
                        <p>Você solicitou a redefinição de senha da sua conta.</p>
                        <p>Clique no link abaixo para redefinir sua senha:</p>
                        <p><a href='{resetLink}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Redefinir Senha</a></p>
                        <p>Este link expira em 1 hora.</p>
                        <p>Se você não solicitou a redefinição de senha, ignore este email.</p>
                        <br/>
                        <p>Atenciosamente,<br/>Equipe Facebook Fake</p>
                    </body>
                    </html>
                ",
                TextBody = $@"
                    Olá, {userName}!
                    
                    Você solicitou a redefinição de senha da sua conta.
                    
                    Acesse o link abaixo para redefinir sua senha:
                    {resetLink}
                    
                    Este link expira em 1 hora.
                    
                    Se você não solicitou a redefinição de senha, ignore este email.
                    
                    Atenciosamente,
                    Equipe Facebook Fake
                "
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpHost, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
