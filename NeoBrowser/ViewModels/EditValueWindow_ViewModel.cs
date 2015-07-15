using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class EditValueWindow_ViewModel : ViewModelBase
    {
        public EditValueWindow_ViewModel()
        {
            ResetCommand = new RelayCommand(Reset, ResetEnabled);
            ConfirmCommand = new RelayCommand(Confirm, ConfirmEnabled);
            IsConfirmed = false;
        }

        #region string OldValue

        private string _oldValue;
        public string OldValue
        {
            get
            {
                return _oldValue;
            }
            set
            {
                if (_oldValue == value) return;
                _oldValue = value;
                RaisePropertyChanged("OldValue");
            }
        }

        #endregion string OldValue
        #region string NewValue

        private string _newValue;
        public string NewValue
        {
            get
            {
                return _newValue;
            }
            set
            {
                if (_newValue == value) return;
                _newValue = value;
                RaisePropertyChanged("NewValue");
            }
        }

        #endregion string NewValue

        #region Reset command
        public ICommand ResetCommand { get; private set; }

        private void Reset()
        {
            NewValue = OldValue;
        }

        private bool ResetEnabled()
        {
            return true;
        }

        #endregion Reset command

        #region Confirm command
        public ICommand ConfirmCommand { get; private set; }

        private void Confirm()
        {
            IsConfirmed = true;
        }

        private bool ConfirmEnabled()
        {
            try
            {
                JObject.Parse(NewValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Confirm command

        public bool IsConfirmed { get; set; }

    }
}
