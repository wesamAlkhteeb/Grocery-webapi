
using System.Net;
using System.Net.Mail;

namespace Grocery.Domain.Helper;

public class EmailService{
    private MailMessage mailMessage;
    private SmtpClient gmailClient;
    public static EmailService emailService = new EmailService();
    private EmailService(){
        mailMessage = new MailMessage
        {
            From = new MailAddress("Wesam.alkhteeb1998@gmail.com"),
            Subject = "Grocery application"
        };
        gmailClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(
                    "wesam.alkhteeb1998@gmail.com", 
                    "efztxkyyxrgrpnzw"
                    ),
            EnableSsl = true
        };
    }


    public void sendMessage(string email ,string code){
        mailMessage.To.Add(new MailAddress(email));
        mailMessage.Body ="<h1>To confirm account use:</h1>"+ 
            $"<p>Code: {code}</P>";
        mailMessage.IsBodyHtml =true;
        gmailClient.Send(mailMessage);
        mailMessage.To.Clear();
    }
}