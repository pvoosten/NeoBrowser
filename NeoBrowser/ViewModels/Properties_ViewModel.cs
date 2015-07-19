using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NeoBrowser.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class Properties_ViewModel : ViewModelBase
    {
        private readonly Client.PropertiesContainer _container;

        public Properties_ViewModel(Client.PropertiesContainer container)
        {
            _container = container;
            EditPropertyCommand = new RelayCommand<string>(EditProperty, EditPropertyEnabled);
            AddPropertyCommand = new RelayCommand(AddProperty, AddPropertyEnabled);
        }

        public JObject Properties
        {
            get
            {
                return _container.Properties;
            }
        }

        #region EditProperty command
        public ICommand EditPropertyCommand { get; private set; }

        private void EditProperty(string param)
        {
            if (!EditPropertyEnabled(param)) return;
            var vm = new EditValueWindow_ViewModel();
            vm.OldValue = Properties.Property(param).Value.ToString(Formatting.Indented);
            vm.NewValue = vm.OldValue;
            var window = new EditValueWindow { Title = "Edit value of \"" + param + "\"", DataContext = vm };
            window.ShowDialog();
            if (vm.IsConfirmed)
            {
                SetPropertyValue(param, vm);
            }
        }

        private async void SetPropertyValue(string param, EditValueWindow_ViewModel vm)
        {
            var v = JToken.Parse(vm.NewValue);
            Properties.Property(param).Value = v;
            await _container.SetProperty(param, v);
        }

        private bool EditPropertyEnabled(string param)
        {
            return true;
        }

        #endregion EditProperty command
        #region string AddPropertyText

        private string _addPropertyText;
        public string AddPropertyText
        {
            get
            {
                return _addPropertyText;
            }
            set
            {
                if (_addPropertyText == value) return;
                _addPropertyText = value;
                RaisePropertyChanged("AddPropertyText");
            }
        }

        #endregion string AddPropertyText

        #region AddProperty command
        public ICommand AddPropertyCommand { get; private set; }

        private void AddProperty()
        {
            throw new NotImplementedException("AddProperty command not yet implemented");
        }

        private bool AddPropertyEnabled()
        {
            return true;
        }

        #endregion AddProperty command


    }
}
