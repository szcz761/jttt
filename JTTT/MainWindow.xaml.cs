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
        Tasks tasks;
      
        public MainWindow()
        {
            InitializeComponent();
            Log.WriteToLog("====================== Uruchomienie programu ======================");
            tasks = new Tasks();
            ListOfTasks.DataContext = tasks;
        }
    

    /*private void Button_Wykonaj_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task(Textbox_Text.Text, Textbox_URL.Text, Textbox_Mail.Text, Textbox_Task_Name.Text);
            ConsoleTextbox.Text = task.process();
        }*/

        private void Button_Add_Task_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task newTask = new Task(Textbox_Text.Text, Textbox_URL.Text, Textbox_Mail.Text, Textbox_Task_Name.Text);
                tasks.AddTask(newTask);
            }
            catch (Exception x)
            {
                Log.WriteToLog("Błąd: " + x);
            }
        }

        private void Button_Clean_Click(object sender, RoutedEventArgs e)
        {
            tasks.RemoveTasks();
        }
    }
}
