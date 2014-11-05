using Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestFriendlyDal;

namespace Domain
{
    public static partial class ProductionDomain
    {
        public class ProductCategory
        {
            public int ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }

            public static IEnumerable<ProductionDto.ProductCategory> GetAll(IDomainAccess ds)
            {
                return ds.Query<ProductCategory>().MakeCacheable().ToList()
                    .Select(x => new ProductionDto.ProductCategory { Id = x.ProductCategoryId, Name = x.ProductCategoryName }); 
            }     
        }
    }
}
