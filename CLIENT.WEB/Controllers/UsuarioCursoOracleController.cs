
using CLIENT.WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CLIENT.WEB.Controllers
{
    public class UsuarioCursoOracleController : Controller
    {
        private string URLService = ConfigurationManager.AppSettings["WebService"];
        
      
        // GET: Usuarios
        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Listar()
        {
            return View();
        }
         
        public ActionResult ListarUsuarios()
        {
            return View();
        }
       
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<UsuarioCursoOracle> UserOracle = new List<UsuarioCursoOracle>();
             
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URLService);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Usuarios");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    UserOracle = JsonConvert.DeserializeObject<List<UsuarioCursoOracle>>(EmpResponse);
                }

                return View(UserOracle);
            }
           
        }

        
    }
}