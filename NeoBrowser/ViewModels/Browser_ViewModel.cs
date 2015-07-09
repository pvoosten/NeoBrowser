using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NeoBrowser.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class Browser_ViewModel : ViewModelBase
    {

        private GraphDatabase _db;

        public Browser_ViewModel()
        {
            if (IsInDesignMode)
            {
                NodeId = 50;
                ActiveNode = new Node_ViewModel();
                ActiveNodeRelationships = new RelationView_ViewModel();
            }
            else
            {
                _db = new GraphDatabase("http://localhost:7474").Authenticate("neo4j", "longbow");
            }
            LoadNodeWithIdCommand = new RelayCommand(LoadNodeWithId, LoadNodeWithIdEnabled);
            IncrementNodeIdCommand = new RelayCommand(IncrementNodeId, IncrementNodeIdEnabled);
            DecrementNodeIdCommand = new RelayCommand(DecrementNodeId, DecrementNodeIdEnabled);
            ActivateNodeCommand = new RelayCommand<Node_ViewModel>(ActivateNode, ActivateNodeEnabled);
        }

        #region ulong NodeId

        private ulong _nodeId;
        public ulong NodeId
        {
            get
            {
                return _nodeId;
            }
            set
            {
                if (_nodeId == value) return;
                _nodeId = value;
                RaisePropertyChanged("NodeId");
            }
        }

        #endregion ulong NodeId


        #region IncrementNodeId command
        public ICommand IncrementNodeIdCommand { get; private set; }

        private void IncrementNodeId()
        {
            NodeId++;
        }

        private bool IncrementNodeIdEnabled()
        {
            return true;
        }

        #endregion IncrementNodeId command

        #region DecrementNodeId command
        public ICommand DecrementNodeIdCommand { get; private set; }

        private void DecrementNodeId()
        {
            NodeId--;
        }

        private bool DecrementNodeIdEnabled()
        {
            return NodeId > 1;
        }

        #endregion DecrementNodeId command


        #region ActivateNode command
        public ICommand ActivateNodeCommand { get; private set; }

        private void ActivateNode(Node_ViewModel node)
        {
            ActiveNode = node;
            ActiveNodeRelationships = new RelationView_ViewModel { SourceNode = node, SelectedEndNode = null };
        }

        private bool ActivateNodeEnabled(Node_ViewModel node)
        {
            return node != null;
        }

        #endregion ActivateNode command


        #region LoadNodeWithId command
        public ICommand LoadNodeWithIdCommand { get; private set; }

        private async void LoadNodeWithId()
        {
            int tries = 10;
            bool itFailed = true;
            while (itFailed && tries-- > 0)
            {
                itFailed = false;
                try
                {
                    var node = await _db.GetNodeWithId(NodeId);
                    var nodeVm = new Node_ViewModel(node);
                    ActivateNode(nodeVm);
                }
                catch (GraphDatabaseException)
                {
                    NodeId++;
                    itFailed = true;
                }
            }
        }

        private bool LoadNodeWithIdEnabled()
        {
            return true;
        }

        #endregion LoadNodeWithId command


        #region Node_ViewModel ActiveNode

        private Node_ViewModel _activeNode;
        public Node_ViewModel ActiveNode
        {
            get
            {
                return _activeNode;
            }
            set
            {
                if (_activeNode == value) return;
                _activeNode = value;
                RaisePropertyChanged("ActiveNode");
            }
        }

        #endregion Node_ViewModel ActiveNode

        #region RelationView_ViewModel ActiveNodeRelationships

        private RelationView_ViewModel _activeNodeRelationships;
        public RelationView_ViewModel ActiveNodeRelationships
        {
            get
            {
                return _activeNodeRelationships;
            }
            set
            {
                if (_activeNodeRelationships == value) return;
                _activeNodeRelationships = value;
                RaisePropertyChanged("ActiveNodeRelationships");
            }
        }

        #endregion RelationView_ViewModel ActiveNodeRelationships


    }
}
