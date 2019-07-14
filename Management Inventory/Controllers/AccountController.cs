using Management_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Management_Inventory.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public static string server = "http://inventorymanagement-swd.azurewebsites.net";
        public ActionResult Index()
        {
            IEnumerable<AccountViewModel> account = null;

            using (var client = new HttpClient())
            {

                string apiUrl = server + "/api/Employees";
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AccountViewModel>>();
                    readTask.Wait();

                    account = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    account = Enumerable.Empty<AccountViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(account);
            }
        }
        public ActionResult Details(string id)
        {
            AccountViewModel account = null;
            string apiUrl = server + "/api/Employees/?id=";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(apiUrl + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AccountViewModel>();
                    readTask.Wait();

                    account = readTask.Result;
                }
            }
            return View(account);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View("Insert");
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(AccountViewModel model)
        {
            string apiUrl = server + "/api/Employees";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<AccountViewModel>(apiUrl, model);
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
        public ActionResult Edit(AccountViewModel account)
        {
            try
            {
                string apiUrl = server + "/api/Employees/" + account.EmployeeId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<AccountViewModel>(apiUrl, account);
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
                string apiUrl = server + "/api/Employees/?id=";
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
