using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTTT
{
    class TaskKwejkSender: Task
    {
        public TaskKwejkSender(string textboxText, string textboxUrl, string textboxMail, string textBoxTaskName)
        {
            SearchPhrase = textboxText;
            SourceUrl = textboxUrl;
            MailAdress = textboxMail;
            TaskName = textBoxTaskName;
            base.TaskProperties = TaskName + ": Obrazek \"" + SearchPhrase + "\" z " + SourceUrl + " dla " + MailAdress;
        }

        public TaskKwejkSender()
        {

        }

        public override void Process()
        {
            try
            {
                Log.WriteToLog("Nacisniecie przyciksu wykonaj. Z parametrami: url: " + SourceUrl + " mail: " + MailAdress + " keyword: " + SearchPhrase);
                var HTML = new PageHtml(SourceUrl);
                var mailsender = new MailSender(MailAdress);
                var URL_image = HTML.SearchSentence(SearchPhrase);
                if (URL_image == "")
                {
                    Log.WriteToLog("Nie znaleziono obrazka! Prawdopodobnie zła nazwa obrazka! \n");
                    return;
                }
                HTML.SaveImage(URL_image, "tmp.png");
                mailsender.SendEmail(SearchPhrase, HTML.SearchSentence(SearchPhrase), "tmp.png");
                Log.WriteToLog("Barwo! Wysłałeś Obrazek o URL: " + URL_image + "\n");
            }
            catch (Exception x)
            {
                Log.WriteToLog("Błąd: " + x);
            }
        }
    }
}
