using AssemblyBrowser.WPF.ViewModel;
using System.Windows;

namespace AssemblyBrowser.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AssemblyBrowserViewModel();
        }
    }
}
