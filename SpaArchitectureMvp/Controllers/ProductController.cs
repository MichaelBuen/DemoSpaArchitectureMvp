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
                return ds.Query<ProductionDomain.Product>().Select(x => x.ProductName).ToList();
            }
        }


        
        public object Post(ProductionDto.Product dto)
        {
            using (var ds = _daf.OpenDomainAccess())
            {
                return new { SavedId = ProductionDomain.Product.Save(ds,dto) };
            }
        }
    }
}
