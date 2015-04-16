using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class MainWindow_ViewModel: ViewModelBase
    {
        private string _neoUrl;
        private string _neoUser;
        private string _neoPassword;

        public MainWindow_ViewModel()
        {
            NeoUrl = "http://localhost:7474";
            NeoUser = "Neo4j";
            NeoPassword = "Neo4j";
            ConnectCommand = new RelayCommand(Connect);
        }

        public ICommand ConnectCommand{get; private set;}

        private void Connect()
        {
            dynamic oldBrowser = App.Current.TryFindResource("browser");
            if (oldBrowser != null)
            {
                oldBrowser.Close();
            }
            App.Current.Resources["browser"] = new GraphBrowser(NeoUrl, NeoUser, NeoPassword);
        }

        public string NeoUrl
        {
            get
            {
                return _neoUrl;
            }
            set
            {
                if (_neoUrl == value) return;
                _neoUrl = value;
                RaisePropertyChanged("NeoUrl");
            }
        }
        public string NeoUser
        {
            get
            {
                return _neoUser;
            }
            set
            {
                if (_neoUser == value) return;
                _neoUser = value;
                RaisePropertyChanged("NeoUser");
            }
        }
        public string NeoPassword
        {
            get
            {
                return _neoPassword;
            }
            set
            {
                if (_neoPassword == value) return;
                _neoPassword = value;
                RaisePropertyChanged("NeoPassword");
            }
        }



    }
}
