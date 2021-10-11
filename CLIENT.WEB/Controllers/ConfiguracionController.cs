using CLIENT.WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
 

namespace CLIENT.WEB.Controllers
{
    public class ConfiguracionController : Controller
    {
        private string URLService = ConfigurationManager.AppSettings["WebService"];
        //public ActionResult Listar()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            List<Configuracion> Config = new List<Configuracion>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Configuracion");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    Config = JsonConvert.DeserializeObject<List<Configuracion>>(EmpResponse);
                }
               
                return View(Config);
            }

        }

        public ActionResult Create()
        {
            //GetPeriodos();
            //ListarPeriodos();
            return View();
        }


        [HttpPost]
        public ActionResult Create(Configuracion configuracion)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:38626/api/Configuracion");
                var postTask = client.PostAsJsonAsync<Configuracion>("configuracion", configuracion);
                postTask.Wait();
                var Res = postTask.Result;
                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Listar");
                }
                 
            }
            ModelState.AddModelError(string.Empty, "Error, contacta con TI");
            return View(configuracion);

        }

        public ActionResult Details(int id)
        {
            Configuracion conf = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService);
                var responseTask = client.GetAsync("api/Configuracion/GetById?id=" + id.ToString());
                responseTask.Wait();
                var Res = responseTask.Result;
                if (Res.IsSuccessStatusCode)
                {
                    var readTask = Res.Content.ReadAsAsync<Configuracion>();
                    readTask.Wait();
                    conf = readTask.Result;
                }
            }
            return View(conf);
        }


        public ActionResult Edit(int id)
        {
            Configuracion conf = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService); 
                var responseTask = client.GetAsync("api/Configuracion/GetById?id="+id.ToString());
                responseTask.Wait();
                var Res = responseTask.Result;
                if (Res.IsSuccessStatusCode)
                {
                    var readTask = Res.Content.ReadAsAsync<Configuracion>();
                    readTask.Wait();
                    conf = readTask.Result; 
                } 
            }
            return View(conf); 
        }

        [HttpPost]
        public ActionResult Edit(Configuracion configuracion)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService);

                var putTask = client.PutAsJsonAsync($"api/Configuracion/",configuracion);
                putTask.Wait();
                var Res = putTask.Result;
                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Listar");
                }
            }
            return View(configuracion);
        }

        public ActionResult Delete(int? id)
        {
            Configuracion conf = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService);
                var responseTask = client.GetAsync("api/Configuracion/GetById?id=" + id.ToString());
                responseTask.Wait();
                var Res = responseTask.Result;
                if (Res.IsSuccessStatusCode)
                {
                    var readTask = Res.Content.ReadAsAsync<Configuracion>();
                    readTask.Wait();
                    conf = readTask.Result;
                }
            }
            return View(conf);
        }
        
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService); 
                var deleteTask = client.DeleteAsync("api/Configuracion/?id="+id.ToString());
                deleteTask.Wait();
                var Res = deleteTask.Result;
                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Listar");
                }
            }
            return RedirectToAction("Listar");
        }



        //Listar Periodos
       
        private async void ListarPeriodos()
        {
            List<Periodo> Per = new List<Periodo>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResP = await client.GetAsync("api/Periodo");
                if (ResP.IsSuccessStatusCode)
                {
                    var EmpResponse = ResP.Content.ReadAsStringAsync().Result;
                    Per = JsonConvert.DeserializeObject<List<Periodo>>(EmpResponse);
                } 
                List<SelectListItem> items = Per.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.NPeriodo,
                        Value = d.IdPeriodo.ToString(),
                        Selected = false 
                    }; 
                });
                ViewBag.items = items;
                //return View(Per);
            }

            
        }

        //Listar periodos
        private static void GetPeriodos()
        {
            var url = $"http://localhost:38626/api/Periodo";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
        }



    }
}