using AssemblyBrowser.Core;
using AssemblyBrowser.Core.AssemblyClasses;
using AssemblyBrowser.WPF.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AssemblyBrowser.WPF.ViewModel
{
    internal class AssemblyBrowserViewModel : INotifyPropertyChanged
    {
        public RelayCommand StartScanningCommand { get; }
        public RelayCommand SetFilePathCommand { get; }

        private Core.AssemblyBrowser _assemblyBrowser;

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        private TreeNode _assemblyTree;
        public TreeNode AssemblyTree
        {
            get { return _assemblyTree; }
            set
            {
                _assemblyTree = value;
                OnPropertyChanged("AssemblyTree");
            }
        }

        public AssemblyBrowserViewModel()
        {
            _assemblyBrowser = new Core.AssemblyBrowser();
            _filePath = "";

            SetFilePathCommand = new RelayCommand(obj =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".dll";
                openFileDialog.Filter = "Assembly (.dll)|*.dll";
                bool? isOK = openFileDialog.ShowDialog();
                if (isOK != null && (bool)isOK)
                    FilePath = openFileDialog.FileName;
            }, obj => true);

            StartScanningCommand = new RelayCommand(obj =>
            {
                AssemblyInformation assemblyInformation = _assemblyBrowser.GetAssemblyInformation(FilePath, AssemblyBrowserFlags.OnlyDeclaredMembers);
                if (assemblyInformation.Exception == null)
                {
                    AssemblyTree = AssemblyInformationConverter.ToTree(assemblyInformation);
                }
                else
                {
                    MessageBox.Show(assemblyInformation.Exception?.Message, "Error");
                }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
