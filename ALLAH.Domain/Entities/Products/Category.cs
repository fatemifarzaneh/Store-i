using ALLAH.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Domain.Entities.Products
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public virtual Category ParentCategory { get; set; }
        public long? ParentCategoryId {  get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }

        
    }
}
