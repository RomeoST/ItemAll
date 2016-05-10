using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using System.ComponentModel;

namespace ItemAll.Forms.Message
{
    /// <summary>
    /// Логика взаимодействия для TextMessage.xaml
    /// </summary>
    public partial class TextMessage : MetroWindow
    {
        private ViewModel _vm;
        public TextMessage(string head, string text)
        {
            InitializeComponent();

            _vm = new ViewModel();
            DataContext = _vm;

            l_text.Content = text;
            _vm.MyTitle = string.IsNullOrEmpty(head) ? "Ошибка" : head;
        }

        public class ViewModel : INotifyPropertyChanged
        {
            private string _myTitle = "Ошибка";

            public string MyTitle
            {
                get { return _myTitle; }
                set
                {
                    if (_myTitle == value) return;

                    _myTitle = value;
                    OnPropertyChanged("MyTitle");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
