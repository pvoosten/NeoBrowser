﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeoBrowser.ViewModels
{
    public class RelationshipList_ViewModel : ViewModelBase
    {
        public RelationshipList_ViewModel()
        {
            if (IsInDesignMode)
            {
                Title = "In/outgoing";
                Relationships = new List<Relationship_ViewModel>
                {
                    new Relationship_ViewModel("Alpha"),
                    new Relationship_ViewModel("Beta")
                };
                NewRelationshipType = "NewRelType";

            }
            SelectedIndex = -1;
        }

        public RelationshipList_ViewModel(string title, List<Relationship_ViewModel> relationships, Action addRelationship, Func<bool> addRelationshipEnabled)
            : this()
        {
            Title = title;
            Relationships = relationships;
            AddRelationshipCommand = new RelayCommand(addRelationship, addRelationshipEnabled);
        }

        #region string Title

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title == value) return;
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        #endregion string Title
        #region string NewRelationshipType

        private string _newRelationshipType;
        public string NewRelationshipType
        {
            get
            {
                return _newRelationshipType;
            }
            set
            {
                if (_newRelationshipType == value) return;
                _newRelationshipType = value;
                RaisePropertyChanged("NewRelationshipType");
            }
        }

        #endregion string NewRelationshipType
        #region List<Relationship_ViewModel> Relationships

        private List<Relationship_ViewModel> _relationships;

        public List<Relationship_ViewModel> Relationships
        {
            get
            {
                return _relationships;
            }
            set
            {
                if (_relationships == value) return;
                _relationships = value;
                RaisePropertyChanged("Relationships");
            }
        }

        #endregion List<Relationship_ViewModel> Relationships
        #region int SelectedIndex

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (_selectedIndex == value) return;
                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        #endregion int SelectedIndex
        #region int RelatedNodeId

        private ulong _relatedNodeId;
        public ulong RelatedNodeId
        {
            get
            {
                return _relatedNodeId;
            }
            set
            {
                if (_relatedNodeId == value) return;
                _relatedNodeId = value;
                RaisePropertyChanged("RelatedNodeId");
            }
        }

        #endregion int RelatedNodeId
        public ICommand AddRelationshipCommand { get; private set; }
    }
}
