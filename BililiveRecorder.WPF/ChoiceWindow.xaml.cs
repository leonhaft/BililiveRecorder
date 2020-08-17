using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// Interaction logic for ChoiceWindow.xaml
    /// </summary>
    public partial class ChoiceWindow : Window, INotifyPropertyChanged
    {
        public string Message { get => _message; set => SetField(ref _message, value); }
        private string _message = string.Empty;

        public bool ConfirmButtonEnabled { get => _confirmBtnEnabled; set => SetField(ref _confirmBtnEnabled, value); }
        private bool _confirmBtnEnabled = true;

        public string ConfirmButtonText { get => _confirmBtnText; set => SetField(ref _confirmBtnText, value); }
        private string _confirmBtnText = string.Empty;

        public bool DenyButtonEnabled { get => _denyBtnEnabled; set => SetField(ref _denyBtnEnabled, value); }
        private bool _denyBtnEnabled = true;

        public string DenyButtonText { get => _denyBtnText; set => SetField(ref _denyBtnText, value); }
        private string _denyBtnText = string.Empty;

        public bool CancelButtonEnabled { get => _cancelBtnEnabled; set => SetField(ref _cancelBtnEnabled, value); }
        private bool _cancelBtnEnabled = true;

        public string CancelButtonText { get => _cancelBtnText; set => SetField(ref _cancelBtnText, value); }
        private string _cancelBtnText = string.Empty;

        public MessageBoxResult Result { get; private set; }

        public ChoiceWindow()
        {
            DataContext = this;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_confirmBtnEnabled == false)
            {
                BtnConfirm.Visibility = Visibility.Hidden;
            }
            else if (string.IsNullOrEmpty(_confirmBtnText) == false)
            {
                BtnConfirm.Content = ConfirmButtonText;
            }

            if (DenyButtonEnabled == false)
            {
                BtnDeny.Visibility = Visibility.Hidden;
            }
            else if (string.IsNullOrEmpty(DenyButtonText) == false)
            {
                BtnDeny.Content = DenyButtonText;
            }

            if (CancelButtonEnabled == false)
            {
                BtnCancel.Visibility = Visibility.Hidden;
            }
            else if (string.IsNullOrEmpty(CancelButtonText) == false)
            {
                BtnCancel.Content = CancelButtonText;
            }

        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;

            Close();
        }

        private void DenyClick(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;

            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e) => Cancel();

        private void Cancel()
        {
            Result = MessageBoxResult.Cancel;

            Close();
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
