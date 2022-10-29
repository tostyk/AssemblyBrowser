using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AssemblyBrowser.WPF.ViewModel;

namespace AssemblyBrowser.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AssemblyBrowserViewModel viewModel = new AssemblyBrowserViewModel();
        }
    }
}
