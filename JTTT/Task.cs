using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTTT
{
    public class Task
    {
        [XmlElement("Searching_Phrase")]
        public string SearchPhrase { get; set; }
        [XmlElement("Source_Url")]
        public string SourceUrl{ get; set; }
        [XmlElement("Client_E-Mail")]
        public string MailAdress { get; set; }
        [XmlElement("Task_Name")]
        public string TaskName { get; set; }
        [XmlIgnore]
        public string TaskProperties { get; set; }
        public Task(string textboxText, string textboxUrl, string textboxMail, string textBoxTaskName)
        {
            SearchPhrase = textboxText;
            SourceUrl = textboxUrl;
            MailAdress = textboxMail;
            TaskName = textBoxTaskName;
            TaskProperties = TaskName + ": Obrazek \"" + SearchPhrase + "\" z " + SourceUrl + " dla " + MailAdress;
        }

        public Task()
        {

        }

        public string process()
        {
            try
            {
                Log.WriteToLog("Nacisniecie przyciksu wykonaj. Z parametrami: url: " + SourceUrl + " mail: " + MailAdress + " keyword: " + SearchPhrase);
                var HTML = new PageHtml(SourceUrl);
                var mailsender = new MailSender(MailAdress);
                var URL_image = HTML.SearchSentence(SearchPhrase);
                if (URL_image == "")        
                    return "Nie znaleziono obrazka! Prawdopodobnie zła nazwa obrazka! \n";
                HTML.SaveImage(URL_image, "tmp.png");
                mailsender.SendEmail(SearchPhrase, HTML.SearchSentence(SearchPhrase), "tmp.png");
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
