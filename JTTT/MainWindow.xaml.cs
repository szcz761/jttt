using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Text;


namespace JTTT
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
      
        public MainWindow()
        {
            InitializeComponent();
            StreamWriter log = File.AppendText("JTTT.log");
            log.WriteLine(DateTime.Now.ToString() + "====================== Uruchomienie programu ======================");
            log.Close();
        }
    


    private void Button_Wykonaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter log = File.AppendText("JTTT.log"))
                {
                    log.WriteLine(DateTime.Now.ToString() + " Nacisniecie przyciksu wykonaj. Z parametrami: url: " + Textbox_URL.Text + " mail: " + Textbox_Mail.Text + " keyword: " + Textbox_Text.Text);
                    log.Close();
                }

                var HTML = new PageHtml(Textbox_URL.Text);
                var mailsender = new MailSender(Textbox_Mail.Text);
                var URL_image = HTML.SearchSentence(Textbox_Text.Text);
                if (URL_image == "")
                {
                    ConsoleTextbox.Text += "Nie znaleziono obrazka! \n";
                    return;
                }
                HTML.SaveImage(URL_image, "tmp.png");
                mailsender.SendEmail(Textbox_Text.Text, HTML.SearchSentence(Textbox_Text.Text),"tmp.png");
                ConsoleTextbox.Text += "Barwo! Wysłałeś Obrazek o URL: "+URL_image+"\n";
            }
            catch(Exception x){
                ConsoleTextbox.Text += ("Expension: " + x + "\n");
                using (StreamWriter log = File.AppendText("JTTT.log"))
                {
                    log.WriteLine(DateTime.Now.ToString() + " Złapano wyjątek o  treści: " + x);
                    log.Close();
                }
            }
        }

        private void Textbox_Text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
