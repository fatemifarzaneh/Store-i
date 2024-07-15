using ALLAH.Domain.Entities.Common;
using ALLAH.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }   
        public string Description { get; set; }
        public int Price {  get; set; }
        public int inventory {  get; set; } //موجودی در انبار
        public bool Displayed {  get; set; } //این محصول در سایت ما نمایش داده شود یا خیر 
        public int ViewCount {  get; set; }
        public virtual Category Category { get; set; }  
        public long CategoryId {  get; set; }
        public virtual ICollection<ProductImages> ProductImagess { get; set; }

        public virtual ICollection<ProductFeatures> ProductFeaturess { get; set; }

        public virtual ICollection <Order> Order { get; set; }
           



    }
}
