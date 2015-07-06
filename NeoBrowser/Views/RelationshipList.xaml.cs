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
    /// Interaction logic for RelationshipList.xaml
    /// </summary>
    public partial class RelationshipList : UserControl
    {
        public RelationshipList()
        {
            InitializeComponent();
            DataContext = this;
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(RelationshipList), new PropertyMetadata("Relationships"));



        public string NewRelationshipType
        {
            get { return (string)GetValue(NewRelationshipTypeProperty); }
            set { SetValue(NewRelationshipTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewRelationshipType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewRelationshipTypeProperty =
            DependencyProperty.Register("NewRelationshipType", typeof(string), typeof(RelationshipList), new PropertyMetadata(null));



        public IEnumerable<Relationship_ViewModel> Relationships
        {
            get { return (IEnumerable<Relationship_ViewModel>)GetValue(RelationshipsProperty); }
            set { SetValue(RelationshipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Relationships.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RelationshipsProperty =
            DependencyProperty.Register("Relationships", typeof(IEnumerable<Relationship_ViewModel>), typeof(RelationshipList), new PropertyMetadata(Enumerable.Empty<Relationship_ViewModel>()));

        internal void Deselect()
        {
            lstIncoming.SelectedIndex = -1;
        }
    }
}
