using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private string _neoUrl;
        private string _neoUser;
        private string _neoPassword;

        public GraphBrowser Browser
        {
            get
            {
                return (GraphBrowser)App.Current.TryFindResource("browser");
            }
            set
            {
                App.Current.Resources["browser"] = value;
            }
        }

        public MainWindow_ViewModel()
        {
            Host = "localhost";
            Port = 7474;
            User = "neo4j";
            Password = "longbow";
            UseSsl = false;
            ConnectCommand = new RelayCommand(Connect);
        }

        public ICommand ConnectCommand { get; private set; }

        private void Connect()
        {
            GraphBrowser.Instance.Connect(Host, Port, User, Password, UseSsl);
            CollapsedWhenConnected = Visibility.Collapsed;
        }

        #region string Host

        private string _host;
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                if (_host == value) return;
                _host = value;
                RaisePropertyChanged("Host");
            }
        }

        #endregion string Host
        #region int Port

        private int _port;
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                if (_port == value) return;
                _port = value;
                RaisePropertyChanged("Port");
            }
        }

        #endregion int Port
        #region string User

        private string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                if (_user == value) return;
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        #endregion string User
        #region string Password

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password == value) return;
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        #endregion string Password
        #region bool UseSsl

        private bool _useSsl;
        public bool UseSsl
        {
            get
            {
                return _useSsl;
            }
            set
            {
                if (_useSsl == value) return;
                _useSsl = value;
                RaisePropertyChanged("UseSsl");
            }
        }

        #endregion bool UseSsl

        #region Visibility CollapsedWhenConnected

        private Visibility _collapsedWhenConnected = Visibility.Visible;
        public Visibility CollapsedWhenConnected
        {
            get
            {
                return _collapsedWhenConnected;
            }
            set
            {
                if (_collapsedWhenConnected == value) return;
                _collapsedWhenConnected = value;
                RaisePropertyChanged("CollapsedWhenConnected");
            }
        }

        #endregion Visibility CollapsedWhenConnected

    }
}
