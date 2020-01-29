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
using Infragistics.Controls.Menus;
using Infragistics.Windows.OutlookBar;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.ViewModels;

namespace PrismOutlook.Modules.Mail.Menus
{
    /// <summary>
    /// Interaction logic for MailGroup.xaml
    /// </summary>
    public partial class MailGroup : OutlookBarGroup, IOutlookBarGroup
    {

        public MailGroup()
        {
            InitializeComponent();

            _dataTree.Loaded += DataTreeOnLoaded;
        }

        private void DataTreeOnLoaded(object sender, RoutedEventArgs e)
        {
            var parentNode = _dataTree.Nodes[0];
            var nodeToSelect = parentNode.Nodes[0];
            nodeToSelect.IsSelected = true;
        }

        public string DefaultNavigationPath
        {
            get
            {
                // Check if not null and type cast to NavigationItem
                if (_dataTree.ActiveDataItem is NavigationItem item)
                {
                    return item.NavigationPath;
                }

                return $"MailList?{FolderParameters.FolderKey}={FolderParameters.Inbox}";
            }
        }
    }
}
