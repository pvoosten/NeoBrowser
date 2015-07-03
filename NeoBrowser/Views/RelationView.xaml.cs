
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
            DependencyProperty.Register("SourceNode", typeof(Node_ViewModel), typeof(RelationView), new PropertyMetadata(null));

        #endregion

        #region Relationships dependency property
        public IEnumerable<Relationship_ViewModel> Relationships
        {
            get { return (IEnumerable<Relationship_ViewModel>)GetValue(RelationshipsProperty); }
            set { SetValue(RelationshipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Relationships.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RelationshipsProperty =
            DependencyProperty.Register("Relationships", typeof(IEnumerable<Relationship_ViewModel>), typeof(RelationView), new PropertyMetadata(Enumerable.Empty<Relationship_ViewModel>()));
        #endregion


        private void lstRelations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox rels;
            bool incoming;
            if (sender == lstIncoming && lstIncoming.SelectedIndex != -1)
            {
                incoming = true;
                rels = lstIncoming;
                lstOutgoing.SelectedIndex = -1;
            }
            else if (sender == lstOutgoing && lstOutgoing.SelectedIndex != -1)
            {
                incoming = false;
                rels = lstOutgoing;
                lstIncoming.SelectedIndex = -1;
            }
            else
            {
                return;
            }
            if (rels.SelectedItems.Count == 1)
            {
                var rel = rels.SelectedValue as Relationship_ViewModel;
                SelectedEndNode = incoming ? rel.StartNode : rel.EndNode;
                propertiesExpander.DataContext = rels.SelectedValue;
                propertiesExpander.IsExpanded = true;
            }
            else
            {
                SelectedEndNode = null;
                propertiesExpander.IsExpanded = false;
            }
        }

    }
}
