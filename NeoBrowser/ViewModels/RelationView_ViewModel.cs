using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.ViewModels
{
    public class RelationView_ViewModel : ViewModelBase
    {

        private const string _IncomingRelationshipsProperty = "IncomingRelationships";
        private const string _OutgoingRelationshipsProperty = "OutgoingRelationships";

        #region Node_ViewModel SelectedEndNode

        private Node_ViewModel _selectedEndNode;
        public Node_ViewModel SelectedEndNode
        {
            get
            {
                return _selectedEndNode;
            }
            set
            {
                if (_selectedEndNode == value) return;
                _selectedEndNode = value;
                RaisePropertyChanged("SelectedEndNode");
            }
        }

        #endregion Node_ViewModel SelectedEndNode

        #region Node_ViewModel SourceNode

        private Node_ViewModel _sourceNode;
        public Node_ViewModel SourceNode
        {
            get
            {
                return _sourceNode;
            }
            set
            {
                if (_sourceNode == value) return;
                _sourceNode = value;
                _sourceNode.PropertyChanged += _sourceNode_PropertyChanged;
                SourceNodeUpdated();
                RaisePropertyChanged("SourceNode");
            }
        }

        void _sourceNode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SourceNodeUpdated();
        }

        private void SourceNodeUpdated()
        {

            IncomingRelationshipList = new RelationshipList_ViewModel("Incoming", SourceNode==null?null:SourceNode.IncomingRelationships);
            OutgoingRelationshipList = new RelationshipList_ViewModel("Outgoing", SourceNode == null ? null : SourceNode.OutgoingRelationships);

        }

        #endregion Node_ViewModel SourceNode

        #region RelationshipList_ViewModel IncomingRelationshipList

        private RelationshipList_ViewModel _incomingRelationshipList;
        public RelationshipList_ViewModel IncomingRelationshipList
        {
            get
            {
                return _incomingRelationshipList;
            }
            set
            {
                if (_incomingRelationshipList == value) return;
                _incomingRelationshipList = value;
                RaisePropertyChanged("IncomingRelationshipList");
            }
        }

        #endregion RelationshipList_ViewModel IncomingRelationshipList

        #region RelationshipList_ViewModel OutgoingRelationshipList

        private RelationshipList_ViewModel _outgoingRelationshipList;
        public RelationshipList_ViewModel OutgoingRelationshipList
        {
            get
            {
                return _outgoingRelationshipList;
            }
            set
            {
                if (_outgoingRelationshipList == value) return;
                _outgoingRelationshipList = value;
                RaisePropertyChanged("OutgoingRelationshipList");
            }
        }

        #endregion RelationshipList_ViewModel OutgoingRelationshipList

    }
}
