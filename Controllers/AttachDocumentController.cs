﻿using WebForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

namespace WebForm.Controllers
{
    public class AttachDocumentController : Controller
    {
        HttpClient client = MvcApplication.httpClient;
        string API_ADD_DOCUMENT = WebConfigurationManager.AppSettings["api_add_document"];
        string requestID = CreateRequestController.requestId;

        // GET: AttachDocument
        public ActionResult Index()
        {
            return View();
        }

        // GET: AttachDocument/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AttachDocument/Create
        public ActionResult Create()
        {
            ViewBag.requestID = requestID;
            return View();
        }

        // POST: AttachDocument/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "DocumentDescription,File")] DocContainer document)
        {
            try
            {
                // TODO: Add insert logic here
                // Create Container for chosen Request. Change path to "Request" and use requestID.
                DocContainer con = new DocContainer();
                con.DocumentDescription = document.DocumentDescription;
                //To Get File Extension  
                string FileExtension = Path.GetExtension(document.File.FileName);
                con.Address = $"file://conquest_documents/Request/{requestID}/StoreDocument{FileExtension}";
                con.ContentType = document.File.ContentType;
                
                con.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_Request", Int32Value = int.Parse(requestID) };
                HttpResponseMessage createFileResponse = await client.PostAsJsonAsync(API_ADD_DOCUMENT, con);

                // Check response
                if (!createFileResponse.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException(createFileResponse.StatusCode.ToString(), "original");
                }

                // Upload document content to Container for chosen Request.
                string result = new StreamReader(document.File.InputStream).ReadToEnd();
                DocDataObject docDataObject = JsonConvert.DeserializeObject<DocDataObject>(createFileResponse.Content.ReadAsStringAsync().Result);
                HttpResponseMessage uploadDocumentResponse = await client.PutAsJsonAsync(docDataObject.UploadUri, result);

                //Check response
                if (!uploadDocumentResponse.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException(uploadDocumentResponse.StatusCode.ToString(), "Cannot upload text");
                }
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: AttachDocument/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AttachDocument/Edit/5
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

        // GET: AttachDocument/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttachDocument/Delete/5
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
    }
}
