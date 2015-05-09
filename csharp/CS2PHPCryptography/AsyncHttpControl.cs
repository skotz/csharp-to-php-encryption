//
// Copyright (c) 2011 Scott Clayton
//
// This file is part of the C# to PHP Encryption Library.
//
// The C# to PHP Encryption Library is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// The C# to PHP Encryption Library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with the C# to PHP Encryption Library.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.ComponentModel;

namespace CS2PHPCryptography
{
    public class AsyncHttpControl
    {
        private BackgroundWorker background;
        private string response;
        private bool busy;
        private HttpAsyncInfo request;
        private bool error;

        private HttpControl http;

        /// <summary>
        /// Whether this object is currently in the process of fetching a request;
        /// </summary>
        public bool IsBusy
        {
            get { return busy; }
        }

        /// <summary>
        /// Raised when a response from the last HTTP request has arrived.
        /// </summary>
        public event ResponseCallback OnHttpResponse;

        public delegate void ResponseCallback(object sender, OnHttpResponseEventArgs e);

        public AsyncHttpControl()
        {
            http = new HttpControl();

            background = new BackgroundWorker();
            background.DoWork += new DoWorkEventHandler(background_DoWork);
            background.RunWorkerCompleted += new RunWorkerCompletedEventHandler(background_RunWorkerCompleted);

            busy = false;
            error = false;
        }

        /// <summary>
        /// Send a GET request to a web page asynchronously. The result will be returned in OnHttpResponse.
        /// </summary>
        /// <param name="url">The address to GET.</param>
        public bool Get(string url, ProxySettings settings)
        {
            if (!busy)
            {
                busy = true;

                request = new HttpAsyncInfo();
                request.url = url;
                request.settings = settings;
                background.RunWorkerAsync(RequestOption.Get);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Send a GET request to a web page asynchronously. The result will be returned in OnHttpResponse.
        /// </summary>
        /// <param name="url">The address to GET.</param>
        public bool Get(string url)
        {
            ProxySettings settings = new ProxySettings() { UseProxy = false };
            return Get(url, settings);
        }

        /// <summary>
        /// Send a POST request to a web page asynchronously. The response will be returned in OnHttpResponse.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="postVars">The list of variables to POST to the server.</param>
        public bool Post(string url, PostPackageBuilder postVars, ProxySettings settings)
        {
            return Post(url, postVars.PostDataString, settings);
        }

        /// <summary>
        /// Send a POST request to a web page asynchronously. The response will be returned in OnHttpResponse.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="postVars">The list of variables to POST to the server.</param>
        public bool Post(string url, PostPackageBuilder postVars)
        {
            ProxySettings settings = new ProxySettings() { UseProxy = false };
            return Post(url, postVars.PostDataString, settings);
        }

        /// <summary>
        /// Send a POST request to a web page asynchronously. The response will be returned in OnHttpResponse.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="data">The data to POST.</param>
        public bool Post(string url, string data)
        {
            ProxySettings settings = new ProxySettings() { UseProxy = false };
            return Post(url, data, settings);
        }

        /// <summary>
        /// Send a POST request to a web page asynchronously. The response will be returned in OnHttpResponse.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="data">The data to POST.</param>
        public bool Post(string url, string data, ProxySettings settings)
        {
            if (!busy)
            {
                busy = true;

                request = new HttpAsyncInfo();
                request.url = url;
                request.settings = settings;
                request.data = data;
                background.RunWorkerAsync(RequestOption.Post);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnHttpResponse != null)
            {
                OnHttpResponse(this, new OnHttpResponseEventArgs(response, error));
                busy = false;
            }
        }

        private void background_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                error = false;

                switch ((RequestOption)e.Argument)
                {
                    case RequestOption.Get:
                        response = http.Get(request.url, request.settings);
                        break;

                    case RequestOption.Post:
                        response = http.Post(request.url, request.data, request.settings);
                        break;
                }
            }
            catch (Exception ex)
            {
                error = true;
                response = "Error getting HTTP request: " + ex.Message;
            }
        }
    }

    internal enum RequestOption
    {
        Post,
        Get
    }

    public struct HttpAsyncInfo
    {
        public ProxySettings settings;
        public string url;
        public string data;
    }

    public class OnHttpResponseEventArgs
    {
        /// <summary>
        /// The body of the response
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// Whether or not there was an error. If there was, then the error will be in the ResponseBody.
        /// </summary>
        public bool Error { get; set; }

        public OnHttpResponseEventArgs(string body, bool error)
        {
            ResponseBody = body;
            Error = error;
        }
    }
}