using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AssemblyBrowser.WPF.ViewModel
{
    public class TreeNode
    {
        public string Text { get; private set; }
        public ObservableCollection<TreeNode> Children { get; private set; }
        public TreeNode(string text)
        {
            Text = text;
            Children = new ObservableCollection<TreeNode>();
        }
        public TreeNode(string text, ObservableCollection<TreeNode> treeNodes)
        {
            Text = text;
            Children = treeNodes;
        }
    }
}
