using NeoBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeoBrowser.Views
{
    /// <summary>
    /// Interaction logic for RelationView.xaml
    /// </summary>
    public partial class RelationView : UserControl
    {
        public RelationView()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region SelectedEndNode dependency property
        public Node_ViewModel SelectedEndNode
        {
            get { return (Node_ViewModel)GetValue(SelectedEndNodeProperty); }
            set { SetValue(SelectedEndNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEndNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEndNodeProperty =
            DependencyProperty.Register("SelectedEndNode", typeof(Node_ViewModel), typeof(RelationView), new PropertyMetadata(null));

        #endregion

        #region SourceNode dependencyproperty

        public Node_ViewModel SourceNode
        {
            get { return (Node_ViewModel)GetValue(SourceNodeProperty); }
            set { SetValue(SourceNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceNodeProperty =
            DependencyProperty.Register("SourceNode", typeof(Node_ViewModel), typeof(RelationView), new PropertyMetadata(null, OnSourceNodeChanged));


        private static void OnSourceNodeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var node = e.NewValue as Node_ViewModel;
            var rv = sender as RelationView;
            sender.SetValue(IncomingRelationshipsProperty, node.IncomingRelationships);
            sender.SetValue(OutgoingRelationshipsProperty, node.OutgoingRelationships);
            node.PropertyChanged += rv.node_PropertyChanged;
        }

        private void node_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var node = sender as Node_ViewModel;
            node.PropertyChanged -= node_PropertyChanged;
            SourceNode = node;
        }

        #endregion

        public IEnumerable<Relationship_ViewModel> IncomingRelationships
        {
            get { return (IEnumerable<Relationship_ViewModel>)GetValue(IncomingRelationshipsProperty); }
            set { SetValue(IncomingRelationshipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncomingRelationships.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncomingRelationshipsProperty =
            DependencyProperty.Register("IncomingRelationships", typeof(IEnumerable<Relationship_ViewModel>), typeof(RelationView), new PropertyMetadata(Enumerable.Empty<Relationship_ViewModel>()));


        public IEnumerable<Relationship_ViewModel> OutgoingRelationships
        {
            get { return (IEnumerable<Relationship_ViewModel>)GetValue(OutgoingRelationshipsProperty); }
            set { SetValue(OutgoingRelationshipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutgoingRelationships.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutgoingRelationshipsProperty =
            DependencyProperty.Register("OutgoingRelationships", typeof(IEnumerable<Relationship_ViewModel>), typeof(RelationView), new PropertyMetadata(Enumerable.Empty<Relationship_ViewModel>()));

        


    }
}
