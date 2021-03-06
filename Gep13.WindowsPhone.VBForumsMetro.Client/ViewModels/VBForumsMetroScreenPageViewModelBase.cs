﻿//-----------------------------------------------------------------------
// <copyright file="VBForumsMetroScreenPageViewModelBase.cs" company="GEP13">
//      Copyright (c) GEP13, 2012. All rights reserved.
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
//      files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, 
//      modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
//      is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
//      OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
//      LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
//      CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

namespace Gep13.WindowsPhone.VBForumsMetro.Client.ViewModels
{
    using System;
    using Caliburn.Micro;
    using Gep13.WindowsPhone.Core.ViewModels;
    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;

    /// <summary>
    /// The base class specifically for the VBForumsMetro application for all screens of the application
    /// </summary>
    public class VBForumsMetroScreenPageViewModelBase : ScreenPageViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the VBForumsMetroScreenPageViewModelBase class
        /// </summary>
        /// <param name="viewModelWorker">Incoming ViewModelWorker provided by Caliburn.Micro</param>
        public VBForumsMetroScreenPageViewModelBase(VBForumsMetroViewModelWorker viewModelWorker)
            : base(viewModelWorker)
        {
        }

        /// <summary>
        /// Helper Method to navigate to the WelcomePage
        /// </summary>
        public void GoToWelcomePage()
        {
            this.VMWorker.NavigationService.UriFor<WelcomeViewModel>()
                .WithParam(vm => vm.BackNavSkipOne, true)
                .Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the Add Account Page
        /// </summary>
        public void GoToAddAccountPage()
        {
            this.VMWorker.NavigationService.UriFor<AddAccountViewModel>()
                .WithParam(x => x.IsEditMode, false)
                .WithParam(vm => vm.BackNavSkipOne, true)
                .Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the Main Page
        /// </summary>
        public void GoToMainPage()
        {
            this.VMWorker.NavigationService.UriFor<MainPageViewModel>()
                .Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the ProfilePage
        /// </summary>
        public void GoToProfilePage()
        {
            this.VMWorker.NavigationService.UriFor<ProfileViewModel>()
                .Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the ReputationListPage
        /// </summary>
        public void GoToReputationListPage()
        {
            this.VMWorker.NavigationService.UriFor<ReputationListViewModel>()
                .Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the Edit Account Page
        /// </summary>
        public void GoToEditAccountPage()
        {
            this.VMWorker.NavigationService.UriFor<AddAccountViewModel>()
                .WithParam(x => x.IsEditMode, true)
                .Navigate();
        }

        /// <summary>
        /// Helper Method to navigatr to the Settings Page
        /// </summary>
        public void GoToSettingsPage()
        {
            this.VMWorker.NavigationService.UriFor<SettingsViewModel>()
                .Navigate();
        }

        /// <summary>
        /// Helper method to navigate to the About Page
        /// </summary>
        public void GoToAboutPage()
        {
            this.VMWorker.NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Helper method to navigate to the Help Page
        /// </summary>
        public void GoToHelpPage()
        {
            VMWorker.NavigationService.UriFor<HelpViewModel>()
                .Navigate();
        }
    }
}
