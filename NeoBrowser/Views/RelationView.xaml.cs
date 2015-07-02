
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


        private void lstRelations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRelations.SelectedItems.Count != 1)
            {
                SelectedEndNode = null;
                propertiesExpander.IsExpanded = false;
            }
            else
            {
                SelectedEndNode = new Node_ViewModel((Client.Node)lstRelations.SelectedValue);
                propertiesExpander.IsExpanded = true;
            }
        }

    }
}
