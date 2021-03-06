﻿//-----------------------------------------------------------------------
// <copyright file="ReputationModel.cs" company="GEP13">
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

namespace Gep13.WindowsPhone.VBForumsMetro.Models
{
    using System;

    /// <summary>
    /// The reputation model.
    /// </summary>
    public class ReputationModel : BaseModel<ReputationModel>
    {
        /// <summary>
        /// The thread name.
        /// </summary>
        private string threadName;

        /// <summary>
        /// The thread uri.
        /// </summary>
        private Uri threadUri;

        /// <summary>
        /// The creation date.
        /// </summary>
        private DateTime creationDate;

        /// <summary>
        /// The author name.
        /// </summary>
        private string authorName;

        /// <summary>
        /// The author uri.
        /// </summary>
        private Uri authorUri;

        /// <summary>
        /// The comment.
        /// </summary>
        private string comment;

        /// <summary>
        /// Gets or sets the thread name.
        /// </summary>
        public string ThreadName
        {
            get
            {
                return this.threadName;
            }

            set
            {
                this.threadName = value;
                this.NotifyOfPropertyChange(() => this.ThreadName);
            }
        }

        /// <summary>
        /// Gets or sets the thread uri.
        /// </summary>
        public Uri ThreadUri
        {
            get
            {
                return this.threadUri;
            }

            set
            {
                this.threadUri = value;
                this.NotifyOfPropertyChange(() => this.ThreadUri);
            }
        }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreationDate
        {
            get
            {
                return this.creationDate;
            }

            set
            {
                this.creationDate = value;
                this.NotifyOfPropertyChange(() => this.CreationDate);
            }
        }

        /// <summary>
        /// Gets or sets the author name.
        /// </summary>
        public string AuthorName
        {
            get
            {
                return this.authorName;
            }

            set
            {
                this.authorName = value;
                this.NotifyOfPropertyChange(() => this.AuthorName);
            }
        }

        /// <summary>
        /// Gets or sets the author uri.
        /// </summary>
        public Uri AuthorUri
        {
            get
            {
                return this.authorUri;
            }

            set
            {
                this.authorUri = value;
                this.NotifyOfPropertyChange(() => this.AuthorUri);
            }
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment
        {
            get
            {
                return this.comment;
            }

            set
            {
                this.comment = value;
                this.NotifyOfPropertyChange(() => this.Comment);
            }
        }
    }
}