using ALLAH.Domain.Entities.Common;

namespace ALLAH.Domain.Entities.Products
{
    
        public class ProductFeatures :BaseEntity
        {
            public virtual Product Product { get; set;}
            public long ProductId { get; set; }
            public string DisplayName { get; set; }
            public string Value {  get; set; }

        }
            



    
}
