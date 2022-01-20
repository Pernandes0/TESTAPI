using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TESTBNI.Models;

namespace ClientBNI.Controllers
{
    public class BarangController : Controller
    {
        readonly HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44369/api/")
        };
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddBarang()
        {
            return View();
        }
        public IActionResult InsertOrUpdate(Barang barang, int id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(barang);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // var token = HttpContext.Session.GetString("JWToken");
                // http.DefaultRequestHeaders.Add("Authorization", token);

                if (barang.Id == 0)
                {
                    var result = http.PostAsync("barangs/post/", byteContent).Result;
                    return Json(result);
                }
                else if (barang.Id != 0)
                {
                    var result = http.PutAsync("barangs/edit/" + id, byteContent).Result;
                    return Json(result);
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult LoadBarang()
        {
            IEnumerable<Barang> barang = null;
            // var token = HttpContext.Session.GetString("JWToken");
            //http.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = http.GetAsync("barangs/get");
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Barang>>();
                readTask.Wait();
                barang = readTask.Result;
            }
            return Json(barang);
        }

        public JsonResult GetById(int id)
        {
            Barang barang = null;
            // var token = HttpContext.Session.GetString("JWToken");
            // http.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = http.GetAsync("barangs/" + id);
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                barang = JsonConvert.DeserializeObject<Barang>(readTask);
            }
            return Json(barang);
        }
        public JsonResult Delete(int id)
        {
            //var token = HttpContext.Session.GetString("JWToken");
            //http.DefaultRequestHeaders.Add("Authorization", token);
            var result = http.DeleteAsync("barangs/" + id).Result;
            return Json(result);
        }
        public JsonResult Check(string name)
        {
            ////var token = HttpContext.Session.GetString("JWToken");
            ////http.DefaultRequestHeaders.Add("Authorization", token);
            //var result = http.GetAsync("barangs/check" + name).Result;
            //return Json(result);

            Barang barang = null;
            //var token = HttpContext.Session.GetString("JWToken");
            //http.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = http.GetAsync("barangs/check" + name);
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                barang = JsonConvert.DeserializeObject<Barang>(readTask);
            }
            return Json(barang);
            //var result = http.GetAsync("barangs/check" + name).Result;
            //return Json(result);
        }
    }
}
