using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Prism.Ioc;
using Prism.Regions;

namespace PrismOutlook.Core.Regions
{
    // Notes: What is this class? 
    // Allows to attach behaviors or additional logic to a region. - in response to views being added/removed from a region

    // Region = Deck Of Cards
    // Every time you add a view to a view to a region, it adds a card to the stack of cards. As you activate a view, it gets moved to the top of of the deck. - ActiveViews
    // Views are not removed from regions, until explicitly removed.
    public class DependentViewRegionBehaviour : RegionBehavior
    {
        // Region Behaviors need a unique key
        public const string BehaviourKey = "DependentViewRegionBehaviour";
        private readonly IContainerExtension _container;

        private readonly Dictionary<object, List<DependentViewInfo>> _dependentViewCache =
            new Dictionary<object, List<DependentViewInfo>>();

        public DependentViewRegionBehaviour(IContainerExtension container)
        {
            _container = container;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViewsOnCollectionChanged;
        }

        private void ActiveViewsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var view in e.NewItems)
                {
                    var dependentViews = new List<DependentViewInfo>();


                    // If the view has been added once ->
                    if (_dependentViewCache.ContainsKey(view))
                    {
                        // -> reuse the view
                        dependentViews = _dependentViewCache[view];
                    }
                    else // Create the view
                    {
                        // Get the attributes
                        var atts = GetCustomAttributes<DependentViewAttribute>(view.GetType());

                        foreach (var att in atts)
                        {
                            var info = CreateDependentViewInfo(att);
                            dependentViews.Add(info);
                        }

                        _dependentViewCache.Add(view, dependentViews);
                    }

                    // Inject the views
                    dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Add(item.View));
                }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var oldView in e.OldItems)
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        var dependentViews = _dependentViewCache[oldView];
                        dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Remove(item.View));

                        // If permanently removed, removed from cache
                        if (!ShouldKeepAlive(oldView))
                            _dependentViewCache.Remove(oldView);
                    }
        }

        private bool ShouldKeepAlive(object oldView)
        {
            var regionLifetime = GetViewOrDataContextLifetime(oldView);
            if (regionLifetime != null) return regionLifetime.KeepAlive;

            return true;
        }

        private IRegionMemberLifetime GetViewOrDataContextLifetime(object view)
        {
            if (view is IRegionMemberLifetime regionLifetime)
                return regionLifetime;


            if (view is FrameworkElement frameworkElement)
                return frameworkElement.DataContext as IRegionMemberLifetime;

            return null;
        }

        private DependentViewInfo CreateDependentViewInfo(DependentViewAttribute attribute)
        {
            var info = new DependentViewInfo();
            info.Region = attribute.Region;

            // create the view instance
            info.View = _container.Resolve(attribute.Type);

            return info;
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }

    public class DependentViewInfo
    {
        public object View { get; set; }
        public string Region { get; set; }
    }
}