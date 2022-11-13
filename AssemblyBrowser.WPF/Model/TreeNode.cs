using System.Collections.ObjectModel;
using System.IO;

namespace AssemblyBrowser.WPF.ViewModel
{
    public class TreeNode
    {
        public string Text { get; private set; }
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            private set
            {
                _imagePath = Path.GetFullPath(value);
            }
        }
        public ObservableCollection<TreeNode> Children { get; private set; }
        public TreeNode(string text, string imagePath)
        {
            Text = text;
            Children = new ObservableCollection<TreeNode>();
            ImagePath = imagePath;
        }
        public TreeNode(string text, string imagePath, ObservableCollection<TreeNode> treeNodes)
        {
            Text = text;
            Children = treeNodes;
            ImagePath = imagePath;
        }
    }
}
