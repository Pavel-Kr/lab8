using Avalonia.Controls;
using Avalonia.Interactivity;
using lab8.ViewModels;
using lab8.Models;

namespace lab8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.FindControl<MenuItem>("Load").Click += async delegate
            {

                var window = new OpenFileDialog()
                {
                    Title = "Open File"
                };
                window.Filters.Add(new FileDialogFilter() { Name = "Binary files (*.bin)", Extensions = { "bin" } });
                string[]? path = await window.ShowAsync((Window)this.VisualRoot);

                var context = this.DataContext as MainWindowViewModel;
                if (path == null) context.Path = "";
                else context.Path = string.Join("\\", path);
            };
            this.FindControl<MenuItem>("Save").Click += async delegate
            {
                var window = new SaveFileDialog()
                {
                    Title = "Save File"
                };
                window.Filters.Add(new FileDialogFilter() { Name = "Binary files (*.bin)", Extensions = { "bin" } });

                string? path = await window.ShowAsync((Window)this.VisualRoot);

                var context = this.DataContext as MainWindowViewModel;
                if (path == null) context.Savepath = "";
                else context.Savepath = path;
            };
            this.FindControl<MenuItem>("Exit").Click += delegate
            {
                this.Close();
            };
            this.FindControl<MenuItem>("About").Click += delegate
            {
                var window = new Info();
                window.ShowDialog((Window)this.VisualRoot);
            };
        }
        public void OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var item = btn.DataContext as KanbanTask;
            var context = this.DataContext as MainWindowViewModel;
            context.DeleteTask(item);
        }
        public async void AddImageClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var item = btn.DataContext as KanbanTask;
            var context = this.DataContext as MainWindowViewModel;
            var window = new OpenFileDialog()
            {
                Title = "Open File"
            };
            window.Filters.Add(new FileDialogFilter() { Name = "Images (*.jpg, *.png)", Extensions = { "jpg", "png" } });
            string[]? path = await window.ShowAsync((Window)this.VisualRoot);
            context.AddImage(item, string.Join(@"\", path));
        }
    }
}
