using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebContactClient.Models
{
    /// <summary>
    /// Contact - This class contains contact entity model fields and validations.
    /// </summary>
    public class ContactViewModel
    {
        #region Entity fields

        [Display(Name = "Contact ID")]
        public int ContactID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "First Name field accept charechters only.")]
        [StringLength(30, ErrorMessage = "First Name field should be less than 30 charechters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Last Name field accept charechters only.")]
        [StringLength(30, ErrorMessage = "Last Name field should be less than 30 charechters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone field accept 10 digit number.")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Status")]
        [DataType(DataType.Text, ErrorMessage = "Please select contact status.")]
        public int StatusID { get; set; }

        [Display(Name = "Status")]
        public string StatusDesc { get; set; }
        public List<SYSCodeViewModel> StatusList { get; set; }
        #endregion Entity fields
    }

    /// <summary>
    /// Contact - This class contains method to interact with Web Api using Http GET, POST, PUT, DELETE request to perform operation.
    /// </summary>
    public class ContactWebApiProvider
    {
        #region Properties
        /// <summary>
        /// Contact - This field is used to store collection of contact model.
        /// </summary>
        IEnumerable<ContactViewModel> ilstContactModel { get; set; }
        
        /// <summary>
        /// Contact - This field is used to store contact model.
        /// </summary>
        ContactViewModel iobjContactModel = new ContactViewModel() { StatusList = new List<SYSCodeViewModel>() };

        /// <summary>
        /// Contact - This property is used to read config file appsetting section by given key
        /// </summary>
        string istrConfigWebApiUrl
        {
            get
            {
                return ConfigManager.iclbAppSettings["WebApiServicesURL"];
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Contact - This method is used to interact with Web Api and get contact by contact id. 
        /// </summary>
        /// <param name="aintContactID">Contact id</param>
        /// <returns>Returns contact model</returns>
        public ContactViewModel GetContactByID(int aintContactID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(istrConfigWebApiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("Contact?id=" + aintContactID);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContactViewModel>();
                    readTask.Wait();

                    return iobjContactModel = readTask.Result;
                }
                else //web api sent error response 
                {
                    return iobjContactModel = new ContactViewModel() { StatusList = new List<SYSCodeViewModel>() };
                }
            }
        }

        /// <summary>
        ///Contact - This method is used to interact with Web Api and get contact List.
        /// </summary>
        /// <returns>Contact list</returns>
        public IEnumerable<ContactViewModel> GetAllContactList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(istrConfigWebApiUrl);
                //HTTP GET

                var responseTask = client.GetAsync("Contact");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ContactViewModel>>();
                    readTask.Wait();

                    return ilstContactModel = readTask.Result;
                }
                else //web api sent error response 
                {
                    return ilstContactModel = Enumerable.Empty<ContactViewModel>();
                }
            }
        }

        /// <summary>
        /// Contact - This method is used to interact with Web Api and create/add contact. 
        /// </summary>
        /// <param name="abusContactModel">Contact model</param>
        /// <returns>Returns true if contact added else false</returns>
        public bool CreateContact(ContactViewModel abusContactModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(istrConfigWebApiUrl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ContactViewModel>("Contact", abusContactModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Contact - This method is used to interact with Web Api and update contact. 
        /// </summary>
        /// <param name="abusContactModel">Contact model</param>
        /// <returns>Returns true if contact updated else false</returns>
        public bool UpdateContact(ContactViewModel abusContactModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(istrConfigWebApiUrl);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<ContactViewModel>("Contact", abusContactModel);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Contact - This method is used to interact with Web Api and delete contact. 
        /// </summary>
        /// <param name="aintContactID">Contact id</param>
        /// <returns>Returns true if contact deleted else false</returns>
        public bool DeleteContact(int aintContactID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(istrConfigWebApiUrl);

                //HTTP POST
                var deleteTask = client.DeleteAsync("Contact?id=" + aintContactID);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        
        #endregion Public Methods
    }
}