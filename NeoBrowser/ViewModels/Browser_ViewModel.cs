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
            _db = new GraphDatabase("http://localhost:7474").Authenticate("neo4j", "longbow");
            LoadNodeWithIdCommand = new RelayCommand(LoadNodeWithId, LoadNodeWithIdEnabled);
            IncrementNodeIdCommand = new RelayCommand(IncrementNodeId, IncrementNodeIdEnabled);
            DecrementNodeIdCommand = new RelayCommand(DecrementNodeId, DecrementNodeIdEnabled);
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

        #region LoadNodeWithId command
        public ICommand LoadNodeWithIdCommand { get; private set; }

        private async void LoadNodeWithId()
        {
            int tries = 10;
            bool itFailed = true;
            while (itFailed && tries --> 0)
            {
                itFailed = false;
                try
                {
                    var node = await _db.GetNodeWithId(NodeId);
                    ActiveNode = new Node_ViewModel(node);
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



    }
}
