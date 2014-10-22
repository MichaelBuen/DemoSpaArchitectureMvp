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

        IDomainAccessFactory _daf;
        public ProductController(IDomainAccessFactory daf)
        {
            _daf = daf;
        }

        public IEnumerable<string> Get()
        {
            using(var ds = _daf.OpenDomainAccess())
            {
                return ds.Query<TheProduction.Product>().Select(x => x.ProductName).ToList();
            }
        }


        
        public object Post(ProductDto dto)
        {
            using (var ds = _daf.OpenDomainAccess())
            {
                return new { SavedId = TheProduction.Product.Save(ds,dto) };
            }
        }
    }
}
