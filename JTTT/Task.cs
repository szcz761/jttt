using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTTT
{
    class Task
    {
        private string TextboxText { get; set; }
        private string TextboxUrl{ get; set; }
        private string TextboxMail { get; set; }
        public Task(string textboxText, string textboxUrl, string textboxMail)
        {
            TextboxText = textboxText;
            TextboxUrl = textboxUrl;
            TextboxMail = textboxMail;
        }

        public string process()
        {
            try
            {
                Log.WriteToLog("Nacisniecie przyciksu wykonaj. Z parametrami: url: " + TextboxUrl + " mail: " + TextboxMail + " keyword: " + TextboxText);
                var HTML = new PageHtml(TextboxUrl);
                var mailsender = new MailSender(TextboxMail);
                var URL_image = HTML.SearchSentence(TextboxText);
                if (URL_image == "")        
                    return "Nie znaleziono obrazka! Prawdopodobnie zła nazwa obrazka! \n";
                HTML.SaveImage(URL_image, "tmp.png");
                mailsender.SendEmail(TextboxText, HTML.SearchSentence(TextboxText), "tmp.png");
                return "Barwo! Wysłałeś Obrazek o URL: " + URL_image + "\n";
            }
            catch (Exception x)
            {    
                Log.WriteToLog("Błąd: " + x);
                return "Błąd: " + x.Message.ToString() + "\n";
            }
        }
    }
}
