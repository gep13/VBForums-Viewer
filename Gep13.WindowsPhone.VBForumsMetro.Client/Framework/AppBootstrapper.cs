﻿//-----------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Client.Framework 
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Caliburn.Micro;

    using Gep13.WindowsPhone.Core.Database;
    using Gep13.WindowsPhone.Core.Diagnostics;
    using Gep13.WindowsPhone.Core.Navigation;
    using Gep13.WindowsPhone.Core.Progress;
    using Gep13.WindowsPhone.Core.Rating;
    using Gep13.WindowsPhone.Core.Storage;
    using Gep13.WindowsPhone.VBForumsMetro.Client.ViewModels;
    using Gep13.WindowsPhone.VBForumsMetro.Core.Database;
    using Gep13.WindowsPhone.VBForumsMetro.Core.Web;
    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;

    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using Telerik.Windows.Controls;

    /// <summary>
    /// This is the main class which is used by Caliburn.Micro
    /// </summary>
    public class AppBootstrapper : PhoneBootstrapper
    {
        /// <summary>
        /// Top level container object for registering new components
        /// </summary>
        private PhoneContainer container;

        /// <summary>
        /// The Caliburn.Micro method to assign the deafult set of conventions
        /// </summary>
        protected static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        protected override void Configure()
        {
            this.container = new PhoneContainer(RootFrame);
            this.container.RegisterPhoneServices();

            this.container.Singleton<InitialViewModel>();
            this.container.Singleton<WelcomeViewModel>();
            this.container.Singleton<AddAccountViewModel>();
            this.container.Singleton<MainPageViewModel>();
            this.container.Singleton<ProfileViewModel>();
            this.container.Singleton<ReputationListViewModel>();

            this.container.Instance<IProgressService>(new ProgressService(RootFrame));
            this.container.Instance<IStorageService>(new StorageService());
            this.container.Instance<INavigationHelperService>(new NavigationHelperService());
            this.container.Instance<IRatingService>(new RatingService("VBForumsMetro"));
            this.container.Instance<IDiagnosticsService>(new DiagnosticsService("VBForumsMetro.txt", "VBForumsMetro", "support@vbforumsmetro.co.uk"));
            this.container.Instance<ISterlingService>(new VBForumsMetroSterlingService());
            this.container.Instance<IVBForumsWebService>(new VBForumsWebService());
            this.container.Singleton<VBForumsMetroViewModelWorker>();

            var phoneService = this.container.GetInstance(typeof(IPhoneService), null) as IPhoneService;
            if (phoneService != null)
            {
                phoneService.Resurrecting += new System.Action(PhoneServiceResurrecting);
                phoneService.Continuing += new System.Action(PhoneServiceContinuing);
            }

            RootFrame.Navigated += new NavigatedEventHandler(this.RootFrameNavigated);

            AddCustomConventions();
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="key">The string</param>
        /// <returns>The instance</returns>
        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        /// <param name="service">The service</param>
        /// <returns>All instances</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        /// <summary>
        /// Default method created by the Caliburn.Micro Framework
        /// </summary>
        /// <param name="instance">The instance that needs to be built up.</param>
        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }

        /// <summary>
        /// OnLaunch event fires when the application first starts up
        /// </summary>
        /// <param name="sender">Responsible party</param>
        /// <param name="e">The arguments coming in with the Event</param>
        protected override void OnLaunch(object sender, LaunchingEventArgs e)
        {
            base.OnLaunch(sender, e);

            var sterlingService = this.GetInstance(typeof(ISterlingService), null) as ISterlingService;
            if (sterlingService != null)
            {
                sterlingService.Activate();
            }

            ApplicationUsageHelper.Init("1.0");

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            // Added an imposed delay to allow the Splash Screen to show properly
            System.Threading.Thread.Sleep(750);
        }

        /// <summary>
        /// Method that fires when the application is activating
        /// </summary>
        /// <param name="sender">Responsible party</param>
        /// <param name="e">The arguments coming in with the Event</param>
        protected override void OnActivate(object sender, ActivatedEventArgs e)
        {
            base.OnActivate(sender, e);

            var sterlingService = this.GetInstance(typeof(ISterlingService), null) as ISterlingService;
            if (sterlingService != null)
            {
                sterlingService.Activate();
            }
        }

        /// <summary>
        /// Method that fires when the application is Deactivating
        /// </summary>
        /// <param name="sender">Responsible party</param>
        /// <param name="e">The arguments coming in with the Event</param>
        protected override void OnDeactivate(object sender, DeactivatedEventArgs e)
        {
            base.OnDeactivate(sender, e);

            var sterlingService = this.GetInstance(typeof(ISterlingService), null) as ISterlingService;
            if (sterlingService != null)
            {
                sterlingService.Deactivate();
            }
        }

        /// <summary>
        /// Method that fires when the application is Closing
        /// </summary>
        /// <param name="sender">Responsible party</param>
        /// <param name="e">The arguments coming in with the Event</param>
        protected override void OnClose(object sender, ClosingEventArgs e)
        {
            base.OnClose(sender, e);

            var sterlingService = this.GetInstance(typeof(ISterlingService), null) as ISterlingService;
            if (sterlingService != null)
            {
                sterlingService.Deactivate();
            }
        }

        /// <summary>
        /// Something bad has happened in the applicaiton
        /// </summary>
        /// <param name="sender">Responsible party</param>
        /// <param name="e">The arguments coming in with the Event</param>
        protected override void OnUnhandledException(object sender, System.Windows.ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
            else
            {
#if DEBUG
                MessageBox.Show(e.ExceptionObject.Message + " " + e.ExceptionObject.StackTrace);
#else
                // do something sensible for production
#endif
                e.Handled = true;
            }
        }

        /// <summary>
        /// Tap into the Continuing Event from the IPhoneService
        /// </summary>
        private static void PhoneServiceContinuing()
        {
        }

        /// <summary>
        /// Tap into the Resurrecting Event from the IPhoneService
        /// </summary>
        private static void PhoneServiceResurrecting()
        {
        }

        /// <summary>
        /// Global handler for when a Navigation happens
        /// </summary>
        /// <param name="sender">The object causing the navigation</param>
        /// <param name="e">Arguments pased in as part of navigation</param>
        private void RootFrameNavigated(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New && e.Uri.ToString().Contains("BackNavSkipOne=True"))
            {
                RootFrame.RemoveBackEntry();
            }
        }
    }
}