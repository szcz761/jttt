using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;



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
            Log.WriteToLog("====================== Uruchomienie programu ======================");
        }
    

    private void Button_Wykonaj_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task(Textbox_Text.Text, Textbox_URL.Text, Textbox_Mail.Text);
            ConsoleTextbox.Text = task.process();
        }

    }
}
