using System.Collections.ObjectModel;
using Prism.Mvvm;
using PrismOutlook.Business;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailGroupViewModel : BindableBase
    {
        private ObservableCollection<NavigationItem> _items;

        public ObservableCollection<NavigationItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public MailGroupViewModel()
        {
            GenerateMenu();
        }

        void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();

            var root = new NavigationItem{Caption = "Personal Folder", NavigationPath = "MailList"};
            root.Items.Add(new NavigationItem {Caption = "Inbox", NavigationPath = ""});
            root.Items.Add(new NavigationItem {Caption = "Deleted", NavigationPath = ""});
            root.Items.Add(new NavigationItem {Caption = "Sent", NavigationPath = ""});

            Items.Add(root);
        }
    }
}