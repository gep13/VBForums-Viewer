﻿//-----------------------------------------------------------------------
// <copyright file="WelcomeViewModel.cs" company="GEP13">
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
    using System.Linq;
    using Gep13.WindowsPhone.VBForumsMetro.Core.Workers;

    using Microsoft.Phone.Controls;

    /// <summary>
    /// The ViewModel class for the Welcome page
    /// </summary>
    public class WelcomeViewModel : VBForumsMetroScreenPageViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the WelcomeViewModel class
        /// </summary>
        /// <param name="viewModelWorker">The View Model Worker from common access properties</param>
        public WelcomeViewModel(VBForumsMetroViewModelWorker viewModelWorker)
            : base(viewModelWorker) 
        {
            this.PurgeNavigationalBackStack();
        }

        /// <summary>
        /// Remove the previous page from teh BackStack
        /// </summary>
        private void PurgeNavigationalBackStack()
        {
            var navEntry = (App.Current.RootVisual as PhoneApplicationFrame).BackStack.ElementAt(0);

            if (navEntry.Source.ToString().Contains("InitialView"))
            {
                (App.Current.RootVisual as PhoneApplicationFrame).RemoveBackEntry();
            }
        }
    }
}