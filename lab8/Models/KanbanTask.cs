using System;
using System.ComponentModel;

namespace lab8.Models
{
    [Serializable()]
    public class KanbanTask : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler? PropertyChanged;
        string header;
        string text;
        string imgSource;
        private void NotifyPropertyChanged(string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public string Header
        {
            get => header;
            set
            {
                header = value;
                NotifyPropertyChanged();
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                NotifyPropertyChanged();
            }
        }
        public string ImgSource
        {
            get => imgSource;
            set
            {
                imgSource = value;
                NotifyPropertyChanged();
            }
        }
        public KanbanTask()
        {
            header = "";
            text = "";
            imgSource = "";
        }
        
    }
}
