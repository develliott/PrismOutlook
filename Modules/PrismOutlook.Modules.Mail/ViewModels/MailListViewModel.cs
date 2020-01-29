using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Services.Interfaces;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailListViewModel : ViewModelBase
    {
        private readonly IMailService _mailService;
        private string _title = "Default";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ObservableCollection<MailMessage> _messages;

        public ObservableCollection<MailMessage> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        private MailMessage _selectedMessage;

        public MailMessage SelectedMessage
        {
            get => _selectedMessage;
            set => SetProperty(ref _selectedMessage, value);
        }

        public MailListViewModel(IMailService mailService)
        {
            _mailService = mailService;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Title = navigationContext.Parameters.GetValue<string>("id");

            Messages = new ObservableCollection<MailMessage>(_mailService.GetInboxItems());
        }
    }
}
