using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileSystemWatcherTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileSystemWatcher watcher = null!;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                var path = dialog.SelectedPath;
                PathTextBox.Text = path;
                WatcherStart(path, "*.txt");
            }
        }

        private void WatcherStart(string path, string filter)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.InternalBufferSize = 64 * 1024; // Set the buffer size to 64KB

            watcher.Changed += OnProcess;
            watcher.Created += OnProcess;
            watcher.Deleted += OnProcess;
            watcher.Renamed +=OnRenamed;
            watcher.Error += OnError;

            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
        }

        private static void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                OnCreated(source, e);
            }
            else if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                OnChanged(source, e);
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                OnDeleted(source, e);
            }
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("有新文件创建了");
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("有文件被修改了");
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("有文件删除了");
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("有文件被重命名了");
        }

        private static void OnError(object source, ErrorEventArgs e)
        {
            var err = e.GetException();
            System.Diagnostics.Trace.WriteLine(err.Message);
        }
    }
}
