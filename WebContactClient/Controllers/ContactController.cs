using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebContactClient.Models;

namespace WebContactClient.Controllers
{
    /// <summary>
    /// Contact - This controller is used to handle user's contact related request/response interaction.
    /// </summary>
    public class ContactController : Controller
    {
        #region Properties

        ContactWebApiProvider ibusProvider = new ContactWebApiProvider();
        IEnumerable<ContactViewModel> ilstContactModel { get; set; }
        ContactViewModel ibusContactModel { get; set; }

        #endregion Properties

        #region Action methods

        /// <summary>
        /// Contact - This is HttpGet method of contact to get all the contact list.
        /// </summary>
        /// <returns>Contact list View</returns>
        [HttpGet]
        [ActionName("ContactList")]
        public ActionResult Index()
        {
            ilstContactModel = ibusProvider.GetAllContactList();
            return View(ilstContactModel);
        }

        /// <summary>
        /// Contact - This method is used to get contact id spacific details.
        /// </summary>
        /// <param name="aintContactID">ContactID</param>
        /// <returns>ContactDetails View</returns>
        [HttpGet]
        [ActionName("DetailsContact")]
        [HandleError(View = "Error")]
        public ActionResult DetailContact(int aintContactID)
        {
            ibusContactModel = ibusProvider.GetContactByID(aintContactID);

            return View(ibusContactModel);
        }

        /// <summary>
        /// Contact - This method is used to Edit contact id spacific details.
        /// </summary>
        /// <param name="aintContactID">ContactID</param>
        /// <returns>ContactDetails View</returns>
        [HttpGet]
        [ActionName("EditContact")]
        public ActionResult EditContact(int aintContactID)
        {
            ibusContactModel = ibusProvider.GetContactByID(aintContactID);
            return View(ibusContactModel);
        }

        /// <summary>
        /// Contact - This method is used to Edit contact id spacific details.
        /// </summary>
        /// <param name="id">ContactID</param>
        /// <returns>View</returns>
        [HttpPost]
        [ActionName("EditContact")]
        public ActionResult UpdateContact(ContactViewModel abusContactModel)
        {
            if (ModelState.IsValid)
            {
                if (ibusProvider != null && ibusProvider.UpdateContact(abusContactModel))
                    return RedirectToAction("ContactList");
                else
                    return View();
            }
            else
                return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("CreateContact")]
        public ActionResult CreateContact()
        {
            ibusContactModel = new ContactViewModel();
            ibusContactModel.StatusList = CodeProvider.GetSYSCodeByGroupID("STATUS").ToList();
            return View(ibusContactModel);
        }

        /// <summary>
        /// Contact - This method is used to create/add new contact.
        /// </summary>
        /// <param name="abusContactModel">Contact model</param>
        /// <returns>List view in contact added else create view</returns>
        [HttpPost]
        [ActionName("CreateContact")]
        public ActionResult CreateContact(ContactViewModel abusContactModel)
        {
            if (ModelState.IsValid)
            {
                if (ibusProvider != null && ibusProvider.CreateContact(abusContactModel))
                    return RedirectToAction("ContactList");
                else
                    return View();
            }
            else
                return View();
        }

        [HttpGet]
        [ActionName("DeleteContact")]
        public ActionResult ShowContactBeforDelete(int aintContactID)
        {
            //To show contact detail before delete and delete record by http Delete request not by get request.
            ibusContactModel = ibusProvider.GetContactByID(aintContactID);
            return View(ibusContactModel);
        }

        [HttpPost]
        [ActionName("DeleteContact")]
        public ActionResult DeleteContact(int aintContactID)
        {
            //To show contact detail before delete and delete record by http Delete request not by get request.
            if (ModelState.IsValid)
            {
                if (ibusProvider != null && ibusProvider.DeleteContact(aintContactID))
                    return RedirectToAction("ContactList");
                else
                    return View();
            }
            else
                return View();
        }

        #endregion Action methods
    }
}
