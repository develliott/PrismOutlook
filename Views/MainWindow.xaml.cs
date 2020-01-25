using System.Windows;
using Infragistics.Themes;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Prism.Regions;
using PrismOutlook.Core;

namespace PrismOutlook.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : XamRibbonWindow
    {
        private readonly IApplicationCommands _applicationCommands;

        public MainWindow(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            InitializeComponent();

            ThemeManager.ApplicationTheme = new Office2013Theme();
        }

        private void XamOutlookBar_OnSelectedGroupChanged(object sender, RoutedEventArgs e)
        {
            var group = ((XamOutlookBar) sender).SelectedGroup as IOutlookBarGroup;
            if (group != null)
            {
                _applicationCommands.NavigateCommand.Execute(group.DefaultNavigationPath);
            }
        }
    }
}
