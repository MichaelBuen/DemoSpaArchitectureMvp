using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using UnitTestFriendlyDal;

using Domain;
using Dto;

namespace SpaArchitectureMvp.Controllers
{
    public class ProductCategoryController : ApiController
    {
        
        NHibernate.ISessionFactory _sf;
        public ProductCategoryController(NHibernate.ISessionFactory sf)
        {
            _sf = sf;
        }


        // GET api/<controller>
        public IEnumerable<ProductCategoryDto> Get()
        {
            using (IDataStore ds = new DataStore(_sf))
            {
                return TheProduction.ProductCategory.GetAll(ds);
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}