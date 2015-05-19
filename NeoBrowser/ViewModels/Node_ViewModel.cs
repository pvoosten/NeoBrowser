using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class Node_ViewModel : ViewModelBase
    {
        private Client.Node _node;
        private bool _deleted;

        public Node_ViewModel()
        {
            if (IsInDesignMode)
            {
                Labels = new List<string> { "Alpha", "Beta", "Gaga" };
            }
        }

        public Node_ViewModel(Client.Node node)
            : this()
        {
            _node = node;
            PropertiesAsJsonText = node.Properties.ToString(Formatting.Indented);
            DeleteCommand = new RelayCommand(Delete, DeleteEnabled);
            _deleted = false;
            Init();
        }

        private async void Init()
        {
            var labels = _node.GetLabels();
            var incomingRelationships = _node.GetRelationShips(Client.Direction.Incoming);
            var outgoingRelationships = _node.GetRelationShips(Client.Direction.Outgoing);
            IncomingRelationships = (await incomingRelationships).Select(r => new Relationship_ViewModel(r)).ToList();
            OutgoingRelationships = (await outgoingRelationships).Select(r => new Relationship_ViewModel(r)).ToList();
            Labels = await labels;
        }

        public string PropertiesAsJsonText { get; private set; }

        #region Delete command
        public ICommand DeleteCommand { get; private set; }

        private void Delete()
        {
            _node.Delete();
            _deleted = true;
        }

        private bool DeleteEnabled()
        {
            return !_deleted;
        }

        #endregion Delete command

        #region List<string> Labels

        private List<string> _labels;
        public List<string> Labels
        {
            get
            {
                return _labels;
            }
            set
            {
                if (_labels == value) return;
                _labels = value;
                RaisePropertyChanged("Labels");
            }
        }

        #endregion List<string> Labels

        #region List<Relationship_ViewModel> IncomingRelationships

        private List<Relationship_ViewModel> _incomingRelationships;
        public List<Relationship_ViewModel> IncomingRelationships
        {
            get
            {
                return _incomingRelationships;
            }
            set
            {
                if (_incomingRelationships == value) return;
                _incomingRelationships = value;
                RaisePropertyChanged("IncomingRelationships");
            }
        }

        #endregion List<Relationship_ViewModel> IncomingRelationships

        #region List<Relationship_ViewModel> OutgoingRelationships

        private List<Relationship_ViewModel> _outgoingRelationships;
        public List<Relationship_ViewModel> OutgoingRelationships
        {
            get
            {
                return _outgoingRelationships;
            }
            set
            {
                if (_outgoingRelationships == value) return;
                _outgoingRelationships = value;
                RaisePropertyChanged("OutgoingRelationships");
            }
        }

        #endregion List<Relationship_ViewModel> OutgoingRelationships
    }
}
