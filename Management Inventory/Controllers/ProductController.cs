using Management_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Management_Inventory.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public static string server = "http://inventorymanagement-swd.azurewebsites.net";
        public ActionResult Index()
        {
            IEnumerable<ProductViewModel> products = null;
            IEnumerable<CategoryViewModel> categories = null;
            IEnumerable<UnitViewModel> units = null;
            string apiUrl = server + "/api/Products";
            string apiUrlCate = server + "/api/Categories";
            string apiUrlUnit = server + "/api/Units";
            using (var client = new HttpClient())
            {

              
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<ProductViewModel>>();
                    readTask.Wait();

                    products = readTask.Result;
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
                client.BaseAddress = new Uri(apiUrlCate);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlCate);
                responseTask.Wait();

               var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<CategoryViewModel>>();
                    readTask.Wait();

                    categories = readTask.Result;
                    products.ToList().ForEach(p => p.listCate = categories.Where(c => p.CategoryId == c.CategoryId));
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = Enumerable.Empty<CategoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrlUnit);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlUnit);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<UnitViewModel>>();
                    readTask.Wait();

                    units= readTask.Result;
                    products.ToList().ForEach(p => p.listUnit = units.Where(c => p.UnitId == c.UnitId));
                }
                else //web api sent error response 
                {
                    //log response status here..

                    units= Enumerable.Empty<UnitViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {
            IEnumerable<CategoryViewModel> categories = null;
            IEnumerable<UnitViewModel> units = null;
            ProductViewModel product = null;
            string apiUrl = server + "/api/Products/?id=";
            string apiUrlCate = server + "/api/Categories";
            string apiUrlUnit = server + "/api/Units";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductViewModel>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrlCate);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlCate);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<CategoryViewModel>>();
                    readTask.Wait();

                    categories = readTask.Result;
                    product.listCate = categories.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = Enumerable.Empty<CategoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrlUnit);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlUnit);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<UnitViewModel>>();
                    readTask.Wait();

                    units = readTask.Result;
                    product.listUnit = units.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    units = Enumerable.Empty<UnitViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(product);
            }
        }
        // GET: Product/Create
        public ActionResult Create()
        {
            IEnumerable<CategoryViewModel> categories = null;
            IEnumerable<UnitViewModel> units = null;
            ProductViewModel product = new ProductViewModel();
            string apiUrlCate = server + "/api/Categories";
            string apiUrlUnit = server + "/api/Units";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrlCate);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlCate);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<CategoryViewModel>>();
                    readTask.Wait();

                    categories = readTask.Result;
                    product.listCate = categories.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = Enumerable.Empty<CategoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrlUnit);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrlUnit);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<UnitViewModel>>();
                    readTask.Wait();

                    units = readTask.Result;
                    product.listUnit = units.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    units = Enumerable.Empty<UnitViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View("Insert",product);
            }
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
             string apiUrl = server + "/api/Products";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ProductViewModel>(apiUrl, model);
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
        public ActionResult Edit(ProductViewModel model)
        {
            try
            {
                string apiUrl = server + "/api/Products/" + model.ProductId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<ProductViewModel>(apiUrl, model);
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
                string apiUrl = server + "/api/Products/?id=";
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
