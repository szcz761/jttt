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
            Task task = new Task(Textbox_Text.Text, Textbox_URL.Text, Textbox_URL.Text);
            ConsoleTextbox.Text = task.process();
        }

        private void Textbox_Text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
