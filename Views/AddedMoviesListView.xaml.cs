using MovieApp.ViewModels.Base;
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

namespace MovieApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddedMoviesListView.xaml
    /// </summary>
    public partial class AddedMoviesListView : UserControl
    {
        public AddedMoviesListView()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty DoubleClickCommandProperty =
        DependencyProperty.Register(
             nameof(DoubleClickCommand),
             typeof(RelayCommand),
             typeof(AddedMoviesListView)
          );

        public RelayCommand DoubleClickCommand
        {

            get { return (RelayCommand)GetValue(DoubleClickCommandProperty); }
            set { SetValue(DoubleClickCommandProperty, value); }
        }
    }
}
