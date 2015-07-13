using GalaSoft.MvvmLight;
using NeoBrowser.Views;
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

        private bool _hitIncoming;

        public RelationView_ViewModel()
        {
            SelectedEndNode = new Node_ViewModel();
            SourceNode = new Node_ViewModel();
            SelectedRelationship = new Relationship_ViewModel("alpha");
        }

        private void RelationshipList_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = sender as RelationshipList_ViewModel;
            if (vm != null && e.PropertyName == "SelectedIndex")
            {
                _hitIncoming = vm == IncomingRelationshipList;
                var selected = _hitIncoming ? IncomingRelationshipList : OutgoingRelationshipList;
                if (selected.SelectedIndex == -1) return;
                var unselected = _hitIncoming ? OutgoingRelationshipList : IncomingRelationshipList;
                unselected.SelectedIndex = -1;
                SelectedRelationship = selected.Relationships[selected.SelectedIndex];
                SelectedEndNode = _hitIncoming ? SelectedRelationship.StartNode : SelectedRelationship.EndNode;
                if (SelectedEndNode == null)
                {
                    SelectedRelationship.PropertyChanged += SelectedRelationship_PropertyChanged;
                    SelectedEndNode = _hitIncoming ? SelectedRelationship.StartNode : SelectedRelationship.EndNode;
                }
            }
        }

        void SelectedRelationship_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_hitIncoming && e.PropertyName == "StartNode" || !_hitIncoming && e.PropertyName == "EndNode")
            {
                SelectedEndNode = _hitIncoming ? SelectedRelationship.StartNode : SelectedRelationship.EndNode;
            }
        }

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

            IncomingRelationshipList = new RelationshipList_ViewModel("Incoming", SourceNode == null ? null : SourceNode.IncomingRelationships);
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
                _incomingRelationshipList.PropertyChanged += RelationshipList_PropertyChanged;
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
                _outgoingRelationshipList.PropertyChanged += RelationshipList_PropertyChanged;
                RaisePropertyChanged("OutgoingRelationshipList");
            }
        }

        #endregion RelationshipList_ViewModel OutgoingRelationshipList
        #region Relationship_ViewModel SelectedRelationship

        private Relationship_ViewModel _selectedRelationship;
        public Relationship_ViewModel SelectedRelationship
        {
            get
            {
                return _selectedRelationship;
            }
            set
            {
                if (_selectedRelationship == value) return;
                _selectedRelationship = value;
                RaisePropertyChanged("SelectedRelationship");
            }
        }

        #endregion Relationship_ViewModel SelectedRelationship

    }
}
