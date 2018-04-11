using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace JTTT
{
    class TaskKwejkDisplay : Task
    {
        public TaskKwejkDisplay(string textboxText, string textboxUrl, string textBoxTaskName)
        {
            SearchPhrase = textboxText;
            SourceUrl = textboxUrl;
            TaskName = textBoxTaskName;
            TaskProperties = TaskName + ": Jeżeli znajde to wyświetlam Obrazek  \"" + SearchPhrase + "\" z " + SourceUrl;
        }
        public TaskKwejkDisplay() { }
        public override void Process(object randomNumb)
        {
            try
            {
                Log.WriteToLog("Nacisniecie przyciksu wykonaj. (wyswietl kwejk) Z parametrami: url: " + SourceUrl + " keyword: " + SearchPhrase);
                var HTML = new PageHtml(SourceUrl);
                //var mailsender = new MailSender(MailAdress);
                var URL_image = HTML.SearchSentence(SearchPhrase);
                if (URL_image == "")
                {
                    Log.WriteToLog("Nie znaleziono obrazka! Prawdopodobnie zła nazwa obrazka! \n");
                    App.Current.Dispatcher.Invoke(new Action(() => 
                        {
                            MainWindow w = (MainWindow)App.Current.MainWindow;
                            w.WriteConsoleTextBox("Nie znaleziono obrazka! Prawdopodobnie zła nazwa obrazka!");
                        }));
                    return;
                }
                //HTML.SaveImage(URL_image, "tmp.png");
                //mailsender.SendEmail(SearchPhrase, HTML.SearchSentence(SearchPhrase), "tmp.png");
                var win = new WindowDisplay($"Znaleziono obrazek z słowem: {SearchPhrase}", URL_image);
                win.Show();
                System.Windows.Threading.Dispatcher.Run();
                Log.WriteToLog("Barwo! Wyświetliłeś Obrazek o URL: " + URL_image + "\n");
            }
            catch (Exception x)
            {
                Log.WriteToLog("Błąd: " + x);
            }
        }
    }
}

