using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskSchedulerUWP.Commands;
using TaskSchedulerUWP.Services;
using Windows.UI.Popups;

namespace TaskSchedulerUWP.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public static string UrlBase { get; set; }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                UrlBase = _url;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand RefreshCommand { get; set; }

        private ObservableCollection<Models.Task> _tasks;
        public ObservableCollection<Models.Task> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                NotifyPropertyChanged();
            }
        }

        TaskApiService _service;

        public TaskViewModel()
        {
            _service = new TaskApiService();
            Url = "http://localhost:49474";

            RefreshCommand =  new RelayCommand(async () =>
            {
                try
                {
                    var tasks = await _service.GetTasks();

                    Tasks = new ObservableCollection<Models.Task>(tasks);
                }
                catch
                {
                    var dialog = new MessageDialog("Erro ao buscar as tarefas, verifique a Url da api!");
                    await dialog.ShowAsync();
                }
            });

            _tasks = new ObservableCollection<Models.Task>();
            _tasks.Add(new Models.Task() { Title = "Tarefa exemplo", Description = "Descrição", Priority = 0 });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
