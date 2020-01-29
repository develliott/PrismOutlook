using System.Collections.ObjectModel;
using Prism.Regions;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Services.Interfaces;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailListViewModel : ViewModelBase
    {
        private readonly IMailService _mailService;

        private ObservableCollection<MailMessage> _messages;

        private MailMessage _selectedMessage;

        public MailListViewModel(IMailService mailService)
        {
            _mailService = mailService;
        }

        public ObservableCollection<MailMessage> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        public MailMessage SelectedMessage
        {
            get => _selectedMessage;
            set => SetProperty(ref _selectedMessage, value);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var folder = navigationContext.Parameters.GetValue<string>(FolderParameters.FolderKey);

            switch (folder)
            {
                case FolderParameters.Inbox:
                {
                    Messages = new ObservableCollection<MailMessage>(_mailService.GetInboxItems());
                    break;
                }
                case FolderParameters.Sent:
                {
                    Messages = new ObservableCollection<MailMessage>(_mailService.GetSentItems());
                    break;
                }
                case FolderParameters.Deleted:
                {
                    Messages = new ObservableCollection<MailMessage>(_mailService.GetDeletedItems());
                    break;
                }
            }
        }
    }
}