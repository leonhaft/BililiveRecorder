using BililiveRecorder.Core;
using BililiveRecorder.Core.Config;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BililiveRecorder.WPF
{
    /// <summary>
    /// Interaction logic for TabSettingsWindow.xaml
    /// </summary>
    public partial class TabSettingsWindow : Window, INotifyPropertyChanged
    {
        public static readonly SolidColorBrush Red = new SolidColorBrush(Color.FromArgb(0xFF, 0xF7, 0x1B, 0x1B));
        public static readonly SolidColorBrush Green = new SolidColorBrush(Color.FromArgb(0xFF, 0x0B, 0xB4, 0x22));

        public ConfigV1 Config { get; set; } = new ConfigV1();

        private string _workPath;
        public string WorkPath
        {
            get => _workPath;
            set
            {
                SetField(ref _workPath, value.TrimEnd('/', '\\'));
                TabSettingsWindow_PropertyChanged(this, new PropertyChangedEventArgs(nameof(WorkPath)));
            }
        }

        private string _statusText = "请选择目录";
        public string StatusText { get => _statusText; set => SetField(ref _statusText, value); }

        private SolidColorBrush _statusColor = Red;
        public SolidColorBrush StatusColor { get => _statusColor; set => SetField(ref _statusColor, value); }

        private bool _status;
        public bool Status { get => _status; set => SetField(ref _status, value); }

        private Mutex mutex;


        public TabSettingsWindow(MainWindow mainWindow, ConfigV1 config)
        {
            Owner = mainWindow;
            config.CopyPropertiesTo(Config);
            DataContext = Config;
            PropertyChanged += TabSettingsWindow_PropertyChanged;
            InitializeComponent();
        }

        private void TabSettingsWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Status))
            {
                if (Status)
                {
                    StatusColor = Green;
                }
                else
                {
                    StatusColor = Red;
                }
            }
            else if (e.PropertyName == nameof(WorkPath))
            {
                CheckPath();
            }
        }

        protected void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Multiselect = false,
                Title = "选择录播姬工作目录路径",
                AddToMostRecentlyUsedList = false,
                EnsurePathExists = true,
                NavigateToShortcut = true,
                InitialDirectory = Config.WorkDirectory,
            };
            if (fileDialog.ShowDialog(this) == CommonFileDialogResult.Ok)
            {
                Config.WorkDirectory = fileDialog.FileName;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            // if (!_CheckSavePath())
            // {
            //     return;
            // }

            DialogResult = true;
            Close();
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            //var box=            new MessageBox
            //{
            //    Title = "关闭录播姬？",
            //    Message = "确定要关闭录播姬吗？",
            //    CountDown = 10,
            //    Left = Left,
            //    Top = Top,
            //    WindowStartupLocation = WindowStartupLocation.CenterOwner
            //});
            //{

            //}
            //else
            //{

            //}

            //var dialogResult = MessageBox.Show(this, "是否立即更新工作目录,程序将重新启动，正在录制的直播将中断,谨慎选择?", "选择是否立即更新", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            var dialog = new ChoiceWindow
            {
                Owner = this,
                Title = "选择是否立即更新",
                Message = "是否立即更新工作目录?是将程序将重新启动，正在录制的直播将中断,谨慎选择",
                ConfirmButtonText = "立即",
                DenyButtonText = "下次启动",
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            if (dialog.ShowDialog() == true)
            {
                if (dialog.Result == MessageBoxResult.OK)
                {

                }
                else if (dialog.Result == MessageBoxResult.No)
                {

                }
                else if (dialog.Result == MessageBoxResult.Cancel)
                {

                }
            }
        }

        private void CheckPath()
        {
            string c = WorkPath;
            string config = System.IO.Path.Combine(c, "config.json");
            bool result = false;

            if (!Directory.Exists(c))
            {
                StatusText = "目录不存在";
                result = false;
            }
            else if (!Directory.EnumerateFiles(c).Any())
            {
                StatusText = "可用的空文件夹";
                result = true;
            }
            else if (!File.Exists(config))
            {
                StatusText = "此文件夹已有其他文件";
                result = false;
            }
            else
            {
                try
                {
                    var j = JObject.Parse(File.ReadAllText(config));
                    if (j["version"] == null || j["data"] == null)
                    {
                        StatusText = "配置文件损坏";
                        result = false;
                    }
                    else
                    {
                        StatusText = "录播姬曾经使用过的目录";
                        result = true;
                    }
                }
                catch (Exception)
                {
                    StatusText = "配置文件不可读";
                    result = false;
                }
            }

            if (!result)
            {
                Status = false;
            }
            else
            { }

        }

        private void Restart()
        {
            if (mutex != null)
            {
                try
                {
                    mutex.ReleaseMutex();
                }
                catch (Exception)
                { }
                finally
                {
                    mutex.Dispose();
                    mutex = null;
                }
            }
            try
            {
                mutex = new Mutex(true, @"Global\BililiveRecorder.WPF.." + WorkPath.GetHashCode(), out bool createdNew);
                if (createdNew)
                {
                    Status = true;
                }
                else
                {
                    Status = false;
                    StatusText = "已有录播姬在此文件夹运行";
                }
            }
            catch (Exception)
            {
                Status = false;
                StatusText = "检查录播姬运行状态时出错";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) { return false; }
            field = value; OnPropertyChanged(propertyName); return true;
        }
    }
}
