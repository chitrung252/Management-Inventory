using Management_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Management_Inventory.Controllers
{
    public class InventoryController : Controller
    {
        // GET: model
        public static string server = "http://inventorymanagement-swd.azurewebsites.net";
        public ActionResult Index()
        {
            IEnumerable<InventoryViewModel> model = null;

            using (var client = new HttpClient())
            {

                string apiUrl = server + "/api/Inventories";
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<InventoryViewModel>>();
                    readTask.Wait();

                    model = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model = Enumerable.Empty<InventoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(model);
            }
        }
        public ActionResult Details(string id)
        {
            InventoryViewModel model = null;
            string apiUrl = server + "/api/Inventories/?id=";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<InventoryViewModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }
            }
            return View(model);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View("Insert");
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(InventoryViewModel model)
        {
            string apiUrl = server + "/api/Inventories";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<InventoryViewModel>(apiUrl, model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(model);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(InventoryViewModel model)
        {
            try
            {
                string apiUrl = server + "/api/Inventories/" + model.InventoryId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<InventoryViewModel>(apiUrl, model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                string apiUrl = server + "/api/Inventories/?id=";
                client.BaseAddress = new Uri(apiUrl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(apiUrl + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
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
