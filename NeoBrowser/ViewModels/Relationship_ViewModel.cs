using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.ViewModels
{
    public class Relationship_ViewModel : ViewModelBase
    {
        private Client.Relationship _rel;

        public Relationship_ViewModel(Client.Relationship rel)
        {
            _rel = rel;
            PropertiesJsonText = _rel.Properties.ToString(Formatting.Indented);
        }

        public string Type
        {
            get
            {
                return _rel.Type;
            }
        }

        public string PropertiesJsonText { get; private set; }

        #region Node_ViewModel StartNode

        private bool _gettingStartNode = false;
        private Node_ViewModel _startNode;
        public Node_ViewModel StartNode
        {
            get
            {
                if (_startNode == null && !_gettingStartNode)
                {
                    _gettingStartNode = true;
                    _rel.GetStartNode().ContinueWith(t => StartNode = new Node_ViewModel(t.Result));
                }
                return _startNode;
            }
            set
            {
                _gettingStartNode = false;
                if (_startNode == value) return;
                _startNode = value;
                RaisePropertyChanged("StartNode");
            }
        }

        #endregion Node_ViewModel StartNode

        #region Node_ViewModel EndNode

        private bool _gettingEndNode = false;
        private Node_ViewModel _endNode;
        public Node_ViewModel EndNode
        {
            get
            {
                if (_endNode == null && !_gettingEndNode)
                {
                    _gettingEndNode = true;
                    _rel.GetEndNode().ContinueWith(t => EndNode = new Node_ViewModel(t.Result));
                }
                return _endNode;
            }
            set
            {
                _gettingEndNode = false;
                if (_endNode == value) return;
                _endNode = value;
                RaisePropertyChanged("EndNode");
            }
        }

        #endregion Node_ViewModel EndNode
    }
}
