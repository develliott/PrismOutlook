﻿using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismOutlook.Core;

namespace PrismOutlook.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand<string> _navigateCommand;

        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        public MainWindowViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;
            applicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        void ExecuteNavigateCommand(string navigationPath)
        {
            if (String.IsNullOrEmpty(navigationPath))
            {
                throw new ArgumentNullException();
            }

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);


        }
    }
}
