using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebContactClient.Models
{
    /// <summary>
    /// SYSCode - This class contains fields of system code view model.
    /// </summary>
    public class SYSCodeViewModel
    {
        #region Entity fields

        public int ID { get; set; }
        public string Descreption { get; set; }
        public string MetaData { get; set; }
        public string StatusGroup { get; set; }
        
        #endregion Entity fields
    }

    /// <summary>
    /// SYSCode - This class has all the static method to interact with web Api and get system code tables details.
    /// </summary>
    public static class CodeProvider
    {
        #region Properties
        /// <summary>
        /// SYSCode - This field is used to store system code collection.
        /// </summary>
        static IEnumerable<SYSCodeViewModel> ilstSYSCodeViewModel { get; set; }
        /// <summary>
        /// SYSCode - This field is used to store system code.
        /// </summary>
        static SYSCodeViewModel ibusSYSCodeViewModel = new SYSCodeViewModel();

        /// <summary>
        /// Config - This property is used to provide web api url  from web.config file.
        /// </summary>
        static string istrConfigWebApiUrl
        {
            get
            {
                return ConfigManager.iclbAppSettings["WebApiServicesURL"];
            }
        }

        #endregion Properties

        #region Static Methods
        /// <summary>
        /// SYSCode - This method is used to get system code collection by code group from Web Api.
        /// </summary>
        /// <param name="astrCodeGroup">Code group id</param>
        /// <returns>SYSCode collection</returns>
        public static IEnumerable<SYSCodeViewModel> GetSYSCodeByGroupID(string astrCodeGroup)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(istrConfigWebApiUrl);
                //HTTP GET

                var responseTask = client.GetAsync("SYSCode?CodeGroupID=" + astrCodeGroup);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SYSCodeViewModel>>();
                    readTask.Wait();

                    return ilstSYSCodeViewModel = readTask.Result;
                }
                else //web api sent error response 
                {
                    return ilstSYSCodeViewModel = Enumerable.Empty<SYSCodeViewModel>();
                }
            }
        }
        
        #endregion Static Methods
    }
}