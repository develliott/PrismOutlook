using System;
using System.Collections.Generic;
using System.Text;
using PrismOutlook.Business;

namespace PrismOutlook.Services.Interfaces
{
    public interface IMailService
    {
        IList<MailMessage> GetInboxItems();
        IList<MailMessage> GetSentItems();
        IList<MailMessage> GetDeletedItems();
    }
}
