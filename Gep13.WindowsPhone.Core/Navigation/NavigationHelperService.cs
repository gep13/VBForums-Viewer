﻿//-----------------------------------------------------------------------
// <copyright file="NavigationHelperService.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.Core.Navigation
{
    /// <summary>
    /// Concrete implemenation of the INavigationHelperService to provide ability to do additional navigation related tasks
    /// </summary>
    public class NavigationHelperService : INavigationHelperService
    {
        /// <summary>
        /// Completely clear the navigational backstack
        /// </summary>
        /// <param name="rootFrame">The starting point for the purging of the backstack</param>
        public void PurgeNavigationalBackStack(Microsoft.Phone.Controls.PhoneApplicationFrame rootFrame)
        {
            var purgeList = new System.Collections.Generic.List<System.Windows.Navigation.JournalEntry>();

            foreach (var entry in rootFrame.BackStack)
            {
                purgeList.Add(entry);
            }

            foreach (var entry in purgeList)
            {
                if (rootFrame.CanGoBack)
                {
                    rootFrame.RemoveBackEntry();
                }
            }
        }
    }
}