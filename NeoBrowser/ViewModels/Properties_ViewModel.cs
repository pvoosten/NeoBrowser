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
        public Properties_ViewModel()
        {
            EditPropertyCommand = new RelayCommand<string>(EditProperty, EditPropertyEnabled);
            AddPropertyCommand = new RelayCommand(AddProperty, AddPropertyEnabled);
        }

        #region ExpandoObject Properties

        private ExpandoObject _properties;
        public ExpandoObject Properties
        {
            get
            {
                return _properties;
            }
            set
            {
                if (_properties == value) return;
                _properties = value;
                RaisePropertyChanged("Properties");
            }
        }

        #endregion ExpandoObject Properties


        #region EditProperty command
        public ICommand EditPropertyCommand { get; private set; }

        private void EditProperty(string param)
        {
            if (!EditPropertyEnabled(param)) return;
            var dct = Properties as IDictionary<string, object>;
            var vm = new EditValueWindow_ViewModel();
            vm.OldValue = ((JObject)dct[param]).ToString(Formatting.Indented);
            vm.NewValue = vm.OldValue;
            var window = new EditValueWindow { Title = "Edit value of \"" + param + "\"", DataContext = vm };
            window.ShowDialog();
            if (vm.IsConfirmed)
            {
                dct[param] = JObject.Parse(vm.NewValue);
            }
        }

        private bool EditPropertyEnabled(string param)
        {
            return param != null && Properties != null && (Properties as IDictionary<string,object>).ContainsKey(param);
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
