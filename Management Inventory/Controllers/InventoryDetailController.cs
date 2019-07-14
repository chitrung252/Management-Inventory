using Management_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Management_Inventory.Controllers
{
    public class InventoryDetailController : Controller
    {
        // GET: model
        public static string server = "http://inventorymanagement-swd.azurewebsites.net";
        public ActionResult Index()
        {
            IEnumerable<InventoryDetailViewModel> model = null;
            IEnumerable<ProductViewModel> products = null;
            IEnumerable<InventoryViewModel> inventories = null;
            string apiUrl = server + "/api/InventoryDetails";
            string apiUrlProduct = server + "/api/Products";
            string apiUrlInventory = server + "/api/Inventories";
            using (var client = new HttpClient())
            { 
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<InventoryDetailViewModel>>();
                    readTask.Wait();

                    model = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model = Enumerable.Empty<InventoryDetailViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri(apiUrlProduct);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlProduct);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductViewModel>>();
                    readTask.Wait();

                    products = readTask.Result;
                    model.ToList().ForEach(p => p.listProduct = products.Where(c => p.ProductId == c.ProductId));
                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<ProductViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri(apiUrlInventory);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlInventory);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<InventoryViewModel>>();
                    readTask.Wait();

                    inventories = readTask.Result;
                    model.ToList().ForEach(p => p.listInventory = inventories.Where(c => p.InventoryId == c.InventoryId));
                }
                else //web api sent error response 
                {
                    //log response status here..

                    inventories = Enumerable.Empty<InventoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(model);
            }
        }
        public ActionResult Details(string id)
        {
            InventoryDetailViewModel model = null;
            IEnumerable<ProductViewModel> products = null;
            IEnumerable<InventoryViewModel> inventories = null;
            string apiUrlProduct = server + "/api/Products";
            string apiUrlInventory = server + "/api/Inventories";
            string apiUrl = server + "/api/InventoryDetails/?id=";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<InventoryDetailViewModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }
            }
            using (var client = new HttpClient())
            { 
                client.BaseAddress = new Uri(apiUrlProduct);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlProduct);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductViewModel>>();
                    readTask.Wait();

                    products = readTask.Result;
                    model.listProduct = products.ToList(); ;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<ProductViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri(apiUrlInventory);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlInventory);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<InventoryViewModel>>();
                    readTask.Wait();

                    inventories = readTask.Result;
                    model.listInventory = inventories.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    inventories = Enumerable.Empty<InventoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(model);
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            InventoryDetailViewModel model = new InventoryDetailViewModel();
            IEnumerable<ProductViewModel> products = null;
            IEnumerable<InventoryViewModel> inventories = null;
            string apiUrlInven = server + "/api/Inventories";
            string apiUrlProduct = server + "/api/Products";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrlProduct);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlProduct);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<ProductViewModel>>();
                    readTask.Wait();

                    products = readTask.Result;
                    model.listProduct = products.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<ProductViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrlInven);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlInven);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<InventoryViewModel>>();
                    readTask.Wait();

                    inventories = readTask.Result;
                    model.listInventory = inventories.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    inventories = Enumerable.Empty<InventoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View("Insert", model);
            }
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(InventoryDetailViewModel model)
        {
            model.InventoryDetailId = model.InventoryDetailId.Trim();
            string apiUrl = server + "/api/InventoryDetails";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<InventoryDetailViewModel>(apiUrl, model);
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
        public ActionResult Edit(InventoryDetailViewModel model)
        {
            try
            {
                string apiUrl = server + "/api/InventoryDetails/" + model.InventoryDetailId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<InventoryDetailViewModel>(apiUrl, model);
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
                string apiUrl = server + "/api/InventoryDetails/?id=";
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