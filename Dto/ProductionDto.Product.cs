using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public static partial class ProductionDto
    {
        public class Product
        {
            public string ProductName { get; set; }
            public int ProductCategoryId { get; set; }
        }
    }
}
