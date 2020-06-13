using WebForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace WebForm.Controllers
{
    public class CreateRequestController : Controller
    {
        HttpClient client = MvcApplication.httpClient;
        string API_LIST_HIERACHY = WebConfigurationManager.AppSettings["api_list_hierachy"];
        string API_CREATE_REQUEST = WebConfigurationManager.AppSettings["api_create_request"];
        public static string requestId;

        // GET: CreateRequest
        public ActionResult Index()
        {
            return View();
        }

        // GET: CreateRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreateRequest/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            List<string> orgNames = new List<string>();

            HierachyNode node = new HierachyNode() { IncludeAncestors = true, IncludeChildren = true, IncludeDescendents = 0, IncludeSiblings = true, IncludeSubItems = true };
            node.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_OrganisationUnit", Int32Value = 0 };
            // POST request
            HttpResponseMessage response = await client.PostAsJsonAsync(API_LIST_HIERACHY, node);
            // Check response
            if (!response.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(response.StatusCode.ToString(), "original");
            }
            // Get headers which contain organisation units
            var responseDetail = response.Content.ReadAsStringAsync().Result;
            AllHeaders netObjects = JsonConvert.DeserializeObject<AllHeaders>(responseDetail);

            // Get organisation ID from first header for each department
            for (int i = 0; i < netObjects.Headers.Length; i++)
            {
                orgNames.Add(netObjects.Headers[i].ObjectName);
            }
            Updated update = new Updated() { OrganisationName = GetSelectListItems(orgNames) };
            
            return View(update);
        }

        // POST: CreateRequest/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "OrganisationUnitID,RequestorName,RequestDetail")] Updated update)
        {
            try
            {
                // TODO: Add insert logic here
                string[] changes = { "RequestDetail", "RequestorName", "OrganisationUnitID" };
                User user = new User();
                user.ChangeSet = new ChangeSet() { Changes = changes };
                user.ChangeSet.Updated = new Updated { RequestDetail = update.RequestDetail, RequestorName = update.RequestorName, OrganisationUnitID = (int) update.OrganisationUnitID + 1 };
                // POST request
                HttpResponseMessage createUserResponse = await client.PostAsJsonAsync(API_CREATE_REQUEST, user);
                // Check response
                if (!createUserResponse.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException(createUserResponse.StatusCode.ToString(), "original");
                }
                // Get request ID
                string requestID = createUserResponse.Content.ReadAsStringAsync().Result;
                requestId = requestID;
                return RedirectToAction("Create","AttachDocument");
            }
            catch
            {
                return View();
            }
        }

        // GET: CreateRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CreateRequest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CreateRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreateRequest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /**
         * Use this method to create drop down list for organisation unit. 
         **/
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();
            int index = 0;
            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = index.ToString(),
                    Text = element
                });
                index++;
            }

            return selectList;
        }
    }
}
