using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using lab8.Models;
using ReactiveUI;

namespace lab8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ObservableCollection<ObservableCollection<KanbanTask>> kanbanTasks;
        public ObservableCollection<ObservableCollection<KanbanTask>> KanbanTasks
        {
            get => kanbanTasks;
            set
            {
                this.RaiseAndSetIfChanged(ref kanbanTasks, value);
            }
        }
        public MainWindowViewModel()
        {
            NewBoard();

        }
        public void NewBoard()
        {
            KanbanTasks = new ObservableCollection<ObservableCollection<KanbanTask>>();
            for (int i = 0; i < 3; i++)
            {
                KanbanTasks.Add(new ObservableCollection<KanbanTask>());
            }
        }
        public void Add(string num)
        {
            int index = Convert.ToInt32(num);
            KanbanTasks[index].Add(new KanbanTask());
        }
        string path;
        public string Path
        {
            get => path;
            set
            {
                path = value;
                if (path != "")
                {
                    Stream openFileStream = File.OpenRead(path);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    KanbanTasks = (ObservableCollection<ObservableCollection<KanbanTask>>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                }
            }
        }
        string savepath;
        public string Savepath
        {
            get => savepath;
            set
            {
                savepath = value;
                if (savepath != "")
                {
                    Stream SaveFileStream = File.Create(savepath);
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(SaveFileStream, KanbanTasks);
                    SaveFileStream.Close();
                }
            }
        }
        public void DeleteTask(KanbanTask item)
        {
            for(int i = 0; i < 3; i++)
            {
                if (KanbanTasks[i].Contains(item))
                {
                    KanbanTasks[i].Remove(item);
                    break;
                }
            }
        }
        public void AddImage(KanbanTask item, string source)
        {
            for(int i = 0; i < 3; i++)
            {
                if (KanbanTasks[i].Contains(item))
                {
                    foreach(KanbanTask task in KanbanTasks[i])
                    {
                        if (task.Equals(item))
                        {
                            task.ImgSource = source;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
