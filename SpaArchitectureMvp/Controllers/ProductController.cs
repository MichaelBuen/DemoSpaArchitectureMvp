using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using UnitTestFriendlyDal;

using Domain;
using Dto;

namespace SpaArchitectureMvp.Controllers
{
    public class ProductController : ApiController
    {

        NHibernate.ISessionFactory _sf;
        public ProductController(NHibernate.ISessionFactory sf)
        {
            _sf = sf;
        }

        public IEnumerable<string> Get()
        {
            using(IDataStore ds = new DataStore(_sf))
            {
                return ds.Query<TheProduction.Product>().Select(x => x.ProductName).ToList();
            }
        }


        
        public object Post(ProductDto dto)
        {
            using (IDataStore ds = new DataStore(_sf))
            {
                return new { SavedId = TheProduction.Product.Save(ds,dto) };
            }
        }
    }
}
