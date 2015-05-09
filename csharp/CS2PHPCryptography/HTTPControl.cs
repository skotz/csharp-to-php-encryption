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
using System.IO;
using System.Net;
using System.Text;

namespace CS2PHPCryptography
{
    public class HttpControl
    {
        // The cookie container allows us to maintain a session with PHP.
        private CookieContainer cookies;

        public HttpControl()
        {
            cookies = new CookieContainer();
        }

        /// <summary>
        /// Send a GET request to a web page. Returns the contents of the page.
        /// </summary>
        /// <param name="url">The address to GET.</param>
        public string Get(string url, ProxySettings settings)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Use a proxy
            if (settings.UseProxy)
            {
                IWebProxy proxy = request.Proxy;
                WebProxy myProxy = new WebProxy();
                Uri newUri = new Uri(settings.ProxyAddress);

                myProxy.Address = newUri;
                myProxy.Credentials = new NetworkCredential(settings.ProxyUsername, settings.ProxyPassword);
                request.Proxy = myProxy;
            }

            request.Method = "GET";
            request.CookieContainer = cookies;
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return result;
        }

        /// <summary>
        /// Send a GET request to a web page. Returns the contents of the page.
        /// </summary>
        /// <param name="url">The address to GET.</param>
        public string Get(string url)
        {
            ProxySettings settings = new ProxySettings() { UseProxy = false };
            return Get(url, settings);
        }

        /// <summary>
        /// Send a POST request to a web page. Returns the contents of the page.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="postVars">The list of variables to POST to the server.</param>
        public string Post(string url, PostPackageBuilder postVars, ProxySettings settings)
        {
            return Post(url, postVars.PostDataString, settings);
        }

        /// <summary>
        /// Send a POST request to a web page. Returns the contents of the page.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="postVars">The list of variables to POST to the server.</param>
        public string Post(string url, PostPackageBuilder postVars)
        {
            ProxySettings settings = new ProxySettings() { UseProxy = false };
            return Post(url, postVars.PostDataString, settings);
        }

        /// <summary>
        /// Send a POST request to a web page. Returns the contents of the page.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="data">The data to POST.</param>
        public string Post(string url, string data)
        {
            ProxySettings settings = new ProxySettings() { UseProxy = false };
            return Post(url, data, settings);
        }

        /// <summary>
        /// Send a POST request to a web page. Returns the contents of the page.
        /// </summary>
        /// <param name="url">The address to POST to.</param>
        /// <param name="data">The data to POST.</param>
        public string Post(string url, string data, ProxySettings settings)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                // Use a proxy
                if (settings.UseProxy)
                {
                    IWebProxy proxy = request.Proxy;
                    WebProxy myProxy = new WebProxy();

                    Uri newUri = new Uri(settings.ProxyAddress);
                    myProxy.Address = newUri;

                    myProxy.Credentials = new NetworkCredential(settings.ProxyUsername, settings.ProxyPassword);
                    request.Proxy = myProxy;
                }

                // Send request
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = buffer.Length;
                request.CookieContainer = cookies;
                Stream postData = request.GetRequestStream();
                postData.Write(buffer, 0, buffer.Length);
                postData.Close();

                // Get and return response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream Answer = response.GetResponseStream();
                StreamReader answer = new StreamReader(Answer);
                return answer.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }
    }

    public class PostPackageBuilder
    {
        private string data;
        private bool firstVariable;

        /// <summary>
        /// The actual string that can be used as the data of the post request.
        /// </summary>
        public string PostDataString
        {
            get { return data; }
        }

        public PostPackageBuilder()
        {
            firstVariable = false;
            data = "";
        }

        /// <summary>
        /// Add a variable to the post packet.
        /// </summary>
        /// <param name="postVariableName">The variable name that will be posted.</param>
        /// <param name="postVariableValue">The value of the variable.</param>
        public void AddVariable(string postVariableName, string postVariableValue)
        {
            data += (firstVariable ? "" : "&") + postVariableName + "=" + postVariableValue;
            firstVariable = false;
        }
    }

    public struct ProxySettings
    {
        public bool UseProxy;
        public string ProxyAddress;
        public string ProxyUsername;
        public string ProxyPassword;

        public ProxySettings(string address, string username, string password)
            : this()
        {
            UseProxy = true;
            ProxyAddress = address;
            ProxyUsername = username;
            ProxyPassword = password;
        }
    }
}