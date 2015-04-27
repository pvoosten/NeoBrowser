using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeoBrowser.ViewModels
{
    public class LabelsMenu_ViewModel : ViewModelBase
    {
        public LabelsMenu_ViewModel()
        {
            GraphBrowser.Instance.Connected += Instance_Connected;
        }

        private async void Instance_Connected(object sender, EventArgs e)
        {
            await GraphBrowser.Instance.GetAllLabelsAsync().ContinueWith(t =>
            {
                AllLabels = t.Result.ToList();
            });
        }

        #region List<string> AllLabels

        private List<string> _allLabels;
        public List<string> AllLabels
        {
            get
            {
                return _allLabels;
            }
            set
            {
                if (_allLabels == value) return;
                _allLabels = value;
                RaisePropertyChanged("AllLabels");
            }
        }

        #endregion List<string> AllLabels


    }
}
