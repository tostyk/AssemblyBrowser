using AssemblyBrowser.Core;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

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
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Core.AssemblyBrowser assemblyBrowser = new Core.AssemblyBrowser();
            AssemblyInformation assemblyInformation;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtEditor.Text = openFileDialog.FileName;
                assemblyInformation = assemblyBrowser.GetAssemblyInformation(txtEditor.Text);
                DrowTreeView(assemblyInformation, tvAssemblyInformation);
            }
        }
        private void DrowTreeView(AssemblyInformation ai, TreeView treeView)
        {
        }
    }
}
