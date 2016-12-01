using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Swenson_He.Models;
using Swenson_He.Repositories;

namespace Swenson_He.Controllers
{
    [RoutePrefix("api/shop")]
    public class ShopController : ApiController
    {

        public static string _dbConnString = ConfigurationManager.AppSettings["PsqlConnectionString"];

        [HttpGet]
        [Route("SearchProducts")]
        public async Task<HttpResponseMessage> SearchProducts(string pt = "", string a = "")
        {
            var repo = new ProductRepo(_dbConnString);
            //int mos = 0;
            //var ptIds = productTypes.Split(',')
            //        .Select(m => { int.TryParse(m, out mos); return mos; })
            //        .Where(m => m != 0)
            //        .ToList();
            //mos = 0;

            //var aIds = attributes.Split(',')
            //        .Select(m => { int.TryParse(m, out mos); return mos; })
            //        .Where(m => m != 0)
            //        .ToList();
            var p1 = string.IsNullOrEmpty(pt) ? null : pt.ToLower().Split(',');
            var p2 = string.IsNullOrEmpty(a) ? null : a.ToLower().Split(',');
            var res = await repo.SearchProductsAsync(p1, p2);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [Route("GetEspressoMachines")]
        public async Task<HttpResponseMessage> GetEspressoMachines()
        {
            var repo = new ProductRepo(_dbConnString);

            var res = await repo.SearchProductsAsync(new string[] {"machine"}, new string[] {"espresso"});
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [Route("GetSmallestEspressoCrossSell")]
        public async Task<HttpResponseMessage> GetSmallestEspressoCrossSell()
        {
            var repo = new ProductRepo(_dbConnString);

            var res = await repo.SearchProductsAsync(new string[] { "pod" }, new string[] { "espresso" });
            var set = new HashSet<string>();
            var ret = new List<Product>();
            foreach (var p in res)
            {
                if (p.Sku.Length > 0 && !set.Contains(p.Sku.Substring(0, p.Sku.Length - 1)))
                {
                    set.Add(p.Sku.Substring(0, p.Sku.Length - 1));
                    ret.Add(p);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }

        [HttpGet]
        [Route("GetSmallestVanilla")]
        public async Task<HttpResponseMessage> GetSmallestVanilla()
        {
            var repo = new ProductRepo(_dbConnString);

            var res = await repo.SearchProductsAsync(null, new string[] { "vanilla" });
            var set = new HashSet<string>();
            var ret = new List<Product>();
            foreach (var p in res)
            {
                if (p.Sku.Length > 0 && !set.Contains(p.Sku.Substring(0, p.Sku.Length - 1)))
                {
                    set.Add(p.Sku.Substring(0, p.Sku.Length - 1));
                    ret.Add(p);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }

        [HttpGet]
        [Route("GetProductTypes")]
        public async Task<HttpResponseMessage> GetProductTypes()
        {
           var repo = new ProductRepo(_dbConnString);
           var res = await repo.GetProductTypesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        [HttpGet]
        [Route("GetAttributes")]
        public async Task<HttpResponseMessage> GetAttributes()
        {
            var repo = new ProductRepo(_dbConnString);
            var res = await repo.GetAttributesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        [HttpGet]
        [Route("GetAttributeTypes")]
        public async Task<HttpResponseMessage> GetAttributeTypes()
        {
            var repo = new ProductRepo(_dbConnString);
            var res = await repo.GetAttributeTypesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
