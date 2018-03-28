using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;

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

        private void Button_Do_Tasks_Click(object sender, RoutedEventArgs e)
        {
            tasks.DoTasks();
        }

        private void Button_Deserialize_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Task> tmp = Serialization.DeserializationFunc("task.xml");
            Log.WriteToLog(tasks._list.Count.ToString());
            while(tmp.Count > 0)
            {
                tasks._list.Add(tmp[0]);
                tmp.RemoveAt(0);
            }
            /*ListOfTasks.InvalidateVisual();
            ListOfTasks.UpdateLayout();
            ListOfTasks.DataContext = tasks;
            ListOfTasks.Items.Refresh();*/
        }

        private void Button_Serialize_Click(object sender, RoutedEventArgs e)
        {
            Serialization.SerializationFunc<ObservableCollection<Task>>(tasks._list);
        }
    }
}
