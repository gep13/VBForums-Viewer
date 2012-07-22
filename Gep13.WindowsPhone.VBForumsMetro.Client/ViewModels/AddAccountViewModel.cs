﻿//-----------------------------------------------------------------------
// <copyright file="AddAccountViewModel.cs" company="GEP13">
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
    using Gep13.WindowsPhone.Core.Workers;
    using Gep13.WindowsPhone.VBForumsMetro.Models;

    /// <summary>
    /// The ViewModel class for the AddAccount page
    /// </summary>
    public class AddAccountViewModel : VBForumsMetroScreenPageViewModelBase
    {
        /// <summary>
        /// The login credential.
        /// </summary>
        private LoginCredentialModel loginCredential;

        /// <summary>
        /// Local variable for whether the user is authenticated
        /// </summary>
        private bool isAuthenticated;

        /// <summary>
        /// Local variable for displaying the status of the user authentication to the user
        /// </summary>
        private string statusLabel;

        /// <summary>
        /// Initializes a new instance of the AddAccountViewModel class
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker from common access properties</param>
        public AddAccountViewModel(ViewModelWorker viewModelWorker)
            : base(viewModelWorker)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether an account is being added or edited.
        /// </summary>
        public bool IsEditMode { get; set; }

        /// <summary>
        /// Gets or sets the login details.
        /// </summary>
        public LoginCredentialModel LoginDetails
        {
            get
            {
                return this.loginCredential;
            }

            set
            {
                this.loginCredential = value;
                this.NotifyOfPropertyChange(() => this.LoginDetails);
                this.NotifyOfPropertyChange(() => this.CanAuthenticateUser);
            }
        }

        /// <summary>
        /// Gets the title for the page
        /// </summary>
        public string PageTitle
        {
            get
            {
                return this.IsEditMode ? "edit account" : "add account";
            }
        }

        /// <summary>
        /// Gets or sets the contents of the StatusLabel displayed on the page
        /// </summary>
        public string StatusLabel
        {
            get
            {
                return this.statusLabel;
            }

            set
            {
                this.statusLabel = value;
                this.NotifyOfPropertyChange(() => this.StatusLabel);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the specified user credentials are authenticated.
        /// </summary>
        public bool IsUserAuthenticated
        {
            get
            {
                return this.isAuthenticated;
            }

            set
            {
                this.isAuthenticated = value;
                this.NotifyOfPropertyChange(() => this.IsUserAuthenticated);
                this.NotifyOfPropertyChange(() => this.CanGoToProfilePage);
                this.NotifyOfPropertyChange(() => this.CanDeleteAccount);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current account can be deleted.
        /// </summary>
        public bool CanDeleteAccount
        {
            get { return this.IsUserAuthenticated || this.IsEditMode; }
        }

        /// <summary>
        /// Gets a value indicating whether it is possible to Authenticate a user
        /// </summary>
        public bool CanAuthenticateUser
        {
            get
            {
                return !string.IsNullOrEmpty(this.LoginDetails.UserName) && !string.IsNullOrEmpty(this.LoginDetails.Password);
            }
        }

        /// <summary>
        /// Gets a value indicating whether it is possible to navigate to the ProfilePage
        /// </summary>
        public bool CanGoToProfilePage
        {
            get
            {
                if (this.IsUserAuthenticated)
                {
                    this.VMWorker.StorageService.Add("firstrunflag", true);
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Helper method to do the work when the ActionIcon is tapped on the Username TextBox
        /// </summary>
        public void UsernameActionIconTapped()
        {
            this.LoginDetails.UserName = string.Empty;
        }

        /// <summary>
        /// Helper method to do the work when the ActionIcon is tapped on the Password Textbox
        /// </summary>
        public void PasswordActionIconTapped()
        {
            this.LoginDetails.Password = string.Empty;
        }

        /// <summary>
        /// Helper method to populate some demo credentials
        /// </summary>
        public void PopulateDemoCredentials()
        {
            this.LoginDetails.UserName = "test";
            this.LoginDetails.Password = "password";
        }

        /// <summary>
        /// A helper method to go off to the VBForums site and Authenticate the current user credentials
        /// </summary>
        public void AuthenticateUser()
        {
            this.StatusLabel = string.Empty;

            // TODO: Need to replace this with actual code.
            this.IsUserAuthenticated = true;
            this.StatusLabel = "user authenticated successfully";
        }

        /// <summary>
        /// A helper method to delete the current credentials
        /// </summary>
        public void DeleteAccount()
        {
            this.VMWorker.StorageService.Add("firstrunflag", false);
            this.LoginDetails.UserName = string.Empty;
            this.LoginDetails.Password = string.Empty;
            this.IsUserAuthenticated = false;
        }

        /// <summary>
        /// An overridden implemenation of the OnViewAttahced method to do specific functionality within this view
        /// </summary>
        /// <param name="view">The current view</param>
        /// <param name="context">The incoming context</param>
        protected override void OnViewAttached(object view, object context)
        {
            this.StatusLabel = string.Empty;
            this.IsUserAuthenticated = false;

            base.OnViewAttached(view, context);
        }

        /// <summary>
        /// An overridden implemenation of the OnViewLoaded method to do specific functionality within this view
        /// </summary>
        /// <param name="view">The current view</param>
        protected override void OnViewLoaded(object view)
        {
            if (this.IsEditMode)
            {
                if (this.LoginDetails == null)
                {
                    this.LoginDetails = new LoginCredentialModel();
                    this.LoginDetails.UserName = this.VMWorker.StorageService.Get<string>("username");
                    this.LoginDetails.Password = this.VMWorker.StorageService.Get<string>("password");
                }
            }
            else
            {
                this.LoginDetails = new LoginCredentialModel();
            }

            base.OnViewLoaded(view);
        }
    }
}