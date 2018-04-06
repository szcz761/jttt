using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JTTT
{
    class DBManager
    {
        public ObservableCollection<Task> lists { get; set; }
        JTTTDBContext _DB;
        public DBManager()
        {
            _DB = new JTTTDBContext();
            lists = new ObservableCollection<Task>(_DB.Tasks.ToList());
        }

        public ObservableCollection<Task> GetTasks()
        {
            lists = new ObservableCollection<Task>(_DB.Tasks.ToList());
            return lists;
        }

        public void AddTask(Task NewTask)
        {
            _DB.Tasks.Add(NewTask);
            _DB.SaveChanges();
            lists = new ObservableCollection<Task>(_DB.Tasks.ToList());
        }

        public void UpdateTextView(ListView Lista)
        {
            Lista.Items.Clear();
            foreach(var it in lists)
            {
                Lista.Items.Add(it);
            }
        }

        public void RemoveTasks()
        {
            var rows = from o in _DB.Tasks
                       select o;

            foreach (var row in rows)
            {
                _DB.Tasks.Remove(row);
            }
            _DB.SaveChanges();
            lists = new ObservableCollection<Task>(_DB.Tasks.ToList());
        }

        public void DoTasks()
        {
            foreach (Task t in GetTasks())
            {
                t.process();
            }
        }

    }
}
