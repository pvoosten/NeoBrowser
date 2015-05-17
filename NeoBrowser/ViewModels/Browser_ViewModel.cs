using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    class Browser_ViewModel : ViewModelBase
    {

        public Browser_ViewModel()
        {
#error load graph db

            LoadNodeWithIdCommand = new RelayCommand<long>(LoadNodeWithId, LoadNodeWithIdEnabled);
        }

        #region LoadNodeWithId command
        public ICommand LoadNodeWithIdCommand { get; private set; }

        private void LoadNodeWithId(long param)
        {
            throw new NotImplementedException("LoadNodeWithId command not yet implemented");
        }

        private bool LoadNodeWithIdEnabled(long param)
        {
            throw new NotImplementedException("LoadNodeWithId command not yet implemented");
        }

        #endregion LoadNodeWithId command

    }
}
