using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

public class MailSender
{
    readonly private string TargetEmail;
    public string MyMail { get; set; } = "automail226303@gmail.com";
    public string MyMailPassword { private get; set; } = "1zxcvbnm";
    public string MailHost { private get; set; } = "smtp.gmail.com";


    public MailSender(string email)
    {
        TargetEmail = email;

        using (StreamWriter log = File.AppendText("JTTT.log"))
        {
            log.WriteLine(DateTime.Now.ToString() + "Stworzono klase MailSender z: Mymail: " + MyMail + " MailHost: " + MailHost + "TargetEmail: " + TargetEmail + "Oraz Defultowym hasłem");
            log.Close();
        }
    }
    public void SendEmail(string keyword, string imageURL,string nameOfImage)
    {
        try
        {
            var message = new MailMessage();
            message.From = new MailAddress(MyMail);
            message.To.Add(new MailAddress(TargetEmail));

            message.Subject = "JTTT Wykonał zadanie! ";

            message.Body = "Witam, zgodnie z polecieniem wkonuje powiezone mi zadanie: \n" + keyword + "\n Oto link do obrazka: " + imageURL + "\n Znajduje się on również w załączniku.";

            var mail = new SmtpClient(MailHost);
            mail.Port = 587;
            mail.Credentials = new NetworkCredential(MyMail, MyMailPassword);
            mail.EnableSsl = true;

            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + "SendEmail(): Stworzono wiadomośc z tekstem (bez złącznika). Parametry: keyword: " + keyword + " imageURL: " + imageURL + "nameOfImage: " + nameOfImage);
                log.Close();
            }

            Attachment att = new Attachment(nameOfImage, MediaTypeNames.Application.Octet);
            message.Attachments.Add(att);
            mail.Send(message);

            message.Dispose();
            mail.Dispose();
            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + "SendEmail(): wysłano wiadomosć wraz z załącznikiem parametry: keyword: " + keyword + " imageURL: " + imageURL + "nameOfImage: " + nameOfImage);
                log.Close();
            }
        }
        catch (Exception ex){
            
            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + "SendEmail(): Expection:  " + ex);
                log.Close();
            }
            throw new Exception("Nie udało się wysłać! Prawdopodobnie zły mail!");
        }
    }
}
