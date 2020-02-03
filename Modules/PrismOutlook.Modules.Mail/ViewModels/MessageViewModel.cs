using System;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageViewModel : BindableBase, IDialogAware
    { 
        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }

        public string Title => "Mail Message";
        public event Action<IDialogResult> RequestClose;
    }
}