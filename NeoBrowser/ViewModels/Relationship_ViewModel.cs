using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (IsInDesignMode)
            {
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

        private Node_ViewModel _startNode;
        public Node_ViewModel StartNode
        {
            get
            {
                if (_rel == null) return null;
                if (_startNode == null)
                {
                    SetStartNode();
                }
                return _startNode;
            }
        }

        private async void SetStartNode()
        {
            _startNode = new Node_ViewModel(await _rel.GetStartNode());
            RaisePropertyChanged("StartNode");
        }

        #endregion Node_ViewModel StartNode

        #region Node_ViewModel EndNode

        private Node_ViewModel _endNode;
        public Node_ViewModel EndNode
        {
            get
            {
                if (_rel == null) return null;
                if (_endNode == null)
                {
                    SetEndNode();
                }
                return _endNode;
            }
        }

        private async void SetEndNode()
        {
            _endNode = new Node_ViewModel(await _rel.GetEndNode());
            RaisePropertyChanged("EndNode");
        }

        #endregion Node_ViewModel EndNode
    }
}
