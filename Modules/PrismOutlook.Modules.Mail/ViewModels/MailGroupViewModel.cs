using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using PrismOutlook.Business;
using PrismOutlook.Core;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailGroupViewModel : BindableBase
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

            var root = new NavigationItem{Caption = "Personal Folder", NavigationPath = "MailList?id=Default"};
            root.Items.Add(new NavigationItem {Caption = "Inbox", NavigationPath = "MailList?id=Inbox" });
            root.Items.Add(new NavigationItem {Caption = "Deleted", NavigationPath = "MailList?id=Deleted" });
            root.Items.Add(new NavigationItem {Caption = "Sent", NavigationPath = "MailList?id=Sent" });

            Items.Add(root);
        }
    }
}