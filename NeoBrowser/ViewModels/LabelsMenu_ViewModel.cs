using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class LabelsMenu_ViewModel : ViewModelBase
    {

        private ICommand _loadNodeCommand;

        public LabelsMenu_ViewModel()
        {
            GraphBrowser.Instance.Connected += Instance_Connected;
            _loadNodeCommand = new RelayCommand<int>(LoadNode);
        }

        private void LoadNode(int nodeId)
        {
            GraphBrowser.Instance.LoadNodeWithId(nodeId);
        }

        private async void Instance_Connected(object sender, EventArgs e)
        {
            await GraphBrowser.Instance.GetAllLabelsAsync().ContinueWith(t =>
            {
                AllLabels = t.Result.Select(CreateMenuItemForLabel).ToList();
            });
        }

        private MenuItem CreateMenuItemForLabel(string label)
        {
            return new MenuItem
            {
                Header = label,
                ItemsSource = GraphBrowser.Instance.GetNodesWithLabel(label).Select(n=> new MenuItem{
                    Header=n,
                    Command=_loadNodeCommand,
                    CommandParameter = n
                })
            };
        }

        #region List<string> AllLabels

        private List<MenuItem> _allLabels;
        public List<MenuItem> AllLabels
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
