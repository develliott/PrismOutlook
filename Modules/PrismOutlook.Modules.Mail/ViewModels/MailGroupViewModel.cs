using Prism.Mvvm;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailGroupViewModel : BindableBase
    {
        public string Message { get; set; } = "Info from Mail Group view model";
    }
}