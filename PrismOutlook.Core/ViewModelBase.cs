using System;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismOutlook.Core
{
    public class ViewModelBase : BindableBase, IConfirmNavigationRequest
    {
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // Reuse view instance by default
            return true;
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }
    }
}