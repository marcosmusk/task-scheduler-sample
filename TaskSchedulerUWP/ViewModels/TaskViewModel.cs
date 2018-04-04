using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerUWP.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Models.Task> _tasks = new ObservableCollection<Models.Task>();
        public ObservableCollection<Models.Task> Tasks { get { return this._tasks; } }

        public TaskViewModel()
        {
            _tasks.Add(new Models.Task() { Title = "TT1" });
            _tasks.Add(new Models.Task() { Title = "TT2" });
            _tasks.Add(new Models.Task() { Title = "TT3" });
            _tasks.Add(new Models.Task() { Title = "TT4" });
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
