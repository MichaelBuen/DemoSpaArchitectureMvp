using Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestFriendlyDal;

namespace Domain
{
    public static partial class TheProduction
    {
        public class ProductCategory
        {
            public int ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }

            public static IEnumerable<ProductCategoryDto> GetAll(IDomainAccess ds)
            {
                return ds.Query<ProductCategory>().MakeCacheable().ToList()
                    .Select(x => new ProductCategoryDto { Id = x.ProductCategoryId, Name = x.ProductCategoryName }); 
            }     
        }
    }
}
