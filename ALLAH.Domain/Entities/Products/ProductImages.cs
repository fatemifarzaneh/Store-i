using ALLAH.Domain.Entities.Common;

namespace ALLAH.Domain.Entities.Products
{
    
        public class ProductImages :BaseEntity
        {
            public virtual Product Product { get; set; }
            public long ProductId { get; set; }
            public string Src { get; set; }
        }
          
   
}
