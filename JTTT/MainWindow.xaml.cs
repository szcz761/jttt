using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;
using Jeson_Test_JTTT4._1_;

namespace JTTT
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Tasks tasks;
        DBManager DataBase;
      
        public MainWindow()
        {
            InitializeComponent();
            Log.WriteToLog("====================== Uruchomienie programu ======================");
            //tasks = new Tasks();
            DataBase = new DBManager();
            //ListOfTasks.DataContext = DataBase.GetTasks();//<<==========================================================TU zmiana ale nie wiem na co 
            DataBase.UpdateTextView(ListOfTasks);
        }

        private void Button_Add_Task_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task newTask;
                if (TIPogoda.IsFocused)
                    newTask = new TaskTempSender(TextBox_Miasto.Text, UpDownControl_Temp.Value, Textbox_Mail.Text, Textbox_Task_Name.Text);
                else
                    newTask = new TaskKwejkSender(Textbox_Text.Text, Textbox_URL.Text, Textbox_Mail.Text, Textbox_Task_Name.Text);
                //tasks.AddTask(newTask);
                DataBase.AddTask(newTask);
                ListOfTasks.Items.Add(newTask);
                //ListOfTasks.SourceUpdated();
            }
            catch (Exception x)
            {
                Log.WriteToLog("Błąd: " + x);
            }
        }

        private void Button_Clean_Click(object sender, RoutedEventArgs e)
        { 
            DataBase.RemoveTasks();
            ListOfTasks.Items.Clear();
        }

        private void Button_Do_Tasks_Click(object sender, RoutedEventArgs e)
        {
            DataBase.DoTasks();
        }

        private void Button_Pogoda_Click(object sender, RoutedEventArgs e)
        {
            Window1 win2 = new Window1();
            win2.Show();
        }

        /* private void Button_Deserialize_Click(object sender, RoutedEventArgs e)
         {
             ObservableCollection<Task> tmp = Serialization.DeserializationFunc("task.xml");
             Log.WriteToLog(tasks._list.Count.ToString());
             while(tmp.Count > 0)
             {
                 //tasks._list.Add(tmp[0]);
                 DataBase.AddTask(tmp[0]);
                 tmp.RemoveAt(0);
             }
             /*ListOfTasks.InvalidateVisual();
             ListOfTasks.UpdateLayout();
             ListOfTasks.DataContext = tasks;
             ListOfTasks.Items.Refresh();
         }

         private void Button_Serialize_Click(object sender, RoutedEventArgs e)
         {
             Serialization.SerializationFunc<ObservableCollection<Task>>(DataBase.GetTasks());
         }*/
    }
}
