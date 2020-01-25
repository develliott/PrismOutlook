using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailListViewModel : BindableBase, INavigationAware
    {
        private string _title = "Default";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MailListViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Title = navigationContext.Parameters.GetValue<string>("id");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
