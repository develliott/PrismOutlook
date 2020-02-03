using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using PrismOutlook.Business;
using PrismOutlook.Core;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailGroupViewModel : ViewModelBase
    {
        private readonly IApplicationCommands _applicationCommands;
        private ObservableCollection<NavigationItem> _items;

        public ObservableCollection<NavigationItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private DelegateCommand<NavigationItem> _selectedCommand;

        public DelegateCommand<NavigationItem> SelectedCommand =>
            _selectedCommand ?? (_selectedCommand = new DelegateCommand<NavigationItem>(ExecuteSelectedCommand));

        public MailGroupViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            GenerateMenu();
        }

        void ExecuteSelectedCommand(NavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                _applicationCommands.NavigateCommand.Execute(navigationItem.NavigationPath);
            }
        }

        void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();

            var root = new NavigationItem
            {
                Caption = "Personal Folder", NavigationPath = $"MailList?{FolderParameters.FolderKey}=Default",
                IsExpanded = true
            };
            root.Items.Add(new NavigationItem
            {
                Caption = Properties.Resources.Folder_Inbox, NavigationPath = GetNavigationPath(FolderParameters.Inbox)
            });
            root.Items.Add(new NavigationItem
            {
                Caption = Properties.Resources.Folder_Sent, NavigationPath = GetNavigationPath(FolderParameters.Sent)
            });
            root.Items.Add(new NavigationItem
            {
                Caption = Properties.Resources.Folder_Deleted,
                NavigationPath = GetNavigationPath(FolderParameters.Deleted)
            });

            Items.Add(root);
        }

        string GetNavigationPath(string folder)
        {
            return $"MailList?{FolderParameters.FolderKey}={folder}";
        }
    }
}