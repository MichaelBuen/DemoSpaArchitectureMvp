
using UnitTestFriendlyDal;

using Dto;

namespace Domain
{
    public static partial class ProductionDomain
    {
        public class Product
        {
            protected internal  int                             ProductId       { get; set; }
            

            public              ProductionDomain.ProductCategory   ProductCategory { get; protected internal set; }
            public              string                             ProductName     { get; protected internal set; }

            public static int Save(IDomainAccess ds, ProductionDto.Product dto)
            {
                var p = new Product
                {
                    ProductName = dto.ProductName,
                    ProductCategory = ds.Load<ProductCategory>(dto.ProductCategoryId)
                };

                return (int)ds.Save(p);
            }
        }
    }
}
