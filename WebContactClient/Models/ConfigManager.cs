using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebContactClient.Models
{
    /// <summary>
    /// Config - This provide configuration details of application we.config file.
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// ConfigManager - This collection is used to store appseting configuration in key value pair.
        /// </summary>
        public static NameValueCollection iclbAppSettings
        {
            get
            {
                return WebConfigurationManager.AppSettings;
            }
        }
    }
}