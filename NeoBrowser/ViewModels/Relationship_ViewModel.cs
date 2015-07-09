using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace NeoBrowser.ViewModels
{
    public class Relationship_ViewModel : ViewModelBase
    {
        private Client.Relationship _rel;
        private readonly string _type;

        public Relationship_ViewModel(Client.Relationship rel)
        {
            _rel = rel;
            _type = _rel.Type;
            PropertiesJsonText = _rel.Properties.ToString(Formatting.Indented);
            var serializer = JsonSerializer.CreateDefault();
            serializer.Converters.Add(new ExpandoObjectConverter());
            Properties = rel.Properties.ToObject<ExpandoObject>(serializer);
        }

        public Relationship_ViewModel(string type)
        {
            if(IsInDesignMode){
                _type = type;
            }
        }

        #region string Type
        
        public string Type
        {
            get
            {
                return _type;
            }
        }

        #endregion string Type

        public string PropertiesJsonText { get; private set; }

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
