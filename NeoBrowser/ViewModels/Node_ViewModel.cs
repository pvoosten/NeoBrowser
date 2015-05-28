using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
                dynamic d = new ExpandoObject();
                d.Alpha = "alpha property";
                d.Beta = "beta property";
                d.Gamma = "Gamma property";
                Properties = d;
            }
        }

        public Node_ViewModel(Client.Node node)
            : this()
        {
            _node = node;
            PropertiesJsonText = node.Properties.ToString(Formatting.Indented);
            var serializer = JsonSerializer.CreateDefault();
            serializer.Converters.Add(new ExpandoObjectConverter());
            Properties = node.Properties.ToObject<ExpandoObject>(serializer);
            DeleteCommand = new RelayCommand(Delete, DeleteEnabled);
            AddLabelCommand = new RelayCommand(AddLabel, AddLabelEnabled);
            RemoveLabelCommand = new RelayCommand<string>(RemoveLabel, RemoveLabelEnabled);
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
            Id = _node.Metadata.Id;
        }

        public string PropertiesJsonText { get; private set; }

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

        #region ulong Id

        private ulong _id;
        public ulong Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value) return;
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        #endregion ulong Id

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


        #region RemoveLabel command
        public ICommand RemoveLabelCommand { get; private set; }

        private void RemoveLabel(string param)
        {
            throw new NotImplementedException("RemoveLabel command not yet implemented");
        }

        private bool RemoveLabelEnabled(string param)
        {
            return true;
        }

        #endregion RemoveLabel command


        #region AddLabel command
        public ICommand AddLabelCommand { get; private set; }

        private void AddLabel()
        {
            throw new NotImplementedException("AddLabel command not yet implemented");
        }

        private bool AddLabelEnabled()
        {
            return !string.IsNullOrEmpty(AddLabelText) && (Labels==null || !Labels.Contains(AddLabelText));
        }

        #endregion AddLabel command


        #region string AddLabelText

        private string _addLabelText;
        public string AddLabelText
        {
            get
            {
                return _addLabelText;
            }
            set
            {
                if (_addLabelText == value) return;
                _addLabelText = value;
                RaisePropertyChanged("AddLabelText");
            }
        }

        #endregion string AddLabelText

    }
}
