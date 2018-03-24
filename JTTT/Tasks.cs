using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTTT
{
    
    class Tasks
    {
        public ObservableCollection<Task> _list { get; set; }

        public Tasks()
        {
            _list = new ObservableCollection<Task>();
        }


        public void AddTask(Task newTask)
        {
            _list.Add(newTask);
        }

        public void RemoveTasks()
        {
            _list.Clear();
        }
    }
}
